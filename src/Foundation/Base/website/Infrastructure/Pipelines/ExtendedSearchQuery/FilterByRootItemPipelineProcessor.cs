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

    using Sitecore.Data;
    using Sitecore.Diagnostics;
    using Sitecore.Services.GraphQL.Content.GraphTypes;
    using Sitecore.Services.GraphQL.Content.GraphTypes.ContentSearch;

    public class FilterByRootItemPipelineProcessor : PipelineProcessor<ExtendedSearchPipelineArgs>
    {
        protected string RootItemPath { get; set; } = String.Empty;

        public override void Process(ExtendedSearchPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            this.RootItemPath = args.FieldContext.GetArgument("rootItem", this.RootItemPath);

            args.SearchQueryable = this.FilterByRootItem(args.SearchQueryable, this.RootItemPath, args.Database);
        }

        protected IQueryable<ContentSearchResult> FilterByRootItem(IQueryable<ContentSearchResult> searchQueryable, string rootItemPath, Database database)
        {
            Assert.ArgumentNotNull(searchQueryable, nameof(searchQueryable));
            Assert.ArgumentNotNull(rootItemPath, nameof(rootItemPath));
            Assert.ArgumentNotNull(database, nameof(database));

            return !string.IsNullOrWhiteSpace(rootItemPath)
                && IdHelper.TryResolveItem(database, rootItemPath, out var rootItem)
                    ? searchQueryable.Where(result => result.AncestorIDs.Contains(rootItem.ID))
                    : searchQueryable;
        }
    }
}