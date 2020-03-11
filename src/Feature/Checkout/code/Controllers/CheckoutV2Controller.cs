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

namespace Wooli.Feature.Checkout.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Foundation.Commerce.Models;
    using Foundation.Commerce.Services.Billing;
    using Foundation.Commerce.Services.Delivery;
    using Foundation.Commerce.Services.Order;
    using Foundation.Extensions.Extensions;

    using Models.Requests;

    using Sitecore.Diagnostics;

    public class CheckoutV2Controller : Controller
    {
        private readonly IBillingService billingService;

        private readonly IOrderService orderService;

        private readonly IDeliveryService deliveryService;

        public CheckoutV2Controller(
            IBillingService billingService,
            IOrderService orderService,
            IDeliveryService deliveryService)
        {
            Assert.ArgumentNotNull(billingService, nameof(billingService));
            Assert.ArgumentNotNull(orderService, nameof(orderService));
            Assert.ArgumentNotNull(deliveryService, nameof(deliveryService));

            this.billingService = billingService;
            this.orderService = orderService;
            this.deliveryService = deliveryService;
        }

        [HttpGet]
        [ActionName("billingOptions")]
        public ActionResult GetBillingOptions()
        {
            return this.Execute(this.billingService.GetBillingOptions);
        }

        [HttpGet]
        [ActionName("deliveryOptions")]
        public ActionResult GetDeliveryOptions()
        {
            return this.Execute(this.deliveryService.GetDeliveryOptions);
        }

        [HttpGet]
        [ActionName("shippingOptions")]
        public ActionResult GetShippingOptions()
        {
            return this.Execute(() => this.deliveryService.GetShippingOptions());
        }

        [HttpPost]
        [ActionName("paymentOptions")]
        public ActionResult SetPaymentOptions(SetPaymentOptionsRequest request)
        {
            return this.Execute(() => this.billingService.SetPaymentOptions(request.BillingAddress, request.FederatedPayment));
        }

        [HttpPost]
        [ActionName("shippingOptions")]
        public ActionResult SetShippingOptions(SetShippingOptionsRequest request)
        {
            return this.Execute(
                () => this.deliveryService.SetShippingOptions(
                    request.OrderShippingPreferenceType,
                    request.ShippingAddresses,
                    request.ShippingMethods));
        }

        [HttpPost]
        [ActionName("orders")]
        public ActionResult SubmitOrder()
        {
            return this.Execute(this.orderService.SubmitOrder);
        }

        // TODO: Create Extension Method
        private ActionResult Execute<TData>(Func<Result<TData>> action) where TData : class
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    var errorMessages =
                        this.ModelState.SelectMany(state => state.Value?.Errors.Select(error => error.ErrorMessage))
                            .ToArray();
                    return this.JsonError(errorMessages, HttpStatusCode.BadRequest);
                }

                var result = action.Invoke();

                return result.Success
                    ? this.JsonOk(result.Data)
                    : this.JsonError(
                        result.Errors?.ToArray(),
                        HttpStatusCode.InternalServerError,
                        tempData: result.Data);
            }
            catch (Exception exception)
            {
                return this.JsonError(exception.Message, HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}