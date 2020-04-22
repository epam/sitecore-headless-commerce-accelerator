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
    using System;
    using System.Linq;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using DependencyInjection;

    using Providers;

    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Services.Orders;
    using Sitecore.Diagnostics;

    [Service(typeof(IOrderManager), Lifetime = Lifetime.Singleton)]
    public class OrderManager : BaseManager, IOrderManager
    {
        private readonly OrderServiceProvider orderServiceProvider;

        public OrderManager(ILogService<CommonLog> logService, IConnectServiceProvider connectServiceProvider) : base(
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

            try
            {
                return this.Execute(
                    new SubmitVisitorOrderRequest(cart),
                    this.orderServiceProvider.SubmitVisitorOrder);
            }
            catch (Exception ex)
            {
                this.LogService.Error(ex.Message);

                // ToDo: fix issues during order submitting: down there: temp implementation of the submit result
                if (this.TryResolveSubmittedOrder(cart, out var managerResponse))
                {
                    return managerResponse;
                }

                throw;
            }
        }

        private bool TryResolveSubmittedOrder(CartBase cart, out SubmitVisitorOrderResult managerResponse)
        {
            var getVisitorOrdersResult = this.GetOrdersHeaders(cart.CustomerId, cart.ShopName);

            if (getVisitorOrdersResult.Success)
            {
                // Getting the latest order
                var orderHeader = getVisitorOrdersResult.OrderHeaders
                    .OrderByDescending(x => x.OrderDate)
                    .FirstOrDefault();

                if (orderHeader != null)
                {
                    var getVisitorOrderResult = this.GetOrder(
                        orderHeader.OrderID,
                        orderHeader.CustomerId,
                        orderHeader.ShopName);

                    if (getVisitorOrderResult.Order != null)
                    {
                        managerResponse = new SubmitVisitorOrderResult
                        {
                            Order = getVisitorOrderResult.Order,
                            Success = true
                        };

                        return true;
                    }
                }
            }

            managerResponse = null;
            return false;
        }
    }
}