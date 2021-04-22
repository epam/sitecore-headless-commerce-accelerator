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

namespace HCA.Foundation.Base.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using GraphQL.Types;

    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.Linq.Utilities;
    using Sitecore.Data;
    using Sitecore.Diagnostics;
    using Sitecore.Globalization;
    using Sitecore.Services.Core.Extensions;
    using Sitecore.Services.GraphQL.Content.GraphTypes;
    using Sitecore.Services.GraphQL.Content.GraphTypes.ContentSearch;
    using Sitecore.Services.GraphQL.GraphTypes.Connections;

    public static class SearchExtensions
    {
        public static Expression<Func<ContentSearchResult, bool>> CreatePredicateFieldsAnd(
            this IEnumerable<Dictionary<string, object>> fields)
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

        public static Expression<Func<ContentSearchResult, bool>> CreatePredicateFieldsOr(
            this IEnumerable<Dictionary<string, object>> fields)
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

        public static IQueryable<ContentSearchResult> FacetOn(
            this IQueryable<ContentSearchResult> searchQueryable,
            IEnumerable<string> facets)
        {
            Assert.ArgumentNotNull(searchQueryable, nameof(searchQueryable));
            Assert.ArgumentNotNull(facets, nameof(facets));

            var queryable = searchQueryable;

            facets.ForEach(facet => queryable = queryable.FacetOn(result => result[facet]));

            return queryable;
        }

        public static IQueryable<ContentSearchResult> FilterByFields(
            this IQueryable<ContentSearchResult> searchQueryable,
            IEnumerable<Dictionary<string, object>> fieldsAnd,
            IEnumerable<Dictionary<string, object>> fieldsOr)
        {
            Assert.ArgumentNotNull(searchQueryable, nameof(searchQueryable));
            Assert.ArgumentNotNull(fieldsAnd, nameof(fieldsAnd));
            Assert.ArgumentNotNull(fieldsOr, nameof(fieldsOr));

            var predicateAnd = fieldsAnd.CreatePredicateFieldsAnd();
            var predicateOr = fieldsOr.CreatePredicateFieldsOr();
            var predicate = predicateAnd.And(predicateOr);

            return searchQueryable.Where(predicate);
        }

        public static IQueryable<ContentSearchResult> FilterByKeyword(
            this IQueryable<ContentSearchResult> searchQueryable,
            string keyword)
        {
            Assert.ArgumentNotNull(searchQueryable, nameof(searchQueryable));
            Assert.ArgumentNotNull(keyword, nameof(keyword));

            return !string.IsNullOrWhiteSpace(keyword)
                ? searchQueryable.Where(result => result.Content.Contains(keyword))
                : searchQueryable;
        }

        public static IQueryable<ContentSearchResult> FilterByLanguage(
            this IQueryable<ContentSearchResult> searchQueryable,
            string lang)
        {
            Assert.ArgumentNotNull(searchQueryable, nameof(searchQueryable));
            Assert.ArgumentNotNull(lang, nameof(lang));

            return Language.TryParse(lang, out var language)
                ? searchQueryable.Where(result => result.Language == language.Name)
                : searchQueryable;
        }

        public static IQueryable<ContentSearchResult> FilterByRootItem(
            this IQueryable<ContentSearchResult> searchQueryable,
            string rootItemPath,
            Database database)
        {
            Assert.ArgumentNotNull(searchQueryable, nameof(searchQueryable));
            Assert.ArgumentNotNull(rootItemPath, nameof(rootItemPath));
            Assert.ArgumentNotNull(database, nameof(database));

            return !string.IsNullOrWhiteSpace(rootItemPath)
                && IdHelper.TryResolveItem(database, rootItemPath, out var rootItem)
                    ? searchQueryable.Where(result => result.AncestorIDs.Contains(rootItem.ID))
                    : searchQueryable;
        }

        public static IQueryable<ContentSearchResult> FilterByVersion(
            this IQueryable<ContentSearchResult> searchQueryable,
            bool version)
        {
            Assert.ArgumentNotNull(searchQueryable, nameof(searchQueryable));
            Assert.ArgumentNotNull(version, nameof(version));

            return version ? searchQueryable.Where(result => result.IsLatestVersion) : searchQueryable;
        }

        public static SearchResults<ContentSearchResult> PaginateAndFinalizeResults(
            this IQueryable<ContentSearchResult> searchQueryable,
            ResolveFieldContext fieldContext)
        {
            Assert.ArgumentNotNull(searchQueryable, nameof(searchQueryable));
            Assert.ArgumentNotNull(fieldContext, nameof(fieldContext));

            return searchQueryable.ApplyEnumerableConnectionArguments(fieldContext).GetResults();
        }

        public static IQueryable<ContentSearchResult> SortResults(
            this IQueryable<ContentSearchResult> searchQueryable,
            string sortBy,
            bool sortDesc)
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