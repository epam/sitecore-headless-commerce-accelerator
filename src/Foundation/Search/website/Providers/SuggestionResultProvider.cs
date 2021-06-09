//    Copyright 2021 EPAM Systems, Inc.
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

namespace HCA.Foundation.Search.Providers
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SolrNetExtension;
    using Sitecore.ContentSearch.SolrProvider.SolrNetIntegration;
    using Sitecore.Diagnostics;

    public abstract class SuggestionResultProvider : ISuggestionResultProvider
    {
        protected abstract ISearchIndex SearchIndex { get; set; }

        public SuggestResult GetSuggestionResults(SuggestHandlerQueryOptions options)
        {
            Assert.ArgumentNotNull(options, nameof(options));

            using (var searchContext = this.SearchIndex?.CreateSearchContext())
            {
                var suggestResult = searchContext?.Suggest(options.Parameters.Query, options)
                    ?.Suggestions[options.Parameters.Dictionary];

                return suggestResult ?? new SuggestResult();
            }
        }
    }
}