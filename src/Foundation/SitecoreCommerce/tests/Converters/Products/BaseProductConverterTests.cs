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

namespace HCA.Foundation.SitecoreCommerce.Tests.Converters.Products
{
    using System.Collections.Generic;

    using Connect.Context.Catalog;
    using Connect.Mappers.Catalog;
    using Connect.Models.Catalog;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Inventory;
    using Sitecore.Commerce.Entities.Prices;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb;
    using Sitecore.FakeDb.AutoFixture;

    using SitecoreCommerce.Converters.Products;

    using Xunit;

    using StockStatus = Connect.Models.Catalog.StockStatus;

    public class BaseProductConverterTests
    {
        protected readonly ICatalogContext CatalogContext;
        protected readonly ICatalogMapper CatalogMapper;

        protected readonly IFixture Fixture;

        public BaseProductConverterTests()
        {
            this.CatalogContext = Substitute.For<ICatalogContext>();
            this.CatalogMapper = Substitute.For<ICatalogMapper>();

            this.Fixture = new Fixture().Customize(new AutoDbCustomization());

            this.CatalogContext.CatalogName.Returns(this.Fixture.Create<string>());
        }

        public static IEnumerable<object[]> SetStockStatusParameters => new List<object[]>
        {
            new object[] { null, new StockInformation() },
            new object[] { new TestBaseProduct(), null },
            new object[] { null, null }
        };

        public static IEnumerable<object[]> SetPricesPricesParameters => new List<object[]>
        {
            new object[] { null },
            new object[] { new Dictionary<string, Price>() },
            new object[]
            {
                new Dictionary<string, Price>
                {
                    { "1", new Price() }
                }
            }
        };

        public static IEnumerable<object[]> SetPricesProductParameters => new List<object[]>
        {
            new object[] { null },
            new object[] { new TestBaseProduct() }
        };

        [Fact]
        public void Initialize_IfItemIsValid_ShouldReturnFilledProduct()
        {
            // arrange
            var dbItem = this.InitializeBaseProductItem();

            var builder = new TestBaseItemToProductConverter(this.CatalogContext, this.CatalogMapper);

            using (var db = new Db
            {
                dbItem
            })
            {
                var productItem = db.GetItem(dbItem.ID);

                // act
                var product = builder.Initialize(productItem);

                // assert
                Assert.NotNull(product);
                Assert.Equal(product.Id, productItem.Name);
                Assert.Equal(product.ProductId, productItem["ProductId"]);
                Assert.Equal(product.SitecoreId, productItem["SitecoreId"]);
                Assert.Equal(product.DisplayName, productItem["DisplayName"]);
                Assert.Equal(product.Description, productItem["Description"]);
                Assert.Equal(product.Brand, productItem["Brand"]);
                Assert.Equal(product.Tags[0], productItem["Tags"]);
                Assert.Equal(product.CustomerAverageRating, decimal.Parse(productItem["Rating"]));
                Assert.Empty(product.ImageUrls);

                Assert.Equal(product.CatalogName, this.CatalogContext.CatalogName);
            }
        }

        [Theory]
        [MemberData(nameof(SetPricesPricesParameters))]
        public void SetPrices_IfDictionaryHasNotProductPrice_ShouldNotThrowException(Dictionary<string, Price> prices)
        {
            // arrange
            var product = this.Fixture.Create<TestBaseProduct>();
            var builder = new TestBaseItemToProductConverter(this.CatalogContext, this.CatalogMapper);

            // act
            var exception = Record.Exception(() => builder.SetPrices(product, prices));

            // assert
            Assert.Null(exception);
        }

