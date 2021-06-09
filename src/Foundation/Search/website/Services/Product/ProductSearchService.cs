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

namespace HCA.Foundation.Search.Services.Product
{
    using Builders.Product;

    using DependencyInjection;

    using Mappers;

    using Models.Common;
    using Models.Entities.Product;

    using Providers;

    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    [Service(typeof(IProductSearchService), Lifetime = Lifetime.Singleton)]
    public class ProductSearchService : IProductSearchService
    {
        private readonly IProductSearchQueryBuilder queryBuilder;

        private readonly ISearchMapper searchMapper;

        private readonly ISearchResponseProvider searchResponseProvider;

        private readonly ISearchResultProvider searchResultProvider;

        public ProductSearchService(
            ISearchMapper searchMapper,
            ISearchResponseProvider searchResponseProvider,
            ISearchResultProvider searchResultProvider,
            IProductSearchQueryBuilder queryBuilder)
        {
            Assert.ArgumentNotNull(searchMapper, nameof(searchMapper));
            Assert.ArgumentNotNull(searchResponseProvider, nameof(searchResponseProvider));
            Assert.ArgumentNotNull(searchResponseProvider, nameof(searchResponseProvider));
            Assert.ArgumentNotNull(queryBuilder, nameof(queryBuilder));

            this.searchMapper = searchMapper;
            this.searchResponseProvider = searchResponseProvider;
            this.searchResultProvider = searchResultProvider;
            this.queryBuilder = queryBuilder;
        }

        public SearchResults<Item> GetSearchResults(ProductSearchOptions options)
        {
            Assert.ArgumentNotNull(options, nameof(options));

            var commerceSearchOptions = this.searchMapper.Map<ProductSearchOptions, CommerceSearchOptions>(options);

            var results =
                this.searchResultProvider.GetSearchResults<CommerceSellableItemSearchResultItem>(
                    queryable => this.queryBuilder.BuildProductQuery(
                        queryable,
                        options,
                        commerceSearchOptions));

            var searchResponse =
                this.searchResponseProvider.CreateFromSearchResultsItems(commerceSearchOptions, results);
            var searchResults =
                this.searchMapper.Map<SearchResponse, SearchResults<Item>>(searchResponse);

            return searchResults;
        }
        
        public Item GetProductItemByProductId(string productId)
        {
            Assert.ArgumentNotNullOrEmpty(productId, nameof(productId));

            var result = this.searchResultProvider.GetSearchResult<CommerceSellableItemSearchResultItem>(
                queryable => this.queryBuilder.BuildProductQuery(
                    queryable,
                    new ProductSearchOptions
                    {
                        SearchKeyword = productId,
                        NumberOfItemsToReturn = 1
                    }));

            return result?.GetItem();
        }
    }
}