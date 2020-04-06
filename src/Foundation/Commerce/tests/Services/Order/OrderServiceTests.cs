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

namespace Wooli.Foundation.Commerce.Tests.Services.Order
{
    using System;
    using System.Collections.Generic;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using Commerce.Mappers.Order;
    using Commerce.Services.Order;

    using Connect.Context;
    using Connect.Managers;
    using Connect.Managers.Cart;

    using Context;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Orders;
    using Sitecore.Commerce.Services;
    using Sitecore.Commerce.Services.Carts;
    using Sitecore.Commerce.Services.Orders;

    using Xunit;

    public class OrderServiceTests
    {
        private readonly ICartManagerV2 cartManager;

        private readonly Fixture fixture;

        private readonly ILogService<CommonLog> logService;

        private readonly IOrderMapper mapper;

        private readonly IOrderManagerV2 orderManager;

        private readonly OrderService service;

        private readonly IStorefrontContext storefrontContext;

        private readonly IVisitorContext visitorContext;

        public OrderServiceTests()
        {
            this.mapper = Substitute.For<IOrderMapper>();
            this.orderManager = Substitute.For<IOrderManagerV2>();
            this.cartManager = Substitute.For<ICartManagerV2>();
            this.storefrontContext = Substitute.For<IStorefrontContext>();
            this.visitorContext = Substitute.For<IVisitorContext>();
            this.logService = Substitute.For<ILogService<CommonLog>>();

            this.service = new OrderService(
                this.mapper,
                this.logService,
                this.orderManager,
                this.cartManager,
                this.storefrontContext,
                this.visitorContext);

            this.fixture = new Fixture();
        }

        [Fact]
        public void GetOrder_IfArgumentEmpty_ShouldThrowArgumentException()
        {
            // arrange
            var orderId = string.Empty;

            // act & assert
            Assert.Throws<ArgumentException>(() => this.service.GetOrder(orderId));
        }

        [Fact]
        public void GetOrder_IfArgumentNull_ShouldThrowArgumentNullException()
        {
            // arrange
            string orderId = null;

            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.service.GetOrder(orderId));
        }

        [Fact]
        public void GetOrder_IfManagerResultFail_ShouldReturnFailResult()
        {
            // arrange
            var (orderId, orderResult) = this.InitGetOrder(false);

            // act
            var result = this.service.GetOrder(orderId);

            // assert
            Assert.False(result.Success);
        }

        [Fact]
        public void GetOrder_IfRequestSuccess_ShouldReturnSuccessResult()
        {
            // arrange
            var (orderId, orderResult) = this.InitGetOrder(true);

            // act
            var result = this.service.GetOrder(orderId);

            // assert
            Assert.True(result.Success);
            this.mapper.Received(1).Map(orderResult.Order);
        }

        [Fact]
        public void GetOrders_IfManagerHeadersResultFail_ShouldReturnFailResult()
        {
            // arrange
            var (page, count, _) = this.InitGetOrders(false, true);

            // act
            var result = this.service.GetOrders(null, null, page, count);

            // assert
            Assert.False(result.Success);
        }

        [Fact]
        public void GetOrders_IfManagerHeadersResultSuccess_IfManagerOrderResultFail_ShouldReturnFailResult()
        {
            // arrange
            var (page, count, _) = this.InitGetOrders(true, false);

            // act
            var result = this.service.GetOrders(null, null, page, count);

            // assert
            Assert.False(result.Success);
        }

        [Fact]
        public void GetOrders_ShouldFilterByDate()
        {
            // arrange
            var (page, count, orderResult) = this.InitGetOrders(true, true);
            var date = new DateTime(2020, 1, 1);

            // act
            var result = this.service.GetOrders(date, date, page, count);

            // assert
            Assert.True(result.Success);
            this.mapper.Received(1).Map(orderResult.Order);
        }

        [Fact]
        public void GetOrders_ShouldReturnSuccessResult()
        {
            // arrange
            var (page, count, orderResult) = this.InitGetOrders(true, true);

            // act
            var result = this.service.GetOrders(null, null, page, count);

            // assert
            Assert.True(result.Success);
            this.mapper.Received(3).Map(orderResult.Order);
        }

