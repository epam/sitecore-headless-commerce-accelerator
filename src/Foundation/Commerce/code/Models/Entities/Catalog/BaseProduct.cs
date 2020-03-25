//    Copyright 2019 EPAM Systems, Inc.
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

namespace Wooli.Foundation.Commerce.Models.Entities.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Foundation.Extensions.Extensions;

    using Providers;

    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    public class BaseProduct
    {
        [Obsolete("Use BaseProduct(Connect.Models.BaseProduct, ICurrencyProvider)")]
        public BaseProduct(Item sellableItem)
        {
            this.Initialize(sellableItem);
        }

        public BaseProduct(Connect.Models.BaseProduct product, ICurrencyProvider currencyProvider)
        {
            this.Initialize(product.Item);

            this.CurrencySymbol = currencyProvider.GetCurrencySymbolByCode(product.CurrencyCode);
            this.ListPrice = product.ListPrice;
            this.AdjustedPrice = product.AdjustedPrice;
            this.StockStatusName = product.StockStatusName;
            this.CustomerAverageRating = product.CustomerAverageRating;
        }

        public string ProductId { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public IList<string> Tags { get; set; }

        public IList<string> ImageUrls { get; set; }

        public string CurrencySymbol { get; set; }

        public decimal? ListPrice { get; set; }

        public decimal? AdjustedPrice { get; set; }

        public string StockStatusName { get; set; }

        public decimal? CustomerAverageRating { get; set; }

        private void Initialize(Item item)
        {
            Assert.ArgumentNotNull(item, nameof(item));

            this.ProductId = item["ProductId"];
            this.DisplayName = item["DisplayName"];
            this.Description = item["Description"];
            this.Brand = item["Brand"];

            this.Tags = item["Tags"]?.Split('|').ToList();

            this.ImageUrls = item.ExtractMediaItems(
                    x =>
                    {
                        var imagesField = (MultilistField)item.Fields["Images"];
                        return imagesField?.TargetIDs.Select(id => id.Guid);
                    })
                ?.Select(x => x.ImageUrl())
                .ToList();
        }
    }
}