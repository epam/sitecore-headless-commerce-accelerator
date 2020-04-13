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

    using Checkout.Controllers;

    using Foundation.Base.Models;
    using Foundation.Base.Models.Result;
    using Foundation.Commerce.Models.Entities.Order;
    using Foundation.Commerce.Services.Order;

    using Models.Requests;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    // TODO: Replace models with new one
    public class OrdersControllerTests
    {
        private readonly OrdersController controller;

        private readonly IFixture fixture;

        public OrdersControllerTests()
        {
            var orderService = Substitute.For<IOrderService>();

            this.fixture = new Fixture();
            this.controller = Substitute.For<OrdersController>(orderService);
        }

        [Fact]
        public void GetOrder_ShouldCallExecuteMethod()
        {
            // act
            this.controller.GetOrder(this.fixture.Create<string>());

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<Order>>>());
        }

        [Fact]
        public void GetOrders_ShouldCallExecuteMethod()
        {
            // act
            this.controller.GetOrders(this.fixture.Create<GetOrdersRequest>());

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<IList<Order>>>>());
        }
    }
}