//    Copyright 2019 EPAM Systems, Inc.
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

namespace Wooli.Foundation.Connect.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DependencyInjection;
    using Providers.Contracts;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Orders;
    using Sitecore.Commerce.Services.Orders;
    using Sitecore.Diagnostics;

    [Service(typeof(IOrderManager))]
    public class OrderManager : IOrderManager
    {
        private readonly OrderServiceProvider orderServiceProvider;

        public OrderManager(IConnectServiceProvider connectServiceProvider)
        {
            Assert.ArgumentNotNull((object) connectServiceProvider, nameof(connectServiceProvider));
            orderServiceProvider = connectServiceProvider.GetOrderServiceProvider();
        }

        public ManagerResponse<SubmitVisitorOrderResult, Order> SubmitVisitorOrder(Cart cart)
        {
            var request = new SubmitVisitorOrderRequest(cart);
            try
            {
                SubmitVisitorOrderResult visitorOrderResult = orderServiceProvider.SubmitVisitorOrder(request);

                SubmitVisitorOrderResult serviceProviderResult = visitorOrderResult;
                return new ManagerResponse<SubmitVisitorOrderResult, Order>(
                    serviceProviderResult,
                    serviceProviderResult.Order);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);

                // ToDo: fix issues during order submitting: down there: temp implementation of the submit result
                if (TryResolveSubmittedOrder(cart, out ManagerResponse<SubmitVisitorOrderResult, Order> managerResponse)
                ) return managerResponse;

                throw;
            }
        }

        public ManagerResponse<GetVisitorOrderResult, Order> GetOrderDetails(string orderId, string customerId,
            string shopName)
        {
            Assert.ArgumentNotNullOrEmpty(orderId, nameof(orderId));
            Assert.ArgumentNotNullOrEmpty(customerId, nameof(customerId));
            Assert.ArgumentNotNullOrEmpty(shopName, nameof(shopName));

            var getVisitorOrderRequest = new GetVisitorOrderRequest(orderId, customerId, shopName);
            GetVisitorOrderResult getVisitorOrderResult = orderServiceProvider.GetVisitorOrder(getVisitorOrderRequest);
            if (!string.IsNullOrEmpty(getVisitorOrderResult.Order?.CustomerId) &&
                getVisitorOrderResult.Order?.CustomerId == customerId)
            {
                GetVisitorOrderResult successServiceProviderResult = getVisitorOrderResult;
                return new ManagerResponse<GetVisitorOrderResult, Order>(successServiceProviderResult,
                    successServiceProviderResult.Order);
            }

            var errorServiceProviderResult = new GetVisitorOrderResult {Success = false};
            return new ManagerResponse<GetVisitorOrderResult, Order>(errorServiceProviderResult, null);
        }

        public ManagerResponse<GetVisitorOrdersResult, OrderHeader[]> GetVisitorOrders(
            string customerId,
            string shopName,
            DateTime? fromDate,
            DateTime? untilDate,
            int page,
            int count)
        {
            var getVisitorOrdersRequest = new GetVisitorOrdersRequest(customerId, shopName);
            GetVisitorOrdersResult visitorOrders = orderServiceProvider.GetVisitorOrders(getVisitorOrdersRequest);

            if (visitorOrders.OrderHeaders == null)
                return new ManagerResponse<GetVisitorOrdersResult, OrderHeader[]>(visitorOrders, new OrderHeader[0]);

            IEnumerable<OrderHeader> query = visitorOrders.OrderHeaders;
            if (fromDate != null) query = query.Where(x => x.OrderDate >= fromDate);

            if (untilDate != null) query = query.Where(oh => oh.OrderDate <= untilDate);

            OrderHeader[] array = query
                .Skip(page * count)
                .Take(count)
                .ToArray();

            return new ManagerResponse<GetVisitorOrdersResult, OrderHeader[]>(visitorOrders, array);
        }

        private bool TryResolveSubmittedOrder(CartBase cart,
            out ManagerResponse<SubmitVisitorOrderResult, Order> managerResponse)
        {
            var getVisitorOrdersRequest = new GetVisitorOrdersRequest(cart.CustomerId, cart.ShopName);
            GetVisitorOrdersResult getVisitorOrdersResult =
                orderServiceProvider.GetVisitorOrders(getVisitorOrdersRequest);

            if (getVisitorOrdersResult.Success)
            {
                // Getting the latest order
                OrderHeader orderHeader = getVisitorOrdersResult.OrderHeaders.OrderByDescending(x => x.OrderDate)
                    .FirstOrDefault();
                if (orderHeader != null)
                {
                    var getVisitorOrderRequest = new GetVisitorOrderRequest(orderHeader.OrderID, orderHeader.CustomerId,
                        orderHeader.ShopName);

                    GetVisitorOrderResult getVisitorOrderResult =
                        orderServiceProvider.GetVisitorOrder(getVisitorOrderRequest);

                    if (getVisitorOrderResult.Order != null)
                    {
                        managerResponse = new ManagerResponse<SubmitVisitorOrderResult, Order>(
                            new SubmitVisitorOrderResult {Order = getVisitorOrderResult.Order, Success = true},
                            getVisitorOrderResult.Order);

                        return true;
                    }
                }
            }

            managerResponse = null;
            return false;
        }
    }
}