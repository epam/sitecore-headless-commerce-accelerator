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

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Prices;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    [Service(typeof(IProductBuilder<Item, Product>), Lifetime = Lifetime.Singleton)]
    public class ProductBuilder : IProductBuilder<Item, Product>
    {
        private readonly IStorefrontContext storefrontContext;
        private readonly IPricingManager pricingManager;

        public ProductBuilder(
            IStorefrontContext storefrontContext,
            IPricingManager pricingManager)
        {
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));
            Assert.ArgumentNotNull(pricingManager, nameof(pricingManager));

            this.storefrontContext = storefrontContext;
            this.pricingManager = pricingManager;
        }

        public IEnumerable<Product> Build(IEnumerable<Item> source)
        {
            var products = source.Select(this.GetProduct).ToList();
            this.SetPrices(products);
            //TODO: this.SetStockStatus(products);

            return products;
        }
        
        public Product Build(Item source)
        {
            var product = this.GetProduct(source);
            this.SetPrices(product);
            //TODO: this.SetStockStatus(product);

            return product;
        }

        private Product GetProduct(Item source)
        {
            var variants = new List<Variant>();
            if (source.HasChildren)
            {
                variants = this.GetVariants(source).ToList();
            }
            var product = new Product(source, variants)
            {
                CatalogName = this.storefrontContext.CatalogName,
                CustomerAverageRating = this.GetProductRating(source)
            };

            return product;
        }

        private IEnumerable<Variant> GetVariants(Item item)
        {
            return item.Children.Select(
                variant => new Variant(variant)
                {
                    CatalogName = this.storefrontContext.CatalogName,
                    CustomerAverageRating = this.GetProductRating(variant)
                });
        }

        private decimal? GetProductRating(Item productItem)
        {
            return decimal.TryParse(productItem["Rating"], out var rating) ? (decimal?)rating : null;
        }

        private void SetPrices(Product product)
        {
            var includeVariants = product.Variants != null && product.Variants.Count > 0;
            var productPrices = this.pricingManager.GetProductPrices(
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

        private void SetPrices(IList<Product> products)
        {
            var productsPrices = this.pricingManager.GetProductBulkPrices(
                    products.Select(_ => _.CatalogName).FirstOrDefault(),
                    products.Select(_ => _.Id),
                    null)
                ?.Result;
            foreach (var product in products)
            {
                this.SetPrices(product, productsPrices);
            }
        }

        private void SetPrices<TProduct>(TProduct product, IDictionary<string, Price> productsPrices)
            where TProduct : BaseProduct
        {
            if (productsPrices == null || !productsPrices.Any() ||
                !productsPrices.TryGetValue(product.Id, out var price))
            {
                return;
            }

            var commercePrice = price as CommercePrice;
            product.CurrencyCode = price.CurrencyCode;
            product.ListPrice = commercePrice?.Amount;
            product.AdjustedPrice = commercePrice?.ListPrice;
        }
    }
}