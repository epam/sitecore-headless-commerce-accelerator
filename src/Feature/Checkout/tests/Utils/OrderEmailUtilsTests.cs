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

namespace HCA.Feature.Checkout.Tests.Utils
{
    using System;

    using Checkout.Utils;

    using Context;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    public class OrderEmailUtilsTests
    {
        private readonly OrderEmailUtils orderEmailUtils;

        private readonly IExmContext exmContext;

        private readonly IFixture fixture;

        public OrderEmailUtilsTests()
        {
            this.fixture = new Fixture();

            this.exmContext = Substitute.For<IExmContext>();

            this.orderEmailUtils = new OrderEmailUtils(this.exmContext);
        }

        [Fact]
        public void ResolveTokens_IfMessageBodyIsNull_ShouldThrowArgumentNullException()
        {
            // act, assert
            Assert.Throws<ArgumentNullException>(() => this.orderEmailUtils.ResolveTokens(null));
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
        public void ResolveTokens_IfIsRenderRequestTrue_ShouldReplaceTokenWithOrderId()
        {
            // arrange
            var messageBody = Constants.OrderEmail.OrderTrackingNumberToken;
            var orderId = this.fixture.Create<string>();
            this.exmContext.IsRenderRequest.Returns(true);
            this.exmContext.GetValue("order_id").Returns(orderId);

            // act
            var actual = this.orderEmailUtils.ResolveTokens(messageBody);

            // assert
            Assert.Equal(orderId, actual);
        }
    }
}