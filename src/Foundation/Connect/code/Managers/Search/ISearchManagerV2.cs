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

namespace Wooli.Foundation.Connect.Managers.Search
{
    using Models;

    using Sitecore.Data.Items;

    /// <summary>
    /// Performs search for products by specified parameters
    /// </summary>
    public interface ISearchManagerV2
    {
        /// <summary>
        /// Get products from context catalog by keyword and search options
        /// </summary>
        /// <param name="searchKeyword">Search by keyword</param>
        /// <param name="searchOptions">Search options: sorting, facets, etc.</param>
        /// <returns>Search results</returns>
        SearchResults GetProducts(
            string searchKeyword,
            SearchOptions searchOptions);

        /// <summary>
        /// Get category item from context catalog by category name
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <returns>Category item</returns>
        Item GetCategoryItem(string categoryName);

        /// <summary>
        /// Get product item from context catalog by product id
        /// </summary>
        /// <param name="productId">Product product id</param>
        /// <returns>Product item</returns>
        Item GetProductItem(string productId);
    }
}