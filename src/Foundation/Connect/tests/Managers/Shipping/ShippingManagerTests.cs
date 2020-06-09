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

namespace HCA.Foundation.Connect.Tests.Managers.Shipping
{
    using System;

    using Base.Models.Logging;
    using Base.Services.Logging;
    using Base.Tests.Customization;

    using Connect.Managers.Shipping;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Providers;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services.Shipping;

    using Xunit;

    using GetShippingMethodsRequest = Sitecore.Commerce.Engine.Connect.Services.Shipping.GetShippingMethodsRequest;

    public class ShippingManagerTests
    {
        private readonly IFixture fixture;

        private readonly ShippingManager shippingManager;

        private readonly ShippingServiceProvider shippingServiceProvider;

        public ShippingManagerTests()
        {
            var connectServiceProvider = Substitute.For<IConnectServiceProvider>();
            var logService = Substitute.For<ILogService<CommonLog>>();

            this.shippingServiceProvider = Substitute.For<ShippingServiceProvider>();

            connectServiceProvider.GetShippingServiceProvider().Returns(this.shippingServiceProvider);
            this.fixture = new Fixture().Customize(new OmitOnRecursionCustomization());

            this.shippingManager = Substitute.For<ShippingManager>(connectServiceProvider, logService);
        }

        [Fact]
        public void GetShippingMethods_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.shippingManager.GetShippingMethods(null, this.fixture.Create<ShippingOptionType>()));
            Assert.Throws<ArgumentNullException>(
                () => this.shippingManager.GetShippingMethods(this.fixture.Create<Cart>(), null));
        }

        [Fact]
        public void GetShippingMethods_ShouldCallExecuteMethod()
        {
            // act
            this.shippingManager.GetShippingMethods(
                this.fixture.Create<CommerceCart>(),
                this.fixture.Create<ShippingOptionType>());

            // assert
            this.shippingManager.Received(1)
                .Execute(Arg.Any<GetShippingMethodsRequest>(), this.shippingServiceProvider.GetShippingMethods);
        }

        [Fact]
        public void GetShippingOptions_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.shippingManager.GetShippingOptions(null));
        }

        [Fact]
        public void GetShippingOptions_ShouldCallExecuteMethod()
        {
            // act
            this.shippingManager.GetShippingOptions(this.fixture.Create<Cart>());

            // assert
            this.shippingManager.Received(1)
                .Execute(Arg.Any<GetShippingOptionsRequest>(), this.shippingServiceProvider.GetShippingOptions);
        }
    }
}