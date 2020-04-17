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
    using System.Collections.Generic;

    using Sitecore.Commerce.Entities.Prices;
    using Sitecore.Commerce.Services.Prices;

    public interface IPricingManager
    {
        ManagerResponse<GetProductBulkPricesResult, IDictionary<string, Price>> GetProductBulkPrices(
            string catalogName,
            IEnumerable<string> productIds,
            params string[] priceTypeIds);

        ManagerResponse<GetProductPricesResult, IDictionary<string, Price>> GetProductPrices(
            string catalogName,
            string productId,
            bool includeVariants,
            params string[] priceTypeIds);
    }
}