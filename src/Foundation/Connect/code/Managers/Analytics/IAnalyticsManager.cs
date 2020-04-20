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

namespace HCA.Foundation.Connect.Managers.Analytics
{
    using Sitecore.Commerce.Services.Catalog;

    /// <summary>
    /// Triggers analytics pipelines which raises different page events (category visited, search initiated, etc.)
    /// </summary>
    public interface IAnalyticsManager
    {
        /// <summary>
        /// Registers category visited page event.
        /// </summary>
        /// <param name="shopName">Shop name</param>
        /// <param name="categoryId">Category id</param>
        /// <param name="categoryName">Category name</param>
        /// <returns>The result of the catalog service provider.</returns>
        CatalogResult VisitedCategoryPage(string shopName, string categoryId, string categoryName);

        /// <summary>
        /// Registers product details page visited page event.
        /// </summary>
        /// <param name="shopName"></param>
        /// <param name="productId"></param>
        /// <param name="productName"></param>
        /// <returns>The result of the catalog service provider.</returns>
        CatalogResult VisitedProductDetailsPage(string shopName, string productId, string productName);

        /// <summary>
        /// Registers search initiated page event.
        /// </summary>
        /// <param name="shopName">Shop name</param>
        /// <param name="searchKeyword">Search keyword</param>
        /// <param name="totalItemsCount">Search total items count</param>
        /// <returns>The result of the catalog service provider.</returns>
        CatalogResult SearchInitiated(
            string shopName,
            string searchKeyword,
            int totalItemsCount);
    }
}