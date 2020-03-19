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

namespace Wooli.Foundation.Connect.Tests.Mappers.Search
{
    using Connect.Mappers.Search;

    using Models;

    using Ploeh.AutoFixture;

    using Sitecore.Commerce.Engine.Connect.Search.Models;

    using Xunit;

    public class SearchMapperTests
    {
        private readonly IFixture fixture;
        private readonly ISearchMapper searchMapper;

        public SearchMapperTests()
        {
            this.fixture = new Fixture();
            this.searchMapper = new SearchMapper();
        }

        [Fact]
        public void SearchMapper_ShouldHaveValidConfiguration()
        {
            //assert
            this.searchMapper.Configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void MapAndReverseMap_ForSearchOptionsToCommerceSearchOptions_ShouldReturnNotNull()
        {
            //act & assert
            Assert.NotNull(this.searchMapper.Map<SearchOptions, CommerceSearchOptions>(this.fixture.Create<SearchOptions>()));
            Assert.NotNull(this.searchMapper.Map<CommerceSearchOptions, SearchOptions>(this.fixture.Create<CommerceSearchOptions>()));
        }
    }
}