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
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Data.Items;
    using TypeLite;

    [TsClass]
    public class ProductVariant : BaseProduct
    {
        public ProductVariant(Item sellableItem) : base(sellableItem)
        {
            this.ProductVariantId = sellableItem.Name;

            var variantProperties = sellableItem["VariationProperties"]?.Split('|') ?? new string[0];
            var properties = variantProperties
                .Where(variantPropertyName => !string.IsNullOrEmpty(variantPropertyName))
                .ToDictionary(variantPropertyName => variantPropertyName, variantPropertyName => sellableItem[variantPropertyName]);

            this.VariantProperties = properties;
        }

        public string ProductVariantId { get; set; }

        public IDictionary<string, string> VariantProperties { get; set; }
    }
}