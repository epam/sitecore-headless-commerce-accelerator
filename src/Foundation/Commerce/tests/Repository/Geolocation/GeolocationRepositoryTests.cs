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

namespace HCA.Foundation.Commerce.Tests.Repository.Geolocation
{
    using System;
    using System.Collections.Generic;

    using Commerce.Mappers.Geolocation;

    using Glass.Mapper.Sc;

    using Models;
    using Models.Entities.Geolocation;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Repositories.Geolocation;
    
    using Xunit;

    public class GeolocationRepositoryTests
    {
        private readonly ISitecoreService sitecoreService;
        private readonly IGeolocationMapper geolocationMapper;

        private readonly GeolocationRepository geolocationRepository;

        private readonly IFixture fixture;

        public GeolocationRepositoryTests()
        {
            this.fixture = new Fixture();

            this.sitecoreService = Substitute.For<ISitecoreService>();
            this.geolocationMapper = Substitute.For<IGeolocationMapper>();

            this.geolocationRepository = new GeolocationRepository(this.sitecoreService, this.geolocationMapper);
        }

        [Fact]
        public void SelectedItemIdsListNull_ShouldReturnEmptyEnumeration()
        {
            // arrange
            var idsList = default(IEnumerable<Guid>);

            // act
            var result = this.geolocationRepository.GetItems(idsList);

            // assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetItems_ShouldReturnItemList()
        {
            // arrange
            var idsList = this.fixture.Create<List<Guid>>();

            this.sitecoreService
                .GetItem<GeolocationItem>(Arg.Any<Guid>())
                .Returns(new GeolocationItem());

            this.geolocationMapper
                .Map<GeolocationItem, GeolocationInfo>(Arg.Any<GeolocationItem>())
                .Returns(new GeolocationInfo());

            // act
            var result = this.geolocationRepository.GetItems(idsList);

            // assert
            Assert.NotEmpty(result);
        }
    }
}
