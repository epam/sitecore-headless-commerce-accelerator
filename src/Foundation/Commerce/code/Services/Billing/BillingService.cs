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

namespace Wooli.Foundation.Commerce.Services.Billing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Connect.Context;
    using Connect.Managers.Payment;

    using Context;

    using Base.Models;

    using Connect.Managers.Cart;
    using Connect.Models;

    using DependencyInjection;

    using Mappers.Payment;

    using Models.Entities.Addresses;
    using Models.Entities.Billing;

    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Diagnostics;

    using Sitecore.Commerce.Entities.Payments;
    using Sitecore.Commerce.Services.Carts;

    using FederatedPaymentInfo = Models.Entities.Payment.FederatedPaymentInfo;
    using PaymentOption = Models.Entities.Payment.PaymentOption;
    using PaymentMethod = Models.Entities.Payment.PaymentMethod;

    [Service(typeof(IBillingService), Lifetime = Lifetime.Singleton)]
    public class BillingService : IBillingService
    {
        private readonly ICartManagerV2 cartManager;
        private readonly IPaymentMapper paymentMapper;
        private readonly IPaymentManagerV2 paymentManager;
        private readonly IStorefrontContext storefrontContext;
        private readonly IVisitorContext visitorContext;

        public BillingService(
            ICartManagerV2 cartManager,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext,
            IPaymentMapper paymentMapper,
            IPaymentManagerV2 paymentManager)
        {
            Assert.ArgumentNotNull(cartManager, nameof(cartManager));
            Assert.ArgumentNotNull(paymentMapper, nameof(paymentMapper));
            Assert.ArgumentNotNull(paymentManager, nameof(paymentManager));
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));
            Assert.ArgumentNotNull(visitorContext, nameof(visitorContext));

            this.cartManager = cartManager;
            this.paymentMapper = paymentMapper;
            this.storefrontContext = storefrontContext;
            this.visitorContext = visitorContext;
            this.paymentManager = paymentManager;
        }

        public Result<BillingInfo> GetBillingInfo()
        {
            var model = new BillingInfo();
            var result = new Result<BillingInfo>(model);

            if (!Sitecore.Context.PageMode.IsExperienceEditor)
            {
                result.SetResult(model);
                var cartResult = this.cartManager.LoadCart(
                    this.storefrontContext.ShopName,
                    this.visitorContext.ContactId);

                if (!cartResult.Success)
                {
                    result.SetErrors(cartResult.SystemMessages.Select(m => m.Message).ToList());

                    return result;
                }

                if (cartResult.Cart.Lines != null && cartResult.Cart.Lines.Any())
                {
                    this.AddPaymentOptions(result, cartResult.Cart);

                    if (result.Success)
                    {
                        this.AddPaymentMethods(result, cartResult.Cart);

                        if (result.Success)
                        {
                            this.AddPaymentClientToken(result);
                        }
                    }
                }
            }

            return result;
        }

        public Result<VoidResult> SetPaymentInfo(Address billingAddress, FederatedPaymentInfo federatedPayment)
        {
            Assert.ArgumentNotNull(billingAddress, nameof(billingAddress));
            Assert.ArgumentNotNull(federatedPayment, nameof(federatedPayment));

            var result = new Result<VoidResult>(new VoidResult());
            var cartResult = this.cartManager.LoadCart(
                this.storefrontContext.ShopName,
                this.visitorContext.ContactId);

            if (!cartResult.Success)
            {
                result.SetErrors(cartResult.SystemMessages.Select(m => m.Message).ToList());

                return result;
            }

            cartResult = this.UpdateCartEmail(cartResult.Cart, billingAddress.Email);

            if (!cartResult.Success)
            {
                result.SetErrors(cartResult.SystemMessages.Select(m => m.Message).ToList());

                return result;
            }

            if (cartResult.Cart.Payment != null && cartResult.Cart.Payment.Any())
            {
                cartResult = this.cartManager.RemovePaymentInfo(cartResult.Cart);

                if (!cartResult.Success)
                {
                    result.SetErrors(cartResult.SystemMessages.Select(m => m.Message).ToList());

                    return result;
                }
            }

            var party = this.CreatePartyFromAddress(billingAddress);
            var federatedPaymentInfo = this.paymentMapper.Map<FederatedPaymentInfo, Sitecore.Commerce.Entities.Carts.FederatedPaymentInfo>(federatedPayment);
            var addPaymentInfoResult = this.cartManager.AddPaymentInfo(cartResult.Cart, party, federatedPaymentInfo);

            if (!addPaymentInfoResult.Success)
            {
                result.SetErrors(addPaymentInfoResult.SystemMessages.Select(m => m.Message).ToList());

                return result;
            }

            return result;
        }

        private Party CreatePartyFromAddress(Address address)
        {
            var party = this.paymentMapper.Map<Address, Party>(address);

            party.PartyId = Guid.NewGuid().ToString("N");
            party.ExternalId = party.PartyId;

            if (string.IsNullOrWhiteSpace(party.Name))
            {
                party.Name = $"billing{party.PartyId}";
            }

            return party;
        }

        private CartResult UpdateCartEmail(Cart cart, string email)
        {
            return this.cartManager.UpdateCart(
                cart,
                new CartBase
                {
                    Email = string.IsNullOrWhiteSpace(email)
                        ? this.visitorContext.CurrentUser.Email
                        : email
                });
        }

        private void AddPaymentOptions(Result<BillingInfo> result, Cart cart)
        {
            var getPaymentOptionsResult = this.paymentManager.GetPaymentOptions(this.storefrontContext.ShopName, cart);

            if (getPaymentOptionsResult.Success)
            {
                result.Data.PaymentOptions =
                    this.paymentMapper
                        .Map<IReadOnlyCollection<Sitecore.Commerce.Entities.Payments.PaymentOption>, List<PaymentOption>
                        >(getPaymentOptionsResult.PaymentOptions);
            }
            else
            {
                result.SetErrors(getPaymentOptionsResult.SystemMessages.Select(m => m.Message).ToList());
            }
        }

        private void AddPaymentMethods(Result<BillingInfo> result, Cart cart)
        {
            var paymentOption = new Sitecore.Commerce.Entities.Payments.PaymentOption
            {
                PaymentOptionType = PaymentOptionType.PayCard
            };

            var getPaymentMethodsResult = this.paymentManager.GetPaymentMethods(cart, paymentOption);

            if (getPaymentMethodsResult.Success)
            {
                result.Data.PaymentMethods =
                    this.paymentMapper
                        .Map<IReadOnlyCollection<Sitecore.Commerce.Entities.Payments.PaymentMethod>, List<PaymentMethod>
                        >(getPaymentMethodsResult.PaymentMethods);
            }
            else
            {
                result.SetErrors(getPaymentMethodsResult.SystemMessages.Select(m => m.Message).ToList());
            }
        }

        private void AddPaymentClientToken(Result<BillingInfo> result)
        {
            var getPaymentClientTokenResult = this.paymentManager.GetPaymentClientToken();

            if (getPaymentClientTokenResult.Success)
            {
                result.Data.PaymentClientToken = getPaymentClientTokenResult.ClientToken;
            }
            else
            {
                result.SetErrors(getPaymentClientTokenResult.SystemMessages.Select(m => m.Message).ToList());
            }
        }
    }
}