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

namespace HCA.Foundation.Commerce.Converters.Search
{
    using Configuration.Providers;

    using DependencyInjection;

    using Foundation.Search.Models.Common;

    using Sitecore.Diagnostics;

    [Service(typeof(ISuggestionOptionsConverter), Lifetime = Lifetime.Singleton)]
    public class SuggestionOptionsConverter : ISuggestionOptionsConverter
    {
        private readonly ISuggesterConfigurationProvider suggesterConfigurationProvider;

        public SuggestionOptionsConverter(ISuggesterConfigurationProvider suggesterConfigurationProvider)
        {
            Assert.ArgumentNotNull(suggesterConfigurationProvider, nameof(suggesterConfigurationProvider));

            this.suggesterConfigurationProvider = suggesterConfigurationProvider;
        }

        public SuggestionOptions Convert(string search)
        {
            var suggesterSettings = this.suggesterConfigurationProvider.Get();

            return new SuggestionOptions
            {
                Count = suggesterSettings?.SuggestionsMaxCount,
                Build = suggesterSettings?.Build,
                Dictionary = suggesterSettings?.DictionaryName,
                Query = search,
                ContextFilterQuery = suggesterSettings?.FilterTemplateId
            };
        }
    }
}