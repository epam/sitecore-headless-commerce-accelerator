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

namespace HCA.Feature.Navigation.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Foundation.Base.Context;
    using Foundation.Base.Services;
    using Foundation.Commerce.Context.Site;
    using Foundation.Commerce.Models.Entities.Catalog;

    using Models.Entities.Breadcrumb;

    using Navigation.Repositories.Breadcrumb;
    using Navigation.Services.Breadcrumb;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Sitecore.Collections;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb.AutoFixture;
    using Sitecore.Web;

    using Xunit;

    using SiteContext = Sitecore.Sites.SiteContext;

    public class BreadcrumbServiceTests
    {
        private readonly IBreadcrumbRepository breadcrumbRepository;
        private readonly IBreadcrumbService breadcrumbService;
        private readonly ISiteContext siteContext;
        private readonly ISitecoreContext sitecoreContext;
        private readonly IFixture fixture;

        public BreadcrumbServiceTests()
        {
            this.fixture = new Fixture().Customize(new AutoDbCustomization());
            this.siteContext = Substitute.For<ISiteContext>();
            
            this.breadcrumbRepository = Substitute.For<IBreadcrumbRepository>();

            this.breadcrumbRepository.GetPageLinks(this.fixture.Create<Item>(), this.fixture.Create<string>())
                .Returns(new List<PageLink>());

            this.sitecoreContext = Substitute.For<ISitecoreContext>();
            this.sitecoreContext.Site.Returns(new SiteContext(new SiteInfo(new StringDictionary())));

            this.breadcrumbService = new BreadcrumbService(this.sitecoreContext, this.siteContext, this.breadcrumbRepository);
        }

        [Fact]
        public void GenerateBreadcrumb_ShouldReturnBreadcrumbs()
        {
            // act
            var breadcrumbs = this.breadcrumbService.GetCurrentPageBreadcrumbs();

            // assert
            Assert.NotNull(breadcrumbs);
        }
    }
}