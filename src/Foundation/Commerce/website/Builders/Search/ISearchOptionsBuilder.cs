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
    using Connect.Models.Search;

    using Models.Entities.Search;

    /// <summary>
    /// Search options builder
    /// </summary>
    public interface ISearchOptionsBuilder
    {
        /// <summary>
        /// Builds search options basing on products search options and default search settings
        /// </summary>
        /// <param name="searchSettings">Predefined default search settings</param>
        /// <param name="searchOptions">Product search options</param>
        /// <returns>Search options</returns>
        SearchOptions Build(SearchSettings searchSettings, ProductSearchOptions searchOptions);
    }
}