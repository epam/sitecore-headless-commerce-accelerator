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
    using System.Collections.Generic;
    using System.Linq;

    using Models.ExtendedSearchQuery;

    using Sitecore.ContentSearch.Linq;
    using Sitecore.Diagnostics;
    using Sitecore.Services.Core.Extensions;
    using Sitecore.Services.GraphQL.Content.GraphTypes.ContentSearch;

    public class FacetOnPipelineProcessor : PipelineProcessor<ExtendedSearchPipelineArgs>
    {
        protected IEnumerable<string> Facets = Enumerable.Empty<string>();

        public override void Process(ExtendedSearchPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            this.Facets = args.FieldContext.GetArgument("facetOn", this.Facets);

            args.SearchQueryable = this.FacetOn(args.SearchQueryable, this.Facets);
        }

        protected IQueryable<ContentSearchResult> FacetOn(IQueryable<ContentSearchResult> searchQueryable, IEnumerable<string> facets)
        {
            Assert.ArgumentNotNull(searchQueryable, nameof(searchQueryable));
            Assert.ArgumentNotNull(facets, nameof(facets));

            var queryable = searchQueryable;

            facets.ForEach(facet => queryable = queryable.FacetOn(result => result[facet]));

            return queryable;
        }
    }
}