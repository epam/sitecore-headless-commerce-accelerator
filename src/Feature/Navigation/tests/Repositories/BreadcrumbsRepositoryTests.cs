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

    using Navigation.Repositories.Breadcrumb;

    using Xunit;

    public class BreadcrumbRepositoryTests : BaseBreadcrumbTests
    {
        protected readonly IBreadcrumbRepository BreadcrumbRepository;

        public BreadcrumbRepositoryTests()
        {
            this.BreadcrumbRepository = new BreadcrumbRepository(this.SitecoreContext);
        }

        [Fact]
        public void GetPageLinks_IfCurrentItemIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.BreadcrumbRepository.GetPageLinks(null, StartItemPath));
        }

        [Fact]
        public void GetPageLinks_IfStartItemPathIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            var currentItem = this.SitecoreTree.GetItem(StartItemPath);

            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.BreadcrumbRepository.GetPageLinks(currentItem, null));
        }

        [Theory]
        [InlineData(StartItemPath, new[] { "Home" })]
        [InlineData("/sitecore/content/HCA/Home/Account", new[] { "Home", "Account" })]
        [InlineData("/sitecore/content/HCA/Home/Account/Login-Register", new[] { "Home", "Account", "Login-Register" })]
        [InlineData("/sitecore/content/HCA/Home/Product", new[] { "Home", "Product" })]
        [InlineData("/sitecore/content/HCA/Home/Product/*", new[] { "Home", "Product", "*" })]
        [InlineData("/sitecore/content/HCA/Home/Shop", new[] { "Home", "Shop" })]
        [InlineData("/sitecore/content/HCA/Home/Shop/*", new[] { "Home", "Shop", "*" })]
        [InlineData("/sitecore/content/HCA/Home/Shop/*/*", new[] { "Home", "Shop", "*", "*" })]
        public void GetPageLinks_ShouldReturnBreadcrumbWithCorrectPageLinkTitles(
            string currentItemPath,
            string[] expectedTitles)
        {
            // arrange
            var currentItem = this.SitecoreTree.GetItem(currentItemPath);

            // act
            var pageLinks = this.BreadcrumbRepository.GetPageLinks(currentItem, StartItemPath);
            var actualTitles = pageLinks.Select(p => p.Title).ToArray();

            // assert
            Assert.Equal(expectedTitles, actualTitles);
        }
    }
}