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

namespace HCA.Foundation.Connect.Builders.Search
{
    using System.Linq;

    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.Data;

    public interface ISearchQueryBuilder : IQueryBuilder<CommerceSellableItemSearchResultItem, CommerceSearchOptions>
    {
        /// <summary>
        /// Builds category search query basing on category name
        /// </summary>
        /// <param name="queryable">Query to update</param>
        /// <param name="categoryName">Category name</param>
        /// <returns>Updated search query</returns>
        IQueryable<CommerceSellableItemSearchResultItem> BuildCategoryQuery(
            IQueryable<CommerceSellableItemSearchResultItem> queryable,
            string categoryName);

        /// <summary>
        /// Builds product search query basing on product id
        /// </summary>
        /// <param name="queryable">Query to update</param>
        /// <param name="productId">Product id</param>
        /// <returns>Updated search query</returns>
        IQueryable<CommerceSellableItemSearchResultItem> BuildProductQuery(
            IQueryable<CommerceSellableItemSearchResultItem> queryable,
            string productId);

        //TODO: create method Build with TSearchOptions and option per current method

        /// <summary>
        /// Builds products search query basing on options
        /// </summary>
        /// <param name="queryable">Query to update</param>
        /// <param name="searchKeyword">Search searchKeyword</param>
        /// <param name="categoryId">Category id</param>
        /// <param name="options">Search options</param>
        /// <returns>Updated search query</returns>
        IQueryable<CommerceSellableItemSearchResultItem> BuildProductsQuery(
            IQueryable<CommerceSellableItemSearchResultItem> queryable,
            string searchKeyword,
            ID categoryId,
            CommerceSearchOptions options);
    }
}