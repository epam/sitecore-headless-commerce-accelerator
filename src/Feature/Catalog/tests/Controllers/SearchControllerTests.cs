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

namespace HCA.Feature.Catalog.Tests.Controllers
{
    using System;

    using Catalog.Controllers;
    using Catalog.Mappers;

    using Foundation.Base.Models.Result;
    using Foundation.Commerce.Models.Entities.Search;
    using Foundation.Commerce.Services.Search;

    using Models.Requests.Search;

    using NSubstitute;

    using Xunit;

    public class SearchControllerTests
    {
        private readonly SearchController controller;

        private readonly IProductSearchService productSearchService;

        private readonly IProductSuggestionService productSuggestionService;

        private readonly ISearchMapper searchMapper;

        public SearchControllerTests()
        {
            this.productSearchService = Substitute.For<IProductSearchService>();
            this.productSuggestionService = Substitute.For<IProductSuggestionService>();
            this.searchMapper = Substitute.For<ISearchMapper>();

            this.controller = Substitute.For<SearchController>(this.productSearchService, this.productSuggestionService, this.searchMapper);
        }

        [Fact]
        public void SearchProducts_ShouldCallExecuteMethod()
        {
            // act
            this.controller.SearchProducts(new ProductsSearchRequest());

            // assert
            this.controller.Received().Execute(Arg.Any<Func<Result<ProductSearchResults>>>());
        }
    }
}