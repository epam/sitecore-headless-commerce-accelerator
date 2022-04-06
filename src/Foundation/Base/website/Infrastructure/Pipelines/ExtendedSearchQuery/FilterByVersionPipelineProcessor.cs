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

    using Sitecore.Diagnostics;
    using Sitecore.Services.GraphQL.Content.GraphTypes.ContentSearch;

    public class FilterByVersionPipelineProcessor : PipelineProcessor<ExtendedSearchPipelineArgs>
    {
        protected bool Version { get; set; } = true;

        public override void Process(ExtendedSearchPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            this.Version = args.FieldContext.GetArgument("latestVersion", this.Version);

            args.SearchQueryable = this.FilterByVersion(args.SearchQueryable, this.Version);
        }

        protected IQueryable<ContentSearchResult> FilterByVersion(IQueryable<ContentSearchResult> searchQueryable, bool version)
        {
            Assert.ArgumentNotNull(searchQueryable, nameof(searchQueryable));
            Assert.ArgumentNotNull(version, nameof(version));

            return version ? searchQueryable.Where(result => result.IsLatestVersion) : searchQueryable;
        }
    }
}