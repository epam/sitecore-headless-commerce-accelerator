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
    using System.Web.Http;

    using Foundation.Commerce.Repositories;
    using Foundation.Commerce.Utils;
    using Foundation.Extensions.Extensions;

    [RoutePrefix(Constants.CommerceRoutePrefix + "/order")]
    public class OrderController : ApiController
    {
        private readonly IOrderRepository orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [Route("get/{orderId}")]
        public IHttpActionResult GetOrder(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return this.JsonError($"{orderId} should be specified.", HttpStatusCode.BadRequest);
            }

            var model = this.orderRepository.GetOrderDetails(orderId);

            if (model.Success)
            {
                return this.JsonOk(model.Data);
            }

            return this.JsonError(model.Errors.ToArray(), HttpStatusCode.InternalServerError);
        }

        [Route("get")]
        public IHttpActionResult GetOrders(DateTime? fromDate = null, DateTime? untilDate = null, int page = 0, int count = 5)
        {
            if (page < 0)
            {
                return this.JsonError($"{nameof(page)} should be positive or zero.", HttpStatusCode.BadRequest);
            }

            if (count <= 0)
            {
                return this.JsonError($"{nameof(page)} should be positive.", HttpStatusCode.BadRequest);
            }

            var model = this.orderRepository.GetOrders(fromDate, fromDate, page, count);
            if (model.Success)
            {
                return this.JsonOk(model.Data);
            }

            return this.JsonError(model.Errors.ToArray(), HttpStatusCode.InternalServerError);
        }
    }
}