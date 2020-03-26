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

namespace Wooli.Foundation.Connect.Tests.Managers.Shipping
{
    using System;
    using System.Linq;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using Connect.Managers.Shipping;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Providers.Contracts;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services;
    using Sitecore.Commerce.Services.Shipping;

    using Xunit;

    public class ShippingManagerV2Tests
    {
        private readonly IShippingManagerV2 shippingManager;
        private readonly ILogService<CommonLog> logService;

        private readonly IFixture fixture;

        private readonly GetShippingMethodsResult getShippingMethodsResult;
        private readonly GetShippingOptionsResult getShippingOptionsResult;

        public ShippingManagerV2Tests()
        {
            var connectServiceProvider = Substitute.For<IConnectServiceProvider>();
            var shippingServiceProvider = Substitute.For<ShippingServiceProvider>();

            connectServiceProvider.GetShippingServiceProvider().Returns(shippingServiceProvider);
            this.fixture = this.CreateOmitOnRecursionFixture();
            this.getShippingMethodsResult = this.fixture.Build<GetShippingMethodsResult>()
                .With(res => res.Success, true)
                .With(res => res.ShippingMethods, null)
                .Create();
            this.getShippingOptionsResult = this.fixture.Build<GetShippingOptionsResult>()
                .With(res => res.Success, true)
                .With(res => res.ShippingOptions, null)
                .Create();
            this.getShippingMethodsResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            this.getShippingOptionsResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            this.logService = Substitute.For<ILogService<CommonLog>>();
            this.shippingManager = new ShippingManagerV2(connectServiceProvider, this.logService);

            shippingServiceProvider.GetShippingMethods(Arg.Any<GetShippingMethodsRequest>())
                .Returns(this.getShippingMethodsResult);
            shippingServiceProvider.GetShippingOptions(Arg.Any<GetShippingOptionsRequest>())
                .Returns(this.getShippingOptionsResult);
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
        public void GetShippingMethods_IfGetShippingMethodsResultIsSuccessful_ShouldNotCallLogService()
        {
            // act
            this.shippingManager.GetShippingMethods(this.fixture.Create<CommerceCart>(), this.fixture.Create<ShippingOptionType>());

            // assert
            this.logService.Received(0).Error(Arg.Any<string>());
        }

        [Fact]
        public void GetShippingMethods_IfGetShippingMethodsResultIsUnsuccessful_ShouldCallLogService()
        {
            // arrange
            this.getShippingMethodsResult.Success = false;

            // act
            this.shippingManager.GetShippingMethods(this.fixture.Create<CommerceCart>(), this.fixture.Create<ShippingOptionType>());

            // assert
            this.logService.Received(1).Error(Arg.Any<string>());
        }

        [Fact]
        public void GetShippingOptions_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.shippingManager.GetShippingOptions(null));
        }

        [Fact]
        public void GetShippingOptions_IfGetShippingOptionsResultIsSuccessful_ShouldNotCallLogService()
        {
            // act
            this.shippingManager.GetShippingOptions(this.fixture.Create<Cart>());

            // assert
            this.logService.Received(0).Error(Arg.Any<string>());
        }

        [Fact]
        public void GetShippingOptions_IfGetShippingOptionsResultIsUnsuccessful_ShouldCallLogService()
        {
            // arrange
            this.getShippingOptionsResult.Success = false;

            // act
            this.shippingManager.GetShippingOptions(this.fixture.Create<CommerceCart>());

            // assert
            this.logService.Received(1).Error(Arg.Any<string>());
        }

        //TODO: Refactor duplication of CreateOmitOnRecursionFixture
        /// <summary>
        /// Creates OmitOnRecursionBehavior as opposite to ThrowingRecursionBehavior with default recursion depth of 1.
        /// </summary>
        /// <returns></returns>
        private IFixture CreateOmitOnRecursionFixture()
        {
            var result = new Fixture();

            result.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => result.Behaviors.Remove(b));
            result.Behaviors.Add(new OmitOnRecursionBehavior());

            return result;
        }
    }
}