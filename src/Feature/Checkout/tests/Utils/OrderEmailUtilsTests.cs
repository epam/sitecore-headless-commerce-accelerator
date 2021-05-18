//    Copyright 2021 EPAM Systems, Inc.
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

namespace HCA.Feature.Checkout.Tests.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    using Checkout.Utils;

    using Configuration.Models;
    using Configuration.Providers;

    using Context;

    using Foundation.Base.Models.Logging;
    using Foundation.Base.Models.Result;
    using Foundation.Base.Services.Logging;
    using Foundation.Commerce.Models.Entities.Order;
    using Foundation.Commerce.Services.Order;

    using NSubstitute;
    using NSubstitute.Core.Arguments;
    using NSubstitute.Extensions;

    using Ploeh.AutoFixture;

    using Xunit;

    public class OrderEmailUtilsTests
    {
        private readonly IExmContext exmContext;

        private readonly IFixture fixture;

        private readonly OrderEmailUtils orderEmailUtils;

        private readonly IOrderService orderService;

        private readonly ILogService<CommonLog> logService;

        private readonly IStorefrontConfigurationProvider storefrontConfigurationProvider;

        public OrderEmailUtilsTests()
        {
            this.fixture = new Fixture();

            this.exmContext = Substitute.For<IExmContext>();
            this.orderService = Substitute.For<IOrderService>();
            this.logService = Substitute.For<ILogService<CommonLog>>();
            this.storefrontConfigurationProvider = Substitute.For<IStorefrontConfigurationProvider>();

            this.orderEmailUtils = new OrderEmailUtils(
                this.exmContext,
                this.orderService,
                this.storefrontConfigurationProvider,
                this.logService);
        }

        [Fact]
        public void ResolveTokens_IfIsRenderRequestFalse_ShouldNotReplaceToken()
        {
            // arrange
            var messageBody = Constants.OrderEmail.OrderTrackingNumberToken;
            this.exmContext.IsRenderRequest.Returns(false);

            // act
            var actual = this.orderEmailUtils.ResolveTokens(messageBody);

            // assert
            Assert.Equal(messageBody, actual);
        }

        [Fact]
        public void ResolveTokens_IfMessageBodyIsNull_ShouldThrowArgumentNullException()
        {
            // act, assert
            Assert.Throws<ArgumentNullException>(() => this.orderEmailUtils.ResolveTokens(null));
        }

        [Fact]
        public void ResolveTokens_IfIsRenderRequestTrue_ShouldReplaceTokenWithTrackingNumber()
        {
            // arrange
            var messageBody = Constants.OrderEmail.OrderTrackingNumberToken;
            var trackingNumber = this.fixture.Create<string>();
            var id = this.fixture.Create<string>();
            
            this.exmContext.IsRenderRequest.Returns(true); 
            this.SetupTrackingNumber(id, trackingNumber);
            this.orderService.GetOrderTrackingNumber(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(trackingNumber);

            // act
            var actual = this.orderEmailUtils.ResolveTokens(messageBody);

            // assert
            Assert.Equal(trackingNumber, actual);
        }

        [Fact]
        public void ResolveTokens_IfIsRenderRequestTrueAndTrackingNumberIsNull_ShouldReplaceTokenWithOrderId()
        {
            // arrange
            var messageBody = Constants.OrderEmail.OrderTrackingNumberToken;
            var orderId = "order_id";

            this.exmContext.IsRenderRequest.Returns(true);
            this.SetupTrackingNumber(orderId, null);

            // act
            var actual = this.orderEmailUtils.ResolveTokens(messageBody);

            // assert
            Assert.Equal(orderId, actual);
        }

        [Fact]
        public void ResolveTokens_IfIsRenderRequestFalse_ShouldReturnMessageBody()
        {
            // arrange
            var messageBody = Constants.OrderEmail.OrderTrackingNumberToken;

            this.exmContext.IsRenderRequest.Returns(false);

            // act
            var actual = this.orderEmailUtils.ResolveTokens(messageBody);

            // assert
            Assert.Equal(messageBody, actual);
        }

        private void SetupTrackingNumber(string orderId, string trackingNumber)
        {
            var orderResult = new Result<Order>();
            orderResult.SetResult(
                new Order
                {
                    TrackingNumber = trackingNumber
                });

            var storefronts = new Storefronts()
            {
                ShopNames = new List<string>
                {
                    ""
                }
            };

            this.exmContext.GetValue(Arg.Any<string>()).Returns(orderId);
            this.exmContext.GetContactIdentifier().Returns(this.fixture.Create<string>());
            this.storefrontConfigurationProvider.Get().Returns(storefronts);
            this.orderService.GetOrder(orderId, Arg.Any<string>(), Arg.Any<string>()).Returns(orderResult);
        }
    }
}