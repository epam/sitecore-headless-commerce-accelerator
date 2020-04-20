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

namespace HCA.Foundation.Connect.Tests.Context.Storefront
{
    using Connect.Context.Storefront;

    using Glass.Mapper.Sc;

    using Models;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Providers;

    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Multishop;
    using Sitecore.Commerce.Providers;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb.AutoFixture;

    using Xunit;

    public class StorefrontContextTests
    {
        private readonly StorefrontContext storefrontContext;

        private readonly ISitecoreService sitecoreService;

        private readonly IShopProvider shopProvider;
        private readonly IConnectStorefrontContext connectStorefrontContext;

        private readonly IFixture fixture;

        public StorefrontContextTests()
        {
            this.fixture = new Fixture().Customize(new AutoDbCustomization());

            this.sitecoreService = Substitute.For<ISitecoreService>();

            var connectEntityProvider = Substitute.For<IConnectEntityProvider>();
            this.shopProvider = Substitute.For<IShopProvider>();
            this.connectStorefrontContext = Substitute.For<IConnectStorefrontContext>();
            connectEntityProvider.GetConnectStorefrontContext().Returns(this.connectStorefrontContext);
            connectEntityProvider.GetShopProvider().Returns(this.shopProvider);

            this.storefrontContext = new StorefrontContext(this.sitecoreService, connectEntityProvider);
        }

        [Fact]
        public void ShopName_ShouldReturnShopNameFromProvider()
        {
            // arrange
            var shop = this.fixture.Create<Shop>();
            this.shopProvider.GetShop().Returns(shop);

            // act
            var shopName = this.storefrontContext.ShopName;

            // assert
            Assert.Equal(shopName, shop.Name);
            this.shopProvider.Received(1).GetShop();
        }

        [Fact]
        public void StorefrontConfiguration_ShouldReturnStorefrontFromConnectContext()
        {
            // arrange
            var storefrontItem = this.fixture.Create<Item>();
            this.connectStorefrontContext.StorefrontConfiguration.Returns(storefrontItem);
            var storefrontConfiguration = this.fixture.Create<StorefrontModel>();
            this.sitecoreService.GetItem<StorefrontModel>(Arg.Any<GetItemOptions>()).Returns(storefrontConfiguration);

            // act
            var storefront = this.storefrontContext.StorefrontConfiguration;

            // assert
            Assert.Equal(storefrontConfiguration, storefront);
            var assert = this.connectStorefrontContext.Received(1).StorefrontConfiguration;
            this.sitecoreService.Received(1).GetItem<StorefrontModel>(Arg.Any<GetItemOptions>());
        }
    }
}