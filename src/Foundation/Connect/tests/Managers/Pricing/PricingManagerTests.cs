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

namespace HCA.Foundation.Connect.Tests.Managers.Pricing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using Connect.Managers.Pricing;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Providers;

    using Sitecore.Commerce.Services.Prices;

    using Xunit;

    using HCA.Foundation.ConnectBase.Providers;
    using ConnectBase = HCA.Foundation.ConnectBase.Pipelines.Arguments;

    public class PricingManagerTests
    {
        private readonly IFixture fixture;

        private readonly ILogService<CommonLog> logService;

        private readonly PricingManager pricingManager;

        private readonly PricingServiceProviderBase pricingServiceProvider;

        public PricingManagerTests()
        {
            var connectServiceProvider = Substitute.For<IConnectServiceProvider>();
            this.pricingServiceProvider = Substitute.For<PricingServiceProviderBase>();
            this.logService = Substitute.For<ILogService<CommonLog>>();

            connectServiceProvider.GetPricingServiceProvider().Returns(this.pricingServiceProvider);

            this.fixture = new Fixture();

            this.pricingManager = Substitute.For<PricingManager>(connectServiceProvider, this.logService);
        }

        public static IEnumerable<object[]> GetProductBulkPricesParameters =>
            new List<object[]>
            {
                new object[] { null, Enumerable.Empty<string>(), new string[0] },
                new object[] { "1", null, new string[0] },
                new object[] { "1", Enumerable.Empty<string>(), null }
            };

        public static IEnumerable<object[]> GetProductPricesParameters =>
            new List<object[]>
            {
                new object[] { null, "1", new string[0] },
                new object[] { "1", null, new string[0] },
                new object[] { "1", "1", null }
            };

        [Fact]
        public void GetProductBulkPrices_IfCatalogNameIsEmpty_ShouldThrowArgumentException()
        {
            // act, assert
            Assert.Throws<ArgumentException>(
                () => this.pricingManager.GetProductBulkPrices(
                    string.Empty,
                    this.fixture.Create<IEnumerable<string>>()));
        }

        [Theory]
        [MemberData(nameof(GetProductBulkPricesParameters))]
        public void GetProductBulkPrices_IfParameterIsNull_ShouldThrowArgumentNullException(
            string catalogName,
            IEnumerable<string> productIds,
            string[] priceTypeIds)
        {
            // act, assert
            Assert.Throws<ArgumentNullException>(
                () => this.pricingManager.GetProductBulkPrices(catalogName, productIds, priceTypeIds));
        }

        [Fact]
        public void GetProductBulkPrices_ShouldCallExecuteMethod()
        {
            // act
            this.pricingManager.GetProductBulkPrices(
                this.fixture.Create<string>(),
                this.fixture.Create<IEnumerable<string>>());

            // assert
            this.pricingManager.Received(1)
                .Execute(Arg.Any<ConnectBase.GetProductBulkPricesRequest>(), this.pricingServiceProvider.GetProductBulkPrices);
        }

        [Theory]
        [InlineData("", "1")]
        [InlineData("1", "")]
        public void GetProductPrices_IfParameterIsEmpty_ShouldThrowArgumentException(
            string catalogName,
            string productId)
        {
            // act, assert
            Assert.Throws<ArgumentException>(
                () => this.pricingManager.GetProductPrices(
                    catalogName,
                    productId,
                    false));
        }

        [Theory]
        [MemberData(nameof(GetProductPricesParameters))]
        public void GetProductPrices_IfParameterIsNull_ShouldThrowArgumentNullException(
            string catalogName,
            string productId,
            string[] priceTypeIds)
        {
            // act, assert
            Assert.Throws<ArgumentNullException>(
                () => this.pricingManager.GetProductPrices(catalogName, productId, false, priceTypeIds));
        }

        [Fact]
        public void GetProductPrices_ShouldCallExecuteMethod()
        {
            // act
            this.pricingManager.GetProductPrices(
                this.fixture.Create<string>(),
                this.fixture.Create<string>(),
                this.fixture.Create<bool>());

            // assert
            this.pricingManager.Received(1)
                .Execute(Arg.Any<ConnectBase.GetProductPricesRequest>(), this.pricingServiceProvider.GetProductPrices);
        }
    }
}