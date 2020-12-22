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

namespace HCA.Foundation.Commerce.Tests.Services.Analytics
{
    using System;

    using AutoFixture;

    using Commerce.Services.Analytics;

    using Connect.Context.Storefront;
    using Connect.Managers.Analytics;

    using Models.Entities.Catalog;

    using NSubstitute;



    using Sitecore.Commerce.Services.Catalog;
    using Sitecore.FakeDb.AutoFixture;

    using Xunit;

    public class CommerceAnalyticsServiceTests
    {
        private readonly IAnalyticsManager analyticsManager;

        private readonly IFixture fixture;

        private readonly IStorefrontContext storefrontContext;

        public CommerceAnalyticsServiceTests()
        {
            this.fixture = new Fixture().Customize(new AutoDbCustomization());

            this.storefrontContext = Substitute.For<IStorefrontContext>();
            this.storefrontContext.ShopName.Returns(this.fixture.Create<string>());

            var successfulResult = new CatalogResult
            {
                Success = true
            };

            this.analyticsManager = Substitute.For<IAnalyticsManager>();
            this.analyticsManager.VisitedCategoryPage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(successfulResult);
            this.analyticsManager.VisitedProductDetailsPage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(successfulResult);
            this.analyticsManager.SearchInitiated(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>())
                .Returns(successfulResult);
        }

        [Fact]
        public void RaiseCategoryVisitedEvent_IfCategoryIsNotNull_ShouldRaiseEvent()
        {
            // arrange
            var category = this.fixture.Create<Category>();
            var service = new AnalyticsService(this.analyticsManager, this.storefrontContext);

            // act
            service.RaiseCategoryVisitedEvent(category);

            // assert
            this.analyticsManager.Received(1)
                .VisitedCategoryPage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void RaiseCategoryVisitedEvent_IfCategoryIsNull_ShouldThrowException()
        {
            // arrange
            Category category = null;
            var service = new AnalyticsService(this.analyticsManager, this.storefrontContext);

            // act & assert
            Assert.ThrowsAny<ArgumentNullException>(() => { service.RaiseCategoryVisitedEvent(category); });
            this.analyticsManager.Received(0)
                .VisitedCategoryPage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void RaiseProductVisitedEvent_IfProductIsNotNull_ShouldRaiseEvent()
        {
            // arrange
            var product = this.fixture.Create<Product>();
            var service = new AnalyticsService(this.analyticsManager, this.storefrontContext);

            // act
            service.RaiseProductVisitedEvent(product);

            // assert
            this.analyticsManager.Received(1)
                .VisitedProductDetailsPage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void RaiseProductVisitedEvent_IfProductIsNull_ShouldThrowException()
        {
            // arrange
            Product product = null;
            var service = new AnalyticsService(this.analyticsManager, this.storefrontContext);

            // act & assert
            Assert.ThrowsAny<ArgumentNullException>(() => { service.RaiseProductVisitedEvent(product); });
            this.analyticsManager.Received(0)
                .VisitedProductDetailsPage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
}