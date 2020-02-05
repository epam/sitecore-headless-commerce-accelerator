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
    using Connect.Managers;
    using Connect.Models;
    using Context;
    using DependencyInjection;
    using ModelInitilizers;
    using ModelMappers;
    using Models;
    using Models.Checkout;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services.Carts;
    using Sitecore.Commerce.Services.Shipping;
    using Sitecore.Diagnostics;
    using Utils;

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
            : base(cartManager, catalogRepository, accountManager, cartModelBuilder, entityMapper, storefrontContext,
                visitorContext)
        {
            ShippingManager = shippingManager;
        }

        protected IShippingManager ShippingManager { get; }

        public Result<ShippingModel> GetShippingMethods(GetShippingArgs getShippingArgs)
        {
            var model = new ShippingModel();
            var result = new Result<ShippingModel>();
            try
            {
                result.SetResult(model);
                ManagerResponse<CartResult, Cart> currentCart =
                    CartManager.GetCurrentCart(StorefrontContext.ShopName, VisitorContext.ContactId);
                if (!currentCart.ServiceProviderResult.Success)
                {
                    result.SetErrors(currentCart.ServiceProviderResult);
                    return result;
                }

                var shippingOptionType =
                    ConnectOptionTypeHelper.ToShippingOptionType(getShippingArgs.ShippingPreferenceType);
                PartyEntity address = null;
                if (getShippingArgs.ShippingAddress != null)
                    address = EntityMapper.MapToPartyEntity(getShippingArgs.ShippingAddress);

                ManagerResponse<GetShippingMethodsResult, IReadOnlyCollection<ShippingMethod>> shippingMethods =
                    ShippingManager.GetShippingMethods(
                        StorefrontContext.ShopName,
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
                foreach (ShippingMethod shippingMethod in shippingMethods.ServiceProviderResult.ShippingMethods)
                {
                    ShippingMethodModel shippingModel = EntityMapper.MapToShippingMethodModel(shippingMethod);
                    result.Data.ShippingMethods.Add(shippingModel);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);
                result.SetErrors(nameof(GetShippingMethods), ex);
            }

            return result;
        }

        public Result<SetShippingModel> SetShippingMethods(SetShippingArgs setShippingArgs)
        {
            var result = new Result<SetShippingModel>();

            try
            {
                result.SetResult(new SetShippingModel {Success = true});
                ManagerResponse<CartResult, Cart> currentCart =
                    CartManager.GetCurrentCart(StorefrontContext.ShopName, VisitorContext.ContactId);

                if (!currentCart.ServiceProviderResult.Success)
                {
                    result.SetErrors(currentCart.ServiceProviderResult);
                    return result;
                }

                Cart cart = currentCart.Result;
                List<PartyEntity> partyEntityList =
                    EntityMapper.MapToPartyEntityList(setShippingArgs.ShippingAddresses);
                var shippingOptionType =
                    ConnectOptionTypeHelper.ToShippingOptionType(setShippingArgs.OrderShippingPreferenceType);
                List<ShippingInfoArgument> shippingInfo =
                    EntityMapper.MapToShippingInfoArgumentList(setShippingArgs.ShippingMethods);

                ManagerResponse<AddShippingInfoResult, Cart> managerResponse =
                    CartManager.AddShippingInfo(cart, partyEntityList, shippingOptionType, shippingInfo);

                if (!managerResponse.ServiceProviderResult.Success)
                {
                    result.SetErrors(managerResponse.ServiceProviderResult);
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.SetErrors(nameof(SetShippingMethods), ex);
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
                ManagerResponse<CartResult, Cart> cartResult =
                    CartManager.GetCurrentCart(StorefrontContext.ShopName, VisitorContext.ContactId);
                if (!cartResult.ServiceProviderResult.Success)
                {
                    result.SetErrors(cartResult.ServiceProviderResult);
                    return result;
                }

                Cart currentCart = cartResult.Result;
                if (currentCart.Lines != null && currentCart.Lines.Any())
                {
                    AddShippingOptionsToModel(result, currentCart);
                    if (result.Success)
                        ////this.AddEmailShippingMethodToResult(model, result);
                        if (result.Success)
                            ////this.AddAvailableCountries((BaseCheckoutDataJsonResult)model);
                            if (result.Success)
                                AddUserInfo(result.Data, result);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);
                result.SetErrors(nameof(GetDeliveryData), ex);
                return result;
            }

            return result;
        }

        protected virtual void AddShippingOptionsToModel(Result<DeliveryModel> result, Cart cart)
        {
            ManagerResponse<GetShippingOptionsResult, List<ShippingOption>> shippingPreferences =
                ShippingManager.GetShippingPreferences(cart);
            if (!shippingPreferences.ServiceProviderResult.Success)
            {
                result.SetErrors(shippingPreferences.ServiceProviderResult);
            }
            else
            {
                result.Data.ShippingOptions = new List<ShippingOptionModel>();
                foreach (ShippingOption shipppingOption in shippingPreferences.Result)
                {
                    ShippingOptionModel model = EntityMapper.MapToShippingOptionModel(shipppingOption);
                    result.Data.ShippingOptions.Add(model);
                }
            }
        }
    }
}