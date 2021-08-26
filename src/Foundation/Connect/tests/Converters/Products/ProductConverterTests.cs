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

namespace HCA.Foundation.Connect.Tests.Converters.Products
{
    using System.Collections.Generic;
    using System.Linq;

    using Connect.Context.Storefront;
    using Connect.Converters.Products;
    using Connect.Managers.Inventory;
    using Connect.Managers.Pricing;

    using Models.Catalog;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Inventory;
    using Sitecore.Commerce.Services.Inventory;
    using Sitecore.Commerce.Services.Prices;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb;

    using Xunit;

    public class ProductConverterTests : BaseProductConverterTests
    {
        private readonly IInventoryManager inventoryManager;
        private readonly IPricingManager pricingManager;
        private readonly ProductConverter productConverter;
        private readonly IVariantConverter<Item> variantConverter;
        private readonly IStorefrontContext storefrontContext;

        private readonly GetProductBulkPricesResult getProductBulkPricesResult;
        private readonly GetProductPricesResult getProductPricesResult;
        private readonly GetStockInformationResult getStockInformationResult;

        public ProductConverterTests()
        {
            this.variantConverter = Substitute.For<IVariantConverter<Item>>();
            this.pricingManager = Substitute.For<IPricingManager>();
            this.inventoryManager = Substitute.For<IInventoryManager>();
            this.storefrontContext = Substitute.For<IStorefrontContext>();

            this.getProductBulkPricesResult = this.Fixture.Create<GetProductBulkPricesResult>();
            this.pricingManager
                .GetProductBulkPrices(Arg.Any<string>(), Arg.Any<IEnumerable<string>>(), Arg.Any<string[]>())
                .Returns(this.getProductBulkPricesResult);
            this.getProductPricesResult = this.Fixture.Create<GetProductPricesResult>();
            this.pricingManager
                .GetProductPrices(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<string[]>())
                .Returns(this.getProductPricesResult);
            this.Fixture.Customize<StockInformation>(
                info => info.With(i => i.Product, this.Fixture.Create<CommerceInventoryProduct>()));
            this.getStockInformationResult = this.Fixture.Create<GetStockInformationResult>();
            this.inventoryManager
                .GetStockInformation(
                    Arg.Any<string>(),
                    Arg.Any<IEnumerable<CommerceInventoryProduct>>(),
                    Arg.Any<StockDetailsLevel>())
                .Returns(this.getStockInformationResult);

            this.productConverter = new ProductConverter(
                this.variantConverter,
                this.CatalogContext,
                this.pricingManager,
                this.inventoryManager,
                this.storefrontContext,
                this.CatalogMapper);
        }

        [Fact]
        public void Convert_IfItemIsValid_ShouldReturnValidProduct()
        {
            // arrange
            var variants = this.Fixture.Create<List<Variant>>();
            this.variantConverter.Convert(Arg.Any<IEnumerable<Item>>()).Returns(variants);
            var dbItem = this.InitializeProductItem();

            using (var db = new Db
            {
                dbItem
            })
            {
                var item = db.GetItem(dbItem.ID);

                // act
                var product = this.productConverter.Convert(item);

                // assert
                Assert.NotNull(product);
                Assert.Equal(variants, product.Variants);
                this.pricingManager
                    .Received(1)
                    .GetProductPrices(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<string[]>());
                this.variantConverter.Received(1).Convert(Arg.Any<IEnumerable<Item>>());
                this.inventoryManager
                    .Received(1)
                    .GetStockInformation(
                        Arg.Any<string>(),
                        Arg.Any<IEnumerable<CommerceInventoryProduct>>(),
                        Arg.Any<StockDetailsLevel>());
            }
        }

        [Fact]
        public void Convert_IfItemsAreValidAndIncludeVariantsIsFalse_ShouldReturnValidProducts()
        {
            // arrange
            var items = this.Fixture.Create<List<Item>>();

            // act
            var products = this.productConverter.Convert(items, false).ToList();

            // assert
            Assert.NotNull(products);
            Assert.Equal(items.Count, products.Count);
            this.pricingManager
                .Received(1)
                .GetProductBulkPrices(Arg.Any<string>(), Arg.Any<IEnumerable<string>>(), Arg.Any<string[]>());
            this.inventoryManager
                .Received(1)
                .GetStockInformation(
                    Arg.Any<string>(),
                    Arg.Any<IEnumerable<CommerceInventoryProduct>>(),
                    Arg.Any<StockDetailsLevel>());
        }

        [Fact]
        public void Convert_IfItemsAreValidAndIncludeVariantsIsTrue_ShouldReturnValidProducts()
        {
            // arrange
            var items = this.Fixture.Create<List<Item>>();

            // act
            var products = this.productConverter.Convert(items, true).ToList();

            // assert
            Assert.NotNull(products);
            Assert.Equal(items.Count, products.Count);
            this.pricingManager
                .Received()
                .GetProductPrices(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<string>(), Arg.Any<string>());
            this.inventoryManager
                .Received(1)
                .GetStockInformation(
                    Arg.Any<string>(),
                    Arg.Any<IEnumerable<CommerceInventoryProduct>>(),
                    Arg.Any<StockDetailsLevel>());
        }

        private DbItem InitializeProductItem()
        {
            var item = this.InitializeBaseProductItem();
            item.Children.Add(this.InitializeBaseProductItem());

            return item;
        }
    }
}