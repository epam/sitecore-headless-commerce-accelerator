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

namespace Wooli.Foundation.Connect.Tests.Managers.Search
{
    using System;
    using System.Linq;

    using Base.Context;

    using Connect.Managers.Search;
    using Connect.Mappers.Search;

    using Loaders;

    using Models;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Providers.Search;

    using Sitecore.Commerce.Engine.Connect.Interfaces;
    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb.AutoFixture;

    using Xunit;

    public class SearchManagerV2Tests
    {
        private readonly IFixture fixture;

        private readonly ISearchMapper searchMapper;
        private readonly ISitecoreContext sitecoreContext;
        private readonly ICommerceTypeLoader commerceTypeLoader;
        private readonly ISearchResponseProvider searchResponseProvider;
        private readonly ICommerceSearchManager comerceSearchManager;
        private readonly ISearchResultProvider<CommerceSellableItemSearchResultItem> searchResultProvider;

        private readonly ISearchManagerV2 searchManager;

        public SearchManagerV2Tests()
        {
            this.fixture = new Fixture().Customize(new AutoDbCustomization());

            this.searchMapper = Substitute.For<ISearchMapper>();
            this.searchMapper.Map<SearchOptions, CommerceSearchOptions>(Arg.Any<SearchOptions>())
                .Returns(this.fixture.Create<CommerceSearchOptions>());

            this.sitecoreContext = Substitute.For<ISitecoreContext>();
            this.searchResponseProvider = Substitute.For<ISearchResponseProvider>();
            this.comerceSearchManager = Substitute.For<ICommerceSearchManager>();
            this.searchResultProvider = Substitute.For<ISearchResultProvider<CommerceSellableItemSearchResultItem>>();

            this.commerceTypeLoader = Substitute.For<ICommerceTypeLoader>();
            this.commerceTypeLoader.CreateInstance<ICommerceSearchManager>()
                .Returns(this.comerceSearchManager);

            this.searchManager = new SearchManagerV2(
                this.searchMapper,
                this.sitecoreContext,
                this.commerceTypeLoader,
                this.searchResponseProvider,
                this.searchResultProvider);
        }

        #region GetProducts

        [Fact]
        public void GetProducts_IfArgumentNull_ShouldThrowArgumentNullException()
        {
            //act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.searchManager.GetProducts(null, this.fixture.Create<SearchOptions>()));
            Assert.Throws<ArgumentNullException>(
                () => this.searchManager.GetProducts(this.fixture.Create<string>(), null));

            this.searchMapper.Received(0).Map<SearchOptions, CommerceSearchOptions>(Arg.Any<SearchOptions>());
            this.searchResultProvider.Received(0)
                .GetSearchResult(
                    Arg.Any<Func<IQueryable<CommerceSellableItemSearchResultItem>,
                        IQueryable<CommerceSellableItemSearchResultItem>>>());
            this.searchResponseProvider.Received(0).CreateFromSearchResultsItems(Arg.Any<CommerceSearchOptions>(), Arg.Any<SearchResults<CommerceSellableItemSearchResultItem>>());
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

            this.searchMapper.Map<SearchOptions, CommerceSearchOptions>(Arg.Any<SearchOptions>())
                .Returns(
                    new CommerceSearchOptions
                    {
                        StartPageIndex = searchOptions.StartPageIndex
                    });

            this.searchResponseProvider
                .CreateFromSearchResultsItems(
                    Arg.Any<CommerceSearchOptions>(),
                    Arg.Any<SearchResults<CommerceSellableItemSearchResultItem>>())
                .Returns(this.fixture.Create<SearchResponse>());

            //act
            var results = this.searchManager.GetProducts(
                this.fixture.Create<string>(),
                searchOptions);

            //assert
            Assert.True(results.CurrentPageNumber == pageNumber);
        }

        [Fact]
        public void GetProducts_IfSearchResponseIsNull_ShouldReturnEmptySearchResults()
        {
            //arrange
            this.searchResponseProvider
                .CreateFromSearchResultsItems(
                    Arg.Any<CommerceSearchOptions>(),
                    Arg.Any<SearchResults<CommerceSellableItemSearchResultItem>>())
                .Returns(null as SearchResponse);

            //act
            var results = this.searchManager.GetProducts(
                this.fixture.Create<string>(),
                this.fixture.Create<SearchOptions>());

            //assert
            Assert.True(results.TotalItemCount == 0);
            Assert.Empty(results.SearchResultItems);
        }

        [Fact]
        public void GetProducts_ShouldReturnNotNullSearchResults()
        {
            //arrange
            var results = this.searchManager.GetProducts(
                this.fixture.Create<string>(),
                this.fixture.Create<SearchOptions>());

            //act & assert
            Assert.NotNull(results);
        }

        #endregion

        #region GetProductItem

        [Fact]
        public void GetProductItem_IfArgumentNull_ShouldThrowArgumentNullException()
        {
            //act & assert
            Assert.Throws<ArgumentNullException>(() => this.searchManager.GetProductItem(null));
            this.searchResultProvider.Received(0)
                .GetSearchResult(
                    Arg.Any<Func<IQueryable<CommerceSellableItemSearchResultItem>,
                        IQueryable<CommerceSellableItemSearchResultItem>>>());
        }

        [Fact]
        public void GetProductItem_IfProductNotFound_ShouldReturnNull()
        {
            //arrange
            var results = this.searchManager.GetProductItem(
                this.fixture.Create<string>());

            //act & assert
            Assert.Null(results);
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
            var results = this.searchManager.GetProductItem(
                this.fixture.Create<string>());

            //assert
            Assert.NotNull(results);
            Assert.False(ID.IsNullOrEmpty(results.ID));
            this.searchResultProvider.Received(1)
                .GetSearchResult(
                    Arg.Any<Func<IQueryable<CommerceSellableItemSearchResultItem>,
                        IQueryable<CommerceSellableItemSearchResultItem>>>());
        }

        #endregion

        #region GetCategoryItem

        [Fact]
        public void GetCategoryItem_IfArgumentNull_ShouldThrowArgumentNullException()
        {
            //act & assert
            Assert.Throws<ArgumentNullException>(() => this.searchManager.GetCategoryItem(null));
            this.searchResultProvider.Received(0)
                .GetSearchResult(
                    Arg.Any<Func<IQueryable<CommerceSellableItemSearchResultItem>,
                        IQueryable<CommerceSellableItemSearchResultItem>>>());
        }
        
        [Fact]
        public void GetCategoryItem_IfCategoryNotFound_ShouldReturnNull()
        {
            //arrange
            var results = this.searchManager.GetCategoryItem(
                this.fixture.Create<string>());

            //act & assert
            Assert.Null(results);
            this.searchResultProvider.Received(1)
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
            var results = this.searchManager.GetCategoryItem(
                this.fixture.Create<string>());

            //assert
            Assert.NotNull(results);
            Assert.False(ID.IsNullOrEmpty(results.ID));
            this.searchResultProvider.Received(1)
                .GetSearchResult(
                    Arg.Any<Func<IQueryable<CommerceSellableItemSearchResultItem>,
                        IQueryable<CommerceSellableItemSearchResultItem>>>());
        }

        #endregion
    }
}