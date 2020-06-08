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

namespace HCA.Foundation.Connect.Builders.Products
{
    using System.Collections.Generic;
    using System.Linq;

    using Context.Catalog;
    using Context.Storefront;

    using DependencyInjection;

    using Managers.Inventory;
    using Managers.Pricing;

    using Mappers.Catalog;

    using Models.Catalog;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Inventory;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    [Service(typeof(IProductBuilder<Item>), Lifetime = Lifetime.Singleton)]
    public class ProductBuilder : BaseProductBuilder, IProductBuilder<Item>
    {
        private static readonly string[] PriceTypes =
        {
            Constants.Pricing.PricingTypes.List, Constants.Pricing.PricingTypes.Adjusted
        };

        private readonly IInventoryManager inventoryManager;
        private readonly IPricingManager pricingManager;
        private readonly IStorefrontContext storefrontContext;
        private readonly IVariantBuilder<Item> variantBuilder;

        public ProductBuilder(
            IVariantBuilder<Item> variantBuilder,
            ICatalogContext catalogContext,
            IPricingManager pricingManager,
            IInventoryManager inventoryManager,
            IStorefrontContext storefrontContext,
            ICatalogMapper catalogMapper) : base(
            catalogContext,
            catalogMapper)
        {
            Assert.ArgumentNotNull(variantBuilder, nameof(variantBuilder));
            Assert.ArgumentNotNull(inventoryManager, nameof(inventoryManager));
            Assert.ArgumentNotNull(pricingManager, nameof(pricingManager));
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));

            this.variantBuilder = variantBuilder;
            this.inventoryManager = inventoryManager;
            this.pricingManager = pricingManager;
            this.storefrontContext = storefrontContext;
        }

        public Product Build(Item source)
        {
            Assert.ArgumentNotNull(source, nameof(source));

            var product = this.InitializeProduct(source, true);
            this.SetPricesWithVariants(product);
            this.SetStockStatus(product);

            return product;
        }

        public IEnumerable<Product> BuildWithoutVariants(IEnumerable<Item> sources)
        {
            Assert.ArgumentNotNull(sources, nameof(sources));

            var products = sources.Select(source => this.InitializeProduct(source, false)).ToList();
            this.SetPricesWithoutVariants(products);
            this.SetStockStatus(products);

            return products;
        }

        private Product InitializeProduct(Item source, bool includeVariants)
        {
            var product = this.Initialize<Product>(source);

            if (includeVariants)
            {
                this.SetVariants(product, source);
            }
            else
            {
                product.Variants = new List<Variant>();
            }

            return product;
        }

        private void SetVariants(Product product, Item source)
        {
            product.Variants = source.HasChildren
                ? this.variantBuilder.Build(source.Children).ToList()
                : new List<Variant>();
        }

        private void SetPricesWithoutVariants(IList<Product> products)
        {
            if (products == null || !products.Any())
            {
                return;
            }

            var catalogName = products.Select(product => product.CatalogName).FirstOrDefault();
            var productIds = products.Select(product => product.Id);
            var prices = this.pricingManager.GetProductBulkPrices(catalogName, productIds, PriceTypes)?.Prices;

            foreach (var product in products)
            {
                this.SetPrices(product, prices);
            }
        }

        private void SetPricesWithVariants(Product product)
        {
            if (product == null)
            {
                return;
            }

            var includeVariants = product.Variants != null && product.Variants.Count > 0;
            var productPrices = this.pricingManager.GetProductPrices(
                    product.CatalogName,
                    product.Id,
                    includeVariants,
                    PriceTypes)
                ?.Prices;

            this.SetPrices(product, productPrices);

            if (includeVariants)
            {
                foreach (var variant in product.Variants)
                {
                    this.SetPrices(variant, productPrices);
                }
            }
        }

        private void SetStockStatus(Product product)
        {
            this.SetStockStatus(
                new List<Product>
                {
                    product
                });
        }

        private void SetStockStatus(IEnumerable<Product> products)
        {
            var productsList = products.ToList();
            var inventoryProducts = this.GetInventoryProducts(productsList);

            var stockInformation = this.GetStockInformation(inventoryProducts);

            if (stockInformation == null)
            {
                return;
            }

            this.SetStockStatus(productsList, stockInformation);
        }

        private IEnumerable<CommerceInventoryProduct> GetInventoryProducts(IEnumerable<Product> products)
        {
            var inventoryProducts = new List<CommerceInventoryProduct>();

            foreach (var product in products)
            {
                this.AddProduct(inventoryProducts, product);
            }

            return inventoryProducts;
        }

        private void AddProduct(List<CommerceInventoryProduct> products, Product product)
        {
            products.Add(this.CatalogMapper.Map<Product, CommerceInventoryProduct>(product));

            foreach (var productVariant in product.Variants)
            {
                products.Add(this.CatalogMapper.Map<Variant, CommerceInventoryProduct>(productVariant));
            }
        }

        private IEnumerable<StockInformation> GetStockInformation(
            IEnumerable<CommerceInventoryProduct> inventoryProducts)
        {
            if (inventoryProducts == null)
            {
                return Enumerable.Empty<StockInformation>();
            }

            var shopName = this.storefrontContext.ShopName;
            return this.inventoryManager
                .GetStockInformation(shopName, inventoryProducts, StockDetailsLevel.StatusAndAvailability)
                ?.StockInformation;
        }

        private void SetStockStatus(List<Product> products, IEnumerable<StockInformation> stockInformation)
        {
            foreach (var information in stockInformation)
            {
                if (information.Product is CommerceInventoryProduct inventoryProduct)
                {
                    var product = products.FirstOrDefault(p => p.Id == inventoryProduct.ProductId);
                    if (product != null)
                    {
                        if (string.IsNullOrEmpty(inventoryProduct.VariantId))
                        {
                            this.SetStockStatus(product, information);
                        }
                        else
                        {
                            var variant = product.Variants.FirstOrDefault(v => v.Id == inventoryProduct.VariantId);

                            this.SetStockStatus(variant, information);
                        }
                    }
                }
            }
        }
    }
}