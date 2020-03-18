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

namespace Wooli.Feature.Catalog.Tests.Controllers
{
    using System;

    using Catalog.Controllers;

    using Foundation.Commerce.Models;
    using Foundation.Commerce.Models.Entities;
    using Foundation.Commerce.Models.Entities.Search;
    using Foundation.Commerce.Services.Catalog;

    using Mappers;

    using Models.Requests;

    using NSubstitute;

    using Xunit;

    public class SearchControllerTests
    {
        private readonly IProductSearchService productSearchService;

        private readonly ICatalogEntityMapper catalogEntityMapper;

        private readonly SearchController controller;

        public SearchControllerTests()
        {
            this.productSearchService = Substitute.For<IProductSearchService>();
            this.catalogEntityMapper = Substitute.For<ICatalogEntityMapper>();

            this.controller = Substitute.For<SearchController>(this.productSearchService, this.catalogEntityMapper);
        }


        [Fact]
        public void GetProducts_ShouldCallExecuteMethod()
        {
            // act
            this.controller.GetProducts(new ProductsSearchRequest());

            // assert
            this.controller.Received().Execute(Arg.Any<Func<Result<ProductsSearchResult>>>());
        }
    }
}