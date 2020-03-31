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

    using Models.Catalog;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    [Service(typeof(IProductBuilder<Item, Product>), Lifetime = Lifetime.Singleton)]
    public class ProductBuilder : BaseProductBuilder, IProductBuilder<Item, Product>
    {
        private readonly IProductBuilder<Item, Variant> variantBuilder;

        public ProductBuilder(
            IProductBuilder<Item, Variant> variantBuilder,
            IStorefrontContext storefrontContext,
            IPricingManager pricingManager) : base(
            storefrontContext,
            pricingManager)
        {
            Assert.ArgumentNotNull(variantBuilder, nameof(variantBuilder));
            this.variantBuilder = variantBuilder;
        }

        public IEnumerable<Product> Build(IEnumerable<Item> source)
        {
            Assert.ArgumentNotNull(source, nameof(source));

            var products = source.Select(this.GetProductWithoutPrices).ToList();
            this.SetPrices(products);

            return products;
        }

        public Product Build(Item source)
        {
            Assert.ArgumentNotNull(source, nameof(source));

            var product = this.GetProductWithoutPrices(source);
            this.SetPrices(product);

            return product;
        }

        private Product GetProductWithoutPrices(Item source)
        {
            var product = this.BuildWithoutPrices<Product>(source);
            this.SetVariants(product, source);

            return product;
        }

        private void SetVariants(Product product, Item source)
        {
            if (source.HasChildren)
            {
                product.Variants = this.variantBuilder.Build(source.Children).ToList();
            }
        }

        private void SetPrices(Product product)
        {
            if (product == null)
            {
                return;
            }

            var includeVariants = product.Variants != null && product.Variants.Count > 0;
            var productPrices = this.PricingManager.GetProductPrices(
                    product.CatalogName,
                    product.Id,
                    includeVariants,
                    null)
                ?.Result;

            this.SetPrices(product, productPrices);

            if (includeVariants)
            {
                foreach (var variant in product.Variants)
                {
                    this.SetPrices(variant, productPrices);
                }
            }
        }
    }
}