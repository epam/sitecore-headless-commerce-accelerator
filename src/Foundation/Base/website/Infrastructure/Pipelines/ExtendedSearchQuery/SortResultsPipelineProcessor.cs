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

    public class SortResultsPipelineProcessor : PipelineProcessor<ExtendedSearchPipelineArgs>
    {
        protected string SortBy { get; set; } = String.Empty;

        protected bool SortDesc { get; set; } = true;

        public override void Process(ExtendedSearchPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            this.SortBy = args.FieldContext.GetArgument("sortBy", this.SortBy);
            this.SortDesc = args.FieldContext.GetArgument("sortDesc", this.SortDesc);

            args.SearchQueryable = this.SortResults(args.SearchQueryable, this.SortBy, this.SortDesc);
        }

        protected IQueryable<ContentSearchResult> SortResults(IQueryable<ContentSearchResult> searchQueryable, string sortBy, bool sortDesc)
        {
            Assert.ArgumentNotNull(searchQueryable, nameof(searchQueryable));
            Assert.ArgumentNotNull(sortBy, nameof(sortBy));
            Assert.ArgumentNotNull(sortDesc, nameof(sortDesc));

            var queryable = searchQueryable;

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                queryable = sortDesc
                    ? searchQueryable.OrderByDescending(result => result[sortBy])
                    : searchQueryable.OrderBy(result => result[sortBy]);
            }

            return queryable;
        }
    }
}