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

namespace Wooli.Foundation.Commerce.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Sitecore.Diagnostics;

    using TypeLite;

    using Wooli.Foundation.Connect.Models;
    using Wooli.Foundation.Extensions.Extensions;

    [TsClass]
    public class ProductModel
    {
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

        public IList<ProductVariantModel> Variants { get; set; }

        public string SitecoreId { get; set; }

        public void Initialize(ICommerceProductModel sellableItemModel)
        {
            Assert.ArgumentNotNull(sellableItemModel, nameof(sellableItemModel));

            this.ProductId = sellableItemModel.ProductId;
            this.SitecoreId = sellableItemModel.SitecoreId;
            this.DisplayName = sellableItemModel.DisplayName;
            this.Description = sellableItemModel.Description;
            this.Brand = sellableItemModel.Brand;


            this.Tags = sellableItemModel.Tags?.Split('|').ToList();

            this.ImageUrls = sellableItemModel
                .ExtractMediaItems(x => x.Images)
                ?.Select(x => x.ImageUrl())
                .ToList();

            var variants = new List<ProductVariantModel>();
            foreach (ICommerceProductVariantModel commerceProductVariantModel in sellableItemModel.Variants ?? new List<ICommerceProductVariantModel>())
            {
                var variant = new ProductVariantModel();
                variant.Initialize(commerceProductVariantModel);
                variants.Add(variant);
            }

            this.Variants = variants;
        }

    }
}
