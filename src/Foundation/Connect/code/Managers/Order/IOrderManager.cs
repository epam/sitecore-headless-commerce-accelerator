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

namespace HCA.Foundation.Connect.Managers.Order
{
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Services.Orders;

    /// <summary>
    /// Performs main operations during checkout process
    /// </summary>
    public interface IOrderManager
    {
        /// <summary>
        /// Gets order by id
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="customerId">Customer id</param>
        /// <param name="shopName">Shop name</param>
        /// <returns>Get visitor order result</returns>
        GetVisitorOrderResult GetOrder(string orderId, string customerId, string shopName);

        /// <summary>
        /// Gets headers of the orders
        /// </summary>
        /// <param name="customerId">Customer id</param>
        /// <param name="shopName">Shop name</param>
        /// <returns>Get visitor orders result</returns>
        GetVisitorOrdersResult GetOrdersHeaders(string customerId, string shopName);

        /// <summary>
        /// Submits order
        /// </summary>
        /// <param name="cart">Cart for submit</param>
        /// <returns>Submit visitor order result</returns>
        SubmitVisitorOrderResult SubmitVisitorOrder(Cart cart);
    }
}