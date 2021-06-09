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

namespace HCA.Foundation.Commerce.Services.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models.Result;

    using Builders.Search;

    using Connect.Builders.Products;
    using Connect.Context.Catalog;

    using DependencyInjection;

    using Foundation.Search.Models.Common;
    using Foundation.Search.Models.Entities.Category;
    using Foundation.Search.Providers;
    using Foundation.Search.Services.Category;
    using Foundation.Search.Services.Product;

    using Mappers.Search;

    using Models.Entities.Catalog;
    using Models.Entities.Search;

    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    using ProductSearchOptions = Foundation.Search.Models.Entities.Product.ProductSearchOptions;

    [Service(typeof(ICommerceSearchService), Lifetime = Lifetime.Singleton)]
    public class CommerceSearchService : ICommerceSearchService
    {
        private readonly IProductSearchService productSearchService;

        private readonly ICategorySearchService categorySearchService;

        private readonly ISearchMapper searchMapper;

        private readonly ISearchOptionsBuilder searchOptionsBuilder;

        private readonly ISearchSettingsProvider searchSettingsProvider;

        private readonly ICatalogContext catalogContext;

        private readonly IProductBuilder<Item> productBuilder;

        public CommerceSearchService(
            ISearchSettingsProvider searchSettingsProvider,
            ICatalogContext catalogContext,
            ISearchOptionsBuilder searchOptionsBuilder,
            IProductSearchService productSearchService,
            ICategorySearchService categorySearchService,
            ISearchMapper searchMapper,
            IProductBuilder<Item> productBuilder)
        {
            Assert.ArgumentNotNull(searchSettingsProvider, nameof(searchSettingsProvider));
            Assert.ArgumentNotNull(catalogContext, nameof(catalogContext));
            Assert.ArgumentNotNull(searchOptionsBuilder, nameof(searchOptionsBuilder));
            Assert.ArgumentNotNull(productSearchService, nameof(productSearchService));
            Assert.ArgumentNotNull(categorySearchService, nameof(categorySearchService));
            Assert.ArgumentNotNull(searchMapper, nameof(searchMapper));
            Assert.ArgumentNotNull(productBuilder, nameof(productBuilder));

            this.searchSettingsProvider = searchSettingsProvider;
            this.catalogContext = catalogContext;
            this.searchOptionsBuilder = searchOptionsBuilder;
            this.productSearchService = productSearchService;
            this.categorySearchService = categorySearchService;
            this.searchMapper = searchMapper;
            this.productBuilder = productBuilder;
        }

        public Result<ProductSearchResults> GetProducts(Models.Entities.Search.ProductSearchOptions productSearchOptions)
        {
            Assert.ArgumentNotNull(productSearchOptions, nameof(productSearchOptions));

            var catalog = productSearchOptions.CategoryId == Guid.Empty
                ? this.catalogContext.CatalogItem
                : Sitecore.Context.Database.GetItem(new ID(productSearchOptions.CategoryId));
            
            var searchSettings = this.searchSettingsProvider.GetSearchSettings(catalog);
            var searchOptions = this.searchOptionsBuilder.Build(searchSettings, productSearchOptions);
            var searchResults = this.productSearchService.GetSearchResults(searchOptions);

            var results = new Result<ProductSearchResults>(
                this.searchMapper.Map<SearchResults<Item>, ProductSearchResults>(searchResults))
            {
                Data =
                {
                    Products = this.searchMapper.Map<List<Connect.Models.Catalog.Product>, List<Product>>(
                        this.productBuilder.Build(searchResults.Results, true).ToList())
                }
            };


            return results;
        }

        public Item GetCategoryByName(string categoryName)
        {
            Assert.ArgumentNotNullOrEmpty(categoryName, nameof(categoryName));

            var searchResults = this.categorySearchService.GetSearchResults(
                new CategorySearchOptions
                {
                    CategoryName = categoryName
                });

            return searchResults?.Results?.FirstOrDefault()?.GetItem();
        }

        public Item GetProductByName(string productName)
        {
            Assert.ArgumentNotNullOrEmpty(productName, nameof(productName));

            var searchResults = this.productSearchService.GetProductItemByProductId(productName);

            return searchResults;
        }
    }
}