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

namespace HCA.Foundation.Connect.Tests.Builders.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoFixture;

    using Base.Context;

    using Connect.Builders.Search;

    using Loaders;

    using NSubstitute;



    using Sitecore.Commerce.Engine.Connect.Interfaces;
    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.Data;
    using Sitecore.FakeDb.AutoFixture;

    using Xunit;

    public class SearchQueryBuilderTests
    {
        private readonly SearchQueryBuilder builder;
        private readonly ICommerceSearchManager commerceSearchManager;
        private readonly IFixture fixture;

        public SearchQueryBuilderTests()
        {
            this.fixture = new Fixture().Customize(new AutoDbCustomization());

            var sitecoreContext = Substitute.For<ISitecoreContext>();

            this.commerceSearchManager = Substitute.For<ICommerceSearchManager>();
            var commerceTypeLoader = Substitute.For<ICommerceTypeLoader>();
            commerceTypeLoader.CreateInstance<ICommerceSearchManager>().Returns(this.commerceSearchManager);

            this.builder = new SearchQueryBuilder(sitecoreContext, commerceTypeLoader);
        }

        [Fact]
        public void BuildCategoryQuery_IfSomeArgumentsNull_ShouldThrowArgumentNullException()
        {
            //act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.builder.BuildCategoryQuery(null, this.fixture.Create<string>()));
            Assert.Throws<ArgumentNullException>(
                () => this.builder.BuildCategoryQuery(
                    new List<CommerceSellableItemSearchResultItem>().AsQueryable(),
                    null));
        }

        [Fact]
        public void BuildProductQuery_IfSomeArgumentsNull_ShouldThrowArgumentNullException()
        {
            //act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.builder.BuildProductQuery(null, this.fixture.Create<string>()));
            Assert.Throws<ArgumentNullException>(
                () => this.builder.BuildProductQuery(
                    new List<CommerceSellableItemSearchResultItem>().AsQueryable(),
                    null));
        }

        [Fact]
        public void BuildProductsQuery_IfSomeArgumentsNull_ShouldThrowArgumentNullException()
        {
            //act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.builder.BuildProductsQuery(
                    null,
                    this.fixture.Create<string>(),
                    this.fixture.Create<ID>(),
                    this.fixture.Create<CommerceSearchOptions>()));
            Assert.Throws<ArgumentNullException>(
                () => this.builder.BuildProductsQuery(
                    new List<CommerceSellableItemSearchResultItem>().AsQueryable(),
                    this.fixture.Create<string>(),
                    this.fixture.Create<ID>(),
                    null));
        }

        [Fact]
        public void BuildProductsQuery_ShouldApplySearchOptionsToSearchQuery()
        {
            //act
            this.builder.BuildProductsQuery(
                new List<CommerceSellableItemSearchResultItem>().AsQueryable(),
                this.fixture.Create<string>(),
                this.fixture.Create<ID>(),
                this.fixture.Create<CommerceSearchOptions>());

            //assert
            this.commerceSearchManager.Received(1)
                .AddSearchOptionsToQuery(
                    Arg.Any<IQueryable<CommerceSellableItemSearchResultItem>>(),
                    Arg.Any<CommerceSearchOptions>());
        }
    }
}