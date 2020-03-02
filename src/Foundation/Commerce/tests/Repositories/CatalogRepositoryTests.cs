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

namespace Wooli.Foundation.Commerce.Tests.Repositories
{
    using Glass.Mapper.Sc;

    using NSubstitute;

    using Sitecore.FakeDb;

    using Wooli.Foundation.Commerce.Context;
    using Wooli.Foundation.Commerce.Models.Catalog;
    using Wooli.Foundation.Commerce.Providers;
    using Wooli.Foundation.Commerce.Repositories;
    using Wooli.Foundation.Connect.Managers;

    using Xunit;

    public class CatalogRepositoryTests
    {
        [Fact]
        public void GetProduct_ProductId_ProductIsReturned()
        {
            // Setup
            var currencyProvider = Substitute.For<ICurrencyProvider>();
            var siteContext = Substitute.For<ISiteContext>();
            var storefrontContext = Substitute.For<IStorefrontContext>();
            var visitorContext = Substitute.For<IVisitorContext>();
            var catalogManager = Substitute.For<ICatalogManager>();
            var sitecoreService = Substitute.For<ISitecoreService>();
            var searchManager = Substitute.For<ISearchManager>();

            var productItem = new DbItem("ProductItem");
            productItem.Fields.Add("ProductId", "productId");

            using (var db = new Db { productItem })
            {
                storefrontContext.CatalogName.Returns("storeName");
                searchManager.GetProduct("storeName", "productId").Returns(db.GetItem(productItem.ID));

                // Execute
                var repository = new CatalogRepository(
                    siteContext,
                    storefrontContext,
                    visitorContext,
                    catalogManager,
                    sitecoreService,
                    searchManager,
                    currencyProvider);
                ProductModel result = repository.GetProduct("productId");

                // Assert
                Assert.NotNull(result);
                Assert.Equal("productId", result.ProductId);
            }
        }
    }
}