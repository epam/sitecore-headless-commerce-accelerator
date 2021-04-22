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
    using System.Collections.Generic;
    using System.Linq;

    using Extensions;

    using global::GraphQL.Types;

    using Sitecore;
    using Sitecore.ContentSearch;
    using Sitecore.Data.Items;
    using Sitecore.Data.Managers;
    using Sitecore.Diagnostics;
    using Sitecore.Services.GraphQL.Content.GraphTypes.ContentSearch;
    using Sitecore.Services.GraphQL.Content.Queries;

    public class ExtendedSearchQuery : SearchQuery
    {
        protected IEnumerable<string> Facets = Enumerable.Empty<string>();

        protected IEnumerable<Dictionary<string, object>> FieldsAnd = Enumerable.Empty<Dictionary<string, object>>();

        protected IEnumerable<Dictionary<string, object>> FieldsOr = Enumerable.Empty<Dictionary<string, object>>();

        public ExtendedSearchQuery()
        {
            this.AddSortArguments();
            this.AddFieldsIncludeArguments();
        }

        protected string RootItemPath { get; set; } = Context.Site.RootPath;

        protected Item RootItem => this.Database.GetItem(this.RootItemPath);

        protected ISearchIndex Index { get; set; }

        protected string Lang { get; set; } = Context.Language.Name ?? LanguageManager.DefaultLanguage.Name;

        protected bool Version { get; set; } = true;

        protected string Keyword { get; set; }

        protected int After { get; set; }

        protected string SortBy { get; set; }

        protected bool SortDesc { get; set; }

        protected virtual void Initialize(ResolveFieldContext fieldContext)
        {
            Assert.ArgumentNotNull(fieldContext, nameof(fieldContext));

            this.RootItemPath = fieldContext.GetArgument("rootItem", this.RootItemPath);
            this.Keyword = fieldContext.GetArgument("keyword", this.Keyword);
            this.Lang = fieldContext.GetArgument("language", this.Lang);
            this.Version = fieldContext.GetArgument("latestVersion", this.Version);

            this.Facets = fieldContext.GetArgument("facetOn", this.Facets);
            this.FieldsAnd = fieldContext.GetArgument("fieldsEqual", this.FieldsAnd);
            this.FieldsOr = fieldContext.GetArgument("fieldsInclude", this.FieldsAnd);

            this.SortBy = fieldContext.GetArgument("sortBy", this.SortBy);
            this.SortDesc = fieldContext.GetArgument("sortDesc", this.SortDesc);

            this.After = fieldContext.GetArgument("after", this.After);

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

                searchQueryable = searchQueryable.FilterByRootItem(this.RootItemPath, this.Database);
                searchQueryable = searchQueryable.FilterByKeyword(this.Keyword);
                searchQueryable = searchQueryable.FilterByLanguage(this.Lang);
                searchQueryable = searchQueryable.FilterByVersion(this.Version);
                searchQueryable = searchQueryable.FacetOn(this.Facets);
                searchQueryable = searchQueryable.FilterByFields(this.FieldsAnd, this.FieldsOr);
                searchQueryable = searchQueryable.SortResults(this.SortBy, this.SortDesc);

                return new ContentSearchResults(searchQueryable.PaginateAndFinalizeResults(fieldContext), this.After);
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