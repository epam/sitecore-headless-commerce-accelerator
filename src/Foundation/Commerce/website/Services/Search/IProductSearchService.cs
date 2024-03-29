﻿//    Copyright 2020 EPAM Systems, Inc.
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

namespace HCA.Foundation.Commerce.Services.Search
{
    using Base.Models.Result;

    using Models.Entities.Catalog;
    using Models.Entities.Search;
    
    /// <summary>
    /// Performs products search
    /// </summary>
    public interface IProductSearchService
    {
        /// <summary>
        /// Gets products using searching options
        /// </summary>
        /// <returns>Product search results</returns>
        Result<ProductSearchResults> GetProducts(ProductSearchOptions productSearchOptions);

        /// <summary>
        /// Gets category item by category name
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <returns>Category item</returns>
        Result<Category> GetCategoryByName(string categoryName);
    }
}