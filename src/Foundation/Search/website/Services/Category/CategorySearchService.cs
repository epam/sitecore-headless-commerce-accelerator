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

namespace HCA.Foundation.Search.Services.Category
{
    using Builders.Category;

    using DependencyInjection;

    using Mappers;

    using Models.Common;
    using Models.Entities.Category;

    using Providers;

    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Diagnostics;

    [Service(typeof(ICategorySearchService), Lifetime = Lifetime.Singleton)]
    public class CategorySearchService : ICategorySearchService
    {
        private readonly ICategorySearchQueryBuilder queryBuilder;

        private readonly ISearchMapper searchMapper;

        private readonly ISearchResultProvider searchResultProvider;

        public CategorySearchService(
            ISearchMapper searchMapper,
            ISearchResultProvider searchResultProvider,
            ICategorySearchQueryBuilder queryBuilder)
        {
            Assert.ArgumentNotNull(searchMapper, nameof(searchMapper));
            Assert.ArgumentNotNull(searchResultProvider, nameof(searchResultProvider));
            Assert.ArgumentNotNull(queryBuilder, nameof(queryBuilder));

            this.searchMapper = searchMapper;
            this.searchResultProvider = searchResultProvider;
            this.queryBuilder = queryBuilder;
        }

        public SearchResults<CategorySearchResultItem> GetSearchResults(CategorySearchOptions options)
        {
            Assert.ArgumentNotNull(options, nameof(options));

            var results = this.searchResultProvider.GetSearchResults<CommerceSellableItemSearchResultItem>(
                queryable => this.queryBuilder.BuildCategoryQuery(queryable, options.CategoryName));

            return this.searchMapper
                .Map<Sitecore.ContentSearch.Linq.SearchResults<CommerceSellableItemSearchResultItem>,
                    SearchResults<CategorySearchResultItem>>(results);
        }
    }
}