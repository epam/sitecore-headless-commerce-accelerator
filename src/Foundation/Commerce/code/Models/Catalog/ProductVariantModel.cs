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

using System.Collections.Generic;
using System.Linq;
using Sitecore.Diagnostics;
using TypeLite;
using Wooli.Foundation.Connect.Models;
using Wooli.Foundation.Extensions.Extensions;

namespace Wooli.Foundation.Commerce.Models.Catalog
{
    [TsClass]
    public class ProductVariantModel
    {
        public string ProductVariantId { get; set; }

        public string ProductId { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public IList<string> Tags { get; set; }

        public IList<string> ImageUrls { get; set; }

        public IDictionary<string, string> VariantProperties { get; set; }

        public string CurrencySymbol { get; set; }

        public decimal? ListPrice { get; set; }

        public decimal? AdjustedPrice { get; set; }

        public string StockStatusName { get; set; }

        public decimal? CustomerAverageRating { get; set; }

        public void Initialize(ICommerceProductVariantModel sellableItemModel)
        {
            Assert.ArgumentNotNull(sellableItemModel, nameof(sellableItemModel));

            ProductVariantId = sellableItemModel.ItemName;
            ProductId = sellableItemModel.ProductId;
            DisplayName = sellableItemModel.DisplayName;
            Description = sellableItemModel.Description;
            Brand = sellableItemModel.Brand;

            Tags = sellableItemModel.Tags?.Split('|').ToList();

            ImageUrls = sellableItemModel
                .ExtractMediaItems(x => x.Images)
                .Select(x => x.ImageUrl())
                .ToList();

            var properties = new Dictionary<string, string>();
            foreach (string variantPropertyName in sellableItemModel.VariationProperties?.Split('|') ?? new string[0])
                if (!string.IsNullOrEmpty(variantPropertyName))
                {
                    string value = sellableItemModel.Item[variantPropertyName];
                    properties.Add(variantPropertyName, value);
                }

            VariantProperties = properties;
        }
    }
}