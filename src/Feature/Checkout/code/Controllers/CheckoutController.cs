//    Copyright 2019 EPAM Systems, Inc.
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

using System.Linq;
using System.Net;
using System.Web.Mvc;
using Wooli.Foundation.Commerce.Models;
using Wooli.Foundation.Commerce.Models.Checkout;
using Wooli.Foundation.Commerce.Repositories;
using Wooli.Foundation.Extensions.Extensions;

namespace Wooli.Feature.Checkout.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IBillingRepository billingRepository;
        private readonly ICheckoutRepository checkoutRepository;

        private readonly IDeliveryRepository deliveryRepository;

        public CheckoutController(ICheckoutRepository checkoutRepository, IDeliveryRepository deliveryRepository,
            IBillingRepository billingRepository)
        {
            this.checkoutRepository = checkoutRepository;
            this.deliveryRepository = deliveryRepository;
            this.billingRepository = billingRepository;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("submitOrder")]
        public ActionResult SubmitOrder()
        {
            Result<SubmitOrderModel> result = checkoutRepository.SubmitOrder();

            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("getDeliveryData")]
        public ActionResult GetDeliveryData()
        {
            Result<DeliveryModel> result = deliveryRepository.GetDeliveryData();

            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("getBillingData")]
        public ActionResult GetBillingData()
        {
            Result<BillingModel> result = billingRepository.GetBillingData();

            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("getShippingMethods")]
        public ActionResult GetShippingMethods()
        {
            var shippingArgs = new GetShippingArgs
            {
                ShippingPreferenceType = "1"
            };

            Result<ShippingModel> result = deliveryRepository.GetShippingMethods(shippingArgs);

            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("setShippingMethods")]
        public ActionResult SetShippingMethods(SetShippingArgs args)
        {
            Result<SetShippingModel> result = deliveryRepository.SetShippingMethods(args);

            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("setPaymentMethods")]
        public ActionResult SetPaymentMethods(SetPaymentArgs paymentArgs)
        {
            Result<VoidResult> result = billingRepository.SetPaymentMethods(paymentArgs);

            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }
    }
}