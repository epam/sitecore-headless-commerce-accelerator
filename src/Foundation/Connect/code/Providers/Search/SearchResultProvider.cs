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

namespace Wooli.Foundation.Connect.Providers.Search
{
    using System;
    using System.Linq;

    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.Diagnostics;

    public abstract class SearchResultProvider<TResult> : ISearchResultProvider<TResult>
        where TResult : SearchResultItem
    {
        protected abstract ISearchIndex SearchIndex { get; set; }

        public TResult GetSearchResult(Func<IQueryable<TResult>, IQueryable<TResult>> buildQuery)
        {
            Assert.ArgumentNotNull(buildQuery, nameof(buildQuery));

            var searchResults = this.GetSearchResults(buildQuery);
            return searchResults?.Hits?.Select(hit => hit.Document).FirstOrDefault();
        }

        public SearchResults<TResult> GetSearchResults(Func<IQueryable<TResult>, IQueryable<TResult>> buildQuery)
        {
            Assert.ArgumentNotNull(buildQuery, nameof(buildQuery));

            using (var searchContext = this.SearchIndex?.CreateSearchContext())
            {
                var queryable = buildQuery.Invoke(searchContext?.GetQueryable<TResult>());
                return queryable.GetResults();
            }
        }
    }
}