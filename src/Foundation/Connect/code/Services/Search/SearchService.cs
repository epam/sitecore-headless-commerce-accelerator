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

namespace HCA.Foundation.Connect.Services.Search
{
    using System.Linq;

    using Builders.Products;
    using Builders.Search;

    using DependencyInjection;

    using Mappers.Search;

    using Models.Catalog;
    using Models.Search;

    using Providers.Search;

    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    [Service(typeof(ISearchService), Lifetime = Lifetime.Singleton)]
    public class SearchService : ISearchService
    {
        private readonly ISearchQueryBuilder queryBuilder;

        private readonly ISearchMapper searchMapper;

        private readonly ISearchResponseProvider searchResponseProvider;

        private readonly ISearchResultProvider searchResultProvider;

        private readonly IProductBuilder<Item> productBuilder;

        public SearchService(
            ISearchMapper searchMapper,
            ISearchResponseProvider searchResponseProvider,
            IProductBuilder<Item> productBuilder,
            ISearchResultProvider searchResultProvider,
            ISearchQueryBuilder queryBuilder)
        {
            Assert.ArgumentNotNull(searchMapper, nameof(searchMapper));
            Assert.ArgumentNotNull(searchResponseProvider, nameof(searchResponseProvider));
            Assert.ArgumentNotNull(searchResponseProvider, nameof(searchResponseProvider));
            Assert.ArgumentNotNull(productBuilder, nameof(productBuilder));
            Assert.ArgumentNotNull(queryBuilder, nameof(queryBuilder));

            this.searchMapper = searchMapper;
            this.searchResponseProvider = searchResponseProvider;
            this.searchResultProvider = searchResultProvider;
            this.productBuilder = productBuilder;
            this.queryBuilder = queryBuilder;
        }

        public SearchResults<Product> GetProducts(SearchOptions searchOptions)
        {
            Assert.ArgumentNotNull(searchOptions, nameof(searchOptions));

            var commerceSearchOptions = this.searchMapper.Map<SearchOptions, CommerceSearchOptions>(searchOptions);

            var results =
                this.searchResultProvider.GetSearchResults<CommerceSellableItemSearchResultItem>(
                    queryable => this.queryBuilder.BuildProductsQuery(
                        queryable,
                        searchOptions.SearchKeyword,
                        ID.Parse(searchOptions.CategoryId),
                        commerceSearchOptions));

            var searchResponse =
                this.searchResponseProvider.CreateFromSearchResultsItems(commerceSearchOptions, results);
            var searchResults = this.searchMapper.Map<SearchResponse, SearchResults<Product>>(searchResponse);
            searchResults.Results = this.productBuilder.BuildWithoutVariants(searchResponse.ResponseItems).ToList();

            return searchResults;
        }

        public Item GetCategoryItem(string categoryName)
        {
            Assert.ArgumentNotNullOrEmpty(categoryName, nameof(categoryName));

            var result = this.searchResultProvider.GetSearchResult<CommerceSellableItemSearchResultItem>(
                queryable => this.queryBuilder.BuildCategoryQuery(queryable, categoryName));
            return result?.GetItem();
        }

        public Item GetProductItem(string productId)
        {
            Assert.ArgumentNotNullOrEmpty(productId, nameof(productId));

            var result = this.searchResultProvider.GetSearchResult<CommerceSellableItemSearchResultItem>(
                queryable => this.queryBuilder.BuildProductQuery(queryable, productId));

            return result?.GetItem();
        }
    }
}