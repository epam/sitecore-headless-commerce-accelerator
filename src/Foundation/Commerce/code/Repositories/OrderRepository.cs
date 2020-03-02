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

namespace Wooli.Foundation.Commerce.Repositories
{
    using System;
    using System.Collections.Generic;
    using Connect.Managers;
    using Context;
    using DependencyInjection;
    using ModelInitializers;
    using ModelMappers;
    using Models;
    using Models.Checkout;
    using Sitecore.Commerce.Entities.Orders;
    using Sitecore.Commerce.Services.Orders;
    using Sitecore.Diagnostics;

    [Service(typeof(IOrderRepository), Lifetime = Lifetime.Singleton)]
    public class OrderRepository : BaseCheckoutRepository, IOrderRepository
    {
        private readonly IOrderManager orderManager;

        private readonly IOrderModelBuilder orderModelBuilder;

        public OrderRepository(
            IOrderManager orderManager,
            ICartManager cartManager,
            ICatalogRepository catalogRepository,
            IAccountManager accountManager,
            ICartModelBuilder cartModelBuilder,
            IOrderModelBuilder orderModelBuilder,
            IEntityMapper entityMapper,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext)
            : base(cartManager, catalogRepository, accountManager, cartModelBuilder, entityMapper, storefrontContext,
                visitorContext)
        {
            this.orderManager = orderManager;
            this.orderModelBuilder = orderModelBuilder;
        }

        public virtual Result<CartModel> GetOrderDetails(string trackingId)
        {
            Assert.ArgumentNotNullOrEmpty(trackingId, nameof(trackingId));

            var result = new Result<CartModel>();
            try
            {
                var orderDetails =
                    orderManager.GetOrderDetails(trackingId, VisitorContext.ContactId, StorefrontContext.ShopName);

                var order = orderDetails.Result;
                if (order != null)
                {
                    CartModel orderModel = orderModelBuilder.Initialize(order);
                    result.SetResult(orderModel);
                    return result;
                }

                result.SetErrors(orderDetails.ServiceProviderResult);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);
                result.SetErrors(nameof(GetOrderDetails), ex);
            }

            return result;
        }

        public virtual Result<OrderHistoryResultModel> GetOrders(DateTime? fromDate, DateTime? untilDate, int page,
            int count)
        {
            var result = new Result<OrderHistoryResultModel>();
            try
            {
                var orderHeaders =
                    orderManager.GetVisitorOrders(VisitorContext.ContactId, StorefrontContext.ShopName, fromDate,
                        untilDate, page, count);

                if (orderHeaders.Result == null)
                {
                    result.SetErrors(orderHeaders.ServiceProviderResult);
                    return result;
                }

                var ordersList = new List<CartModel>();
                foreach (var orderHeader in orderHeaders.Result)
                {
                    var orderDetails =
                        orderManager.GetOrderDetails(orderHeader.OrderID, VisitorContext.ContactId,
                            StorefrontContext.ShopName);

                    var order = orderDetails.Result;
                    if (order != null)
                    {
                        CartModel orderModel = orderModelBuilder.Initialize(order);
                        ordersList.Add(orderModel);
                    }
                    else
                    {
                        result.SetErrors(orderDetails.ServiceProviderResult);
                    }
                }

                var model = new OrderHistoryResultModel(ordersList)
                {
                    CurrentPageNumber = page
                };

                result.SetResult(model);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);
                result.SetErrors(nameof(GetOrders), ex);
            }

            return result;
        }
    }
}