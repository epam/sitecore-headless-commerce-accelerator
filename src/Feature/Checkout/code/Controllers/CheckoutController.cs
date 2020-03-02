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
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Wooli.Foundation.Commerce.Models.Checkout;
    using Wooli.Foundation.Commerce.Repositories;
    using Wooli.Foundation.Extensions.Extensions;

    public class CheckoutController : Controller
    {
        private readonly IBillingRepository billingRepository;

        private readonly ICheckoutRepository checkoutRepository;

        private readonly IDeliveryRepository deliveryRepository;

        public CheckoutController(
            ICheckoutRepository checkoutRepository,
            IDeliveryRepository deliveryRepository,
            IBillingRepository billingRepository)
        {
            this.checkoutRepository = checkoutRepository;
            this.deliveryRepository = deliveryRepository;
            this.billingRepository = billingRepository;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("getBillingData")]
        public ActionResult GetBillingData()
        {
            var result = this.billingRepository.GetBillingData();

            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("getDeliveryData")]
        public ActionResult GetDeliveryData()
        {
            var result = this.deliveryRepository.GetDeliveryData();

            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("getShippingMethods")]
        public ActionResult GetShippingMethods()
        {
            var shippingArgs = new GetShippingArgs { ShippingPreferenceType = "1" };

            var result = this.deliveryRepository.GetShippingMethods(shippingArgs);

            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("setPaymentMethods")]
        public ActionResult SetPaymentMethods(SetPaymentArgs paymentArgs)
        {
            var result = this.billingRepository.SetPaymentMethods(paymentArgs);

            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("setShippingMethods")]
        public ActionResult SetShippingMethods(SetShippingArgs args)
        {
            var result = this.deliveryRepository.SetShippingMethods(args);

            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("submitOrder")]
        public ActionResult SubmitOrder()
        {
            var result = this.checkoutRepository.SubmitOrder();

            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }
    }
}