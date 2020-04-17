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

    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.Diagnostics;

    public abstract class SearchResultProvider : ISearchResultProvider
    {
        protected abstract ISearchIndex SearchIndex { get; set; }

        public TResult GetSearchResult<TResult>(Func<IQueryable<TResult>, IQueryable<TResult>> searchQuery)
            where TResult : SearchResultItem
        {
            Assert.ArgumentNotNull(searchQuery, nameof(searchQuery));

            var searchResults = this.GetSearchResults(searchQuery);
            return searchResults?.Hits?.Select(hit => hit.Document).FirstOrDefault();
        }

        public SearchResults<TResult> GetSearchResults<TResult>(
            Func<IQueryable<TResult>, IQueryable<TResult>> searchQuery)
            where TResult : SearchResultItem
        {
            Assert.ArgumentNotNull(searchQuery, nameof(searchQuery));

            using (var searchContext = this.SearchIndex?.CreateSearchContext())
            {
                var queryable = searchQuery.Invoke(searchContext?.GetQueryable<TResult>());
                return queryable.GetResults();
            }
        }
    }
}