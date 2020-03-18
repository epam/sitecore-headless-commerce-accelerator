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

namespace Wooli.Feature.Catalog.Tests.Mappings
{
    using System.Collections.Specialized;

    using Foundation.Commerce.Models;
    using Foundation.Commerce.Models.Entities;
    using Foundation.Commerce.Models.Entities.Search;

    using Mappers;

    using Models.Requests;

    using Xunit;

    public class CatalogEntityMapperTests
    {
        private readonly ICatalogEntityMapper mapper;

        public CatalogEntityMapperTests()
        {
            this.mapper = new CatalogEntityMapper();
        }

        [Fact]
        public void Map_IfFacetValuesIsEmpty_ShouldReturnEmptyNameValueCollection()
        {
            // arrange
            var request = new ProductsSearchRequest
            {
                FacetValues = string.Empty
            };

            // act
            var options = this.mapper.Map<ProductsSearchRequest, ProductsSearchOptions>(request);

            // assert
            Assert.Empty(options.FacetValues);
        }

        [Fact]
        public void Map_IfProductsSearchRequestObjectIsValid_ShouldReturnValidProductsSearchOptionsObject()
        {
            // arrange
            var searchKeyword = "keyword";
            var page = 0;
            var pageSite = 1;
            var sortField = "field";
            var currentCatalogItemId = "cci";
            var currentItemId = "ci";
            var sortDirection = SortDirection.Asc;
            var facetName = "facetName";
            var facetValue = "facetValue";
            var facetValuesCollection = new NameValueCollection
            {
                { facetName, facetValue }
            };

            var request = new ProductsSearchRequest
            {
                SearchKeyword = searchKeyword,
                CurrentCatalogItemId = currentCatalogItemId,
                CurrentItemId = currentItemId,
                FacetValues = $"{facetName}={facetValue}",
                Page = page,
                PageSize = pageSite,
                SortDirection = sortDirection,
                SortField = sortField
            };

            // act
            var options = this.mapper.Map<ProductsSearchRequest, ProductsSearchOptions>(request);

            // assert
            Assert.Equal(searchKeyword, options.SearchKeyword);
            Assert.Equal(page, options.PageNumber);
            Assert.Equal(pageSite, options.PageSize);
            Assert.Equal(sortField, options.SortField);
            Assert.Equal(currentCatalogItemId, options.CurrentCatalogItemId);
            Assert.Equal(currentItemId, options.CurrentItemId);
            Assert.Equal(sortDirection, options.SortDirection);
            Assert.Equal(facetValuesCollection, options.FacetValues);
        }
    }
}