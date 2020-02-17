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
    using DependencyInjection;
    using Providers.Contracts;
    using Sitecore.Commerce.Entities.Prices;
    using Sitecore.Commerce.Services.Prices;
    using Sitecore.Diagnostics;
    using GetProductBulkPricesRequest = Sitecore.Commerce.Engine.Connect.Services.Prices.GetProductBulkPricesRequest;
    using GetProductPricesRequest = Sitecore.Commerce.Engine.Connect.Services.Prices.GetProductPricesRequest;

    [Service(typeof(IPricingManager))]
    public class PricingManager : IPricingManager
    {
        private static readonly string[] DefaultPriceTypeIds = new string[5]
        {
            "List",
            "Adjusted",
            "LowestPricedVariant",
            "LowestPricedVariantListPrice",
            "HighestPricedVariant"
        };

        private readonly PricingServiceProvider pricingServiceProvider;

        public PricingManager(IConnectServiceProvider connectServiceProvider)
        {
            Assert.ArgumentNotNull((object) connectServiceProvider, nameof(connectServiceProvider));
            pricingServiceProvider = connectServiceProvider.GetPricingServiceProvider();
        }

        public ManagerResponse<GetProductBulkPricesResult, IDictionary<string, Price>> GetProductBulkPrices(
            string catalogName, IEnumerable<string> productIds, params string[] priceTypeIds)
        {
            if (priceTypeIds == null) priceTypeIds = DefaultPriceTypeIds;

            var bulkPricesRequest = new GetProductBulkPricesRequest(catalogName, productIds, priceTypeIds)
            {
                DateTime = DateTime.UtcNow
            };

            var productBulkPrices =
                pricingServiceProvider.GetProductBulkPrices(bulkPricesRequest);
            var result = productBulkPrices.Prices ?? new Dictionary<string, Price>();
            return new ManagerResponse<GetProductBulkPricesResult, IDictionary<string, Price>>(productBulkPrices,
                result);
        }

        public ManagerResponse<GetProductPricesResult, IDictionary<string, Price>> GetProductPrices(string catalogName,
            string productId, bool includeVariants, params string[] priceTypeIds)
        {
            if (priceTypeIds == null) priceTypeIds = DefaultPriceTypeIds;

            var pricesRequest =
                new GetProductPricesRequest(catalogName, productId, priceTypeIds)
                {
                    DateTime = DateTime.UtcNow,
                    IncludeVariantPrices = includeVariants
                };

            var serviceProviderResult = pricingServiceProvider.GetProductPrices(pricesRequest);

            return new ManagerResponse<GetProductPricesResult, IDictionary<string, Price>>(serviceProviderResult,
                serviceProviderResult.Prices ?? new Dictionary<string, Price>());
        }
    }
}