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

namespace HCA.Foundation.Connect.Tests.Services.Search
{
    using System;
    using System.Linq;

    using Connect.Builders.Products;
    using Connect.Builders.Search;
    using Connect.Mappers.Search;
    using Connect.Services.Search;

    using Models.Catalog;
    using Models.Search;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Providers.Search;

    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb.AutoFixture;

    using Xunit;

    public class SearchServiceTests
    {
        private readonly IFixture fixture;

        private readonly ISearchQueryBuilder queryBuilder;

        private readonly ISearchMapper searchMapper;

        private readonly ISearchResponseProvider searchResponseProvider;

        private readonly ISearchResultProvider searchResultProvider;
        private readonly IProductBuilder<Item> productBuilder;
        private readonly ISearchService searchService;

        public SearchServiceTests()
        {
            this.fixture = new Fixture().Customize(new AutoDbCustomization());

            this.searchMapper = Substitute.For<ISearchMapper>();
            this.searchResponseProvider = Substitute.For<ISearchResponseProvider>();
            this.productBuilder = Substitute.For<IProductBuilder<Item>>();
            this.searchResultProvider = Substitute.For<ISearchResultProvider>();
            this.queryBuilder = Substitute.For<ISearchQueryBuilder>();

            this.searchService = new SearchService(
                this.searchMapper,
                this.searchResponseProvider,
                this.productBuilder,
                this.searchResultProvider,
                this.queryBuilder);
        }

        [Fact]
        public void GetCategoryItem_IfArgumentNull_ShouldThrowArgumentNullException()
        {
            //act & assert
            Assert.Throws<ArgumentNullException>(() => this.searchService.GetCategoryItem(null));
            this.searchResultProvider.Received(0)
                .GetSearchResult(
                    Arg.Any<Func<IQueryable<CommerceSellableItemSearchResultItem>,
                        IQueryable<CommerceSellableItemSearchResultItem>>>());
        }

        [Fact]
        public void GetCategoryItem_IfCategoryNotFound_ShouldReturnNull()
        {
            //arrange
            var results = this.searchService.GetCategoryItem(this.fixture.Create<string>());

            //act & assert
            Assert.Null(results);
            this.searchResultProvider.Received(1)
                .GetSearchResult(
                    Arg.Any<Func<IQueryable<CommerceSellableItemSearchResultItem>,
                        IQueryable<CommerceSellableItemSearchResultItem>>>());
        }

        [Fact]
        public void GetProductItem_IfArgumentNull_ShouldThrowArgumentNullException()
        {
            //act & assert
            Assert.Throws<ArgumentNullException>(() => this.searchService.GetProductItem(null));
            this.searchResultProvider.Received(0)
                .GetSearchResult(
                    Arg.Any<Func<IQueryable<CommerceSellableItemSearchResultItem>,
                        IQueryable<CommerceSellableItemSearchResultItem>>>());
        }

        [Fact]
        public void GetProductItem_IfCategoryIsFound_ShouldReturnItem()
        {
            //arrange
            var commerceItem = Substitute.For<CommerceSellableItemSearchResultItem>();
            commerceItem.GetItem().Returns(this.fixture.Create<Item>());

            this.searchResultProvider.GetSearchResult(
                    Arg.Any<Func<IQueryable<CommerceSellableItemSearchResultItem>,
                        IQueryable<CommerceSellableItemSearchResultItem>>>())
                .Returns(commerceItem);

            //act
            var results = this.searchService.GetCategoryItem(this.fixture.Create<string>());

            //assert
            Assert.NotNull(results);
            Assert.False(ID.IsNullOrEmpty(results.ID));
            this.searchResultProvider.Received(1)
                .GetSearchResult(
                    Arg.Any<Func<IQueryable<CommerceSellableItemSearchResultItem>,
                        IQueryable<CommerceSellableItemSearchResultItem>>>());
        }

        [Fact]
        public void GetProductItem_IfProductIsFound_ShouldReturnItem()
        {
            //arrange
            var commerceItem = Substitute.For<CommerceSellableItemSearchResultItem>();
            commerceItem.GetItem().Returns(this.fixture.Create<Item>());

            this.searchResultProvider.GetSearchResult(
                    Arg.Any<Func<IQueryable<CommerceSellableItemSearchResultItem>,
                        IQueryable<CommerceSellableItemSearchResultItem>>>())
                .Returns(commerceItem);

            //act
            var results = this.searchService.GetProductItem(this.fixture.Create<string>());

            //assert
            Assert.NotNull(results);
            Assert.False(ID.IsNullOrEmpty(results.ID));
            this.searchResultProvider.Received(1)
                .GetSearchResult(
                    Arg.Any<Func<IQueryable<CommerceSellableItemSearchResultItem>,
                        IQueryable<CommerceSellableItemSearchResultItem>>>());
        }

        [Fact]
        public void GetProductItem_IfProductNotFound_ShouldReturnNull()
        {
            //act
            var results = this.searchService.GetProductItem(this.fixture.Create<string>());

            //assert
            Assert.Null(results);
            this.searchResultProvider.Received(1)
                .GetSearchResult(
                    Arg.Any<Func<IQueryable<CommerceSellableItemSearchResultItem>,
                        IQueryable<CommerceSellableItemSearchResultItem>>>());
        }

        [Fact]
        public void GetProducts_IfArgumentNull_ShouldThrowArgumentNullException()
        {
            //act & assert
            Assert.Throws<ArgumentNullException>(() => this.searchService.GetProducts(null));
        }

        [Fact]
        public void GetProducts_IfSearchResponseIsNotNull_ShouldReturnNotEmptySearchResults()
        {
            //arrange
            var pageNumber = this.fixture.Create<int>();
            var searchOptions = new SearchOptions
            {
                StartPageIndex = pageNumber
            };
            var searchResponse = this.fixture.Create<SearchResponse>();

            this.searchMapper.Map<SearchOptions, CommerceSearchOptions>(Arg.Any<SearchOptions>())
                .Returns(
                    new CommerceSearchOptions
                    {
                        StartPageIndex = searchOptions.StartPageIndex
                    });
            this.searchMapper.Map<SearchResponse, Models.Search.SearchResults<Product>>(searchResponse)
                .Returns(
                    new Models.Search.SearchResults<Product>
                    {
                        TotalItemCount = searchResponse.TotalItemCount
                    });

            this.searchResponseProvider
                .CreateFromSearchResultsItems(
                    Arg.Any<CommerceSearchOptions>(),
                    Arg.Any<Sitecore.ContentSearch.Linq.SearchResults<CommerceSellableItemSearchResultItem>>())
                .Returns(searchResponse);

            //act
            var results = this.searchService.GetProducts(searchOptions);

            //assert
            Assert.Equal(searchResponse.TotalItemCount, results.TotalItemCount);

            this.searchMapper.Received(1).Map<SearchResponse, Models.Search.SearchResults<Product>>(searchResponse);
        }
    }
}