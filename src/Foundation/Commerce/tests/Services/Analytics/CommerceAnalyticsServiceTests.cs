//    Copyright 2019 EPAM Systems, Inc.
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

namespace Wooli.Foundation.Commerce.Tests.Services.Analytics
{
    using System;
    using Commerce.Services.Analytics;
    using Connect.Managers;
    using Context;
    using Models.Catalog;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Xunit;

    public class CommerceAnalyticsServiceTests
    {
        private readonly Fixture fixture;
        private readonly IAnalyticsManager analyticsManager;
        private readonly IStorefrontContext storefrontContext;

        public CommerceAnalyticsServiceTests()
        {
            this.fixture = new Fixture();
            
            this.storefrontContext = Substitute.For<IStorefrontContext>();
            this.storefrontContext.ShopName.Returns(this.fixture.Create<string>());
            
            this.analyticsManager = Substitute.For<IAnalyticsManager>();
        }

        [Fact]
        public void RaiseProductVisitedEvent_IfProductIsNull_ShouldThrowException()
        {
            //arrange
            ProductModel product = null;
            var manager = Substitute.For<IAnalyticsManager>();
            var service = new CommerceAnalyticsService(analyticsManager, storefrontContext);

            //act & assert
            Assert.ThrowsAny<ArgumentNullException>(() => { service.RaiseProductVisitedEvent(product); });
            manager.Received(0).VisitedProductDetailsPage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void RaiseCategoryVisitedEvent_IfCategoryIsNull_ShouldThrowException()
        {
            //arrange
            CategoryModel category = null;
            var manager = Substitute.For<IAnalyticsManager>();
            var service = new CommerceAnalyticsService(analyticsManager, storefrontContext);

            //act & assert
            Assert.ThrowsAny<ArgumentNullException>(() => { service.RaiseCategoryVisitedEvent(category); });
            manager.Received(0).VisitedCategoryPage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
}