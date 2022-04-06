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
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Models.ExtendedSearchQuery;

    using Sitecore.ContentSearch.Linq.Utilities;
    using Sitecore.Diagnostics;
    using Sitecore.Services.Core.Extensions;
    using Sitecore.Services.GraphQL.Content.GraphTypes.ContentSearch;

    public class FilterByFieldsPipelineProcessor : PipelineProcessor<ExtendedSearchPipelineArgs>
    {
        protected IEnumerable<Dictionary<string, object>> FieldsAnd = Enumerable.Empty<Dictionary<string, object>>();

        protected IEnumerable<Dictionary<string, object>> FieldsOr = Enumerable.Empty<Dictionary<string, object>>();

        public override void Process(ExtendedSearchPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            this.FieldsAnd = args.FieldContext.GetArgument("fieldsEqual", this.FieldsAnd);
            this.FieldsOr = args.FieldContext.GetArgument("fieldsInclude", this.FieldsOr);

            args.SearchQueryable = this.FilterByFields(args.SearchQueryable, this.FieldsAnd, this.FieldsOr);
        }

        protected IQueryable<ContentSearchResult> FilterByFields(
            IQueryable<ContentSearchResult> searchQueryable,
            IEnumerable<Dictionary<string, object>> fieldsAnd,
            IEnumerable<Dictionary<string, object>> fieldsOr)
        {
            Assert.ArgumentNotNull(searchQueryable, nameof(searchQueryable));
            Assert.ArgumentNotNull(fieldsAnd, nameof(fieldsAnd));
            Assert.ArgumentNotNull(fieldsOr, nameof(fieldsOr));

            var predicateAnd = this.CreatePredicateFieldsAnd(fieldsAnd);
            var predicateOr = this.CreatePredicateFieldsOr(fieldsOr);
            var predicate = predicateAnd.And(predicateOr);

            return searchQueryable.Where(predicate);
        }

        protected Expression<Func<ContentSearchResult, bool>> CreatePredicateFieldsAnd(IEnumerable<Dictionary<string, object>> fields)
        {
            Assert.ArgumentNotNull(fields, nameof(fields));

            var predicate = PredicateBuilder.True<ContentSearchResult>();

            fields.ForEach(
                field =>
                {
                    var name = field["name"].ToString();
                    var value = field["value"].ToString();

                    predicate = predicate.And(result => result[name] == value);
                });

            return predicate;
        }

        protected Expression<Func<ContentSearchResult, bool>> CreatePredicateFieldsOr(IEnumerable<Dictionary<string, object>> fields)
        {
            Assert.ArgumentNotNull(fields, nameof(fields));

            var predicate = PredicateBuilder.True<ContentSearchResult>();

            fields?.ForEach(
                field =>
                {
                    var name = field["name"].ToString();
                    var value = field["value"].ToString();

                    predicate = predicate.Or(result => result[name] == value);
                });

            return predicate;
        }
    }
}