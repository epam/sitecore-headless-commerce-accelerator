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

namespace HCA.Foundation.Connect.Providers.Search
{
    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.SearchTypes;

    /// <summary>
    /// Proxy provider for static SearchResponse
    /// </summary>
    public interface ISearchResponseProvider
    {
        /// <summary>
        /// Creates a new instance of the <see cref="T:Sitecore.Commerce.Engine.Connect.Search.SearchResponse" /> class based on
        /// search results of <see cref="T:Sitecore.ContentSearch.SearchTypes.SearchResultItem" /> objects.
        /// </summary>
        /// <param name="searchOptions">The commerce search options </param>
        /// <param name="sitecoreSearchResults">The sitecore search results</param>
        /// <typeparam name="T">
        /// The type of items in the provided <paramref name="sitecoreSearchResults" />.  Must derive from
        /// <see cref="T:Sitecore.ContentSearch.SearchTypes.SearchResultItem" />.
        /// </typeparam>
        /// <returns>A new instance of the <see cref="T:Sitecore.Commerce.Engine.Connect.Search.SearchResponse" /> class.</returns>
        SearchResponse CreateFromSearchResultsItems<T>(
            CommerceSearchOptions searchOptions,
            SearchResults<T> sitecoreSearchResults)
            where T : SearchResultItem;
    }
}