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

namespace HCA.Foundation.Connect.Tests.Builders.Products
{
    using System.Collections.Generic;
    using System.Linq;

    using Connect.Builders.Products;
    using Connect.Context.Storefront;
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

    public class ProductBuilderTests : BaseProductBuilderTests
    {
        private readonly IInventoryManager inventoryManager;
        private readonly IPricingManager pricingManager;
        private readonly ProductBuilder productBuilder;
        private readonly IVariantBuilder<Item> variantBuilder;
        private readonly IStorefrontContext storefrontContext;

        private readonly GetProductBulkPricesResult getProductBulkPricesResult;
        private readonly GetProductPricesResult getProductPricesResult;
        private readonly GetStockInformationResult getStockInformationResult;

        public ProductBuilderTests()
        {
            this.variantBuilder = Substitute.For<IVariantBuilder<Item>>();
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

            this.productBuilder = new ProductBuilder(
                this.variantBuilder,
                this.CatalogContext,
                this.pricingManager,
                this.inventoryManager,
                this.storefrontContext,
                this.CatalogMapper);
        }

        [Fact]
        public void Build_IfItemIsValid_ShouldReturnValidProduct()
        {
            // arrange
            var variants = this.Fixture.Create<List<Variant>>();
            this.variantBuilder.Build(Arg.Any<IEnumerable<Item>>()).Returns(variants);
            var dbItem = this.InitializeProductItem();

            using (var db = new Db
            {
                dbItem
            })
            {
                var item = db.GetItem(dbItem.ID);

                // act
                var product = this.productBuilder.Build(item);

                // assert
                Assert.NotNull(product);
                Assert.Equal(variants, product.Variants);
                this.pricingManager
                    .Received(1)
                    .GetProductPrices(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<string[]>());
                this.variantBuilder.Received(1).Build(Arg.Any<IEnumerable<Item>>());
                this.inventoryManager
                    .Received(1)
                    .GetStockInformation(
                        Arg.Any<string>(),
                        Arg.Any<IEnumerable<CommerceInventoryProduct>>(),
                        Arg.Any<StockDetailsLevel>());
            }
        }

        [Fact]
        public void Build_IfItemsAreValidAndIncludeVariantsIsFalse_ShouldReturnValidProducts()
        {
            // arrange
            var items = this.Fixture.Create<List<Item>>();

            // act
            var products = this.productBuilder.Build(items, false).ToList();

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
        public void Build_IfItemsAreValidAndIncludeVariantsIsTrue_ShouldReturnValidProducts()
        {
            // arrange
            var items = this.Fixture.Create<List<Item>>();

            // act
            var products = this.productBuilder.Build(items, true).ToList();

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