        [Fact]
        public void SetPrices_IfPriceExistInDictionary_ShouldSetPrices()
        {
            // arrange
            var price = this.Fixture.Create<CommercePrice>();
            var product = this.Fixture.Build<TestBaseProduct>()
                .With(prod => prod.CurrencyCode, null)
                .With(prod => prod.AdjustedPrice, null)
                .With(prod => prod.ListPrice, null)
                .Create();
            var prices = new Dictionary<string, Price>
            {
                { product.Id, price }
            };

            var builder = new TestBaseItemToProductConverter(this.CatalogContext, this.CatalogMapper);

            // act
            builder.SetPrices(product, prices);

            // assert
            Assert.Equal(price.CurrencyCode, product.CurrencyCode);
            Assert.Equal(price.Amount, product.ListPrice);
            Assert.Equal(price.ListPrice, product.AdjustedPrice);
        }

        [Theory]
        [MemberData(nameof(SetPricesProductParameters))]
        public void SetPrices_IfProductInvalid_ShouldNotThrowException(BaseProduct product)
        {
            // arrange
            var prices = this.Fixture.Create<Dictionary<string, Price>>();
            var builder = new TestBaseItemToProductConverter(this.CatalogContext, this.CatalogMapper);

            // act
            var exception = Record.Exception(() => builder.SetPrices(product, prices));

            // assert
            Assert.Null(exception);
        }

        [Theory]
        [MemberData(nameof(SetStockStatusParameters))]
        public void SetStockStatus_IfParameterIsNull_ShouldNotThrowException(
            BaseProduct product,
            StockInformation stockInformation)
        {
            // arrange
            var builder = new TestBaseItemToProductConverter(this.CatalogContext, this.CatalogMapper);

            // act
            var exception = Record.Exception(() => builder.SetStockStatus(product, stockInformation));

            // assert
            Assert.Null(exception);
        }

        [Fact]
        public void SetStockStatus_IfParametersNotNull_ShouldSetStockStatus()
        {
            // arrange
            var product = new TestBaseProduct();
            var stockInformation = this.Fixture.Create<StockInformation>();
            var stockStatus = this.Fixture.Create<StockStatus>();
            this.CatalogMapper
                .Map<Sitecore.Commerce.Entities.Inventory.StockStatus, StockStatus>(
                    Arg.Any<Sitecore.Commerce.Entities.Inventory.StockStatus>())
                .Returns(stockStatus);

            var builder = new TestBaseItemToProductConverter(this.CatalogContext, this.CatalogMapper);

            // act
            builder.SetStockStatus(product, stockInformation);

            // assert
            this.CatalogMapper.Received(1)
                .Map<Sitecore.Commerce.Entities.Inventory.StockStatus, StockStatus>(
                    Arg.Any<Sitecore.Commerce.Entities.Inventory.StockStatus>());
            Assert.Equal(stockStatus, product.StockStatus);
        }

        protected DbItem InitializeBaseProductItem()
        {
            return new DbItem(this.Fixture.Create<string>(), new ID())
            {
                { "ProductId", this.Fixture.Create<string>() },
                { "SitecoreId", this.Fixture.Create<string>() },
                { "DisplayName", this.Fixture.Create<string>() },
                { "Description", this.Fixture.Create<string>() },
                { "Brand", this.Fixture.Create<string>() },
                { "Tags", this.Fixture.Create<string>() },
                { "Rating", this.Fixture.Create<decimal>().ToString() },
                { "Images", string.Empty }
            };
        }

        private class TestBaseItemToProductConverter : BaseItemToProductConverter
        {
            public TestBaseItemToProductConverter(ICatalogContext catalogContext, ICatalogMapper catalogMapper) : base(
                catalogContext,
                catalogMapper)
            {
            }

            public TestBaseProduct Initialize(Item source)
            {
                return this.Initialize<TestBaseProduct>(source);
            }

            public new void SetPrices(BaseProduct product, IDictionary<string, Price> prices)
            {
                base.SetPrices(product, prices);
            }

            public new void SetStockStatus(BaseProduct product, StockInformation stockInformation)
            {
                base.SetStockStatus(product, stockInformation);
            }
        }

        private class TestBaseProduct : BaseProduct
        {
        }
    }
}