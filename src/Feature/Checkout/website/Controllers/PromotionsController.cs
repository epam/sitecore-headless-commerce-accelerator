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
    using Foundation.Commerce.Services.Promotion;

    using Sitecore.Diagnostics;

    public class PromotionsController : BaseController
    {
        private readonly IPromotionService promotionService;

        public PromotionsController(IPromotionService promotionService)
        {
            Assert.ArgumentNotNull(promotionService, nameof(promotionService));
            this.promotionService = promotionService;
        }

        [HttpGet]
        [ActionName("freeShippingSubtotal")]
        public ActionResult GetFreeShippingSubtotal()
        {
            return this.Execute(() => this.promotionService.GetFreeShippingSubtotal());
        }
    }
}