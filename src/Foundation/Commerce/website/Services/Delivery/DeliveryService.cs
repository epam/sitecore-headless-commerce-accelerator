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

namespace HCA.Foundation.Commerce.Services.Delivery
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models.Result;

    using Connect.Context.Storefront;
    using Connect.Managers.Account;
    using Connect.Managers.Cart;
    using Connect.Managers.Shipping;

    using Context;

    using DependencyInjection;

    using Mappers.Shipping;

    using Models.Entities.Addresses;
    using Models.Entities.Checkout;
    using Models.Entities.Delivery;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Diagnostics;

    using ShippingInfo = Models.Entities.Shipping.ShippingInfo;
    using ShippingMethod = Models.Entities.Shipping.ShippingMethod;

    [Service(typeof(IDeliveryService), Lifetime = Lifetime.Singleton)]
    public class DeliveryService : IDeliveryService
    {
        private readonly IAccountManager accountManager;

        private readonly ICartManager cartManager;

        private readonly IShippingManager shippingManager;

        private readonly IShippingMapper shippingMapper;

        private readonly IStorefrontContext storefrontContext;

        private readonly IVisitorContext visitorContext;

        public DeliveryService(
            IAccountManager accountManager,
            ICartManager cartManager,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext,
            IShippingMapper shippingMapper,
            IShippingManager shippingManager)
        {
            Assert.ArgumentNotNull(accountManager, nameof(accountManager));
            Assert.ArgumentNotNull(cartManager, nameof(cartManager));
            Assert.ArgumentNotNull(shippingMapper, nameof(shippingMapper));
            Assert.ArgumentNotNull(shippingManager, nameof(shippingManager));
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));
            Assert.ArgumentNotNull(visitorContext, nameof(visitorContext));

            this.accountManager = accountManager;
            this.cartManager = cartManager;
            this.shippingMapper = shippingMapper;
            this.storefrontContext = storefrontContext;
            this.visitorContext = visitorContext;
            this.shippingManager = shippingManager;
        }

        public Result<DeliveryInfo> GetDeliveryInfo()
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

        public Result<ShippingInfo> GetShippingInfo()
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
                this.shippingMapper
                    .Map<IReadOnlyCollection<Sitecore.Commerce.Entities.Shipping.ShippingMethod>, List<ShippingMethod>>(
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

            var commerceParties = this.shippingMapper.Map<List<Address>, List<CommerceParty>>(shippingAddresses);
            var shippingOptionType = this.shippingMapper.Map<string, ShippingOptionType>(shippingPreferenceType);
            var cart = this.RemoveAllShipmentFromCart(cartResult.Cart);

            if (commerceParties != null && commerceParties.Any())
            {
                cart.Parties.AddRange(commerceParties);
            }

            var shippings = new List<Sitecore.Commerce.Entities.Carts.ShippingInfo>();

            foreach (var shippingMethod in shippingMethods)
            {
                shippingMethod.LineIds = cart.Lines.Select(lineItem => lineItem.ExternalCartLineId).ToList();
                shippings.Add(this.shippingMapper.Map<ShippingMethod, CommerceShippingInfo>(shippingMethod));
            }

            var addShippingInfoResult = this.cartManager.AddShippingInfo(cart, shippingOptionType, shippings);

            if (!addShippingInfoResult.Success)
            {
                result.SetErrors(addShippingInfoResult.SystemMessages.Select(m => m.Message).ToList());
            }

            return result;
        }

        private void AddShippingOptions(Result<DeliveryInfo> result, Cart cart)
        {
            var getShippingOptionsResult = this.shippingManager.GetShippingOptions(cart);

            if (!getShippingOptionsResult.Success)
            {
                result.SetErrors(getShippingOptionsResult.SystemMessages.Select(m => m.Message).ToList());
            }
            else
            {
                result.Data.ShippingOptions =
                    this.shippingMapper
                        .Map<IReadOnlyCollection<ShippingOption>,
                            List<Models.Entities.Shipping.ShippingOption>>(getShippingOptionsResult.ShippingOptions);
            }
        }

        private void AddUserInfo<T>(BaseCheckoutInfo baseCheckoutInfo, Result<T> result)
            where T : class
        {
            var getPartiesResult = this.accountManager.GetCustomerParties(this.visitorContext.ContactId);

            if (getPartiesResult.Success)
            {
                // Mapping of each Party to Address instead of mapping collection
                // because Runtime Polymorphism in AutoMapper doesn't work correctly for collections. 
                // Here Party is actually CommerceParty
                baseCheckoutInfo.UserAddresses = getPartiesResult.Parties
                    .Select(party => this.shippingMapper.Map<Party, Address>(party))
                    .ToList();
            }
            else
            {
                result.SetErrors(getPartiesResult.SystemMessages.Select(m => m.Message).ToList());
            }
        }

        private Cart RemoveAllShipmentFromCart(Cart cart)
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