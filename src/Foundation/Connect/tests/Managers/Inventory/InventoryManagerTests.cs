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

namespace HCA.Foundation.Connect.Tests.Managers.Inventory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using Connect.Managers.Inventory;
    using HCA.Foundation.ConnectBase.Entities;
    using NSubstitute;

    using Ploeh.AutoFixture;

    using Providers;

    using Sitecore.Commerce.Entities.Inventory;
    using Sitecore.Commerce.Services.Inventory;

    using Xunit;

    public class InventoryManagerTests
    {
        private readonly IFixture fixture;

        private readonly InventoryManager inventoryManager;

        private readonly InventoryServiceProvider inventoryServiceProvider;

        private readonly ILogService<CommonLog> logService;

        public InventoryManagerTests()
        {
            var connectServiceProvider = Substitute.For<IConnectServiceProvider>();
            this.inventoryServiceProvider = Substitute.For<InventoryServiceProvider>();

            connectServiceProvider.GetInventoryServiceProvider().Returns(this.inventoryServiceProvider);

            this.logService = Substitute.For<ILogService<CommonLog>>();
            this.fixture = new Fixture();

            this.inventoryManager = Substitute.For<InventoryManager>(connectServiceProvider, this.logService);
        }

        public static IEnumerable<object[]> GetStockInformationParameters =>
            new List<object[]>
            {
                new object[] { null, Enumerable.Empty<CommerceInventoryProduct>(), StockDetailsLevel.All },
                new object[] { "1", null, StockDetailsLevel.All },
                new object[] { "1", Enumerable.Empty<CommerceInventoryProduct>(), null },
                new object[] { null, null, StockDetailsLevel.All },
                new object[] { null, Enumerable.Empty<CommerceInventoryProduct>(), null },
                new object[] { "1", null, null },
                new object[] { null, null, null }
            };

        [Theory]
        [MemberData(nameof(GetStockInformationParameters))]
        public void GetStockInformation_IfParameterIsNull_ShouldThrowArgumentNullException(
            string shopName,
            IEnumerable<CommerceInventoryProduct> inventoryProducts,
            StockDetailsLevel detailsLevel)
        {
            // act, assert
            Assert.Throws<ArgumentNullException>(
                () => this.inventoryManager.GetStockInformation(shopName, inventoryProducts, detailsLevel));
        }

        [Fact]
        public void GetStockInformation_IfShopNameIsEmpty_ShouldThrowArgumentException()
        {
            // act, assert
            Assert.Throws<ArgumentException>(
                () => this.inventoryManager.GetStockInformation(
                    string.Empty,
                    Enumerable.Empty<CommerceInventoryProduct>(),
                    StockDetailsLevel.All));
        }

        [Fact]
        public void GetStockInformation_ShouldCallExecuteMethod()
        {
            // arrange
            var shopName = this.fixture.Create<string>();
            var inventoryProducts = this.fixture.Create<IEnumerable<CommerceInventoryProduct>>();
            var detailsLevel = this.fixture.Create<StockDetailsLevel>();

            // act
            this.inventoryManager.GetStockInformation(shopName, inventoryProducts, detailsLevel);

            // assert
            this.inventoryManager.Received(1)
                .Execute(Arg.Any<GetStockInformationRequest>(), this.inventoryServiceProvider.GetStockInformation);
        }
    }
}