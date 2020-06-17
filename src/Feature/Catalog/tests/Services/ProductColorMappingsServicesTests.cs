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

namespace HCA.Feature.Catalog.Tests.Services
{
    using System.Collections.Generic;

    using Catalog.Services;

    using Foundation.Base.Providers.SiteSettings;

    using Models;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Sitecore.Data;

    using Xunit;

    public class ProductColorMappingsServicesTests
    {
        private readonly IFixture fixture;

        private readonly IProductColorMappingsServices service;

        private readonly ISiteSettingsProvider siteSettingsProvider;

        public ProductColorMappingsServicesTests()
        {
            this.siteSettingsProvider = Substitute.For<ISiteSettingsProvider>();
            this.service = new ProductColorMappingsServices(this.siteSettingsProvider);

            this.fixture = new Fixture();
        }

        [Fact]
        public void GetProductColorMappings_IfMappingFolderIsNull_ShouldReturnEmptyDictionary()
        {
            // arrange
            this.siteSettingsProvider
                .GetSetting<IProductColorMappingFolder>(Arg.Any<ID>())
                .Returns((IProductColorMappingFolder)null);

            // act
            var result = this.service.GetProductColorMappings();

            // assert
            Assert.Equal(new Dictionary<string, string>(), result);
        }

        [Fact]
        public void GetProductColorMappings_IfMappingFolderSuccess_ShouldReturnDictionary()
        {
            // arrange
            var color = this.fixture.Create<string>();
            var hex = this.fixture.Create<string>();

            var mappings = new List<IProductColorMapping>
            {
                new ProductColorMapping
                {
                    ColorName = color,
                    ColorHEX = hex
                }
            };

            var mappingFolder = Substitute.For<IProductColorMappingFolder>();
            mappingFolder.Mappings.Returns(mappings);

            this.siteSettingsProvider
                .GetSetting<IProductColorMappingFolder>(Arg.Any<ID>())
                .Returns(mappingFolder);

            // act
            var result = this.service.GetProductColorMappings();

            // assert
            Assert.Equal(hex, result[color]);
        }
    }
}