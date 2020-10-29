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

namespace HCA.Foundation.Commerce.Tests.Builders.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Commerce.Builders.Search;
    using Commerce.Mappers.Search;

    using Models.Entities.Search;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    using Connect = Connect.Models.Search;

    public class SearchOptionsBuilderTests
    {
        private readonly ISearchMapper searchMapper;
        private readonly ISearchOptionsBuilder builder;
        private readonly IFixture fixture;

        public SearchOptionsBuilderTests()
        {
            this.searchMapper = Substitute.For<ISearchMapper>();
            this.fixture = new Fixture();
            this.builder = new SearchOptionsBuilder(this.searchMapper);
        }

        [Fact]
        public void Build_IfAnyArgumentIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.builder.Build(null, this.fixture.Create<ProductSearchOptions>()));
            Assert.Throws<ArgumentNullException>(
                () => this.builder.Build(this.fixture.Create<Connect.SearchSettings>(), null));
        }

        [Fact]
        public void Build_ShouldReturnSearchOptions()
        {
            //arrange
            var searchSettings = this.fixture.Create<Connect.SearchSettings>();
            
            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();

            // act
            var searchOptions = this.builder.Build(searchSettings, productSearchOptions);

            // assert
            Assert.NotNull(searchOptions);
        }

        [Fact]
        public void Build_ShouldMapSearchKeyword()
        {
            //arrange
            var searchSettings = this.fixture.Create<Connect.SearchSettings>();
            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();

            // act
            var searchOptions = this.builder.Build(searchSettings, productSearchOptions);

            // assert
            Assert.True(searchOptions.SearchKeyword == productSearchOptions.SearchKeyword);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void
            Build_IfSearchOptionsPageSizeIsLessThanZero_ShouldSetNumberOfItemsToReturnToSearchSettingsItemsPerPage(
                int pageSize)
        {
            // arrange
            var searchSettings = this.fixture.Create<Connect.SearchSettings>();

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();
            productSearchOptions.PageSize = pageSize;

            // act 
            var searchOptions = this.builder.Build(searchSettings, productSearchOptions);

            // assert
            Assert.True(searchOptions.NumberOfItemsToReturn == searchSettings.ItemsPerPage);
        }

        [Fact]
        public void Build_IfSearchOptionsPageSizeIsMoreThanZero_ShouldSetNumberOfItemsToReturnToSearchOptionsPageSize()
        {
            //arrange
            var searchSettings = this.fixture.Create<Connect.SearchSettings>();

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();
            productSearchOptions.PageSize = 1;

            // act 
            var searchOptions = this.builder.Build(searchSettings, productSearchOptions);

            // assert
            Assert.True(searchOptions.NumberOfItemsToReturn == productSearchOptions.PageSize);
        }

        [Fact]
        public void Build_ShouldSetCategoryIdToSearchOptionsCategoryId()
        {
            //arrange
            var searchSettings = this.fixture.Create<Connect.SearchSettings>();
            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();

            // act 
            var searchOptions = this.builder.Build(searchSettings, productSearchOptions);

            // assert
            Assert.True(searchOptions.CategoryId == productSearchOptions.CategoryId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Build_ShouldSetStartPageIndexIsEqualToPageNumber(int pageNumber)
        {
            //arrange
            var searchSettings = this.fixture.Create<Connect.SearchSettings>();

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();
            productSearchOptions.PageNumber = pageNumber;

            // act 
            var searchOptions = this.builder.Build(searchSettings, productSearchOptions);

            // assert
            Assert.True(searchOptions.StartPageIndex == pageNumber);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void Build_IfSortFieldIsNullOrEmptyInSearchOptions_ShouldSetFirstOrDefaultSortFieldFromSearchSettings(
            int count)
        {
            //arrange
            var searchSettings = this.fixture.Create<Connect.SearchSettings>();
            searchSettings.SortFieldNames = Enumerable.Repeat(this.fixture.Create<string>(), count);

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = string.Empty;

            // act 
            var searchOptions = this.builder.Build(searchSettings, productSearchOptions);

            // assert
            Assert.True(
                searchOptions.SortField == null ||
                searchOptions.SortField == searchSettings.SortFieldNames.FirstOrDefault());
        }

        [Fact]
        public void Build_IfSortFieldIsNotEmptyInSearchOptions_ShouldSetSortFieldFromSearchOptions()
        {
            //arrange
            var searchSettings = this.fixture.Create<Connect.SearchSettings>();
            
            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();

            // act 
            var searchOptions = this.builder.Build(searchSettings, productSearchOptions);

            // assert
            Assert.True(searchOptions.SortField == productSearchOptions.SortField);
        }

        [Theory]
        [InlineData(SortDirection.Asc)]
        [InlineData(SortDirection.Desc)]
        public void Build_ShouldMapSortDirection(object sortDirection)
        {
            //arrange
            var searchSettings = this.fixture.Create<Connect.SearchSettings>();

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();
            productSearchOptions.SortDirection = (SortDirection)sortDirection;

            // act 
            var searchOptions = this.builder.Build(searchSettings, productSearchOptions);

            // assert
            Assert.True(
                (searchOptions.SortDirection == Connect.SortDirection.Asc &&
                    productSearchOptions.SortDirection == SortDirection.Asc) ||
                (searchOptions.SortDirection == Connect.SortDirection.Desc &&
                    productSearchOptions.SortDirection == SortDirection.Desc));
        }

        [Fact]
        public void Build_ShouldSetSearchSettingsAndSearchOptionsFacetIntersectionByNameToFacets()
        {
            //arrange
            var commonFacet = this.fixture.Create<Connect.Facet>();

            var searchSettings = this.fixture.Create<Connect.SearchSettings>();
            searchSettings.Facets = this.ArrangeSearchSettingsFacets(commonFacet.Name, commonFacet.DisplayName);

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();
            productSearchOptions.Facets = this.ArrangeSearchOptionsFacets(commonFacet.Name, commonFacet.Values);

            this.searchMapper.Map<IEnumerable<Facet>, IEnumerable<Connect.Facet>>(Arg.Any<IEnumerable<Facet>>())
                .Returns(
                    args =>
                    {
                        var facets = args[0] as IEnumerable<Facet>;
                        return facets?.Select(
                            facet => new Connect.Facet
                            {
                                Name = facet.Name,
                                DisplayName = facet.DisplayName,
                                Values = facet.Values
                            });
                    });

            // act 
            var searchOptions = this.builder.Build(searchSettings, productSearchOptions);

            // assert
            Assert.NotNull(searchOptions.Facets);
            Assert.True(searchOptions.Facets.Count() == 1);
            Assert.True(searchOptions.Facets.FirstOrDefault().Name == commonFacet.Name);
            Assert.True(searchOptions.Facets.FirstOrDefault().DisplayName == commonFacet.DisplayName);
            Assert.NotNull(searchOptions.Facets.FirstOrDefault(facet => facet.Name == commonFacet.Name).Values);
            Assert.NotEmpty(searchOptions.Facets.FirstOrDefault(facet => facet.Name == commonFacet.Name).Values);
        }

        [Fact]
        public void Build_IfSortFieldIsNonExistent_ShouldThrowException()
        {
            // arrange
            var searchSettings = this.fixture.Create<Connect.SearchSettings>();
            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();

            // act & assert
            Assert.Throws<Exception>(() => this.builder.Build(searchSettings, productSearchOptions));
        }

        private IEnumerable<Connect.Facet> ArrangeSearchSettingsFacets(
            string commonFacetName,
            string commonFacetDisplayName)
        {
            var searchSettingsFacets = this.fixture.Create<IList<Connect.Facet>>().ToList();
            searchSettingsFacets.Add(
                new Connect.Facet
                {
                    Name = commonFacetName,
                    DisplayName = commonFacetDisplayName
                });
            return searchSettingsFacets;
        }

        private IEnumerable<Facet> ArrangeSearchOptionsFacets(string commonFacetName, IEnumerable<object> values)
        {
            var productSearchOptionsFacets = this.fixture.Create<IList<Facet>>().ToList();
            productSearchOptionsFacets.Add(
                new Facet
                {
                    Name = commonFacetName,
                    Values = this.fixture.Create<IList<object>>()
                });
            return productSearchOptionsFacets;
        }
    }
}