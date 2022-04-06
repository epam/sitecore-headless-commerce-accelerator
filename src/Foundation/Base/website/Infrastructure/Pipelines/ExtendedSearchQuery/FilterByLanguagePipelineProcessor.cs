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

namespace HCA.Foundation.Base.Infrastructure.Pipelines.ExtendedSearchQuery
{
    using System.Linq;

    using Models.ExtendedSearchQuery;

    using Sitecore;
    using Sitecore.Data.Managers;
    using Sitecore.Diagnostics;
    using Sitecore.Globalization;
    using Sitecore.Services.GraphQL.Content.GraphTypes.ContentSearch;

    public class FilterByLanguagePipelineProcessor : PipelineProcessor<ExtendedSearchPipelineArgs>
    {
        protected string Lang { get; set; } = Context.Language.Name ?? LanguageManager.DefaultLanguage.Name;

        public override void Process(ExtendedSearchPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            this.Lang = args.FieldContext.GetArgument("language", this.Lang);

            args.SearchQueryable = this.FilterByLanguage(args.SearchQueryable, this.Lang);
        }

        protected IQueryable<ContentSearchResult> FilterByLanguage(IQueryable<ContentSearchResult> searchQueryable, string lang)
        {
            Assert.ArgumentNotNull(searchQueryable, nameof(searchQueryable));
            Assert.ArgumentNotNull(lang, nameof(lang));

            return Language.TryParse(lang, out var language)
                ? searchQueryable.Where(result => result.Language == language.Name)
                : searchQueryable;
        }
    }
}