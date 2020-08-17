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

namespace HCA.Foundation.Commerce.Tests.Services.Geolocation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base.Providers.SiteSettings;

    using Commerce.Services.Geolocation;
    using Models;
    using Models.Entities.Geolocation;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Repositories.Geolocation;

    using Sitecore.Data;

    using Xunit;

    public class GeolocationServiceTests
    {
        private const string UnknownUnitOfLengthMessage = "invalid unit - unknown unit of length";
        private const string InvalidUnitCode = "invalid unit";
        private const string ValidUnitCode = "km";

        private const double SearchCenterLat = -34.089553;
        private const double SearchCenterLng = 150.894855;
        private const double SearchRadius = 41.24;

        private readonly IGeolocationRepository geolocationRepository;
        private readonly ISiteSettingsProvider siteSettingsProvider;

        private readonly IFixture fixture;

        private readonly GeolocationService geolocationService;

        public GeolocationServiceTests()
        {
            this.fixture = new Fixture();

            this.geolocationRepository = Substitute.For<IGeolocationRepository>();
            this.siteSettingsProvider = Substitute.For<ISiteSettingsProvider>();

            this.geolocationService = new GeolocationService(this.geolocationRepository, this.siteSettingsProvider);
        }

        [Fact]
        public void GetLocations_ShouldReturnErrorMessage()
        {
            // arrange
            var expectResult = this.fixture.Build<GeolocationInfo>()
                .With(x => x.Latitude, -34.397)
                .With(x => x.Longitude, 150.644)
                .Create();

            var unitOfLengthSettings = GetUnitOfLengthSettings(withAvailableUnits: true);

            this.siteSettingsProvider
                .GetSetting<IGeolocationSettings>(Arg.Any<ID>())
                .Returns(unitOfLengthSettings);

            this.geolocationRepository
                .GetItems(Arg.Any<IEnumerable<Guid>>())
                .Returns(new List<GeolocationInfo>{ expectResult });

            // act
            var result = this.geolocationService.Find(
                SearchCenterLat,
                SearchCenterLng,
                SearchRadius,
                ValidUnitCode,
                new List<Guid>());

            var actualResult = result.Data.Locations.ElementAt(0);

            // assert
            Assert.Equal(expectResult, actualResult);
        }

        [Fact]
        public void ReceivedUnknownUnitOfLength_ShouldReturnErrorMessage()
        {
            // arrange
            var unitOfLengthSettings = GetUnitOfLengthSettings(withAvailableUnits: false);

            this.siteSettingsProvider
                .GetSetting<IGeolocationSettings>(Arg.Any<ID>())
                .Returns(unitOfLengthSettings);

            // act
            var result = this.geolocationService.Find(
                SearchCenterLat,
                SearchCenterLng,
                SearchRadius,
                InvalidUnitCode,
                new List<Guid>());

            var actualMessage = result.Errors.FirstOrDefault();

            // assert
            Assert.Equal(UnknownUnitOfLengthMessage, actualMessage);
        }

        private static IUnitOfLength GetUnitOfLength()
        {
            var unitKm = Substitute.For<IUnitOfLength>();
            unitKm.UnitCode.Returns(ValidUnitCode);
            unitKm.KilometerToUnitMultiplier.Returns(1);

            return unitKm;
        }

        private static IGeolocationSettings GetUnitOfLengthSettings(bool withAvailableUnits = false)
        {
            var unitOfLengthSettings = Substitute.For<IGeolocationSettings>();

            var availableUnits = withAvailableUnits
                ? new List<IUnitOfLength>{ GetUnitOfLength() }
                : new List<IUnitOfLength>();

            unitOfLengthSettings
                .AvailableUnits
                .Returns(availableUnits);

            return unitOfLengthSettings;
        }
    }
}
