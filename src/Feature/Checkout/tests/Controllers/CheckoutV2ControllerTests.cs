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
    using Foundation.Commerce.Services.Billing;
    using Foundation.Commerce.Services.Delivery;
    using Foundation.Commerce.Services.Order;

    using Models.Requests;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using Wooli.Foundation.Extensions.Models;

    using Xunit;

    using Address = Foundation.Commerce.Models.Entities.Address;

    public class CheckoutV2ControllerTests
    {
        private readonly IBillingService billingService;

        private readonly IDeliveryService deliveryService;

        private readonly IOrderService orderService;

        private readonly CheckoutV2Controller controller;

        public CheckoutV2ControllerTests()
        {
            this.billingService = Substitute.For<IBillingService>();
            this.deliveryService = Substitute.For<IDeliveryService>();
            this.orderService = Substitute.For<IOrderService>();

            var httpContext = Substitute.For<HttpContextBase>();
            httpContext.Response.Returns(Substitute.For<HttpResponseBase>());

            this.controller = new CheckoutV2Controller(this.billingService, this.orderService, this.deliveryService);
            this.controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), this.controller);
        }

        #region GetBillingOptions

        [Fact]
        public void GetBillingOptions_IfExceptionIsThrownInService_ShouldReturnErrorResponse()
        {
            // arrange
            this.billingService.GetBillingOptions().Throws<NullReferenceException>();

            // act
            var jsonResult = this.controller.GetBillingOptions() as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void GetBillingOptions_IfServiceResultIsUnsuccessful_ShouldReturnErrorResponse()
        {
            // arrange
            var result = new Result<BillingInfo>
            {
                Success = false
            };

            this.billingService.GetBillingOptions().Returns(result);

            // act
            var jsonResult = this.controller.GetBillingOptions() as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void GetBillingOptions_IfServiceResultIsSuccessful_ShouldReturnSuccessResponse()
        {
            // arrange
            var result = new Result<BillingInfo>();

            this.billingService.GetBillingOptions().Returns(result);

            // act
            var jsonResult = this.controller.GetBillingOptions() as JsonResult;
            var okResult = jsonResult?.Data as OkJsonResultModel<BillingInfo>;

            // assert
            Assert.NotNull(okResult);
        }

        #endregion

        #region GetDeliveryOptions

        [Fact]
        public void GetDeliveryOptions_IfExceptionIsThrownInService_ShouldReturnErrorResponse()
        {
            // arrange
            this.deliveryService.GetDeliveryOptions().Throws<NullReferenceException>();

            // act
            var jsonResult = this.controller.GetBillingOptions() as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void GetDeliveryOptions_IfServiceResultIsUnsuccessful_ShouldReturnErrorResponse()
        {
            // arrange
            var result = new Result<DeliveryInfo>
            {
                Success = false
            };

            this.deliveryService.GetDeliveryOptions().Returns(result);

            // act
            var jsonResult = this.controller.GetDeliveryOptions() as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void GetDeliveryOptions_IfServiceResultIsSuccessful_ShouldReturnSuccessResponse()
        {
            // arrange
            var result = new Result<DeliveryInfo>();

            this.deliveryService.GetDeliveryOptions().Returns(result);

            // act
            var jsonResult = this.controller.GetDeliveryOptions() as JsonResult;
            var okResult = jsonResult?.Data as OkJsonResultModel<DeliveryInfo>;

            // assert
            Assert.NotNull(okResult);
        }

        #endregion

        #region GetShippingOptions

        [Fact]
        public void GetShippingOptions_IfExceptionIsThrownInService_ShouldReturnErrorResponse()
        {
            // arrange
            this.deliveryService.GetShippingOptions().Throws<NullReferenceException>();

            // act
            var jsonResult = this.controller.GetBillingOptions() as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void GetShippingOptions_IfServiceResultIsUnsuccessful_ShouldReturnErrorResponse()
        {
            // arrange
            var result = new Result<ShippingInfo>
            {
                Success = false
            };

            this.deliveryService.GetShippingOptions().Returns(result);

            // act
            var jsonResult = this.controller.GetDeliveryOptions() as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void GetShippingOptions_IfServiceResultIsSuccessful_ShouldReturnSuccessResponse()
        {
            // arrange
            var result = new Result<ShippingInfo>();

            this.deliveryService.GetShippingOptions().Returns(result);

            // act
            var jsonResult = this.controller.GetShippingOptions() as JsonResult;
            var okResult = jsonResult?.Data as OkJsonResultModel<ShippingInfo>;

            // assert
            Assert.NotNull(okResult);
        }

        #endregion

        #region SetPaymentOptions

        [Fact]
        public void SetPaymentOptions_IfExceptionIsThrownInService_ShouldReturnErrorResponse()
        {
            // arrange
            this.billingService.SetPaymentOptions(Arg.Any<Address>(), Arg.Any<FederatedPaymentInfo>())
                .Throws<NullReferenceException>();

            // act
            var jsonResult = this.controller.SetPaymentOptions(new SetPaymentOptionsRequest()) as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void SetPaymentOptions_IfServiceResultIsUnsuccessful_ShouldReturnErrorResponse()
        {
            // arrange
            var result = new Result<VoidResult>
            {
                Success = false
            };

            this.billingService.SetPaymentOptions(Arg.Any<Address>(), Arg.Any<FederatedPaymentInfo>()).Returns(result);

            // act
            var jsonResult = this.controller.SetPaymentOptions(new SetPaymentOptionsRequest()) as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void SetPaymentOptions_IfModelIsInvalid_ShouldReturnErrorResponse()
        {
            // arrange
            this.controller.ModelState.AddModelError("key", "error");

            // act
            var jsonResult = this.controller.SetPaymentOptions(new SetPaymentOptionsRequest()) as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void SetPaymentOptions_IfServiceResultIsSuccessful_ShouldReturnSuccessResponse()
        {
            // arrange
            var result = new Result<VoidResult>();

            this.billingService.SetPaymentOptions(Arg.Any<Address>(), Arg.Any<FederatedPaymentInfo>()).Returns(result);

            // act
            var jsonResult = this.controller.SetPaymentOptions(new SetPaymentOptionsRequest()) as JsonResult;
            var okResult = jsonResult?.Data as OkJsonResultModel<VoidResult>;

            // assert
            Assert.NotNull(okResult);
        }

        #endregion

        #region SetShippingOptions

        [Fact]
        public void SetShippingOptions_IfExceptionIsThrownInService_ShouldReturnErrorResponse()
        {
            // arrange
            this.deliveryService.SetShippingOptions(
                    Arg.Any<string>(),
                    Arg.Any<List<Address>>(),
                    Arg.Any<List<ShippingMethod>>())
                .Throws<NullReferenceException>();

            // act
            var jsonResult = this.controller.SetShippingOptions(new SetShippingOptionsRequest()) as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void SetShippingOptions_IfServiceResultIsUnsuccessful_ShouldReturnErrorResponse()
        {
            // arrange
            var result = new Result<VoidResult>
            {
                Success = false
            };

            this.deliveryService.SetShippingOptions(
                    Arg.Any<string>(),
                    Arg.Any<List<Address>>(),
                    Arg.Any<List<ShippingMethod>>())
                .Returns(result);

            // act
            var jsonResult = this.controller.SetShippingOptions(new SetShippingOptionsRequest()) as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void SetShippingOptions_IfModelIsInvalid_ShouldReturnErrorResponse()
        {
            // arrange
            this.controller.ModelState.AddModelError("key", "error");

            // act
            var jsonResult = this.controller.SetShippingOptions(new SetShippingOptionsRequest()) as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void SetShippingOptions_IfServiceResultIsSuccessful_ShouldReturnSuccessResponse()
        {
            // arrange
            var result = new Result<VoidResult>();

            this.deliveryService.SetShippingOptions(
                    Arg.Any<string>(),
                    Arg.Any<List<Address>>(),
                    Arg.Any<List<ShippingMethod>>())
                .Returns(result);

            // act
            var jsonResult = this.controller.SetShippingOptions(new SetShippingOptionsRequest()) as JsonResult;
            var okResult = jsonResult?.Data as OkJsonResultModel<VoidResult>;

            // assert
            Assert.NotNull(okResult);
        }

        #endregion

        #region SubmitOrder

        [Fact]
        public void SubmitOrder_IfExceptionIsThrownInService_ShouldReturnErrorResponse()
        {
            // arrange
            this.orderService.SubmitOrder().Throws<NullReferenceException>();

            // act
            var jsonResult = this.controller.SubmitOrder() as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void SubmitOrder_IfServiceResultIsUnsuccessful_ShouldReturnErrorResponse()
        {
            // arrange
            var result = new Result<SubmitOrderModel>
            {
                Success = false
            };

            this.orderService.SubmitOrder().Returns(result);

            // act
            var jsonResult = this.controller.SubmitOrder() as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void SubmitOrder_IfServiceResultIsSuccessful_ShouldReturnSuccessResponse()
        {
            // arrange
            var result = new Result<SubmitOrderModel>();

            this.orderService.SubmitOrder().Returns(result);

            // act
            var jsonResult = this.controller.SubmitOrder() as JsonResult;
            var okResult = jsonResult?.Data as OkJsonResultModel<SubmitOrderModel>;

            // assert
            Assert.NotNull(okResult);
        }

        #endregion
    }
}
