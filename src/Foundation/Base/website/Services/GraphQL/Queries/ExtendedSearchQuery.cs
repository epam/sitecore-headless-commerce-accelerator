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

namespace HCA.Foundation.Base.Services.GraphQL.Queries
{
    using Extensions;

    using global::GraphQL.Types;

    using Models.ExtendedSearchQuery;

    using Pipeline;

    using Sitecore.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.ContentSearch;
    using Sitecore.Diagnostics;
    using Sitecore.Services.GraphQL.Content.GraphTypes.ContentSearch;
    using Sitecore.Services.GraphQL.Content.Queries;

    public class ExtendedSearchQuery : SearchQuery
    {
        protected readonly IPipelineService PipelineService;

        public ExtendedSearchQuery()
        {
            this.AddSortArguments();
            this.AddFieldsIncludeArguments();

            this.PipelineService = ServiceLocator.ServiceProvider.GetService<IPipelineService>();
        }

        protected ISearchIndex Index { get; set; }

        protected int After { get; set; }

        protected virtual void Initialize(ResolveFieldContext fieldContext)
        {
            Assert.ArgumentNotNull(fieldContext, nameof(fieldContext));

            var defaultIndexName = $"sitecore_{this.Database.Name}_index";
            var indexName = fieldContext.GetArgument("index", defaultIndexName);
            this.Index = ContentSearchManager.GetIndex(indexName);
        }

        protected override ContentSearchResults Resolve(ResolveFieldContext fieldContext)
        {
            Assert.ArgumentNotNull(fieldContext, nameof(fieldContext));

            this.Initialize(fieldContext);

            if (this.Index == null)
            {
                return new ContentSearchResults(null, 0);
            }

            using (var searchContext = this.Index.CreateSearchContext())
            {
                var searchQueryable = searchContext.GetQueryable<ContentSearchResult>();

                var args = new ExtendedSearchPipelineArgs
                {
                    SearchQueryable = searchQueryable,
                    Database = this.Database,
                    FieldContext = fieldContext
                };

                this.PipelineService.RunPipeline(HCA.Foundation.Base.Constants.Pipelines.ExtendedSearchQuery, args);

                return new ContentSearchResults(args.SearchQueryable.PaginateAndFinalizeResults(fieldContext), this.After);
            }
        }

        private void AddFieldsIncludeArguments()
        {
            var fieldsInclude = new QueryArgument<ListGraphType<ItemSearchFieldQueryValueGraphType>>
            {
                Name = "fieldsInclude",
                Description = "The same as fieldsEqual but uses OR instead of AND."
            };

            this.Arguments.Add(fieldsInclude);
        }

        private void AddSortArguments()
        {
            var sortBy = new QueryArgument<StringGraphType>
            {
                Name = "sortBy",
                Description = "Field name to sort by."
            };
            var sortDesc = new QueryArgument<BooleanGraphType>
            {
                Name = "sortDesc",
                Description = "Sort descending or ascending."
            };

            this.Arguments.Add(sortBy);
            this.Arguments.Add(sortDesc);
        }
    }
}