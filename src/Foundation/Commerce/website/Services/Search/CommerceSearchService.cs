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

        private readonly ISearchOptionsConverter searchOptionsConverter;

        private readonly ISearchResultsConverter searchResultsConverter;

        public CommerceSearchService(
            ISearchOptionsConverter searchOptionsConverter,
            IProductSearchService productSearchService,
            ICategorySearchService categorySearchService,
            ISearchResultsConverter searchResultsConverter)
        {
            Assert.ArgumentNotNull(searchOptionsConverter, nameof(searchOptionsConverter));
            Assert.ArgumentNotNull(productSearchService, nameof(productSearchService));
            Assert.ArgumentNotNull(categorySearchService, nameof(categorySearchService));
            Assert.ArgumentNotNull(searchResultsConverter, nameof(searchResultsConverter));
            
            this.searchOptionsConverter = searchOptionsConverter;
            this.productSearchService = productSearchService;
            this.categorySearchService = categorySearchService;
            this.searchResultsConverter = searchResultsConverter;
        }
        
        public Result<ProductSearchResults> GetProducts(Models.Entities.Search.ProductSearchOptions productSearchOptions)
        {
            Assert.ArgumentNotNull(productSearchOptions, nameof(productSearchOptions));
            
            var searchOptions = this.searchOptionsConverter.Convert(productSearchOptions);
            var searchResults = this.productSearchService.GetSearchResults(searchOptions);

            return new Result<ProductSearchResults>(this.searchResultsConverter.Convert(searchResults));
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