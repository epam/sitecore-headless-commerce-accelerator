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

    using Context;

    using DependencyInjection;

    using Mappers.Catalog;

    using Models.Entities.Catalog;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;

    using Connect.Converters.Products;

    using Foundation.Search.Models.Entities.Product;
    using Foundation.Search.Services.Product;

    using Search;

    using Connect = Connect.Models.Catalog;
    using IProductSearchService = Search.IProductSearchService;

    [Service(typeof(ICatalogService), Lifetime = Lifetime.Transient)]
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogMapper catalogMapper;
        private readonly IProductSearchService productSearchService;
        private readonly ISiteContext siteContext;
        private readonly IProductConverter<Item> productConverter;

        public CatalogService(
            ISiteContext siteContext,
            ICatalogMapper catalogMapper,
            IProductSearchService productSearchService,
            IProductConverter<Item> productConverter)
        {
            Assert.ArgumentNotNull(siteContext, nameof(siteContext));
            Assert.ArgumentNotNull(catalogMapper, nameof(catalogMapper));
            Assert.ArgumentNotNull(productSearchService, nameof(productSearchService));
            Assert.ArgumentNotNull(productConverter, nameof(productConverter));

            this.siteContext = siteContext;
            this.catalogMapper = catalogMapper;
            this.productSearchService = productSearchService;
            this.productConverter = productConverter;
        }

        public Result<Product> GetProduct(string productId)
        {
            var item = this.productSearchService.GetProductByName(productId);
            if (item == null)
            {
                return new Result<Product>(new Product(), new List<string>() { "Product Not Found." })
                {
                    Success = false
                };
            }

            var product = this.productConverter.Convert(item);
            return new Result<Product>(this.catalogMapper.Map<Connect.Product, Product>(product));
        }

        public Result<Product> GetCurrentProduct()
        {
            var item = this.siteContext.CurrentProductItem;
            if (item == null)
            {
                return new Result<Product>(null);
            }

            var product = this.productConverter.Convert(item);
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