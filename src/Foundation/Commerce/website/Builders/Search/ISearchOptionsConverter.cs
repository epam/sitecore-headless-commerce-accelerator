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

namespace HCA.Foundation.Commerce.Builders.Search
{
    using Models.Entities.Search;

    /// <summary>
    /// Converts resulting search options basing on products search options
    /// </summary>
    public interface ISearchOptionsConverter
    {
        /// <summary>
        /// Converts products search options to search options 
        /// </summary>
        /// <param name="searchOptions">Product search options</param>
        /// <returns>Search options</returns>
        Foundation.Search.Models.Entities.Product.ProductSearchOptions Convert(ProductSearchOptions searchOptions);
    }
}