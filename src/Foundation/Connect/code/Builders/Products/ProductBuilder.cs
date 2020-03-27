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

namespace Wooli.Foundation.Connect.Builders.Products
{
    using System.Collections.Generic;
    using System.Linq;

    using Context;

    using DependencyInjection;

    using Managers;

    using Models;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    [Service(typeof(IProductBuilder<Item, Product>), Lifetime = Lifetime.Singleton)]
    public class ProductBuilder : IProductBuilder<Item, Product>
    {
        private readonly IStorefrontContext storefrontContext;
        private readonly ICatalogManager catalogManager;

        public ProductBuilder(IStorefrontContext storefrontContext, ICatalogManager catalogManager)
        {
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));
            Assert.ArgumentNotNull(catalogManager, nameof(catalogManager));

            this.storefrontContext = storefrontContext;
            this.catalogManager = catalogManager;
        }

        //TODO: update build logic
        public IEnumerable<Product> Build(IEnumerable<Item> source)
        {
            var products = new List<Product>();
            products.AddRange(
                source.Select(
                    item => new Product(item, new List<Variant>())
                    {
                        CatalogName = this.storefrontContext.CatalogName,
                        CustomerAverageRating = this.catalogManager.GetProductRating(item)
                    }));

            this.catalogManager.GetProductBulkPrices(products);
            return products;
        }
    }
}