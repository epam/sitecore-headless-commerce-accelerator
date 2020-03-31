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

namespace Wooli.Foundation.Commerce.Tests.Services.Search
{
    using System;

    using Base.Models;

    using Builders.Search;

    using Commerce.Builders.Search;
    using Commerce.Mappers.Search;
    using Commerce.Services.Search;

    using Connect.Managers.Search;
    using Connect.Models.Catalog;
    using Connect.Models.Search;
    using Connect.Providers.Search;

    using Models.Entities.Search;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    public class ProductSearchServiceTests
    {
        private readonly ISearchManagerV2 searchManager;
        private readonly ISearchMapper searchMapper;
        private readonly ISearchOptionsBuilder searchOptionsBuilder;
        private readonly ISearchSettingsProvider searchSettingsProvider;
        private readonly IProductSearchService service;
        private readonly IFixture fixture;

        public ProductSearchServiceTests()
        {
            this.searchManager = Substitute.For<ISearchManagerV2>();
            this.searchMapper = Substitute.For<ISearchMapper>();
            this.searchOptionsBuilder = Substitute.For<ISearchOptionsBuilder>();
            this.searchSettingsProvider = Substitute.For<ISearchSettingsProvider>();

            this.service = new ProductSearchService(this.searchSettingsProvider, this.searchOptionsBuilder, this.searchManager, this.searchMapper);
            this.fixture = new Fixture();
        }

        [Fact]
        public void GetProducts_IfArgumentIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.service.GetProducts(null));
        }

        [Fact] 
        public void GetProducts_ShouldCallSearchSettingsProvider()
        {
            // act 
            this.service.GetProducts(this.fixture.Create<ProductSearchOptions>());

            // assert
            this.searchSettingsProvider.Received(1)
                .GetSearchSettings();
        }

        [Fact]
        public void GetProducts_ShouldCallSearchOptionsBuilder()
        {
            // act 
            this.service.GetProducts(this.fixture.Create<ProductSearchOptions>());

            // assert
            this.searchOptionsBuilder.Received(1)
                .Build(Arg.Any<SearchSettings>(), Arg.Any<ProductSearchOptions>());
        }

        [Fact]
        public void GetProducts_ShouldCallGetProducts()
        {
            // act 
            this.service.GetProducts(this.fixture.Create<ProductSearchOptions>());

            // assert
            this.searchManager.Received(1)
                .GetProducts(Arg.Any<SearchOptions>());
        }

        [Fact]
        public void GetProducts_ShouldMapSearchResultsToProductSearchResults()
        {
            // act 
            this.service.GetProducts(this.fixture.Create<ProductSearchOptions>());

            // assert
            this.searchMapper.Received(1)
                .Map<SearchResultsV2<Product>, ProductSearchResults>(Arg.Any<SearchResultsV2<Product>>());
        }

        [Fact]
        public void GetProducts_ShouldReturnResultModel()
        {
            //act
            var result = this.service.GetProducts(this.fixture.Create<ProductSearchOptions>());

            //assert
            Assert.IsType<Result<ProductSearchResults>>(result);
        }
    }
}