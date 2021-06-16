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

namespace HCA.Feature.Navigation.Repositories.Breadcrumb
{
    using System.Collections.Generic;
    using System.Linq;

    using Foundation.Base.Context;
    using Foundation.Base.Services;
    using Foundation.DependencyInjection;

    using Models.Entities.Breadcrumb;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    [Service(typeof(IBreadcrumbRepository), Lifetime = Lifetime.Transient)]
    public class BreadcrumbRepository : IBreadcrumbRepository
    {
        private readonly ISitecoreContext sitecoreContext;
        private readonly ILinkManagerService linkManagerService;

        public BreadcrumbRepository(
            ISitecoreContext sitecoreContext,
            ILinkManagerService linkManagerService)
        {
            Assert.ArgumentNotNull(sitecoreContext, nameof(sitecoreContext));
            Assert.ArgumentNotNull(linkManagerService, nameof(linkManagerService));

            this.sitecoreContext = sitecoreContext;
            this.linkManagerService = linkManagerService;
        }

        public List<PageLink> GetPageLinks(Item currentItem, string startItemPath)
        {
            Assert.ArgumentNotNull(currentItem, nameof(currentItem));
            Assert.ArgumentNotNull(startItemPath, nameof(startItemPath));

            var ancestors = currentItem.Axes.GetAncestors().SkipWhile(item => item.Paths.Path != startItemPath);
            var pageLinks = ancestors.Select(
                    item => new PageLink
                    {
                        Title = !string.IsNullOrWhiteSpace(item?.DisplayName) ? item?.DisplayName : item?.Name,
                        Link = this.linkManagerService.GetItemUrl(item)
                    })
                .ToList();

            pageLinks.Add(
                new PageLink
                {
                    Title = !string.IsNullOrWhiteSpace(currentItem?.DisplayName) ? currentItem?.DisplayName : currentItem?.Name,
                    Link = this.linkManagerService.GetItemUrl(currentItem)
                });
            return pageLinks;
        }
    }
}