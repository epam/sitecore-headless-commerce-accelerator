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
    using System;
    using System.Linq;

    using Models.ExtendedSearchQuery;

    using Sitecore.Diagnostics;
    using Sitecore.Services.GraphQL.Content.GraphTypes.ContentSearch;

    public class FilterByKeywordPipelineProcessor : PipelineProcessor<ExtendedSearchPipelineArgs>
    {
        protected string Keyword { get; set; } = String.Empty;

        public override void Process(ExtendedSearchPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            this.Keyword = args.FieldContext.GetArgument("keyword", this.Keyword);

            args.SearchQueryable = this.FilterByKeyword(args.SearchQueryable, this.Keyword);
        }

        protected IQueryable<ContentSearchResult> FilterByKeyword(IQueryable<ContentSearchResult> searchQueryable, string keyword)
        {
            Assert.ArgumentNotNull(searchQueryable, nameof(searchQueryable));
            Assert.ArgumentNotNull(keyword, nameof(keyword));

            return !string.IsNullOrWhiteSpace(keyword)
                ? searchQueryable.Where(result => result.Content.Contains(keyword))
                : searchQueryable;
        }
    }
}