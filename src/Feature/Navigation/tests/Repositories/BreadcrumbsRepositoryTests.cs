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

namespace HCA.Feature.Navigation.Tests.Repositories
{
    using System;
    using System.Linq;

    using Foundation.Base.Services;

    using Navigation.Repositories.Breadcrumb;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Sitecore.Data.Items;

    using Xunit;

    public class BreadcrumbRepositoryTests : BaseBreadcrumbTests
    {
        protected readonly BreadcrumbRepository breadcrumbRepository;

        protected readonly Fixture fixture;

        protected readonly ILinkManagerService linkManagerService;

        public BreadcrumbRepositoryTests()
        {
            this.fixture = new Fixture();
            this.linkManagerService = Substitute.For<ILinkManagerService>();
            this.breadcrumbRepository = Substitute.For<BreadcrumbRepository>(this.SitecoreContext, this.linkManagerService);
        }

        [Fact]
        public void GetPageLinks_IfCurrentItemIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.breadcrumbRepository.GetPageLinks(null, StartItemPath));
        }

        [Fact]
        public void GetPageLinks_IfStartItemPathIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            var currentItem = this.SitecoreTree.GetItem(StartItemPath);

            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.breadcrumbRepository.GetPageLinks(currentItem, null));
        }

        [Theory]
        [InlineData(StartItemPath, new[] { "Home" }, new[] { "/en/sitecore/content/HCA/Home" })]
        [InlineData("/sitecore/content/HCA/Home/Account", new[] { "Home", "Account" }, new[] { "/en/sitecore/content/HCA/Home", "/en/sitecore/content/HCA/Home/Account" })]
        [InlineData("/sitecore/content/HCA/Home/Account/Login-Register", new[] { "Home", "Account", "Login-Register" }, new[] { "/en/sitecore/content/HCA/Home", "/en/sitecore/content/HCA/Home/Account", "/en/sitecore/content/HCA/Home/Account/Login-Register" })]
        [InlineData("/sitecore/content/HCA/Home/Product", new[] { "Home", "Product" }, new[] { "/en/sitecore/content/HCA/Home", "/en/sitecore/content/HCA/Home/Product" })]
        [InlineData("/sitecore/content/HCA/Home/Product/*", new[] { "Home", "Product", "*" }, new[] { "/en/sitecore/content/HCA/Home", "/en/sitecore/content/HCA/Home/Product", "/en/sitecore/content/HCA/Home/Product/*" })]
        [InlineData("/sitecore/content/HCA/Home/Shop", new[] { "Home", "Shop" }, new[] { "/en/sitecore/content/HCA/Home", "/en/sitecore/content/HCA/Home/Shop" })]
        [InlineData("/sitecore/content/HCA/Home/Shop/*", new[] { "Home", "Shop", "Desktops" }, new[] { "/en/sitecore/content/HCA/Home", "/en/sitecore/content/HCA/Home/Shop", "/en/sitecore/content/HCA/Home/Shop/*" })]
        [InlineData("/sitecore/content/HCA/Home/Shop/*/*", new[] { "Home", "Shop", "Desktops", "SPARK DESKTOP" }, new[] { "/en/sitecore/content/HCA/Home", "/en/sitecore/content/HCA/Home/Shop", "/en/sitecore/content/HCA/Home/Shop/*", "/en/sitecore/content/HCA/Home/Shop/*/*" })]
        public void GetPageLinks_ShouldReturnBreadcrumbWithCorrectPageLinkTitles(
            string currentItemPath,
            string[] expectedTitles,
            string[] expectedLinks)
        {
            // arrange
            var currentItem = this.SitecoreTree.GetItem(currentItemPath);
            this.linkManagerService.GetItemUrl(currentItem)
                .ReturnsForAnyArgs(
                    x => 
                    {
                        int idx = this.linkManagerService.ReceivedCalls().ToList().Count();
                        return expectedLinks[idx-1];
                    });

            // act
            var pageLinks = this.breadcrumbRepository.GetPageLinks(currentItem, StartItemPath);
            var actualTitles = pageLinks.Select(p => p.Title).ToArray();
            var actualLinks = pageLinks.Select(p => p.Link).ToArray();

            // assert
            Assert.Equal(expectedTitles, actualTitles);
            Assert.Equal(expectedLinks, actualLinks);
        }
    }
}