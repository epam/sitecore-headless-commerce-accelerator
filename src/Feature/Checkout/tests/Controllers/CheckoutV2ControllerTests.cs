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

    using Checkout.Controllers;

    using Foundation.Base.Models;
    using Foundation.Commerce.Models;
    using Foundation.Commerce.Services.Billing;
    using Foundation.Commerce.Services.Delivery;
    using Foundation.Commerce.Services.Order;

    using Models.Requests;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    public class CheckoutV2ControllerTests
    {
        private readonly IBillingService billingService;

        private readonly IDeliveryService deliveryService;

        private readonly IOrderService orderService;

        private readonly CheckoutV2Controller controller;

        private readonly IFixture fixture;

        public CheckoutV2ControllerTests()
        {
            this.billingService = Substitute.For<IBillingService>();
            this.deliveryService = Substitute.For<IDeliveryService>();
            this.orderService = Substitute.For<IOrderService>();
            this.controller = Substitute.For<CheckoutV2Controller>(this.billingService, this.orderService, this.deliveryService);
            this.fixture = new Fixture();
        }
        
        [Fact]
        public void GetBillingInfo_ShouldCallExecuteMethod()
        {
            // act
            this.controller.GetBillingInfo();

            // assert
            this.controller.Received(1).Execute(this.billingService.GetBillingInfo);
        }

        [Fact]
        public void GetDeliveryOptions_ShouldCallExecuteMethod()
        {
           // act
           this.controller.GetDeliveryOptions();

            // assert
            this.controller.Received(1).Execute(this.deliveryService.GetDeliveryOptions);
        }

        [Fact]
        public void GetShippingOptions_ShouldCallExecuteMethod()
        {
            // act
            this.controller.GetShippingOptions();

            // assert
            this.controller.Received(1).Execute(this.deliveryService.GetShippingOptions);
        }

        [Fact]
        public void SetPaymentInfo_ShouldCallExecuteMethod()
        {
            // act
            this.controller.SetPaymentInfo(this.fixture.Create<SetPaymentInfoRequest>());

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<VoidResult>>>());
        }

        [Fact]
        public void SetShippingOptions_ShouldCallExecuteMethod()
        {
            // act
            this.controller.SetShippingOptions(this.fixture.Create<SetShippingOptionsRequest>());

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<VoidResult>>>());
        }

        [Fact]
        public void SubmitOrder_ShouldCallExecuteMethod()
        {
            // act
            this.controller.SubmitOrder();

            // assert
            this.controller.Received(1).Execute(this.orderService.SubmitOrder);
        }
    }
}
