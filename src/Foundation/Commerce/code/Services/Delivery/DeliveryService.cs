//    Copyright 2020 EPAM Systems, Inc.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

namespace Wooli.Foundation.Commerce.Services.Delivery
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Connect.Context;
    using Connect.Managers;
    using Connect.Managers.Account;
    using Connect.Managers.Shipping;
    using Connect.Models;

    using Context;

    using DependencyInjection;

    using ModelMappers;

    using Models;
    using Models.Entities.Addresses;
    using Models.Entities.Checkout;
    using Models.Entities.Delivery;
    using Models.Entities.Shipping;

    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Diagnostics;

    using Utils;

    using Cart = Sitecore.Commerce.Entities.Carts.Cart;
    using ShippingMethod = Models.Entities.Shipping.ShippingMethod;
    using ShippingOption = Models.Entities.Shipping.ShippingOption;

    [Service(typeof(IDeliveryService), Lifetime = Lifetime.Singleton)]
    public class DeliveryService : IDeliveryService
    {
        private readonly IAccountManagerV2 accountManager;
        private readonly ICartManagerV2 cartManager;
        private readonly IEntityMapper entityMapper;
        private readonly IShippingManagerV2 shippingManager;
        private readonly IStorefrontContext storefrontContext;
        private readonly IVisitorContext visitorContext;

        public DeliveryService(
            IAccountManagerV2 accountManager,
            ICartManagerV2 cartManager,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext,
            IEntityMapper entityMapper,
            IShippingManagerV2 shippingManager)
        {
            Assert.ArgumentNotNull(accountManager, nameof(accountManager));
            Assert.ArgumentNotNull(cartManager, nameof(cartManager));
            Assert.ArgumentNotNull(entityMapper, nameof(entityMapper));
            Assert.ArgumentNotNull(shippingManager, nameof(shippingManager));
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));
            Assert.ArgumentNotNull(visitorContext, nameof(visitorContext));

            this.accountManager = accountManager;
            this.cartManager = cartManager;
            this.entityMapper = entityMapper;
            this.storefrontContext = storefrontContext;
            this.visitorContext = visitorContext;
            this.shippingManager = shippingManager;
        }

        public Result<DeliveryInfo> GetDeliveryOptions()
        {
            var model = new DeliveryInfo
            {
                NewPartyId = Guid.NewGuid().ToString("N").ToLower()
            };
            var result = new Result<DeliveryInfo>(model);
            var cartResult = this.cartManager.LoadCart(
                this.storefrontContext.ShopName,
                this.visitorContext.ContactId);

            if (!cartResult.Success)
            {
                result.SetErrors(cartResult.SystemMessages.Select(m => m.Message).ToList());

                return result;
            }

            var currentCart = cartResult.Cart;

            if (currentCart.Lines != null && currentCart.Lines.Any())
            {
                this.AddShippingOptions(result, currentCart);

                if (result.Success)
                {
                    this.AddUserInfo(result.Data, result);
                }
            }

            return result;
        }

        public Result<ShippingInfo> GetShippingOptions()
        {
            var model = new ShippingInfo();
            var result = new Result<ShippingInfo>(model);
            var cartResult = this.cartManager.LoadCart(
                this.storefrontContext.ShopName,
                this.visitorContext.ContactId);

            if (!cartResult.Success)
            {
                result.SetErrors(cartResult.SystemMessages.Select(m => m.Message).ToList());

                return result;
            }

            var shippingMethodsResult = this.shippingManager.GetShippingMethods(
                cartResult.Cart,
                ShippingOptionType.ShipToAddress);

            if (!shippingMethodsResult.Success)
            {
                result.SetErrors(shippingMethodsResult.SystemMessages.Select(m => m.Message).ToList());

                return result;
            }

            result.Data.ShippingMethods =
                this.entityMapper
                    .Map<List<ShippingMethod>, IReadOnlyCollection<Sitecore.Commerce.Entities.Shipping.ShippingMethod>>(
                        shippingMethodsResult.ShippingMethods);

            return result;
        }

        public Result<VoidResult> SetShippingOptions(
            string shippingPreferenceType,
            List<Address> shippingAddresses,
            List<ShippingMethod> shippingMethods)
        {
            Assert.ArgumentNotNullOrEmpty(shippingPreferenceType, nameof(shippingPreferenceType));
            Assert.ArgumentNotNull(shippingAddresses, nameof(shippingAddresses));
            Assert.ArgumentNotNull(shippingMethods, nameof(shippingMethods));

            var result = new Result<VoidResult>(new VoidResult());
            var cartResult = this.cartManager.LoadCart(
                this.storefrontContext.ShopName,
                this.visitorContext.ContactId);

            if (!cartResult.Success)
            {
                result.SetErrors(cartResult.SystemMessages.Select(m => m.Message).ToList());

                return result;
            }

            var parties = this.entityMapper.Map<List<PartyEntity>, List<Address>>(shippingAddresses);
            var shippingOptionType =
                ConnectOptionTypeHelper.ToShippingOptionType(shippingPreferenceType);
            var cart = this.RemoveAllShipmentFromCart(cartResult.Cart);

            if (parties != null && parties.Any())
            {
                cart.Parties.AddRange(parties);
            }

            foreach (var shippingMethod in shippingMethods)
            {
                shippingMethod.LineIds = cart.Lines.Select(lineItem => lineItem.ExternalCartLineId).ToList();
            }

            var shippings =
                this.entityMapper.Map<List<Sitecore.Commerce.Entities.Carts.ShippingInfo>, List<ShippingMethod>>(
                    shippingMethods);
            var addShippingInfoResult = this.cartManager.AddShippingInfo(cart, shippingOptionType, shippings);

            if (!addShippingInfoResult.Success)
            {
                result.SetErrors(addShippingInfoResult.SystemMessages.Select(m => m.Message).ToList());
            }

            return result;
        }

        protected virtual void AddShippingOptions(Result<DeliveryInfo> result, Cart cart)
        {
            var getShippingOptionsResult = this.shippingManager.GetShippingOptions(cart);

            if (!getShippingOptionsResult.Success)
            {
                result.SetErrors(getShippingOptionsResult.SystemMessages.Select(m => m.Message).ToList());
            }
            else
            {
                result.Data.ShippingOptions =
                    this.entityMapper
                        .Map<List<ShippingOption>,
                            IReadOnlyCollection<Sitecore.Commerce.Entities.Shipping.ShippingOption>>(
                            getShippingOptionsResult.ShippingOptions);
            }
        }

        protected virtual void AddUserInfo<T>(BaseCheckoutInfo baseCheckoutInfo, Result<T> result)
            where T : class
        {
            var getPartiesResult = this.accountManager.GetCustomerParties(this.visitorContext.ContactId);

            if (getPartiesResult.Success)
            {
                baseCheckoutInfo.UserAddresses = new List<Address>();
                foreach (var party in getPartiesResult.Parties)
                {
                    var address = this.entityMapper.Map<Address, Party>(party);
                    // TODO: Check this mapping and map collection instead of each item
                    //var commerceParty = party as CommerceParty;
                    //address.Name = commerceParty.Name;
                    //address.CountryCode = commerceParty.CountryCode;
                    baseCheckoutInfo.UserAddresses.Add(address);
                }
            }
            else
            {
                result.SetErrors(getPartiesResult.SystemMessages.Select(m => m.Message).ToList());
            }
        }

        protected virtual Cart RemoveAllShipmentFromCart(Cart cart)
        {
            if (cart.Shipping != null && cart.Shipping.Any())
            {
                var list = cart.Parties.ToList();

                foreach (var shippingInfo in cart.Shipping)
                {
                    var shipment = shippingInfo;
                    var party = list.Find(
                        cp => cp.PartyId.Equals(shipment.PartyID, StringComparison.OrdinalIgnoreCase));
                    if (party != null)
                    {
                        list.Remove(party);
                    }
                }

                cart.Parties = list;
                cart.Shipping = null;
            }

            return cart;
        }
    }
}