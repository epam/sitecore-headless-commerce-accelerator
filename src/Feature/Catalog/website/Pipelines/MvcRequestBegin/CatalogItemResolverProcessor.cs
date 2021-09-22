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

namespace HCA.Feature.Catalog.Pipelines.MvcRequestBegin
{
    using System;
    using System.Web;

    using Foundation.Commerce.Context;
    using Foundation.Commerce.Infrastructure.Pipelines;
    using Foundation.DependencyInjection;

    using Sitecore;
    using Sitecore.Diagnostics;
    using Sitecore.Mvc.Pipelines.Request.RequestBegin;
    using Sitecore.Web;

    using Constants = Catalog.Constants;

    [Service]
    public class CatalogItemResolverProcessor
    {
        private readonly ICatalogItemResolver catalogItemResolver;
        private readonly ISiteContext siteContext;

        public CatalogItemResolverProcessor(ICatalogItemResolver catalogItemResolver, ISiteContext siteContext)
        {
            Assert.ArgumentNotNull(catalogItemResolver, nameof(catalogItemResolver));
            Assert.ArgumentNotNull(siteContext, nameof(siteContext));

            this.catalogItemResolver = catalogItemResolver;
            this.siteContext = siteContext;
        }

        public void Process(RequestBeginArgs args)
        {
            var currentItem = Context.Item;
            var url = args.RequestContext.HttpContext.Request.Url;

            if (currentItem != null && currentItem.Name == "*")
            {
                var urlSegments = this.GetItemUrlSegments(url);

                this.catalogItemResolver.ProcessItemAndApplyContext(currentItem, urlSegments);

                if (this.siteContext.CurrentProduct == null && this.siteContext.CurrentCategory == null)
                {
                    WebUtil.Redirect(Sitecore.Configuration.Settings.GetSetting(Constants.Settings.ItemNotFoundUrl));
                }
            }
        }

        private string[] GetItemUrlSegments(System.Uri url)
        {
            string[] segments = null;

            var itemPath = HttpUtility.ParseQueryString(url?.Query ?? "").Get("item");
            var absolutePath = url?.AbsolutePath;

            if (!string.IsNullOrEmpty(itemPath))
            {
                segments = itemPath.Trim('/').Split('/');
            }
            else if (!string.IsNullOrEmpty(absolutePath))
            {
                segments = HttpUtility.UrlDecode(absolutePath).Trim('/').Split('/');
            }

            return segments;
        }
    }
}