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

namespace Wooli.Foundation.Commerce.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Providers;

    using Sitecore.Data.Items;
    using TypeLite;

    [TsClass]
    [ExcludeFromCodeCoverage]
    public class Product : BaseProduct
    {
        [Obsolete("Use Product(Connect.Models.Product, ICurrencyProvider)")]
        public Product(Item sellableItem) : base(sellableItem)
        {
            this.SitecoreId = sellableItem["SitecoreId"];
            this.Variants = sellableItem.Children.Select(commerceProductVariant => new ProductVariant(commerceProductVariant)).ToList();
        }

        public Product(Connect.Models.Product product, ICurrencyProvider currencyProvider)
            : base(product, currencyProvider)
        {
            this.SitecoreId = product.Item["SitecoreId"];

            var variants = new List<ProductVariant>();
            foreach (var productVariant in product.Variants)
            {
                var variant = new ProductVariant(productVariant, currencyProvider);

                variants.Add(variant);
            }

            this.Variants = variants;
        }

        public string SitecoreId { get; set; }

        public IList<ProductVariant> Variants { get; set; }
    }
}