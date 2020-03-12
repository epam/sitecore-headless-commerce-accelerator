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
    using Foundation.Commerce.Services.Order;
    using Foundation.Extensions.Extensions;

    using Models.Requests;

    using Sitecore.Diagnostics;

    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            Assert.ArgumentNotNull(orderService, nameof(orderService));

            this.orderService = orderService;
        }

        [HttpGet]
        [ActionName("order")]
        public ActionResult GetOrder(string orderId)
        {
            return this.Execute(() => this.orderService.GetOrderDetails(orderId));
        }

        [HttpGet]
        [ActionName("get")]
        public ActionResult GetOrders(GetOrdersRequest request)
        {
            return this.Execute(
                () => this.orderService.GetOrders(request.FromDate, request.UntilDate, request.Page, request.Count));
        }

        // TODO: Create Extension Method
        private ActionResult Execute<T>(Func<Result<T>> action) where T : class
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    var errorMessages = this.ModelState.Values
                        .SelectMany(state => state?.Errors)
                        .Select(error => error.ErrorMessage)
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