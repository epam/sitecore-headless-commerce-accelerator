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

namespace HCA.Foundation.Connect.Managers.Pricing
{
    using System;
    using System.Collections.Generic;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using DependencyInjection;

    using Providers;

    using Sitecore.Commerce.Services.Prices;
    using Sitecore.Diagnostics;

    using GetProductBulkPricesRequest = Sitecore.Commerce.Engine.Connect.Services.Prices.GetProductBulkPricesRequest;
    using GetProductPricesRequest = Sitecore.Commerce.Engine.Connect.Services.Prices.GetProductPricesRequest;

    [Service(typeof(IPricingManager), Lifetime = Lifetime.Singleton)]
    public class PricingManager : BaseManager, IPricingManager
    {
        private readonly PricingServiceProvider pricingServiceProvider;

        public PricingManager(IConnectServiceProvider connectServiceProvider, ILogService<CommonLog> logService)
            : base(logService)
        {
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));

            this.pricingServiceProvider = connectServiceProvider.GetPricingServiceProvider();
        }

        public GetProductBulkPricesResult GetProductBulkPrices(
            string catalogName,
            IEnumerable<string> productIds,
            params string[] priceTypeIds)
        {
            Assert.ArgumentNotNullOrEmpty(catalogName, nameof(catalogName));
            Assert.ArgumentNotNull(productIds, nameof(productIds));
            Assert.ArgumentNotNull(priceTypeIds, nameof(priceTypeIds));

            var bulkPricesRequest = new GetProductBulkPricesRequest(catalogName, productIds, priceTypeIds)
            {
                DateTime = DateTime.UtcNow
            };

            return this.Execute(bulkPricesRequest, this.pricingServiceProvider.GetProductBulkPrices);
        }

        public GetProductPricesResult GetProductPrices(
            string catalogName,
            string productId,
            bool includeVariants,
            params string[] priceTypeIds)
        {
            Assert.ArgumentNotNullOrEmpty(catalogName, nameof(catalogName));
            Assert.ArgumentNotNullOrEmpty(productId, nameof(productId));
            Assert.ArgumentNotNull(priceTypeIds, nameof(priceTypeIds));

            var pricesRequest = new GetProductPricesRequest(catalogName, productId, priceTypeIds)
            {
                DateTime = DateTime.UtcNow,
                IncludeVariantPrices = includeVariants
            };

            return this.Execute(pricesRequest, this.pricingServiceProvider.GetProductPrices);
        }
    }
}