        [Fact]
        public void SubmitOrder_IfCartResultFalse_ShouldReturnFalseResult()
        {
            // arrange
            this.InitSubmitOrder(false, true);

            // act
            var result = this.service.SubmitOrder();

            // assert
            Assert.False(result.Success);
        }

        [Fact]
        public void SubmitOrder_IfSubmitOrderSuccess_ShouldReturnSuccessResult()
        {
            // arrange
            var submitOrderResult = this.InitSubmitOrder(true, true);

            // act
            var result = this.service.SubmitOrder();

            // assert
            Assert.True(result.Success);
            Assert.Equal(submitOrderResult.Order.TrackingNumber, result.Data.ConfirmationId);
        }

        [Fact]
        public void SubmitOrder_IfSubmitOrderFalse_ShouldReturnFalseResult()
        {
            // arrange
            var submitOrderResult = this.InitSubmitOrder(true, false);

            // act
            var result = this.service.SubmitOrder();

            // assert
            Assert.False(result.Success);
        }

        private (string, GetVisitorOrderResult) InitGetOrder(bool success)
        {
            var orderId = this.fixture.Create<string>();
            this.storefrontContext.ShopName.Returns(this.fixture.Create<string>());
            this.visitorContext.ContactId.Returns(this.fixture.Create<string>());

            var orderResult = this.fixture
                .Build<GetVisitorOrderResult>()
                .With(or => or.Order, Substitute.For<Order>())
                .With(or => or.Success, success)
                .Create();

            orderResult.Order.OrderID = orderId;

            if (!success)
            {
                orderResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            }

            this.orderManager
                .GetOrder(orderId, this.visitorContext.ContactId, this.storefrontContext.ShopName)
                .Returns(orderResult);

            return (orderId, orderResult);
        }

        private (int, int, GetVisitorOrderResult) InitGetOrders(bool headerSuccess, bool orderSuccess)
        {
            var (orderId, orderResult) = this.InitGetOrder(orderSuccess);

            var page = 0;
            var count = 3;

            var orderHeaders = new List<OrderHeader>
            {
                this.fixture.Build<OrderHeader>()
                    .With(oh => oh.OrderID, orderId)
                    .With(oh => oh.OrderDate, new DateTime(2019, 1, 1))
                    .Create(),
                this.fixture.Build<OrderHeader>()
                    .With(oh => oh.OrderID, orderId)
                    .With(oh => oh.OrderDate, new DateTime(2020, 1, 1))
                    .Create(),
                this.fixture.Build<OrderHeader>()
                    .With(oh => oh.OrderID, orderId)
                    .With(oh => oh.OrderDate, new DateTime(2021, 1, 1))
                    .Create()
            };

            var orderHeadersResult = this.fixture
                .Build<GetVisitorOrdersResult>()
                .With(oh => oh.OrderHeaders, orderHeaders)
                .With(oh => oh.Success, headerSuccess)
                .Create();

            if (!headerSuccess)
            {
                orderHeadersResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            }

            var headersResult = this.orderManager
                .GetOrdersHeaders(this.visitorContext.ContactId, this.storefrontContext.ShopName)
                .Returns(orderHeadersResult);

            return (page, count, orderResult);
        }

        private SubmitVisitorOrderResult InitSubmitOrder(bool cartSuccess, bool submitOrderSuccess)
        {
            var cartResult = this.fixture
                .Build<CartResult>()
                .With(cr => cr.Cart, Substitute.For<Cart>())
                .With(cr => cr.Success, cartSuccess)
                .Create();

            if (!cartSuccess)
            {
                cartResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            }

            this.cartManager
                .LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId)
                .Returns(cartResult);

            var submitOrderResult = this.fixture
                .Build<SubmitVisitorOrderResult>()
                .With(cr => cr.Order, new Order())
                .With(cr => cr.CartWithErrors, new Cart())
                .With(cr => cr.Success, submitOrderSuccess)
                .Create();

            if (!submitOrderSuccess)
            {
                submitOrderResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            }

            this.orderManager
                .SubmitVisitorOrder(cartResult.Cart)
                .Returns(submitOrderResult);

            return submitOrderResult;
        }
    }
}