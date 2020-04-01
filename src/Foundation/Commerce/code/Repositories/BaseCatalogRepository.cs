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

namespace Wooli.Foundation.Commerce.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Connect.Context;
    using Connect.Managers;
    using Connect.Models;
    using Connect.Models.Catalog;

    using Context;

    using Glass.Mapper.Sc;

    using Models.Catalog;

    using Providers;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    using ProductModel = Models.Catalog.ProductModel;

    public class BaseCatalogRepository
    {
        public const string CurrentCatalogItemRenderingModelKey = "CurrentCatalogItemRenderingModel";

        public BaseCatalogRepository(
            ICurrencyProvider currencyProvider,
            ISiteContext siteContext,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext,
            ICatalogManager catalogManager,
            ISitecoreService sitecoreService)
        {
            this.CurrencyProvider = currencyProvider;
            this.SiteContext = siteContext;
            this.StorefrontContext = storefrontContext;
            this.VisitorContext = visitorContext;
            this.CatalogManager = catalogManager;
            this.SitecoreService = sitecoreService;
        }

        public ICatalogManager CatalogManager { get; }

        public ICurrencyProvider CurrencyProvider { get; }

        public ISiteContext SiteContext { get; }

        public ISitecoreService SitecoreService { get; }

        public IStorefrontContext StorefrontContext { get; }

        public IVisitorContext VisitorContext { get; }

        protected CategoryModel GetCategoryModel(Item categoryItem)
        {
            if (categoryItem == null)
            {
                return null;
            }

            var categoryModel = new CategoryModel(categoryItem);

            return categoryModel;
        }

        protected virtual ProductModel GetProductModel(IVisitorContext visitorContext, Item productItem)
        {
            Assert.ArgumentNotNull(visitorContext, nameof(visitorContext));

            if (productItem == null)
            {
                return null;
            }

            var variantEntityList = new List<Variant>();
            if (productItem.HasChildren)
            {
                variantEntityList = this.LoadVariants(productItem);
            }

            var product = new Product(productItem);
            product.Variants = variantEntityList;
            product.CatalogName = this.StorefrontContext.CatalogName;

            product.CustomerAverageRating = this.CatalogManager.GetProductRating(productItem);

            this.CatalogManager.GetProductPrice(product);
            this.CatalogManager.GetStockInfo(product, this.StorefrontContext.ShopName);

            var renderingModel = new ProductModel(productItem);

            renderingModel.CurrencySymbol = this.CurrencyProvider.GetCurrencySymbolByCode(product.CurrencyCode);
            renderingModel.ListPrice = product.ListPrice;
            renderingModel.AdjustedPrice = product.AdjustedPrice;
            renderingModel.StockStatusName = product.StockStatusName;
            renderingModel.CustomerAverageRating = product.CustomerAverageRating;

            foreach (var renderingModelVariant in renderingModel.Variants)
            {
                var variant =
                    product.Variants.FirstOrDefault(x => x.Id == renderingModelVariant.ProductVariantId);
                if (variant == null)
                {
                    continue;
                }

                renderingModelVariant.CurrencySymbol =
                    this.CurrencyProvider.GetCurrencySymbolByCode(variant.CurrencyCode);
                renderingModelVariant.ListPrice = variant.ListPrice;
                renderingModelVariant.AdjustedPrice = variant.AdjustedPrice;
                renderingModelVariant.StockStatusName = variant.StockStatusName;
                renderingModelVariant.CustomerAverageRating = variant.CustomerAverageRating;
            }

            return renderingModel;
        }

        private List<Variant> LoadVariants(Item productItem)
        {
            var variants = new List<Variant>();
            foreach (Item variantItem in productItem.Children)
            {
                var variantEntity = new Variant(variantItem);
                variantEntity.CustomerAverageRating = this.CatalogManager.GetProductRating(variantItem);

                variants.Add(variantEntity);
            }

            return variants;
        }
    }
}