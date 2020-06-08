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
    using System.Linq;

    using Base.Models.Logging;
    using Base.Models.Result;
    using Base.Services.Logging;

    using Builders.Order;

    using Connect.Context.Storefront;
    using Connect.Managers.Cart;
    using Connect.Managers.Order;

    using Context;

    using DependencyInjection;

    using Models.Entities.Order;

    using Newtonsoft.Json;

    using Sitecore.Diagnostics;

    [Service(typeof(IOrderService), Lifetime = Lifetime.Singleton)]
    public class OrderService : IOrderService
    {
        private readonly IOrderBuilder builder;

        private readonly ICartManager cartManager;

        private readonly ILogService<CommonLog> logService;

        private readonly IOrderManager orderManager;

        private readonly IStorefrontContext storefrontContext;

        private readonly IVisitorContext visitorContext;

        public OrderService(
            IOrderBuilder orderBuilder,
            ILogService<CommonLog> logService,
            IOrderManager orderManager,
            ICartManager cartManager,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext)
        {
            Assert.ArgumentNotNull(orderManager, nameof(orderManager));
            Assert.ArgumentNotNull(cartManager, nameof(cartManager));
            Assert.ArgumentNotNull(orderBuilder, nameof(orderBuilder));
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));
            Assert.ArgumentNotNull(visitorContext, nameof(visitorContext));

            this.orderManager = orderManager;
            this.logService = logService;
            this.builder = orderBuilder;
            this.cartManager = cartManager;
            this.storefrontContext = storefrontContext;
            this.visitorContext = visitorContext;
        }

        public Result<Order> GetOrder(string orderId)
        {
            Assert.ArgumentNotNullOrEmpty(orderId, nameof(orderId));

            var result = new Result<Order>();

            var orderResult = this.orderManager.GetOrder(
                orderId,
                this.visitorContext.ContactId,
                this.storefrontContext.ShopName);

            if (!orderResult.Success)
            {
                result.SetErrors(orderResult.SystemMessages.Select(sm => sm.Message).ToList());
            }

            result.SetResult(this.builder.Build(orderResult.Order));

            return result;
        }

        public Result<IList<Order>> GetOrders(DateTime? fromDate, DateTime? untilDate, int page, int count)
        {
            var result = new Result<IList<Order>>();

            var headersResult = this.orderManager.GetOrdersHeaders(
                this.visitorContext.ContactId,
                this.storefrontContext.ShopName);

            if (headersResult.Success)
            {
                IList<Order> orders = headersResult.OrderHeaders
                    .Where(header => fromDate == null || header.OrderDate >= fromDate)
                    .Where(header => untilDate == null || header.OrderDate <= untilDate)
                    .Skip(page * count)
                    .Take(count)
                    .Select(
                        header =>
                        {
                            var orderResult = this.orderManager.GetOrder(
                                header.OrderID,
                                this.visitorContext.ContactId,
                                this.storefrontContext.ShopName);

                            if (orderResult.Success)
                            {
                                return this.builder.Build(orderResult.Order);
                            }

                            result.SetErrors(orderResult.SystemMessages.Select(sm => sm.Message).ToList());
                            return null;
                        })
                    .Where(order => order != null)
                    .ToList();

                result.SetResult(orders);
            }
            else
            {
                result.SetErrors(headersResult.SystemMessages.Select(sm => sm.Message).ToList());
            }

            return result;
        }

        public Result<OrderConfirmation> SubmitOrder()
        {
            var result = new Result<OrderConfirmation>();

            var cartResult = this.cartManager.LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId);

            if (cartResult.Success)
            {
                var submitResult = this.orderManager.SubmitVisitorOrder(cartResult.Cart);

                if (submitResult.Success)
                {
                    result.SetResult(
                        new OrderConfirmation
                        {
                            ConfirmationId = submitResult.Order.TrackingNumber
                        });
                }
                else
                {
                    this.logService.Error(JsonConvert.SerializeObject(submitResult.CartWithErrors));
                    result.SetErrors(submitResult.SystemMessages.Select(sm => sm.Message).ToList());
                }
            }
            else
            {
                result.SetErrors(cartResult.SystemMessages.Select(sm => sm.Message).ToList());
            }

            return result;
        }
    }
}