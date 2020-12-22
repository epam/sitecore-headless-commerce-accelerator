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

namespace HCA.Foundation.Connect.Tests.Managers.Order
{
    using System;

    using AutoFixture;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using Connect.Managers.Order;

    using NSubstitute;



    using Providers;

    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Services.Orders;

    using Xunit;

    public class OrderManagerTests
    {
        private readonly IFixture fixture;

        private readonly OrderManager manager;

        private readonly OrderServiceProvider orderServiceProvider;

        public OrderManagerTests()
        {
            var connectServiceProvider = Substitute.For<IConnectServiceProvider>();
            var logService = Substitute.For<ILogService<CommonLog>>();
            this.orderServiceProvider = Substitute.For<OrderServiceProvider>();
            connectServiceProvider.GetOrderServiceProvider().Returns(this.orderServiceProvider);

            this.manager = Substitute.For<OrderManager>(logService, connectServiceProvider);

            this.fixture = new Fixture();
        }

        [Theory]
        [InlineData("", "1", "1")]
        [InlineData("1", "", "1")]
        [InlineData("1", "1", "")]
        public void GetOrder_IfParameterIsEmpty_ShouldThrowArgumentException(
            string orderId,
            string customerId,
            string shopName)
        {
            // act & assert
            Assert.Throws<ArgumentException>(() => this.manager.GetOrder(orderId, customerId, shopName));
        }

        [Theory]
        [InlineData(null, "1", "1")]
        [InlineData("1", null, "1")]
        [InlineData("1", "1", null)]
        public void GetOrder_IfParameterIsNull_ShouldThrowArgumentNullException(
            string orderId,
            string customerId,
            string shopName)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.manager.GetOrder(orderId, customerId, shopName));
        }

        [Fact]
        public void GetOrder_ShouldCallExecuteMethod()
        {
            // act
            this.manager.GetOrder(
                this.fixture.Create<string>(),
                this.fixture.Create<string>(),
                this.fixture.Create<string>());

            // assert
            this.manager.Received(1)
                .Execute(Arg.Any<GetVisitorOrderRequest>(), this.orderServiceProvider.GetVisitorOrder);
        }

        [Theory]
        [InlineData("", "1")]
        [InlineData("1", "")]
        public void GetOrdersHeaders_IfParameterIsEmpty_ShouldThrowArgumentException(string customerId, string shopName)
        {
            // act & assert
            Assert.Throws<ArgumentException>(() => this.manager.GetOrdersHeaders(customerId, shopName));
        }

        [Theory]
        [InlineData(null, "1")]
        [InlineData("1", null)]
        public void GetOrdersHeaders_IfParameterIsNull_ShouldThrowArgumentNullException(
            string customerId,
            string shopName)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.manager.GetOrdersHeaders(customerId, shopName));
        }

        [Fact]
        public void GetOrdersHeaders_ShouldCallExecuteMethod()
        {
            // act
            this.manager.GetOrdersHeaders(this.fixture.Create<string>(), this.fixture.Create<string>());

            // assert
            this.manager.Received(1)
                .Execute(Arg.Any<GetVisitorOrdersRequest>(), this.orderServiceProvider.GetVisitorOrders);
        }

        [Fact]
        public void SubmitVisitorOrder_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.manager.SubmitVisitorOrder(null));
        }

        [Fact]
        public void SubmitVisitorOrder_ShouldCallExecuteMethod()
        {
            // act
            this.manager.SubmitVisitorOrder(new Cart());

            // assert
            this.manager.Received(1)
                .Execute(Arg.Any<SubmitVisitorOrderRequest>(), this.orderServiceProvider.SubmitVisitorOrder);
        }
    }
}