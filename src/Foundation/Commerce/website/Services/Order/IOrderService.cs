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

namespace HCA.Foundation.Commerce.Services.Order
{
    using System;
    using System.Collections.Generic;

    using Base.Models.Result;

    using Models.Entities.Order;

    /// <summary>
    /// Performs main operations during checkout process
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="orderId">Order's id</param>
        /// <returns>Order</returns>
        Result<Order> GetOrder(string orderId);

        /// <summary>
        /// Get list of orders
        /// </summary>
        /// <param name="fromDate">Date for filter from</param>
        /// <param name="untilDate">Date for filter until</param>
        /// <param name="page">current page</param>
        /// <param name="count">numbers of item per page</param>
        /// <returns>List of Orders</returns>
        Result<IList<Order>> GetOrders(DateTime? fromDate, DateTime? untilDate, int page, int count);

        /// <summary>
        /// Submits current user order
        /// </summary>
        /// <returns>Order's submit confirmation</returns>
        Result<OrderConfirmation> SubmitOrder();
    }
}