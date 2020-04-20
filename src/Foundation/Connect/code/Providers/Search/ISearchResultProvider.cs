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
    using System;
    using System.Linq;

    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.SearchTypes;

    /// <summary>
    /// Provides search results basing on search query
    /// </summary>
    public interface ISearchResultProvider
    {
        /// <summary>
        /// Gets FirstOrDefault search result of type <see cref="T:TResult" /> basing on search query
        /// </summary>
        /// <typeparam name="TResult">Type of search result</typeparam>
        /// <param name="searchQuery">Search query</param>
        /// <returns></returns>
        TResult GetSearchResult<TResult>(
            Func<IQueryable<TResult>, IQueryable<TResult>>
                searchQuery) where TResult : SearchResultItem;

        /// <summary>
        /// Gets search results of <see cref="T:SearchResults&lt;TResult&rt;" /> basing on search query
        /// </summary>
        /// <typeparam name="TResult">Type of search result</typeparam>
        /// <param name="searchQuery">Search query</param>
        /// <returns></returns>
        SearchResults<TResult> GetSearchResults<TResult>(
            Func<IQueryable<TResult>, IQueryable<TResult>>
                searchQuery) where TResult : SearchResultItem;
    }
}