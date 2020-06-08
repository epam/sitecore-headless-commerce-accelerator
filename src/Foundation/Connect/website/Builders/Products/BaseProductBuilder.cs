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

    using Base.Extensions;

    using Context.Catalog;

    using Mappers.Catalog;

    using Models.Catalog;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Inventory;
    using Sitecore.Commerce.Entities.Prices;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    using StockStatus = Sitecore.Commerce.Entities.Inventory.StockStatus;

    /// <summary>
    /// Sets Base Product properties
    /// </summary>
    public abstract class BaseProductBuilder
    {
        protected readonly ICatalogContext CatalogContext;
        protected readonly ICatalogMapper CatalogMapper;

        protected BaseProductBuilder(
            ICatalogContext catalogContext,
            ICatalogMapper catalogMapper)
        {
            Assert.ArgumentNotNull(catalogContext, nameof(catalogContext));
            Assert.ArgumentNotNull(catalogMapper, nameof(catalogMapper));

            this.CatalogContext = catalogContext;
            this.CatalogMapper = catalogMapper;
        }

        /// <summary>
        /// Initialize entity of TProduct from source item and catalog data
        /// </summary>
        /// <param name="source">Item to set properties from</param>
        /// <returns>Entity of TProduct type</returns>
        protected TProduct Initialize<TProduct>(Item source)
            where TProduct : BaseProduct, new()
        {
            var entity = new TProduct();
            this.Initialize(entity, source);
            this.SetCatalogName(entity);

            return entity;
        }

        /// <summary>
        /// Sets prices to product
        /// </summary>
        /// <param name="product">Product to set properties to</param>
        /// <param name="prices">Prices dictionary with product id as key</param>
        protected void SetPrices(BaseProduct product, IDictionary<string, Price> prices)
        {
            if (product?.Id == null || prices == null || !prices.Any() ||
                !prices.TryGetValue(product.Id, out var price))
            {
                return;
            }

            var commercePrice = price as CommercePrice;
            product.CurrencyCode = price.CurrencyCode;
            product.ListPrice = commercePrice?.Amount;
            product.AdjustedPrice = commercePrice?.ListPrice;
        }

        /// <summary>
        /// Sets stock status to product
        /// </summary>
        /// <param name="product">Product to set stock status</param>
        /// <param name="stockInformation">Stock information</param>
        protected void SetStockStatus(BaseProduct product, StockInformation stockInformation)
        {
            if (stockInformation == null || product == null)
            {
                return;
            }

            product.StockStatus =
                this.CatalogMapper.Map<StockStatus, Models.Catalog.StockStatus>(stockInformation.Status);
        }

        /// <summary>
        /// Initialize properties directly from item fields. Alternative to GlassMapper for product entities.
        /// </summary>
        /// <param name="product">Product to set properties to</param>
        /// <param name="source">Item to set properties from</param>
        private void Initialize(BaseProduct product, Item source)
        {
            product.Id = source.Name;
            product.ProductId = source["ProductId"];
            product.SitecoreId = source["SitecoreId"];

            product.DisplayName = source["DisplayName"];
            product.Description = source["Description"];
            product.Brand = source["Brand"];
            product.Tags = source["Tags"]?.Split('|').ToList();
            product.CustomerAverageRating =
                decimal.TryParse(source["Rating"], out var rating) ? (decimal?)rating : null;
            product.ImageUrls = source.ExtractMediaItems(
                    x =>
                    {
                        var imagesField = (MultilistField)source.Fields["Images"];
                        return imagesField?.TargetIDs.Select(id => id.Guid);
                    })
                ?.Select(x => x.ImageUrl())
                .ToList();
        }

        /// <summary>
        /// Sets catalog name from context
        /// </summary>
        /// <param name="product">Product to set properties to</param>
        private void SetCatalogName(BaseProduct product)
        {
            product.CatalogName = this.CatalogContext.CatalogName;
        }
    }
}