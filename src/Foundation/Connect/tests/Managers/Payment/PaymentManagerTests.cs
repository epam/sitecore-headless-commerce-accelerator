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

namespace HCA.Foundation.Connect.Tests.Managers.Payment
{
    using System;

    using Base.Models.Logging;
    using Base.Services.Logging;
    using Base.Tests.Customization;

    using Connect.Managers.Payment;
    using Connect.Mappers.Payment;

    using Models.Payment;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Providers;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Payments;
    using Sitecore.Commerce.Services;
    using Sitecore.Commerce.Services.Payments;

    using Xunit;

    using GetPaymentMethodsRequest = Sitecore.Commerce.Engine.Connect.Services.Payments.GetPaymentMethodsRequest;

    public class PaymentManagerTests
    {
        private readonly IFixture fixture;

        private readonly PaymentManager paymentManager;

        private readonly PaymentServiceProvider paymentServiceProvider;

        public PaymentManagerTests()
        {
            var connectServiceProvider = Substitute.For<IConnectServiceProvider>();
            var logService = Substitute.For<ILogService<CommonLog>>();
            var paymentMapper = Substitute.For<IPaymentMapper>();

            this.paymentServiceProvider = Substitute.For<PaymentServiceProvider>();

            connectServiceProvider.GetPaymentServiceProvider().Returns(this.paymentServiceProvider);
            this.fixture = new Fixture().Customize(new OmitOnRecursionCustomization());

            this.paymentManager = Substitute.For<PaymentManager>(connectServiceProvider, paymentMapper, logService);
        }

        [Fact]
        public void GetPaymentClientToken_ShouldCallExecuteMethod()
        {
            // act
            this.paymentManager.GetPaymentClientToken();

            // assert
            this.paymentManager.Received(1)
                .Execute(
                    Arg.Any<ServiceProviderRequest>(),
                    Arg.Any<Func<ServiceProviderRequest, PaymentClientTokenResult>>());
        }

        [Fact]
        public void GetPaymentMethods_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.paymentManager.GetPaymentMethods(null, this.fixture.Create<PaymentOption>()));
            Assert.Throws<ArgumentNullException>(
                () => this.paymentManager.GetPaymentMethods(this.fixture.Create<Cart>(), null));
        }

        [Fact]
        public void GetPaymentMethods_ShouldCallExecuteMethod()
        {
            // act
            this.paymentManager.GetPaymentMethods(
                this.fixture.Create<CommerceCart>(),
                this.fixture.Create<PaymentOption>());

            // assert
            this.paymentManager.Received(1)
                .Execute(Arg.Any<GetPaymentMethodsRequest>(), this.paymentServiceProvider.GetPaymentMethods);
        }

        [Fact]
        public void GetPaymentOptions_IfParameterIsEmpty_ShouldThrowArgumentException()
        {
            // act & assert
            Assert.Throws<ArgumentException>(
                () => this.paymentManager.GetPaymentOptions(string.Empty, this.fixture.Create<Cart>()));
        }

        [Fact]
        public void GetPaymentOptions_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.paymentManager.GetPaymentOptions(null, this.fixture.Create<Cart>()));
            Assert.Throws<ArgumentNullException>(
                () => this.paymentManager.GetPaymentOptions(this.fixture.Create<string>(), null));
        }

        [Fact]
        public void GetPaymentOptions_ShouldCallExecuteMethod()
        {
            // act
            this.paymentManager.GetPaymentOptions(this.fixture.Create<string>(), this.fixture.Create<Cart>());

            // assert
            this.paymentManager.Received(1)
                .Execute(Arg.Any<GetPaymentOptionsRequest>(), this.paymentServiceProvider.GetPaymentOptions);
        }
    }
}