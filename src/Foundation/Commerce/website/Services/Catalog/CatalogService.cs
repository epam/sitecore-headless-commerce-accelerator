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
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models.Result;

    using DependencyInjection;

    using Models.Entities.Catalog;
    using Models.Entities.Search;

    using Search;

    using Sitecore.Diagnostics;

    [Service(typeof(ICatalogService), Lifetime = Lifetime.Transient)]
    public class CatalogService : ICatalogService
    {
        private readonly IProductSearchService productSearchService;

        public CatalogService(IProductSearchService productSearchService)
        {
            Assert.ArgumentNotNull(productSearchService, nameof(productSearchService));
            this.productSearchService = productSearchService;
        }

        public Result<Product> GetProduct(string productId)
        {
            var result = this.productSearchService.GetProducts(
                new ProductSearchOptions
                {
                    PageSize = 1,
                    SearchKeyword = productId
                });
            if (result == null || !result.Success || result.Data == null)
            {
                return new Result<Product>(
                    new Product(),
                    new List<string>
                    {
                        "Product Not Found."
                    })
                {
                    Success = false
                };
            }

            return new Result<Product>(result.Data.Products?.FirstOrDefault());
        }

        public Result<Category> GetCategory(string categoryName)
        {
            //TODO: Return categorySearchResults from search service instead
            return this.productSearchService.GetCategoryByName(categoryName);
        }
    }
}
