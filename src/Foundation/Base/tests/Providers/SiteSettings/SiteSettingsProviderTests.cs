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

namespace HCA.Foundation.Base.Tests.Providers.SiteSettings
{
    using Base.Providers.SiteDefinitions;
    using Base.Providers.SiteSettings;

    using Glass.Mapper.Sc;

    using Models;

    using NSubstitute;

    using Sitecore.Data;

    using Xunit;

    public class SiteSettingsProviderTests
    {
        private readonly ISiteSettingsProvider provider;

        private readonly ISitecoreService sitecoreService;

        private readonly ISiteDefinitionsProvider siteDefinitionsProvider;

        public SiteSettingsProviderTests()
        {
            this.siteDefinitionsProvider = Substitute.For<ISiteDefinitionsProvider>();
            this.sitecoreService = Substitute.For<ISitecoreService>();

            this.provider = new SiteSettingsProvider(this.siteDefinitionsProvider, this.sitecoreService);
        }

        [Fact]
        public void GetProductColorMappings_IfMappingCurrentSiteDefinitionReturnNull_ShouldCallGetItem()
        {
            // arrange
            this.siteDefinitionsProvider.GetCurrentSiteDefinition().Returns((SiteDefinition)null);

            // act
            this.provider.GetSetting<IItem>(new ID());

            // assert
            this.sitecoreService.Received(1).GetItem<IItem>(Arg.Any<GetItemOptions>());
        }
    }

    public interface IItem
    {
    }
}