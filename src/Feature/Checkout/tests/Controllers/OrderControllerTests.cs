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

namespace Wooli.Feature.Checkout.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Checkout.Controllers;

    using Foundation.Commerce.Models;
    using Foundation.Commerce.Models.Checkout;
    using Foundation.Commerce.Models.Entities;
    using Foundation.Commerce.Services.Order;
    using Foundation.Extensions.Models;

    using Models.Requests;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using Xunit;

    public class OrdersControllerTests
    {
        public OrdersControllerTests()
        {
            this.orderService = Substitute.For<IOrderService>();
            this.controller = new OrdersController(this.orderService);

            var httpContext = Substitute.For<HttpContextBase>();
            this.controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), this.controller);
        }

        private readonly OrdersController controller;
        private readonly IOrderService orderService;

        [Fact]
        public void GetOrder_IfExceptionIsThrownInService_ShouldReturnErrorResponse()
        {
            // arrange
            const string request = "request";

            this.orderService.GetOrderDetails(request)
                .Throws<NullReferenceException>();

            // act
            var jsonResult = this.controller.GetOrder(request) as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void GetOrder_IfServiceResultIsSuccessful_ShouldReturnSuccessResponse()
        {
            // arrange
            const string request = "request";
            var result = new Result<CartModel>();

            this.orderService.GetOrderDetails(request)
                .Returns(result);

            // act
            var jsonResult = this.controller.GetOrder(request) as JsonResult;
            var okResult = jsonResult?.Data as OkJsonResultModel<CartModel>;

            // assert
            Assert.NotNull(okResult);
        }

        [Fact]
        public void GetOrder_IfServiceResultIsUnsuccessful_ShouldReturnErrorResponse()
        {
            // arrange
            const string request = "request";
            var result = new Result<CartModel>
            {
                Success = false
            };

            this.orderService.GetOrderDetails(request)
                .Returns(result);

            // act
            var jsonResult = this.controller.GetOrder(request) as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void GetOrders_IfExceptionIsThrownInService_ShouldReturnErrorResponse()
        {
            // arrange
            var request = new GetOrdersRequest();

            this.orderService.GetOrders(request.FromDate, request.UntilDate, request.Page, request.Count)
                .Throws<NullReferenceException>();

            // act
            var jsonResult = this.controller.GetOrders(request) as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void GetOrders_IfServiceResultIsSuccessful_ShouldReturnSuccessResponse()
        {
            // arrange
            var request = new GetOrdersRequest();
            var result = new Result<OrderHistoryResultModel>();

            this.orderService.GetOrders(request.FromDate, request.UntilDate, request.Page, request.Count)
                .Returns(result);

            // act
            var jsonResult = this.controller.GetOrders(request) as JsonResult;
            var okResult = jsonResult?.Data as OkJsonResultModel<OrderHistoryResultModel>;

            // assert
            Assert.NotNull(okResult);
        }

        [Fact]
        public void GetOrders_IfServiceResultIsUnsuccessful_ShouldReturnErrorResponse()
        {
            // arrange
            var request = new GetOrdersRequest();
            var result = new Result<OrderHistoryResultModel>
            {
                Success = false
            };

            this.orderService.GetOrders(request.FromDate, request.UntilDate, request.Page, request.Count)
                .Returns(result);

            // act
            var jsonResult = this.controller.GetOrders(request) as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }
    }
}