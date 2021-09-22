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
    using Foundation.Base.Context;
    using Foundation.Base.Services;
    using Foundation.Commerce.Context.Site;
    using Foundation.DependencyInjection;

    using Models.Entities.Breadcrumb;

    using Repositories.Breadcrumb;

    using Sitecore;
    using Sitecore.Diagnostics;

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

        public Breadcrumb GetCurrentPageBreadcrumbs()
        {
            var currentItem = Context.Item;
            var breadcrumbs = new Breadcrumb
            {
                PageLinks = this.breadcrumbRepository.GetPageLinks(currentItem, this.sitecoreContext.Site.StartPath)
            };

            if (currentItem == null || currentItem.Name != "*")
            {
                return breadcrumbs;
            }

            breadcrumbs.PageLinks.RemoveAt(breadcrumbs.PageLinks.Count - 1);
            breadcrumbs.PageLinks.Add(
                new PageLink
                {
                    Title = this.siteContext.CurrentCategory != null ? this.siteContext.CurrentCategory.DisplayName : this.siteContext.CurrentProduct?.DisplayName
                });

            return breadcrumbs;
        }
    }
}
