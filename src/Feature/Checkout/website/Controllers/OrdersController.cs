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
    using Foundation.Commerce.Services.Order;

    using Models.Requests;

    using Sitecore.Diagnostics;

    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            Assert.ArgumentNotNull(orderService, nameof(orderService));

            this.orderService = orderService;
        }

        [HttpGet]
        [ActionName("order")]
        public ActionResult GetOrder(string id)
        {
            return this.Execute(() => this.orderService.GetOrder(id));
        }

        [HttpGet]
        [ActionName("orders")]
        public ActionResult GetOrders(GetOrdersRequest request)
        {
            return this.Execute(
                () => this.orderService.GetOrders(request.FromDate, request.UntilDate, request.Page, request.Count));
        }
    }
}