namespace Wooli.Foundation.Connect.Tests.Models.Mappings
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Connect.Models;

    using Ploeh.AutoFixture;

    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.SearchTypes;

    using Wooli.Foundation.Connect.Models.Mappings;
    using Wooli.Foundation.Connect.Models.Search;
    using Xunit;

    using FacetValue = Connect.Models.Search.FacetValue;

    public class SearchProfileTests
    {
        private readonly IMapper mapper;

        private readonly IFixture fixture;

        public SearchProfileTests()
        {
            var configuration = new MapperConfiguration(
                cfg => { cfg.AddProfile<SearchProfile>(); });

            this.mapper = new Mapper(configuration);

            this.fixture = new Fixture();
        }

        [Fact]
        public void Configuration_ShouldBeValid()
        {
            // arrange
            var configuration = new MapperConfiguration(
                cfg => { cfg.AddProfile<SearchProfile>(); });

            // act, assert
            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_IfCommerceQueryFacetObjectIsValid_ShouldReturnValidQueryFacetObject()
        {
            // arrange
            var commerceQueryFacet = this.fixture.Create<CommerceQueryFacet>();

            // act
            var queryFacet = this.mapper.Map<CommerceQueryFacet, QueryFacet>(commerceQueryFacet);

            // assert
            Assert.Equal(commerceQueryFacet.DisplayName, queryFacet.DisplayName);
            Assert.Equal(commerceQueryFacet.Name, queryFacet.Name);
        }

        [Fact]
        public void Map_IfCommerceQueryFacetFoundValuesIsNull_ShouldReturnEmptyFoundValues()
        {
            // arrange
            var commerceQueryFacet = this.fixture.Create<CommerceQueryFacet>();
            commerceQueryFacet.FoundValues = null;

            // act
            var queryFacet = this.mapper.Map<CommerceQueryFacet, QueryFacet>(commerceQueryFacet);

            // assert
            Assert.NotNull(queryFacet.FoundValues);
            Assert.Empty(queryFacet.FoundValues);
        }

        [Fact]
        public void Map_IfLinqFacetValueObjectIsValid_ShouldReturnValidFacetValueObject()
        {
            // arrange
            var linqFacetValue = new Sitecore.ContentSearch.Linq.FacetValue(
                this.fixture.Create<string>(),
                this.fixture.Create<int>());

            // act
            var facetValue = this.mapper.Map<Sitecore.ContentSearch.Linq.FacetValue, FacetValue>(linqFacetValue);

            // assert
            Assert.NotNull(facetValue);
            Assert.Equal(linqFacetValue.Name, facetValue.Name);
            Assert.Equal(linqFacetValue.AggregateCount, facetValue.AggregateCount);
        }

        [Fact]
        public void Map_IfSearchResponseIsValid_ShouldReturnValidSearchResultsV2Object()
        {
            // arrange
            var searchOptions = this.fixture.Create<CommerceSearchOptions>();
            var sitecoreSearchResult = new SearchResults<SearchResultItem>(
                Enumerable.Empty<SearchHit<SearchResultItem>>(),
                this.fixture.Create<int>());
            var searchResponse = SearchResponse.CreateFromSearchResultsItems(searchOptions, sitecoreSearchResult);

            // act
            var searchResult = this.mapper.Map<SearchResponse, SearchResultsV2<Product>>(searchResponse);

            // assert
            Assert.Equal(searchResponse.TotalItemCount, searchResult.TotalItemCount);
        }
    }
}
