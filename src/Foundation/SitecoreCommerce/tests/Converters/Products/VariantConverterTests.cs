﻿//    Copyright 2020 EPAM Systems, Inc.
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

namespace HCA.Foundation.SitecoreCommerce.Tests.Converters.Products
{
    using System.Collections.Generic;
    using System.Linq;

    using Ploeh.AutoFixture;

    using Sitecore.Data.Items;
    using Sitecore.FakeDb;

    using SitecoreCommerce.Converters.Products;

    using Xunit;
    
    public class VariantConverterTests : BaseProductConverterTests
    {
        private readonly ItemToVariantConverter itemToVariantConverter;

        private readonly Dictionary<string, string> variantProperties;

        public VariantConverterTests()
        {
            this.itemToVariantConverter = new ItemToVariantConverter(this.CatalogContext, this.CatalogMapper);

            this.variantProperties = new Dictionary<string, string>();
            this.variantProperties.Add(this.Fixture.Create<string>(), this.Fixture.Create<string>());
            this.variantProperties.Add(this.Fixture.Create<string>(), this.Fixture.Create<string>());
        }

        [Fact]
        public void Convert_IfItemValid_ShouldReturnFilledVariant()
        {
            // arrange
            var dbItem = this.InitializeVariantItem();

            using (var db = new Db
            {
                dbItem
            })
            {
                var item = db.GetItem(dbItem.ID);

                // act
                var variants = this.itemToVariantConverter.Convert(
                        new List<Item>
                        {
                            item
                        })
                    ?.ToList();
                var variant = variants?.FirstOrDefault();

                // assert
                Assert.NotNull(variant);
                foreach (var variantProperty in this.variantProperties)
                {
                    var value = item[variantProperty.Key];
                    Assert.Equal(value, variant.Properties[variantProperty.Key]);
                }
            }
        }

        private DbItem InitializeVariantItem()
        {
            var item = this.InitializeBaseProductItem();

            var variantPropertiesKeys = string.Join("|", this.variantProperties.Keys);
            item.Add("VariationProperties", variantPropertiesKeys);

            foreach (var variantProperty in this.variantProperties)
            {
                item.Add(variantProperty.Key, variantProperty.Value);
            }

            return item;
        }
    }
}