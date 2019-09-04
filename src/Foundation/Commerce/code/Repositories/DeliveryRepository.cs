//    Copyright 2019 EPAM Systems, Inc.
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

namespace Wooli.Foundation.Commerce.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services.Carts;
    using Sitecore.Commerce.Services.Shipping;
    using Sitecore.Diagnostics;

    using Wooli.Foundation.Commerce.Context;
    using Wooli.Foundation.Commerce.ModelInitilizers;
    using Wooli.Foundation.Commerce.ModelMappers;
    using Wooli.Foundation.Commerce.Models;
    using Wooli.Foundation.Commerce.Utils;
    using Wooli.Foundation.Connect.Managers;
    using Wooli.Foundation.Connect.Models;
    using Wooli.Foundation.DependencyInjection;

    [Service(typeof(IDeliveryRepository), Lifetime = Lifetime.Singleton)]
    public class DeliveryRepository : BaseCheckoutRepository, IDeliveryRepository
    {
        public DeliveryRepository(
            IShippingManager shippingManager,
            ICartManager cartManager,
            ICatalogRepository catalogRepository,
            IAccountManager accountManager,
            ICartModelBuilder cartModelBuilder,
            IEntityMapper entityMapper,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext)
            : base(cartManager, catalogRepository, accountManager, cartModelBuilder, entityMapper, storefrontContext, visitorContext)
        {
            this.ShippingManager = shippingManager;
        }

        protected IShippingManager ShippingManager { get; }

        public Result<ShippingModel> GetShippingMethods(GetShippingArgs getShippingArgs)
        {
            var model = new ShippingModel();
            var result = new Result<ShippingModel>();
            try
            {
                result.SetResult(model);
                ManagerResponse<CartResult, Cart> currentCart = this.CartManager.GetCurrentCart(this.StorefrontContext.ShopName, this.VisitorContext.ContactId);
                if (!currentCart.ServiceProviderResult.Success)
                {
                    result.SetErrors(currentCart.ServiceProviderResult);
                    return result;
                }

                ShippingOptionType shippingOptionType = ConnectOptionTypeHelper.ToShippingOptionType(getShippingArgs.ShippingPreferenceType);
                PartyEntity address = null;
                if (getShippingArgs.ShippingAddress != null)
                {
                    address = this.EntityMapper.MapToPartyEntity(getShippingArgs.ShippingAddress);
                }

                ManagerResponse<GetShippingMethodsResult, IReadOnlyCollection<ShippingMethod>> shippingMethods =
                    this.ShippingManager.GetShippingMethods(
                        this.StorefrontContext.ShopName,
                        currentCart.Result,
                        shippingOptionType,
                        address,
                        null);
                if (!currentCart.ServiceProviderResult.Success)
                {
                    result.SetErrors(currentCart.ServiceProviderResult);
                    return result;
                }

                result.Data.ShippingMethods = new List<ShippingMethodModel>();
                foreach (var shippingMethod in shippingMethods.ServiceProviderResult.ShippingMethods)
                {
                    var shippingModel = this.EntityMapper.MapToShippingMethodModel(shippingMethod);
                    result.Data.ShippingMethods.Add(shippingModel);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);
                result.SetErrors(nameof(this.GetShippingMethods), ex);
            }

            return result;
        }

        public Result<SetShippingModel> SetShippingMethods(SetShippingArgs setShippingArgs)
        {
            var result = new Result<SetShippingModel>();

            try
            {
                result.SetResult(new SetShippingModel() { Success = true });
                var currentCart = this.CartManager.GetCurrentCart(this.StorefrontContext.ShopName, this.VisitorContext.ContactId);

                if (!currentCart.ServiceProviderResult.Success)
                {
                    result.SetErrors(currentCart.ServiceProviderResult);
                    return result;
                }

                var cart = currentCart.Result;
                var partyEntityList = this.EntityMapper.MapToPartyEntityList(setShippingArgs.ShippingAddresses);
                var shippingOptionType = ConnectOptionTypeHelper.ToShippingOptionType(setShippingArgs.OrderShippingPreferenceType);
                var shippingInfo = this.EntityMapper.MapToShippingInfoArgumentList(setShippingArgs.ShippingMethods);

                var managerResponse = this.CartManager.AddShippingInfo(cart, partyEntityList, shippingOptionType, shippingInfo);

                if (!managerResponse.ServiceProviderResult.Success)
                {
                    result.SetErrors(managerResponse.ServiceProviderResult);
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.SetErrors(nameof(this.SetShippingMethods), ex);
            }

            return result;
        }

        public virtual Result<DeliveryModel> GetDeliveryData()
        {
            var model = new DeliveryModel
            {
                NewPartyId = Guid.NewGuid().ToString("N").ToLower()
            };
            var result = new Result<DeliveryModel>();
            try
            {
                result.SetResult(model);
                ManagerResponse<CartResult, Cart> cartResult = this.CartManager.GetCurrentCart(this.StorefrontContext.ShopName, this.VisitorContext.ContactId);
                if (!cartResult.ServiceProviderResult.Success)
                {
                    result.SetErrors(cartResult.ServiceProviderResult);
                    return result;
                }

                Cart currentCart = cartResult.Result;
                if (currentCart.Lines != null && currentCart.Lines.Any())
                {
                    this.AddShippingOptionsToModel(result, currentCart);
                    if (result.Success)
                    {
                        ////this.AddEmailShippingMethodToResult(model, result);
                        if (result.Success)
                        {
                            ////this.AddAvailableCountries((BaseCheckoutDataJsonResult)model);
                            if (result.Success)
                            {
                                this.AddUserInfo(result.Data, result);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);
                result.SetErrors(nameof(this.GetDeliveryData), ex);
                return result;
            }

            return result;
        }

        protected virtual void AddShippingOptionsToModel(Result<DeliveryModel> result, Cart cart)
        {
            ManagerResponse<GetShippingOptionsResult, List<ShippingOption>> shippingPreferences = this.ShippingManager.GetShippingPreferences(cart);
            if (!shippingPreferences.ServiceProviderResult.Success)
            {
                result.SetErrors(shippingPreferences.ServiceProviderResult);
            }
            else
            {
                result.Data.ShippingOptions = new List<ShippingOptionModel>();
                foreach (var shipppingOption in shippingPreferences.Result)
                {
                    var model = this.EntityMapper.MapToShippingOptionModel(shipppingOption);
                    result.Data.ShippingOptions.Add(model);
                }
            }
        }
    }
}
