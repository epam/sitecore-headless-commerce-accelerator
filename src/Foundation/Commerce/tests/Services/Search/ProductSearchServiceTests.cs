////    Copyright 2020 EPAM Systems, Inc.
////
////    Licensed under the Apache License, Version 2.0 (the "License");
////    you may not use this file except in compliance with the License.
////    You may obtain a copy of the License at
////
////      http://www.apache.org/licenses/LICENSE-2.0
////
////    Unless required by applicable law or agreed to in writing, software
////    distributed under the License is distributed on an "AS IS" BASIS,
////    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
////    See the License for the specific language governing permissions and
////    limitations under the License.

//namespace HCA.Foundation.Commerce.Tests.Services.Search
//{
//    using System;

//    using Base.Models.Result;

//    using Commerce.Builders.Search;
//    using Commerce.Mappers.Search;
//    using Commerce.Services.Search;
//    using Commerce.Services.Search.Product;

//    using Connect.Context.Catalog;
//    using Connect.Models.Catalog;

//    using Foundation.Search.Models.Common;
//    using Foundation.Search.Models.Entities.Product;
//    using Foundation.Search.Providers;
//    using Foundation.Search.Services;

//    using Glass.Mapper.Sc;

//    using Models.Entities.Search;

//    using NSubstitute;

//    using Ploeh.AutoFixture;

//    using Sitecore.Data.Items;

//    using Xunit;

//    using Search = Foundation.Search.Models.Entities.Product;

//    public class ProductSearchServiceTests
//    {
//        private readonly ISearchService<ProductSearchResultItem, Search.ProductSearchOptions> searchManager;
//        private readonly ISearchMapper searchMapper;
//        private readonly ISitecoreService sitecoreService;
//        private readonly ICatalogContext catalogContext;
//        private readonly ISearchOptionsBuilder searchOptionsBuilder;
//        private readonly ISearchSettingsProvider searchSettingsProvider;
//        private readonly ISearchService service;
//        private readonly IFixture fixture;

//        public ProductSearchServiceTests()
//        {
//            this.searchManager = Substitute.For<ISearchService<ProductSearchResultItem, Search.ProductSearchOptions>>();
//            this.searchMapper = Substitute.For<ISearchMapper>();
//            this.sitecoreService = Substitute.For<ISitecoreService>();
//            this.catalogContext = Substitute.For<ICatalogContext>();
//            this.searchOptionsBuilder = Substitute.For<ISearchOptionsBuilder>();
//            this.searchSettingsProvider = Substitute.For<ISearchSettingsProvider>();

//            this.service = new SearchService(
//                this.searchSettingsProvider,
//                this.sitecoreService,
//                this.catalogContext,
//                this.searchOptionsBuilder,
//                this.searchManager,
//                this.searchMapper);
//            this.fixture = new Fixture();
//        }

//        [Fact]
//        public void GetProducts_IfArgumentIsNull_ShouldThrowArgumentNullException()
//        {
//            // act & assert
//            Assert.Throws<ArgumentNullException>(() => this.service.GetProducts(null));
//        }

//        [Fact]
//        public void GetProducts_ShouldCallSearchSettingsProvider()
//        {
//            // act
//            this.service.GetProducts(this.fixture.Create<Models.Entities.Search.ProductSearchOptions>());

//            // assert
//            this.searchSettingsProvider.Received(1)
//                .GetSearchSettings(Arg.Any<Item>());
//        }

//        [Fact]
//        public void GetProducts_ShouldCallSearchOptionsBuilder()
//        {
//            // act
//            this.service.GetProducts(this.fixture.Create<Models.Entities.Search.ProductSearchOptions>());

//            // assert
//            this.searchOptionsBuilder.Received(1)
//                .Build(Arg.Any<SearchSettings>(), Arg.Any<Models.Entities.Search.ProductSearchOptions>());
//        }

//        [Fact]
//        public void GetProducts_ShouldCallGetProducts()
//        {
//            // act
//            this.service.GetProducts(this.fixture.Create<Models.Entities.Search.ProductSearchOptions>());

//            // assert
//            this.searchManager.Received(1)
//                .GetSearchResults(Arg.Any<Search.ProductSearchOptions>());
//        }

//        [Fact]
//        public void GetProducts_ShouldMapSearchResultsToProductSearchResults()
//        {
//            // act
//            this.service.GetProducts(this.fixture.Create<Models.Entities.Search.ProductSearchOptions>());

//            // assert
//            this.searchMapper.Received(1)
//                .Map<SearchResults<Product>, ProductSearchResults>(Arg.Any<SearchResults<Product>>());
//        }

//        [Fact]
//        public void GetProducts_ShouldReturnResultModel()
//        {
//            //act
//            var result = this.service.GetProducts(this.fixture.Create<Models.Entities.Search.ProductSearchOptions>());

//            //assert
//            Assert.IsType<Result<ProductSearchResults>>(result);
//        }
//    }
//}