//    Copyright 2021 EPAM Systems, Inc.
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

namespace HCA.Feature.Navigation.Services.Breadcrumb
{
    using System;
    using System.Linq;

    using Foundation.Base.Context;
    using Foundation.Commerce.Context;
    using Foundation.DependencyInjection;

    using Models.Entities.Breadcrumb;

    using Repositories.Breadcrumb;

    using Sitecore;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Links;

    using Constants = Navigation.Constants;

    [Service(typeof(IBreadcrumbService), Lifetime = Lifetime.Transient)]
    public class BreadcrumbService : IBreadcrumbService
    {
        private readonly IBreadcrumbRepository breadcrumbRepository;
        private readonly ISiteContext siteContext;
        private readonly ISitecoreContext sitecoreContext;

        public BreadcrumbService(
            ISitecoreContext sitecoreContext,
            ISiteContext siteContext,
            IBreadcrumbRepository breadcrumbRepository)
        {
            Assert.ArgumentNotNull(sitecoreContext, nameof(sitecoreContext));
            Assert.ArgumentNotNull(siteContext, nameof(siteContext));
            Assert.ArgumentNotNull(breadcrumbRepository, nameof(breadcrumbRepository));

            this.sitecoreContext = sitecoreContext;
            this.siteContext = siteContext;
            this.breadcrumbRepository = breadcrumbRepository;
        }

        public Breadcrumb GenerateBreadcrumb()
        {
            var startItemPath = this.sitecoreContext.Site.StartPath;
            var currentItem = Context.Item;

            var pageLinks = this.breadcrumbRepository.GetPageLinks(currentItem, startItemPath);

            var breadcrumb = new Breadcrumb
            {
                PageLinks = pageLinks
            };

            breadcrumb = this.ResolveCategoryPage(breadcrumb);
            breadcrumb = this.ResolveProductPage(breadcrumb);

            return breadcrumb;
        }

        public Item GetProductCategory(Item productItem)
        {
            Assert.ArgumentNotNull(productItem, nameof(productItem));

            var productCategoryItem = productItem.Axes.GetAncestors()
                .LastOrDefault(item => item.TemplateID.ToString() == Constants.CategoryTemplateId);

            return productCategoryItem;
        }

        public Item GetShopPage()
        {
            return this.sitecoreContext.Database.GetItem(Constants.ShopPageId);
        }

        public Breadcrumb ResolveCategoryPage(Breadcrumb breadcrumb)
        {
            if (this.siteContext.IsCategory)
            {
                breadcrumb.PageLinks.RemoveAll(pageLink => pageLink.Title == "*");

                var categorySegmentIndex = breadcrumb.PageLinks.FindIndex(
                    pageLink => pageLink.Title.Equals("shop", StringComparison.InvariantCultureIgnoreCase));

                var currentCategory = this.siteContext.CurrentCategoryItem;
                var categoryName = currentCategory?.DisplayName;

                if (categorySegmentIndex != -1 && !string.IsNullOrEmpty(categoryName))
                {
                    breadcrumb.PageLinks[categorySegmentIndex] = new PageLink
                    {
                        Title = categoryName,
                        Link = ""
                    };
                }
            }

            return breadcrumb;
        }

        public Breadcrumb ResolveProductPage(Breadcrumb breadcrumb)
        {
            if (this.siteContext.IsProduct)
            {
                var productSegmentIndex = breadcrumb.PageLinks.FindIndex(pageLink => pageLink.Title == "*");
                var categorySegmentIndex = breadcrumb.PageLinks.FindIndex(
                    pageLink => pageLink.Title.Equals("product", StringComparison.InvariantCultureIgnoreCase));

                var currentProduct = this.siteContext.CurrentProductItem;
                var productName = currentProduct?.DisplayName;
                var categoryName = this.GetProductCategory(currentProduct)?.DisplayName;

                if (productSegmentIndex != -1 && !string.IsNullOrEmpty(productName))
                {
                    breadcrumb.PageLinks[productSegmentIndex] = new PageLink
                    {
                        Title = productName,
                        Link = ""
                    };
                }

                if (categorySegmentIndex != -1 && !string.IsNullOrEmpty(categoryName))
                {
                    var shopPageUrl = LinkManager.GetItemUrl(this.GetShopPage());
                    var categoryPageUrl = $"{shopPageUrl}/{categoryName}";
                    breadcrumb.PageLinks[categorySegmentIndex] = new PageLink
                    {
                        Title = categoryName,
                        Link = categoryPageUrl
                    };
                }
            }

            return breadcrumb;
        }
    }
}