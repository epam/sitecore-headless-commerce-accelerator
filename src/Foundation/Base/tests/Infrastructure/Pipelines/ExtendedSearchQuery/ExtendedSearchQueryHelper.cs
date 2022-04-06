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

namespace HCA.Foundation.Base.Tests.Infrastructure.Pipelines.ExtendedSearchQuery
{
    using GraphQL.Types;
    using HCA.Foundation.Base.Models.ExtendedSearchQuery;

    using System.Linq;
    using Sitecore.Data;
    using Sitecore.Services.GraphQL.Content.GraphTypes.ContentSearch;

    public static class ExtendedSearchQueryHelper
    {
        public static ExtendedSearchPipelineArgs GeneratePipelineArgs(IQueryable<ContentSearchResult> queryable, ResolveFieldContext context, Database db)
        {
            return new ExtendedSearchPipelineArgs()
            {
                SearchQueryable = queryable,
                FieldContext = context,
                Database = db
            };
        }
    }
}