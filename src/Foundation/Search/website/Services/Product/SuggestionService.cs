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

namespace HCA.Foundation.Search.Services.Product
{
    using DependencyInjection;

    using Mappers;

    using Models.Common;

    using Providers;

    using Sitecore.ContentSearch.SolrNetExtension;
    using Sitecore.Diagnostics;

    [Service(typeof(ISuggestionService), Lifetime = Lifetime.Singleton)]
    public class SuggestionService : ISuggestionService
    {
        private readonly ISuggestionMapper suggestionMapper;

        private readonly ISuggestionResultProvider suggestionResultProvider;

        public SuggestionService(
            ISuggestionMapper suggestionMapper,
            ISuggestionResultProvider suggestionResultProvider)
        {
            Assert.ArgumentNotNull(suggestionMapper, nameof(suggestionMapper));
            Assert.ArgumentNotNull(suggestionResultProvider, nameof(suggestionResultProvider));

            this.suggestionMapper = suggestionMapper;
            this.suggestionResultProvider = suggestionResultProvider;
        }

        public SuggestionResult GetSuggestions(SuggestionOptions suggestionOptions)
        {
            Assert.ArgumentNotNull(suggestionOptions, nameof(suggestionOptions));

            var suggestQueryOptions =
                this.suggestionMapper.Map<SuggestionOptions, SuggestHandlerQueryOptions>(suggestionOptions);
            var suggestResult = this.suggestionResultProvider.GetSuggestionResults(suggestQueryOptions);
            var suggestionResponse = this.suggestionMapper.Map<SuggestResult, SuggestionResult>(suggestResult);

            return suggestionResponse;
        }
    }
}