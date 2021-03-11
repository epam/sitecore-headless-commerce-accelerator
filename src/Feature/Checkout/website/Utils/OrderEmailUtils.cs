//    Copyright 2021 EPAM Systems, Inc.
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

namespace HCA.Feature.Checkout.Utils
{
    using Configuration.Providers;

    using Context;

    using Foundation.Base.Models.Logging;
    using Foundation.Base.Services.Logging;
    using Foundation.Commerce.Services.Order;
    using Foundation.DependencyInjection;

    using Sitecore.Diagnostics;

    [Service(typeof(IOrderEmailUtils), Lifetime = Lifetime.Singleton)]
    public class OrderEmailUtils : IOrderEmailUtils
    {
        private readonly IExmContext exmContext;
        private readonly ILogService<CommonLog> logService;
        private readonly IOrderService orderService;
        private readonly IStorefrontConfigurationProvider storefrontConfigurationProvider;

        public OrderEmailUtils(
            IExmContext exmContext,
            IOrderService orderService,
            IStorefrontConfigurationProvider storefrontConfigurationProvider,
            ILogService<CommonLog> logService)
        {
            Assert.ArgumentNotNull(exmContext, nameof(exmContext));
            Assert.ArgumentNotNull(storefrontConfigurationProvider, nameof(storefrontConfigurationProvider));
            Assert.ArgumentNotNull(orderService, nameof(orderService));
            Assert.ArgumentNotNull(logService, nameof(logService));

            this.exmContext = exmContext;
            this.storefrontConfigurationProvider = storefrontConfigurationProvider;
            this.orderService = orderService;
            this.logService = logService;
        }

        public string ResolveTokens(string messageBody)
        {
            Assert.ArgumentNotNull(messageBody, nameof(messageBody));

            return this.ResolveOrderTrackingNumberToken(messageBody);
        }

        private string ResolveOrderTrackingNumberToken(string messageBody)
        {
            if (this.exmContext.IsRenderRequest)
            {
                var orderId = this.exmContext.GetValue(Constants.OrderEmail.OrderIdKey);
                var contactId = this.exmContext.GetContactIdentifier();
                var shopNames = this.storefrontConfigurationProvider.Get()?.ShopNames.ToArray();

                if (shopNames != null)
                {
                    var trackingNumber = this.TryGetTrackingNumber(orderId, contactId, shopNames);
                    this.logService.Info($"Order {orderId} has tracking number: {trackingNumber}");

                    return messageBody.Replace(
                        Constants.OrderEmail.OrderTrackingNumberToken,
                        trackingNumber ?? orderId);
                }

                return messageBody.Replace(Constants.OrderEmail.OrderTrackingNumberToken, orderId);
            }

            return messageBody;
        }

        private string TryGetTrackingNumber(string orderId, string contactId, params string[] shopNames)
        {
            foreach (var shopName in shopNames)
            {
                var order = this.orderService.GetOrder(orderId, contactId, shopName);
                var trackingNumber = order.Data?.TrackingNumber;

                if (!string.IsNullOrWhiteSpace(trackingNumber))
                {
                    return trackingNumber;
                }
            }

            return null;
        }
    }
}