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

namespace HCA.Foundation.Commerce.Tests.Utils
{
    using Commerce.Utils;

    using Models.Entities.Geolocation;

    using Ploeh.AutoFixture;

    using Xunit;

    public class GeolocationHelperTests
    {
        private const double EarthRadiusKm = 6371;
        private const double EarthRadiusMi = 3958.756;

        private const double SearchCenterLat = -34.089553;
        private const double SearchCenterLng = 150.894855;

        private readonly GeolocationInfo testLocation;
        
        public GeolocationHelperTests()
        {
            var fixture = new Fixture();

            this.testLocation = fixture
                .Build<GeolocationInfo>()
                .With(x => x.Latitude, -34.397)
                .With(x => x.Longitude, 150.644)
                .Create();
        }

        [Theory]
        [InlineData(41.24, EarthRadiusKm, true)]
        [InlineData(41.23, EarthRadiusKm, false)]
        [InlineData(25.63, EarthRadiusMi, true)]
        [InlineData(25.62, EarthRadiusMi, false)]
        public void FilterLocations_ReturnAppropriateValue(
            double searchRadius,
            double earthRadius,
            bool expectedResult)
        {
            // act 
            var actualResult = this.testLocation.InRadius(
                SearchCenterLat,
                SearchCenterLng,
                searchRadius,
                earthRadius);

            // assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
