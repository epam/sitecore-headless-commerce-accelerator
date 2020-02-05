//    Copyright 2019 EPAM Systems, Inc.
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

using System;
using Sitecore.Data.Items;
using Sitecore.Pipelines.HttpRequest;

namespace Wooli.Foundation.Commerce.Infrastructure.Pipelines.HttpRequestBegin
{
    public class CatalogItemResolverProcessor : HttpRequestProcessor
    {
        private readonly ICatalogItemResolver catalogItemResolver;

        public CatalogItemResolverProcessor(ICatalogItemResolver catalogItemResolver)
        {
            this.catalogItemResolver = catalogItemResolver;
        }

        public override void Process(HttpRequestArgs args)
        {
            Uri requestUrl = args.HttpContext.Request.Url;
            if (requestUrl == null || requestUrl.AbsolutePath.StartsWith("/sitecore")) return;

            if (Sitecore.Context.Item == null) return;

            Item currentItem = Sitecore.Context.Item;
            string[] urlSegments = requestUrl.Segments;

            catalogItemResolver.ProcessItemAndApplyContext(currentItem, urlSegments);
        }
    }
}