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

namespace Wooli.Foundation.Commerce.Services.Catalog
{
    using Models;
    using Models.Entities;
    using Models.Entities.Search;

    /// <summary>
    /// Performs main operations with catalog
    /// </summary>
    public interface IProductSearchService
    {
        /// <summary>
        /// Gets product list by given search parameters
        /// </summary>
        /// <param name="options">Search parameters object</param>
        /// <returns>List of products</returns>
        Result<ProductsSearchResult> GetProducts(ProductsSearchOptions options);
    }
}