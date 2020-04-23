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

namespace HCA.Feature.Checkout.Utils
{
    using Context;

    using Foundation.DependencyInjection;

    using Sitecore.Diagnostics;

    [Service(typeof(IOrderEmailUtils), Lifetime = Lifetime.Singleton)]
    public class OrderEmailUtils : IOrderEmailUtils
    {
        private const string OrderIdKey = "order_id";

        private readonly IExmContext exmContext;

        public OrderEmailUtils(IExmContext exmContext)
        {
            Assert.ArgumentNotNull(exmContext, nameof(exmContext));

            this.exmContext = exmContext;
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
                var orderId = this.exmContext.GetValue(OrderIdKey);
                return messageBody.Replace(Constants.OrderEmail.OrderTrackingNumberToken, orderId);
            }

            return messageBody;
        }
    }
}