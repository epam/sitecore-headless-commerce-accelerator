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

namespace Wooli.Foundation.Connect.Managers.Order
{
    using Base.Models.Logging;
    using Base.Services.Logging;

    using Providers.Contracts;

    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Services.Orders;
    using Sitecore.Diagnostics;

    public class OrderManagerV2 : BaseManager, IOrderManagerV2
    {
        private readonly OrderServiceProvider orderServiceProvider;

        public OrderManagerV2(ILogService<CommonLog> logService, IConnectServiceProvider connectServiceProvider) : base(
            logService)
        {
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));

            this.orderServiceProvider = connectServiceProvider.GetOrderServiceProvider();
        }

        public GetVisitorOrderResult GetOrder(string orderId, string customerId, string shopName)
        {
            Assert.ArgumentNotNullOrEmpty(orderId, nameof(orderId));
            Assert.ArgumentNotNullOrEmpty(customerId, nameof(customerId));
            Assert.ArgumentNotNullOrEmpty(shopName, nameof(shopName));

            return this.Execute(
                new GetVisitorOrderRequest(orderId, customerId, shopName),
                this.orderServiceProvider.GetVisitorOrder);
        }

        public GetVisitorOrdersResult GetOrdersHeaders(string customerId, string shopName)
        {
            Assert.ArgumentNotNullOrEmpty(customerId, nameof(customerId));
            Assert.ArgumentNotNullOrEmpty(shopName, nameof(shopName));

            return this.Execute(
                new GetVisitorOrdersRequest(customerId, shopName),
                this.orderServiceProvider.GetVisitorOrders);
        }

        public SubmitVisitorOrderResult SubmitVisitorOrder(Cart cart)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));

            return this.Execute(
                new SubmitVisitorOrderRequest(cart),
                this.orderServiceProvider.SubmitVisitorOrder);
        }
    }
}