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

namespace HCA.Foundation.Connect.Managers.Shipping
{
    using Base.Models.Logging;
    using Base.Services.Logging;

    using DependencyInjection;

    using Providers;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services.Shipping;
    using Sitecore.Diagnostics;

    using GetShippingMethodsRequest = Sitecore.Commerce.Engine.Connect.Services.Shipping.GetShippingMethodsRequest;

    [Service(typeof(IShippingManager), Lifetime = Lifetime.Singleton)]
    public class ShippingManager : BaseManager, IShippingManager
    {
        private readonly ShippingServiceProvider shippingServiceProvider;

        public ShippingManager(IConnectServiceProvider connectServiceProvider, ILogService<CommonLog> logService)
            : base(logService)
        {
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));

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
    }
}