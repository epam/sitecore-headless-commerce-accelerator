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

namespace Wooli.Foundation.Connect.Managers.Shipping
{
    using System;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using DependencyInjection;

    using Providers.Contracts;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services;
    using Sitecore.Commerce.Services.Shipping;
    using Sitecore.Diagnostics;

    using GetShippingMethodsRequest = Sitecore.Commerce.Engine.Connect.Services.Shipping.GetShippingMethodsRequest;

    [Service(typeof(IShippingManagerV2), Lifetime = Lifetime.Singleton)]
    public class ShippingManagerV2 : IShippingManagerV2
    {
        private readonly ShippingServiceProvider shippingServiceProvider;
        private readonly ILogService<CommonLog> logService;

        public ShippingManagerV2(IConnectServiceProvider connectServiceProvider, ILogService<CommonLog> logService)
        {
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));
            Assert.ArgumentNotNull(logService, nameof(logService));

            this.logService = logService;
            this.shippingServiceProvider = connectServiceProvider.GetShippingServiceProvider();
        }

        public GetShippingMethodsResult GetShippingMethods(Cart cart, ShippingOptionType shippingOptionType)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(shippingOptionType, nameof(shippingOptionType));

            var shippingOption = new ShippingOption
            {
                ShippingOptionType = shippingOptionType
            };
            var request = new GetShippingMethodsRequest(shippingOption, null, cart as CommerceCart);

            return this.Execute(request, this.shippingServiceProvider.GetShippingMethods);
        }

        public GetShippingOptionsResult GetShippingOptions(Cart cart)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));

            return this.Execute(new GetShippingOptionsRequest(cart), this.shippingServiceProvider.GetShippingOptions);
        }

        //TODO: Remove Duplication of execute method from managers
        private TResult Execute<TRequest, TResult>(TRequest request, Func<TRequest, TResult> action)
            where TRequest : ServiceProviderRequest
            where TResult : ServiceProviderResult
        {
            var providerResult = action.Invoke(request);

            if (!providerResult.Success)
            {
                foreach (var systemMessage in providerResult.SystemMessages)
                {
                    this.logService.Error(systemMessage.Message);
                }
            }

            return providerResult;
        }
    }
}