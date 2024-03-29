﻿//    Copyright 2020 EPAM Systems, Inc.
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

namespace HCA.Foundation.Commerce.Tests.Converters.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Commerce.Converters.Search;
    using Commerce.Mappers.Search;

    using Foundation.Search.Models.Common;
    using Foundation.Search.Providers;

    using Models.Entities.Search;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    using Facet = Models.Entities.Search.Facet;
    using SortDirection = Foundation.Search.Models.Common.SortDirection;

    public class SearchOptionsConverterTests
    {
        private readonly ISearchMapper searchMapper;
        private readonly ISearchOptionsConverter converter;
        private readonly ISearchSettingsProvider searchSettingsProvider;
        private readonly IFixture fixture;

        public SearchOptionsConverterTests()
        {
            this.searchMapper = Substitute.For<ISearchMapper>();
            this.searchSettingsProvider = Substitute.For<ISearchSettingsProvider>();

            this.fixture = new Fixture();
            this.converter = new SettingsSearchOptionsConverter(this.searchMapper, this.searchSettingsProvider);
        }

        [Fact]
        public void Convert_IfAnyArgumentIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.converter.Convert(null));
        }

        [Fact]
        public void Convert_ShouldReturnSearchOptions()
        {
            //arrange
            var searchSettings = this.fixture.Create<SearchSettings>();
            this.searchSettingsProvider.GetSearchSettings().Returns(searchSettings);

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();

            // act
            var searchOptions = this.converter.Convert(productSearchOptions);

            // assert
            Assert.NotNull(searchOptions);
        }

        [Fact]
        public void Convert_ShouldMapSearchKeyword()
        {
            //arrange
            var searchSettings = this.fixture.Create<SearchSettings>();
            this.searchSettingsProvider.GetSearchSettings().Returns(searchSettings);

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();

            // act
            var searchOptions = this.converter.Convert(productSearchOptions);

            // assert
            Assert.True(searchOptions.SearchKeyword == productSearchOptions.SearchKeyword);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void
            Convert_IfSearchOptionsPageSizeIsLessThanZero_ShouldSetNumberOfItemsToReturnToSearchSettingsItemsPerPage(
                int pageSize)
        {
            // arrange
            var searchSettings = this.fixture.Create<SearchSettings>();
            this.searchSettingsProvider.GetSearchSettings().Returns(searchSettings);

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();
            productSearchOptions.PageSize = pageSize;

            // act 
            var searchOptions = this.converter.Convert(productSearchOptions);

            // assert
            Assert.True(searchOptions.NumberOfItemsToReturn == searchSettings.ItemsPerPage);
        }

        [Fact]
        public void Convert_IfSearchOptionsPageSizeIsMoreThanZero_ShouldSetNumberOfItemsToReturnToSearchOptionsPageSize()
        {
            //arrange
            var searchSettings = this.fixture.Create<SearchSettings>();
            this.searchSettingsProvider.GetSearchSettings().Returns(searchSettings);

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();
            productSearchOptions.PageSize = 1;

            // act 
            var searchOptions = this.converter.Convert(productSearchOptions);

            // assert
            Assert.True(searchOptions.NumberOfItemsToReturn == productSearchOptions.PageSize);
        }

        [Fact]
        public void Convert_ShouldSetCategoryIdToSearchOptionsCategoryId()
        {
            //arrange
            var searchSettings = this.fixture.Create<SearchSettings>();
            this.searchSettingsProvider.GetSearchSettings().Returns(searchSettings);

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();

            // act 
            var searchOptions = this.converter.Convert(productSearchOptions);

            // assert
            Assert.True(searchOptions.CategoryId == productSearchOptions.CategoryId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Convert_ShouldSetStartPageIndexIsEqualToPageNumber(int pageNumber)
        {
            //arrange
            var searchSettings = this.fixture.Create<SearchSettings>();
            this.searchSettingsProvider.GetSearchSettings().Returns(searchSettings);

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();
            productSearchOptions.PageNumber = pageNumber;

            // act 
            var searchOptions = this.converter.Convert(productSearchOptions);

            // assert
            Assert.True(searchOptions.StartPageIndex == pageNumber);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void Convert_IfSortFieldIsNullOrEmptyInSearchOptions_ShouldSetFirstOrDefaultSortFieldFromSearchSettings(
            int count)
        {
            //arrange

            var searchSettings = this.fixture.Create<SearchSettings>();
            searchSettings.SortFieldNames = Enumerable.Repeat(this.fixture.Create<string>(), count);
            this.searchSettingsProvider.GetSearchSettings().Returns(searchSettings);

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = string.Empty;

            // act 
            var searchOptions = this.converter.Convert(productSearchOptions);

            // assert
            Assert.True(
                searchOptions.SortField == null ||
                searchOptions.SortField == searchSettings.SortFieldNames.FirstOrDefault());
        }

        [Fact]
        public void Convert_IfSortFieldIsNotEmptyInSearchOptions_ShouldSetSortFieldFromSearchOptions()
        {
            //arrange
            var searchSettings = this.fixture.Create<SearchSettings>();
            this.searchSettingsProvider.GetSearchSettings().Returns(searchSettings);

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();

            // act 
            var searchOptions = this.converter.Convert(productSearchOptions);

            // assert
            Assert.True(searchOptions.SortField == productSearchOptions.SortField);
        }

        [Theory]
        [InlineData(SortDirection.Asc)]
        [InlineData(SortDirection.Desc)]
        public void Convert_ShouldMapSortDirection(object sortDirection)
        {
            //arrange
            var searchSettings = this.fixture.Create<SearchSettings>();
            this.searchSettingsProvider.GetSearchSettings().Returns(searchSettings);

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();
            productSearchOptions.SortDirection = (SortDirection)sortDirection;

            // act 
            var searchOptions = this.converter.Convert(productSearchOptions);

            // assert
            Assert.True(
                (searchOptions.SortDirection == Foundation.Search.Models.Common.SortDirection.Asc &&
                    productSearchOptions.SortDirection == SortDirection.Asc) ||
                (searchOptions.SortDirection == Foundation.Search.Models.Common.SortDirection.Desc &&
                    productSearchOptions.SortDirection == SortDirection.Desc));
        }

        [Fact]
        public void Convert_ShouldSetSearchSettingsAndSearchOptionsFacetIntersectionByNameToFacets()
        {
            //arrange
            var searchSettings = this.fixture.Create<SearchSettings>();
            var commonFacet = this.fixture.Create<Foundation.Search.Models.Common.Facet>();
            searchSettings.Facets = this.ArrangeSearchSettingsFacets(commonFacet.Name, commonFacet.DisplayName);

            this.searchSettingsProvider.GetSearchSettings().Returns(searchSettings);

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();
            productSearchOptions.SortField = searchSettings.SortFieldNames?.FirstOrDefault();
            productSearchOptions.Facets = this.ArrangeSearchOptionsFacets(commonFacet.Name);

            this.searchMapper.Map<IEnumerable<Facet>, IEnumerable<Foundation.Search.Models.Common.Facet>>(Arg.Any<IEnumerable<Facet>>())
                .Returns(
                    args =>
                    {
                        var facets = args[0] as IEnumerable<Facet>;
                        return facets?.Select(
                            facet => new Foundation.Search.Models.Common.Facet
                            {
                                Name = facet.Name,
                                DisplayName = facet.DisplayName,
                                Values = facet.Values
                            });
                    });

            // act 
            var searchOptions = this.converter.Convert(productSearchOptions);

            // assert
            Assert.NotNull(searchOptions.Facets);
            Assert.True(searchOptions.Facets.Count() == 1);
            Assert.True(searchOptions.Facets.FirstOrDefault()?.Name == commonFacet.Name);
            Assert.True(searchOptions.Facets.FirstOrDefault()?.DisplayName == commonFacet.DisplayName);
            Assert.NotNull(searchOptions.Facets.FirstOrDefault(facet => facet.Name == commonFacet.Name)?.Values);
            Assert.NotEmpty(searchOptions.Facets.FirstOrDefault(facet => facet.Name == commonFacet.Name)?.Values);
        }

        [Fact]
        public void Convert_IfSortFieldIsNonExistent_ShouldThrowException()
        {
            // arrange
            var searchSettings = this.fixture.Create<SearchSettings>();
            this.searchSettingsProvider.GetSearchSettings().Returns(searchSettings);

            var productSearchOptions = this.fixture.Create<ProductSearchOptions>();

            // act & assert
            Assert.Throws<Exception>(() => this.converter.Convert(productSearchOptions));
        }

        private IEnumerable<Foundation.Search.Models.Common.Facet> ArrangeSearchSettingsFacets(
            string commonFacetName,
            string commonFacetDisplayName)
        {
            var searchSettingsFacets = this.fixture.Create<IList<Foundation.Search.Models.Common.Facet>>().ToList();
            searchSettingsFacets.Add(
                new Foundation.Search.Models.Common.Facet
                {
                    Name = commonFacetName,
                    DisplayName = commonFacetDisplayName
                });
            return searchSettingsFacets;
        }

        private IEnumerable<Facet> ArrangeSearchOptionsFacets(string commonFacetName)
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