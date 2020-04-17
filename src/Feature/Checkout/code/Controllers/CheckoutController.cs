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

namespace HCA.Feature.Checkout.Controllers
{
    using System.Web.Mvc;

    using Foundation.Base.Controllers;
    using Foundation.Commerce.Services.Billing;
    using Foundation.Commerce.Services.Delivery;
    using Foundation.Commerce.Services.Order;

    using Models.Requests;

    using Sitecore.Diagnostics;

    public class CheckoutController : BaseController
    {
        private readonly IBillingService billingService;

        private readonly IDeliveryService deliveryService;

        private readonly IOrderService orderService;

        public CheckoutController(
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
        [ActionName("billingInfo")]
        public ActionResult GetBillingInfo()
        {
            return this.Execute(this.billingService.GetBillingInfo);
        }

        [HttpGet]
        [ActionName("deliveryInfo")]
        public ActionResult GetDeliveryInfo()
        {
            return this.Execute(this.deliveryService.GetDeliveryInfo);
        }

        [HttpGet]
        [ActionName("shippingInfo")]
        public ActionResult GetShippingInfo()
        {
            return this.Execute(this.deliveryService.GetShippingInfo);
        }

        [HttpPost]
        [ActionName("paymentInfo")]
        public ActionResult SetPaymentInfo(SetPaymentInfoRequest request)
        {
            return this.Execute(
                () => this.billingService.SetPaymentInfo(request.BillingAddress, request.FederatedPayment));
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
    }
}