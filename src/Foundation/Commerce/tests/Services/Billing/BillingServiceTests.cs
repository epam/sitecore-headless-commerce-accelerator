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

namespace HCA.Foundation.Commerce.Tests.Services.Billing
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Commerce.Services.Billing;
    using Connect.Managers.Cart;

    using HCA.Foundation.Base.Tests.Customization;

    using Ploeh.AutoFixture.Xunit2;
    using Sitecore.Collections;
    using Sitecore.Commerce.Services;
    using Sitecore.Commerce.Services.Carts;
    using Sitecore.FakeDb.Sites;
    using Sitecore.Sites;
    using System.Diagnostics.CodeAnalysis;

    using Base.Models.Result;

    using Commerce.Mappers.Payment;

    using Connect.Context.Storefront;
    using Connect.Managers.Payment;
    using Connect.Models;
    using Connect.Models.Payment;

    using Context;

    using Models.Entities.Addresses;
    using Models.Entities.Billing;

    using NSubstitute;
    using NSubstitute.Core;

    using Ploeh.AutoFixture;

    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Payments;
    using Sitecore.Commerce.Services.Payments;

    using Xunit;

    public class BillingServiceTests
    {
        private readonly FakeSiteContext siteContext;
        private readonly ICartManager cartManager;
        private readonly IStorefrontContext storefrontContex;
        private readonly IVisitorContext visitorContex;
        private readonly IPaymentMapper paymentMapper;
        private readonly IPaymentManager paymentManager;

        private readonly IBillingService billingService;

        public BillingServiceTests()
        {
            var stringDictionary = new StringDictionary
            {
                { "database", "master" },
                { "enableWebEdit", "true" },
                { "masterDatabase", "master" }
            };

            this.siteContext = new FakeSiteContext(stringDictionary);

            this.cartManager = Substitute.For<ICartManager>();
            this.paymentMapper = Substitute.For<IPaymentMapper>();
            this.storefrontContex = Substitute.For<IStorefrontContext>();
            this.visitorContex = Substitute.For<IVisitorContext>();
            this.paymentManager = Substitute.For<IPaymentManager>();

            this.billingService = new BillingService(
                this.cartManager,
                this.storefrontContex,
                this.visitorContex,
                this.paymentMapper,
                this.paymentManager);
        }

        [Fact]
        public void GetBillingInfo_IfEditModeIsTrue_ShouldReturnsEmptyBillingInfo()
        {
            //arrange
            this.siteContext.SetDisplayMode(DisplayMode.Edit, DisplayModeDuration.Remember);

            //act
            Result<BillingInfo> result;
            using (new SiteContextSwitcher(this.siteContext))
            {
                result = this.billingService.GetBillingInfo();
            }

            //assert
            Assert.True(result.Success);
        }

        [Theory, AutoNSubstituteData]
        public void GetBillingInfo_IfLoadCartFailed_ShouldReturnsError(CartResult cartResult, SystemMessage systemMessage)
        {
            //arrange
            cartResult.Success = false;
            cartResult.SystemMessages.Add(systemMessage);
            this.cartManager.LoadCart(Arg.Any<string>(), Arg.Any<string>()).Returns(cartResult);

            //act
            Result<BillingInfo> result;
            using (new SiteContextSwitcher(this.siteContext))
            {
                result = this.billingService.GetBillingInfo();
            }

            //assert
            Assert.False(result.Success);
            Assert.Equal(systemMessage.Message, result.Errors[0]);
        }

        [Theory, AutoNSubstituteData]
        public void GetBillingInfo_IfCartIsEmpty_ShouldReturnsEmptyBillingInfo(CartResult cartResult)
        {
            //arrange
            cartResult.Cart = new Cart();
            this.cartManager.LoadCart(Arg.Any<string>(), Arg.Any<string>()).Returns(cartResult);

            //act
            Result<BillingInfo> result;
            using (new SiteContextSwitcher(this.siteContext))
            {
                result = this.billingService.GetBillingInfo();
            }

            //assert
            Assert.True(result.Success);
        }

        [Theory, AutoNSubstituteData]
        public void GetBillingInfo_IfGetPaymentOptionsFailed_ShouldReturnsError(
            CartResult cartResult,
            GetPaymentOptionsResult optionsResult,
            SystemMessage systemMessage)
        {
            //arrange
            cartResult.Success = true;
            this.cartManager.LoadCart(Arg.Any<string>(), Arg.Any<string>()).Returns(cartResult);

            optionsResult.Success = false;
            optionsResult.SystemMessages.Add(systemMessage);
            this.paymentManager.GetPaymentOptions(Arg.Any<string>(), Arg.Any<Cart>()).Returns(optionsResult);

            //act
            Result<BillingInfo> result;
            using (new SiteContextSwitcher(this.siteContext))
            {
                result = this.billingService.GetBillingInfo();
            }

            //assert
            Assert.False(result.Success);
            Assert.Equal(systemMessage.Message, result.Errors[0]);
        }

        [Theory, AutoNSubstituteData]
        public void GetBillingInfo_IfGetPaymentMethodsFailed_ShouldReturnsError(
            CartResult cartResult,
            GetPaymentOptionsResult optionsResult,
            GetPaymentMethodsResult methodsResult,
            SystemMessage systemMessage,
            List<Models.Entities.Payment.PaymentOption> mappedPaymentOptions)
        {
            //arrange
            cartResult.Success = true;
            this.cartManager.LoadCart(Arg.Any<string>(), Arg.Any<string>()).Returns(cartResult);

            optionsResult.Success = true;
            this.paymentManager.GetPaymentOptions(Arg.Any<string>(), Arg.Any<Cart>()).Returns(optionsResult);

            methodsResult.Success = false;
            methodsResult.SystemMessages.Add(systemMessage);
            this.paymentManager.GetPaymentMethods(Arg.Any<Cart>(), Arg.Any<PaymentOption>()).Returns(methodsResult);

            this.paymentMapper.Map<IReadOnlyCollection<PaymentOption>, List<Models.Entities.Payment.PaymentOption>>(optionsResult.PaymentOptions)
                .Returns(mappedPaymentOptions);

            //act
            Result<BillingInfo> result;
            using (new SiteContextSwitcher(this.siteContext))
            {
                result = this.billingService.GetBillingInfo();
            }

            //assert
            Assert.False(result.Success);
            Assert.Equal(systemMessage.Message, result.Errors[0]);
        }

        [Theory, AutoNSubstituteData]
        public void GetBillingInfo_IfGetClientTokenFailed_ShouldReturnsError(
            CartResult cartResult,
            GetPaymentOptionsResult optionsResult,
            GetPaymentMethodsResult methodsResult,
            PaymentClientTokenResult clientTokenResult,
            List<Models.Entities.Payment.PaymentOption> mappedPaymentOptions,
            List<Models.Entities.Payment.PaymentMethod> mappedPaymentMethods,
            SystemMessage systemMessage)
        {
            //arrange
            cartResult.Success = true;
            this.cartManager.LoadCart(Arg.Any<string>(), Arg.Any<string>()).Returns(cartResult);

            optionsResult.Success = true;
            optionsResult.SystemMessages.Add(systemMessage);
            this.paymentManager.GetPaymentOptions(Arg.Any<string>(), Arg.Any<Cart>()).Returns(optionsResult);

            methodsResult.Success = true;
            methodsResult.SystemMessages.Add(systemMessage);
            this.paymentManager.GetPaymentMethods(Arg.Any<Cart>(), Arg.Any<PaymentOption>())
                .Returns(methodsResult);

            this.paymentMapper.Map<IReadOnlyCollection<PaymentOption>, List<Models.Entities.Payment.PaymentOption>>(optionsResult.PaymentOptions)
               .Returns(mappedPaymentOptions);

            this.paymentMapper.Map<IReadOnlyCollection<PaymentMethod>, List<Models.Entities.Payment.PaymentMethod>>(methodsResult.PaymentMethods)
                .Returns(mappedPaymentMethods);

            clientTokenResult.Success = false;
            clientTokenResult.SystemMessages.Add(systemMessage);
            this.paymentManager.GetPaymentClientToken().Returns(clientTokenResult);

            //act
            Result<BillingInfo> result;
            using (new SiteContextSwitcher(this.siteContext))
            {
                result = this.billingService.GetBillingInfo();
            }

            //assert
            Assert.False(result.Success);
            Assert.Equal(systemMessage.Message, result.Errors[0]);
        }

        [Theory, AutoNSubstituteData]
        public void SetPaymentInfo_IfAddressIsNull_ShouldReturnsException(Models.Entities.Payment.FederatedPaymentInfo federatedPaymentInfo)
        {
            //arrange
            Action Act = () => this.billingService.SetPaymentInfo(null, federatedPaymentInfo);

            //act & assert
            Assert.Throws<ArgumentNullException>(Act);
        }

        [Theory, AutoNSubstituteData]
        public void SetPaymentInfo_IfFederatedPaymentInfoIsNull_ShouldReturnsException(Address address)
        {
            //arrange
            Action Act = () => this.billingService.SetPaymentInfo(address, null);

            //act & assert
            Assert.Throws<ArgumentNullException>(Act);
        }

        [Theory, AutoNSubstituteData]
        public void SetPaymentInfo__IfLoadCartFailed_ShouldReturnsError(
            CartResult cartResult,
            Address address,
            Models.Entities.Payment.FederatedPaymentInfo federatedPaymentInfo,
            SystemMessage systemMessage)
        {
            //arrange
            cartResult.Success = false;
            cartResult.SystemMessages.Add(systemMessage);
            this.cartManager.LoadCart(Arg.Any<string>(), Arg.Any<string>()).Returns(cartResult);

            //act
            var result = this.billingService.SetPaymentInfo(address, federatedPaymentInfo);

            //assert
            Assert.False(result.Success);
            Assert.Equal(systemMessage.Message, result.Errors[0]);
        }

        [Theory, AutoNSubstituteData]
        public void SetPaymentInfo_IfUpdateCartFailed_ShouldReturnsError(
            CartResult loadCartResult,
            CartResult updateCartResult,
            Address address,
            Models.Entities.Payment.FederatedPaymentInfo federatedPaymentInfo,
            SystemMessage systemMessage)
        {
            //arrange
            loadCartResult.Success = true;
            this.cartManager.LoadCart(Arg.Any<string>(), Arg.Any<string>()).Returns(loadCartResult);
            updateCartResult.Success = false;
            updateCartResult.SystemMessages.Add(systemMessage);
            this.cartManager.UpdateCart(Arg.Any<Cart>(), Arg.Any<CartBase>()).Returns(updateCartResult);

            //act
            var result = this.billingService.SetPaymentInfo(address, federatedPaymentInfo);

            //assert
            Assert.False(result.Success);
            Assert.Equal(systemMessage.Message, result.Errors[0]);
        }

        [Theory, AutoNSubstituteData]
        public void SetPaymentInfo_IfRemovePaymentInfoFailed_ShouldReturnsError(
            CartResult loadCartResult,
            CartResult updateCartResult,
            CartResult removeCartResult,
            Address address,
            Models.Entities.Payment.FederatedPaymentInfo federatedPaymentInfo,
            SystemMessage systemMessage)
        {
            //arrange
            loadCartResult.Success = true;
            this.cartManager.LoadCart(Arg.Any<string>(), Arg.Any<string>()).Returns(loadCartResult);
            updateCartResult.Success = true;
            this.cartManager.UpdateCart(Arg.Any<Cart>(), Arg.Any<CartBase>()).Returns(updateCartResult);
            removeCartResult.Success = false;
            removeCartResult.SystemMessages.Add(systemMessage);
            this.cartManager.RemovePaymentInfo(Arg.Any<Cart>()).Returns(removeCartResult);

            //act
            var result = this.billingService.SetPaymentInfo(address, federatedPaymentInfo);

            //assert
            Assert.False(result.Success);
            Assert.Equal(systemMessage.Message, result.Errors[0]);
        }

        [Theory, AutoNSubstituteData]
        public void SetPaymentInfo_IfAddPaymentInfoFailed_ShouldReturnsError(
            CartResult loadCartResult,
            CartResult updateCartResult,
            CartResult removeCartResult,
            AddPaymentInfoResult addCartResult,
            Address address,
            Party party,
            Models.Entities.Payment.FederatedPaymentInfo federatedPaymentInfo,
            SystemMessage systemMessage)
        {
            //arrange
            loadCartResult.Success = true;
            this.cartManager.LoadCart(Arg.Any<string>(), Arg.Any<string>()).Returns(loadCartResult);

            updateCartResult.Success = true;
            this.cartManager.UpdateCart(Arg.Any<Cart>(), Arg.Any<CartBase>()).Returns(updateCartResult);

            removeCartResult.Success = true;
            this.cartManager.RemovePaymentInfo(Arg.Any<Cart>()).Returns(removeCartResult);

            addCartResult.Success = false;
            addCartResult.SystemMessages.Add(systemMessage);
            this.cartManager.AddPaymentInfo(Arg.Any<Cart>(), Arg.Any<Party>(), Arg.Any<FederatedPaymentInfo>()).Returns(addCartResult);

            this.paymentMapper.Map<Address, Party>(address).Returns(party);

            //act
            var result = this.billingService.SetPaymentInfo(address, federatedPaymentInfo);

            //assert
            Assert.False(result.Success);
            Assert.Equal(systemMessage.Message, result.Errors[0]);
        }
    }
}