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

namespace HCA.Feature.StoreLocator.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Foundation.Base.Models.Result;
    using Foundation.Commerce.Models;
    using Foundation.Commerce.Models.Entities.Geolocation;
    using Foundation.Commerce.Services.Geolocation;

    using Models;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using StoreLocator.Providers;

    using StoreLocator.Services;

    using Xunit;

    public class StoreLocatorServiceTests
    {
        private const double Latitude = -34.0f;
        private const double Longitude = -145.0f;
        private const double Radius = 100.0f;
        private const string UnitOfLength = "km";

        private IEnumerable<Guid> selectedLocations;
        private IUnitOfLength selectedUnitUfLength;

        private readonly IGeolocationService geolocationService;
        private readonly IStoreLocatorProvider storeLocatorProvider;

        private readonly IStoreLocatorService storeLocatorService;

        public StoreLocatorServiceTests()
        {
            var fixture = new Fixture();

            this.geolocationService = Substitute.For<IGeolocationService>();
            this.geolocationService
                .Find(
                    Arg.Any<double>(),
                    Arg.Any<double>(),
                    Arg.Any<double>(),
                    Arg.Any<string>(),
                    Arg.Any<IEnumerable<Guid>>())
                .Returns(fixture.Create<Result<SearchByGeolocationResult>>());

            var storeLocator = this.GetLocatorTestData(fixture);

            this.storeLocatorProvider = Substitute.For<IStoreLocatorProvider>();
            this.storeLocatorProvider
                .GetLocator()
                .Returns(storeLocator);

            this.storeLocatorService = new StoreLocatorService(this.geolocationService, this.storeLocatorProvider);
        }

        [Fact]
        public void GetLocations_ShouldCallGetLocatorMethod()
        {
            // act
            this.storeLocatorService.FindStoresByGeolocation(Latitude, Longitude, Radius);

            // assert
            this.storeLocatorProvider
                .Received(1)
                .GetLocator();
        }

        [Fact] 
        public void GetLocations_ShouldCallFindMethod()
        {
            // act
            this.storeLocatorService.FindStoresByGeolocation(Latitude, Longitude, Radius);

            // assert
            this.geolocationService
                .Received(1)
                .Find(
                    Arg.Is(Latitude),
                    Arg.Is(Longitude),
                    Arg.Is(Radius),
                    Arg.Is(this.selectedUnitUfLength.UnitCode),
                    Arg.Is(this.selectedLocations));
        }

        private IStoreLocator GetLocatorTestData(IFixture fixture)
        {
            this.selectedLocations = fixture.Create<IEnumerable<Guid>>();

            this.selectedUnitUfLength = Substitute.For<IUnitOfLength>();
            this.selectedUnitUfLength.UnitCode = UnitOfLength;

            var storeLocator = Substitute.For<IStoreLocator>();
            storeLocator.Stores = this.selectedLocations;
            storeLocator.SelectedUnitOfLength = this.selectedUnitUfLength;

            return storeLocator;
        }
    }
}
