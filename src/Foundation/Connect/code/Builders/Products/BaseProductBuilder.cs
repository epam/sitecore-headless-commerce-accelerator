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

    using Extensions.Extensions;

    using Managers;

    using Models.Catalog;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Prices;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    /// <summary>
    /// Sets Base Product properties
    /// </summary>
    public abstract class BaseProductBuilder
    {
        protected readonly IPricingManager PricingManager;
        private readonly IStorefrontContext storefrontContext;

        protected BaseProductBuilder(
            IStorefrontContext storefrontContext,
            IPricingManager pricingManager)
        {
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));
            Assert.ArgumentNotNull(pricingManager, nameof(pricingManager));

            this.storefrontContext = storefrontContext;
            this.PricingManager = pricingManager;
        }

        /// <summary>
        /// Builds entity of TEntity type without setting prices
        /// </summary>
        /// <typeparam name="TEntity">BaseProduct or it derivatives</typeparam>
        /// <param name="source">Item to set properties from</param>
        /// <returns>Entity of TEntity type without prices set</returns>
        protected TEntity BuildWithoutPrices<TEntity>(Item source) where TEntity : BaseProduct, new()
        {
            var entity = new TEntity();
            this.Initialize(entity, source);
            this.SetCatalogName(entity);
            entity.StockStatusName = "InStock";
            //TODO: this.SetCurrency(entity)
            //TODO: this.SetStockStatus(entity)

            return entity;
        }

        /// <summary>
        /// Initialize properties directly from item fields. Alternative to GlassMapper for product entities.
        /// </summary>
        /// <typeparam name="TEntity">BaseProduct or it derivatives</typeparam>
        /// <param name="entity">Entity to set properties to</param>
        /// <param name="source">Item to set properties from</param>
        protected void Initialize<TEntity>(TEntity entity, Item source) where TEntity : BaseProduct
        {
            entity.Id = source.Name;
            entity.ProductId = source["ProductId"];
            entity.SitecoreId = source["SitecoreId"];

            entity.DisplayName = source["DisplayName"];
            entity.Description = source["Description"];
            entity.Brand = source["Brand"];
            entity.Tags = source["Tags"]?.Split('|').ToList();
            entity.CustomerAverageRating = decimal.TryParse(source["Rating"], out var rating) ? (decimal?)rating : null;
            entity.ImageUrls = source.ExtractMediaItems(
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
        /// <typeparam name="TEntity">BaseProduct or it derivatives</typeparam>
        /// <param name="entity">Entity to set properties to</param>
        protected void SetCatalogName<TEntity>(TEntity entity) where TEntity : BaseProduct
        {
            entity.CatalogName = this.storefrontContext.CatalogName;
        }

        /// <summary>
        /// Sets prices using bulk prices manager method for getting them
        /// </summary>
        /// <typeparam name="TEntity">BaseProduct or it derivatives</typeparam>
        /// <param name="entities">Entity to set properties to</param>
        protected void SetPrices<TEntity>(IList<TEntity> entities) where TEntity : BaseProduct
        {
            if (entities == null || !entities.Any())
            {
                return;
            }

            var prices = this.PricingManager.GetProductBulkPrices(
                    entities.Select(_ => _.CatalogName).FirstOrDefault(),
                    entities.Select(_ => _.Id),
                    null)
                ?.Result;
            foreach (var entity in entities)
            {
                this.SetPrices(entity, prices);
            }
        }

        /// <summary>
        /// Sets prices to entity
        /// </summary>
        /// <typeparam name="TEntity">BaseProduct or it derivatives</typeparam>
        /// <param name="entity">Entity to set properties to</param>
        /// <param name="prices">Prices dictionary with product id as key</param>
        protected void SetPrices<TEntity>(TEntity entity, IDictionary<string, Price> prices)
            where TEntity : BaseProduct
        {
            if (prices == null || !prices.Any() ||
                !prices.TryGetValue(entity.Id, out var price))
            {
                return;
            }

            var commercePrice = price as CommercePrice;
            entity.CurrencyCode = price.CurrencyCode;
            entity.ListPrice = commercePrice?.Amount;
            entity.AdjustedPrice = commercePrice?.ListPrice;
        }
    }
}