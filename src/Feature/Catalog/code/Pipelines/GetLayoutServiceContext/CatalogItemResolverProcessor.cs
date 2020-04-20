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

namespace HCA.Feature.Catalog.Pipelines.GetLayoutServiceContext
{
    using System.Net.Http;
    using System.Web;

    using Foundation.Commerce.Infrastructure.Pipelines;
    using Foundation.DependencyInjection;
    using Foundation.ReactJss.Infrastructure;

    using Sitecore.JavaScriptServices.Configuration;
    using Sitecore.LayoutService.ItemRendering.Pipelines.GetLayoutServiceContext;

    [Service]
    public class CatalogItemResolverProcessor : BaseSafeJssGetLayoutServiceContextProcessor
    {
        private readonly ICatalogItemResolver catalogItemResolver;

        public CatalogItemResolverProcessor(
            ICatalogItemResolver catalogItemResolver,
            IConfigurationResolver configurationResolver)
            : base(configurationResolver)
        {
            this.catalogItemResolver = catalogItemResolver;
        }

        protected override void DoProcessSafe(GetLayoutServiceContextArgs args, AppConfiguration application)
        {
            if (args.RenderedItem == null)
            {
                return;
            }

            var currentItem = args.RenderedItem;

            // Trying to get original url path
            var itemPath = HttpContext.Current.Request.Url.ParseQueryString()?["item"];
            var urlSegments = itemPath?.Trim('/').Split('/');
            if (args.RenderedItem == null)
            {
                return;
            }

            this.catalogItemResolver.ProcessItemAndApplyContext(currentItem, urlSegments);
        }
    }
}