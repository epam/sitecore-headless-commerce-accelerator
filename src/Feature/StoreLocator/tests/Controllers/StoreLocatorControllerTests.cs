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

namespace HCA.Feature.StoreLocator.Tests.Controllers
{
    using System;

    using Foundation.Base.Models.Result;
    using Foundation.Commerce.Models.Entities.Geolocation;

    using Models.Requests;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using StoreLocator.Controllers;
    using StoreLocator.Services;

    using Xunit;

    public class StoreLocatorControllerTests
    {
        private readonly IStoreLocatorService storeLocatorService;
        private readonly StoreLocationRequest requestModel;

        private StoreLocatorController controller;

        public StoreLocatorControllerTests()
        {
            var fixture = new Fixture();

            this.storeLocatorService = Substitute.For<IStoreLocatorService>();
            this.storeLocatorService
                .FindStoresByGeolocation(
                    Arg.Any<double>(),
                    Arg.Any<double>(),
                    Arg.Any<double>())
                .Returns(fixture.Create<Result<SearchByGeolocationResult>>());

            this.requestModel = fixture.Create<StoreLocationRequest>();
        }

        [Fact]
        public void GetStoreLocations_ShouldCallExecuteMethod()
        {
            //arrange
            this.controller = Substitute.For<StoreLocatorController>(this.storeLocatorService);

            // act
            this.controller.SearchLocations(this.requestModel);

            // assert

            this.controller
                .Received(1)
                .Execute(Arg.Any<Func<Result<SearchByGeolocationResult>>>());
        }

        [Fact]
        public void GetStoreLocations_ShouldCallFindStoresByGeolocationMethod()
        {
            //arrange
            this.controller = new StoreLocatorController(this.storeLocatorService);

            // act
            this.controller.SearchLocations(this.requestModel);

            // assert
            this.storeLocatorService
                .Received(1)
                .FindStoresByGeolocation(
                    Arg.Is(this.requestModel.Lat),
                    Arg.Is(this.requestModel.Lng),
                    Arg.Is(this.requestModel.Radius));
        }
    }
}