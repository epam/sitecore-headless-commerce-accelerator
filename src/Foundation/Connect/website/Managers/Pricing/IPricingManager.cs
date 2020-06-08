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
    using System.Collections.Generic;

    using Sitecore.Commerce.Services.Prices;

    /// <summary>
    /// Gets pricing of catalog products
    /// </summary>
    public interface IPricingManager
    {
        /// <summary>
        /// Gets product bulk prices
        /// </summary>
        /// <param name="catalogName">Product catalog name</param>
        /// <param name="productIds">Product Ids</param>
        /// <param name="priceTypeIds">Price type Ids</param>
        /// <returns>Product bulk price result</returns>
        GetProductBulkPricesResult GetProductBulkPrices(
            string catalogName,
            IEnumerable<string> productIds,
            params string[] priceTypeIds);

        /// <summary>
        /// Gets product prices
        /// </summary>
        /// <param name="catalogName">Product catalog name</param>
        /// <param name="productId">Product Id</param>
        /// <param name="includeVariants">Need to include product variants</param>
        /// <param name="priceTypeIds">Price type Ids</param>
        /// <returns>Product prices result</returns>
        GetProductPricesResult GetProductPrices(
            string catalogName,
            string productId,
            bool includeVariants,
            params string[] priceTypeIds);
    }
}