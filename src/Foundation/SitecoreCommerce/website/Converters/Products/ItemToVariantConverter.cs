﻿//    Copyright 2021 EPAM Systems, Inc.
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

namespace HCA.Foundation.SitecoreCommerce.Converters.Products
{
    using System.Collections.Generic;
    using System.Linq;

    using Connect.Context.Catalog;
    using Connect.Converters.Products;
    using Connect.Mappers.Catalog;
    using Connect.Models.Catalog;

    using DependencyInjection;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    [Service(typeof(IVariantConverter<Item>), Lifetime = Lifetime.Singleton)]
    public class ItemToVariantConverter : BaseItemToProductConverter, IVariantConverter<Item>
    {
        public ItemToVariantConverter(
            ICatalogContext catalogContext,
            ICatalogMapper catalogMapper) : base(catalogContext, catalogMapper)
        {
        }

        public IEnumerable<Variant> Convert(IEnumerable<Item> sources)
        {
            Assert.ArgumentNotNull(sources, nameof(sources));

            var variants = sources.Select(this.InitializeVariant).ToList();

            return variants;
        }

        private Variant InitializeVariant(Item source)
        {
            var variant = this.Initialize<Variant>(source);
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
    }
}