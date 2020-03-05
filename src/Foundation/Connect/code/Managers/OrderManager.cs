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
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));
            this.orderServiceProvider = connectServiceProvider.GetOrderServiceProvider();
        }

        public ManagerResponse<GetVisitorOrderResult, Order> GetOrderDetails(
            string orderId,
            string customerId,
            string shopName)
        {
            Assert.ArgumentNotNullOrEmpty(orderId, nameof(orderId));
            Assert.ArgumentNotNullOrEmpty(customerId, nameof(customerId));
            Assert.ArgumentNotNullOrEmpty(shopName, nameof(shopName));

            var getVisitorOrderRequest = new GetVisitorOrderRequest(orderId, customerId, shopName);
            var getVisitorOrderResult = this.orderServiceProvider.GetVisitorOrder(getVisitorOrderRequest);
            if (!string.IsNullOrEmpty(getVisitorOrderResult.Order?.CustomerId)
                && (getVisitorOrderResult.Order?.CustomerId == customerId))
            {
                var successServiceProviderResult = getVisitorOrderResult;
                return new ManagerResponse<GetVisitorOrderResult, Order>(
                    successServiceProviderResult,
                    successServiceProviderResult.Order);
            }

            var errorServiceProviderResult = new GetVisitorOrderResult
            {
                Success = false
            };
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
            var visitorOrders = this.orderServiceProvider.GetVisitorOrders(getVisitorOrdersRequest);

            if (visitorOrders.OrderHeaders == null)
            {
                return new ManagerResponse<GetVisitorOrdersResult, OrderHeader[]>(visitorOrders, new OrderHeader[0]);
            }

            IEnumerable<OrderHeader> query = visitorOrders.OrderHeaders;
            if (fromDate != null)
            {
                query = query.Where(x => x.OrderDate >= fromDate);
            }

            if (untilDate != null)
            {
                query = query.Where(oh => oh.OrderDate <= untilDate);
            }

            var array = query.Skip(page * count).Take(count).ToArray();

            return new ManagerResponse<GetVisitorOrdersResult, OrderHeader[]>(visitorOrders, array);
        }

        public ManagerResponse<SubmitVisitorOrderResult, Order> SubmitVisitorOrder(Cart cart)
        {
            var request = new SubmitVisitorOrderRequest(cart);
            try
            {
                var visitorOrderResult = this.orderServiceProvider.SubmitVisitorOrder(request);

                var serviceProviderResult = visitorOrderResult;
                return new ManagerResponse<SubmitVisitorOrderResult, Order>(
                    serviceProviderResult,
                    serviceProviderResult.Order);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);

                // ToDo: fix issues during order submitting: down there: temp implementation of the submit result
                if (this.TryResolveSubmittedOrder(cart, out var managerResponse))
                {
                    return managerResponse;
                }

                throw;
            }
        }

        private bool TryResolveSubmittedOrder(
            CartBase cart,
            out ManagerResponse<SubmitVisitorOrderResult, Order> managerResponse)
        {
            var getVisitorOrdersRequest = new GetVisitorOrdersRequest(cart.CustomerId, cart.ShopName);
            var getVisitorOrdersResult = this.orderServiceProvider.GetVisitorOrders(getVisitorOrdersRequest);

            if (getVisitorOrdersResult.Success)
            {
                // Getting the latest order
                var orderHeader = getVisitorOrdersResult.OrderHeaders.OrderByDescending(x => x.OrderDate)
                    .FirstOrDefault();
                if (orderHeader != null)
                {
                    var getVisitorOrderRequest = new GetVisitorOrderRequest(
                        orderHeader.OrderID,
                        orderHeader.CustomerId,
                        orderHeader.ShopName);

                    var getVisitorOrderResult = this.orderServiceProvider.GetVisitorOrder(getVisitorOrderRequest);

                    if (getVisitorOrderResult.Order != null)
                    {
                        managerResponse = new ManagerResponse<SubmitVisitorOrderResult, Order>(
                            new SubmitVisitorOrderResult
                            {
                                Order = getVisitorOrderResult.Order,
                                Success = true
                            },
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