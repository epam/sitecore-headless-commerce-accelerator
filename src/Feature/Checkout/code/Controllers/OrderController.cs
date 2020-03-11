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
    using System.Web.Http.Description;
    using System.Web.Mvc;

    using Foundation.Commerce.Models;
    using Foundation.Commerce.Models.Checkout;
    using Foundation.Commerce.Repositories;
    using Foundation.Extensions.Extensions;

    using Models.Requests;

    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [ResponseType(typeof(CartModel))]
        [HttpGet, ActionName("getOrder")]
        public ActionResult GetOrder(string orderId)
        {
            return this.Execute(() => this.orderRepository.GetOrderDetails(orderId));
        }

        [ResponseType(typeof(OrderHistoryResultModel))]
        [HttpGet, ActionName("get")]
        public ActionResult GetOrders(GetOrdersRequest request)
        {
            return this.Execute(
                () => this.orderRepository.GetOrders(request.FromDate, request.UntilDate, request.Page, request.Count));
        }

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