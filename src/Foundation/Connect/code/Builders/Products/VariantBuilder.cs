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

    using Models.Catalog;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    [Service(typeof(IProductBuilder<Item, Variant>), Lifetime = Lifetime.Singleton)]
    public class VariantBuilder : BaseProductBuilder, IProductBuilder<Item, Variant>
    {
        public VariantBuilder(
            IStorefrontContext storefrontContext,
            IPricingManager pricingManager) : base(storefrontContext, pricingManager)
        {
        }

        public IEnumerable<Variant> Build(IEnumerable<Item> source)
        {
            Assert.ArgumentNotNull(source, nameof(source));

            var variants = source.Select(this.BuildVariantWithoutPrices).ToList();
            this.SetPrices(variants);

            return variants;
        }

        public Variant Build(Item source)
        {
            Assert.ArgumentNotNull(source, nameof(source));

            var variant = this.BuildVariantWithoutPrices(source);
            this.SetPrices(variant);

            return variant;
        }

        private Variant BuildVariantWithoutPrices(Item source)
        {
            var variant = this.BuildWithoutPrices<Variant>(source);
            this.SetVariantProperties(variant, source);

            return variant;
        }

        private void SetVariantProperties(Variant entity, Item source)
        {
            var properties = source["VariationProperties"]?.Split('|') ?? new string[0];

            entity.Properties = properties.Where(property => !string.IsNullOrEmpty(property))
                .ToDictionary(
                    propertyName => propertyName,
                    propertyName => source[propertyName]);
        }

        private void SetPrices(Variant variant)
        {
            if (variant == null)
            {
                return;
            }

            var prices = this.PricingManager.GetProductPrices(
                    variant.CatalogName,
                    variant.Id,
                    false,
                    null)
                ?.Result;

            this.SetPrices(variant, prices);
        }
    }
}