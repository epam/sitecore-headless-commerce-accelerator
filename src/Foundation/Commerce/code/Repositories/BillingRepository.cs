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
    using Sitecore.Commerce.Entities.Payments;
    using Sitecore.Commerce.Services;
    using Sitecore.Commerce.Services.Carts;
    using Sitecore.Commerce.Services.Payments;
    using Sitecore.Diagnostics;
    using PaymentMethodModel = Models.Checkout.PaymentMethodModel;
    using PaymentOptionModel = Models.Checkout.PaymentOptionModel;

    [Service(typeof(IBillingRepository), Lifetime = Lifetime.Singleton)]
    public class BillingRepository : BaseCheckoutRepository, IBillingRepository
    {
        public BillingRepository(
            IPaymentManager paymentManager,
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
            PaymentManager = paymentManager;
        }

        protected IPaymentManager PaymentManager { get; }


        public virtual Result<BillingModel> GetBillingData()
        {
            var result = new Result<BillingModel>();
            var model = new BillingModel();

            if (!Sitecore.Context.PageMode.IsExperienceEditor)
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

                    Cart cartResult = currentCart.Result;
                    if (cartResult.Lines != null && cartResult.Lines.Any())
                    {
                        ////result.Initialize(result, visitorContext);
                        AddPaymentOptions(result, cartResult);
                        if (result.Success)
                        {
                            AddPaymentMethods(result, cartResult);
                            if (result.Success)
                            {
                                AddPaymentClientToken(result);
                                if (result.Success)
                                    AddUserInfo(result.Data, result);
                                ////if (result.Success)
                                ////{
                                ////    this.AddAvailableCountries((BaseCheckoutDataJsonResult)model);
                                ////    if (result.Success)
                                ////        this.CheckForDigitalProductInCart(model, cartResult);
                                ////}
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message, ex, this);
                    result.SetErrors(nameof(GetBillingData), ex);
                    return result;
                }

            return result;
        }

        public Result<VoidResult> SetPaymentMethods(SetPaymentArgs args)
        {
            var result = new Result<VoidResult>();
            var model = new VoidResult();

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

                ManagerResponse<CartResult, Cart> updateCartResponse = CartManager.UpdateCart(
                    StorefrontContext.ShopName,
                    currentCart.Result,
                    new CartBase
                    {
                        Email = string.IsNullOrWhiteSpace(args.BillingAddress.Email)
                            ? VisitorContext.CurrentUser.Email
                            : args.BillingAddress.Email
                    });

                if (!updateCartResponse.ServiceProviderResult.Success &&
                    updateCartResponse.ServiceProviderResult.SystemMessages.Any())
                {
                    result.SetErrors(updateCartResponse.ServiceProviderResult);
                    return result;
                }

                PartyEntity billingParty = EntityMapper.MapToPartyEntity(args.BillingAddress);
                FederatedPaymentArgs federatedPaymentArgs =
                    EntityMapper.MapToFederatedPaymentArgs(args.FederatedPayment);

                ManagerResponse<AddPaymentInfoResult, Cart> paymentInfoResponse = CartManager.AddPaymentInfo(
                    StorefrontContext.ShopName,
                    updateCartResponse.Result,
                    billingParty,
                    federatedPaymentArgs);

                if (!paymentInfoResponse.ServiceProviderResult.Success)
                {
                    result.SetErrors(paymentInfoResponse.ServiceProviderResult);
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.SetErrors(nameof(SetPaymentMethods), ex);
            }

            return result;
        }

        protected virtual void AddPaymentOptions(Result<BillingModel> result, Cart cart)
        {
            ManagerResponse<GetPaymentOptionsResult, IEnumerable<PaymentOption>> paymentOptions =
                PaymentManager.GetPaymentOptions(StorefrontContext.ShopName, cart);

            if (paymentOptions.ServiceProviderResult.Success && paymentOptions.Result != null)
            {
                result.Data.PaymentOptions = new List<PaymentOptionModel>();
                foreach (PaymentOption paymentOption in paymentOptions.Result)
                {
                    var model = new PaymentOptionModel
                    {
                        Name = paymentOption.Name,
                        Description = paymentOption.Description,
                        PaymentOptionTypeName = paymentOption.PaymentOptionType.Name
                    };

                    result.Data.PaymentOptions.Add(model);
                }
            }
            else
            {
                result.SetErrors(paymentOptions.ServiceProviderResult);
            }
        }

        protected virtual void AddPaymentMethods(Result<BillingModel> result, Cart cart)
        {
            var paymentOption = new PaymentOption
            {
                PaymentOptionType = PaymentOptionType.PayCard
            };

            ManagerResponse<GetPaymentMethodsResult, IEnumerable<PaymentMethod>> paymentMethods =
                PaymentManager.GetPaymentMethods(cart, paymentOption);

            if (paymentMethods.ServiceProviderResult.Success && paymentMethods.Result != null)
            {
                result.Data.PaymentMethods = new List<PaymentMethodModel>();
                foreach (PaymentMethod paymentMethod in paymentMethods.Result)
                {
                    var model = new PaymentMethodModel();
                    model.Description = paymentMethod.Description;
                    model.ExternalId = paymentMethod.PaymentOptionId;
                    result.Data.PaymentMethods.Add(model);
                }
            }
            else
            {
                result.SetErrors(paymentMethods.ServiceProviderResult);
            }
        }

        protected virtual void AddPaymentClientToken(Result<BillingModel> result)
        {
            ManagerResponse<ServiceProviderResult, string> paymentClientToken = PaymentManager.GetPaymentClientToken();
            if (paymentClientToken.ServiceProviderResult.Success)
                result.Data.PaymentClientToken = paymentClientToken.Result;
            result.SetErrors(paymentClientToken.ServiceProviderResult);
        }
    }
}