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

namespace HCA.Foundation.Commerce.Services.Catalog
{
    using Base.Models.Result;

    using Connect.Builders.Products;
    using Connect.Services.Search;

    using Context;

    using DependencyInjection;

    using Mappers.Catalog;

    using Models.Entities.Catalog;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    using Connect = Connect.Models.Catalog;

    [Service(typeof(ICatalogService), Lifetime = Lifetime.Transient)]
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogMapper catalogMapper;
        private readonly ISearchService searchService;
        private readonly ISiteContext siteContext;
        private readonly IProductBuilder<Item> productBuilder;

        public CatalogService(
            ISiteContext siteContext,
            ICatalogMapper catalogMapper,
            ISearchService searchService,
            IProductBuilder<Item> productBuilder)
        {
            Assert.ArgumentNotNull(siteContext, nameof(siteContext));
            Assert.ArgumentNotNull(catalogMapper, nameof(catalogMapper));
            Assert.ArgumentNotNull(searchService, nameof(searchService));
            Assert.ArgumentNotNull(productBuilder, nameof(productBuilder));

            this.siteContext = siteContext;
            this.catalogMapper = catalogMapper;
            this.searchService = searchService;
            this.productBuilder = productBuilder;
        }

        public Result<Product> GetProduct(string productId)
        {
            var item = this.searchService.GetProductItem(productId);
            if (item == null)
            {
                return new Result<Product>(null);
            }

            var product = this.productBuilder.Build(item);
            return new Result<Product>(this.catalogMapper.Map<Connect.Product, Product>(product));
        }

        public Result<Product> GetCurrentProduct()
        {
            var item = this.siteContext.CurrentProductItem;
            if (item == null)
            {
                return new Result<Product>(null);
            }

            var product = this.productBuilder.Build(item);
            return new Result<Product>(this.catalogMapper.Map<Connect.Product, Product>(product));
        }

        public Result<Category> GetCurrentCategory()
        {
            var categoryItem = this.siteContext.CurrentCategoryItem;
            return categoryItem != null
                ? new Result<Category>(this.catalogMapper.Map<Item, Category>(categoryItem))
                : new Result<Category>(null);
        }
    }
}