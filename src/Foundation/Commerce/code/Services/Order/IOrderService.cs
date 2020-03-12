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

namespace Wooli.Foundation.Commerce.Services.Order
{
    using System;

    using Models;
    using Models.Checkout;

    /// <summary>
    /// Performs main operations during checkout process
    /// </summary>
    public interface IOrderService
    {
        Result<CartModel> GetOrderDetails(string trackingId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="untilDate"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Result<OrderHistoryResultModel> GetOrders(DateTime? fromDate, DateTime? untilDate, int page, int count);

        /// <summary>
        /// Submits current user order
        /// </summary>
        /// <returns>Submit order result</returns>
        Result<SubmitOrderModel> SubmitOrder();
    }
}