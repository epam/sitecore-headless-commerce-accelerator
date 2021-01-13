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
    using System;
    using System.Linq;

    using Foundation.Commerce.Context;

    using Models.Entities.Breadcrumb;

    using Navigation.Repositories.Breadcrumb;
    using Navigation.Services.Breadcrumb;

    using NSubstitute;
    using Sitecore.Data;
    using Xunit;

    public class BreadcrumbServiceTests : BaseBreadcrumbTests
    {
        protected readonly IBreadcrumbRepository BreadcrumbRepository;

        protected readonly BreadcrumbService BreadcrumbService;
        protected readonly ISiteContext SiteContext;

        public BreadcrumbServiceTests()
        {
            this.SiteContext = Substitute.For<ISiteContext>();
            this.BreadcrumbRepository = Substitute.For<IBreadcrumbRepository>();

            this.BreadcrumbService = new BreadcrumbService(
                this.SitecoreContext,
                this.SiteContext,
                this.BreadcrumbRepository);
        }

        [Fact]
        public void GetProductCategory_IfProductItemIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.BreadcrumbService.GetProductCategory(null));
        }

        [Fact]
        public void GetProductCategory_ShouldReturnCategoryItem()
        {
            // arrange
            var productItem = this.SitecoreTree.GetItem("/sitecore/content/HCA/Home/Shop/*/*");

            // act
            var categoryItem = this.BreadcrumbService.GetProductCategory(productItem);

            // assert
            Assert.Equal(new ID(CategoryTemplateId), categoryItem.TemplateID);
        }

        [Fact]
        public void GetShopPage_ShouldReturnShopPage()
        {
            // act
            var shopPageItem = this.BreadcrumbService.GetShopPage();

            // assert
            Assert.Equal(new ID(ShopPageId), shopPageItem.ID);
        }

        [Theory]
        [InlineData(new[] { "Home", "Shop", "*", "*" }, new[] { "Home", CategoryName })]
        public void ResolveCategoryPage_ShouldReturnBreadcrumbWithCorrectPageLinkTitles(
            string[] initialTitles,
            string[] expectedTitles)
        {
            // arrange
            var categoryItem = this.SitecoreTree.GetItem(CategoryId);
            this.SiteContext.IsCategory.Returns(x => true);
            this.SiteContext.CurrentCategoryItem.Returns(x => categoryItem);

            var initialPageLinks = initialTitles.Select(title => new PageLink { Title = title, Link = "" }).ToList();
            var breadcrumb = new Breadcrumb
            {
                PageLinks = initialPageLinks
            };

            // act
            var result = this.BreadcrumbService.ResolveCategoryPage(breadcrumb);
            var resultTitles = result.PageLinks.Select(p => p.Title).ToArray();

            // assert
            Assert.Equal(expectedTitles, resultTitles);
        }

        [Theory]
        [InlineData(new[] { "Home", "Product", "*" }, new[] { "Home", CategoryName, ProductName })]
        public void ResolveProductPage_ShouldReturnBreadcrumbWithCorrectPageLinkTitles(
            string[] initialTitles,
            string[] expectedTitles)
        {
            // arrange
            var productItem = this.SitecoreTree.GetItem(ProductId);
            this.SiteContext.IsProduct.Returns(x => true);
            this.SiteContext.CurrentProductItem.Returns(x => productItem);

            var initialPageLinks = initialTitles.Select(title => new PageLink { Title = title, Link = "" }).ToList();
            var breadcrumb = new Breadcrumb
            {
                PageLinks = initialPageLinks
            };

            // act
            var result = this.BreadcrumbService.ResolveProductPage(breadcrumb);
            var resultTitles = result.PageLinks.Select(p => p.Title).ToArray();

            // assert
            Assert.Equal(expectedTitles, resultTitles);
        }
    }
}