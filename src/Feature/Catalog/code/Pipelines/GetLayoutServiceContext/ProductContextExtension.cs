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

namespace Wooli.Feature.Catalog.Pipelines.GetLayoutServiceContext
{
    using Foundation.Commerce.Services.Analytics;
    using Foundation.Commerce.Services.Catalog;
    using Foundation.ReactJss.Infrastructure;

    using Sitecore.JavaScriptServices.Configuration;
    using Sitecore.LayoutService.ItemRendering.Pipelines.GetLayoutServiceContext;

    public class ProductContextExtension : BaseSafeJssGetLayoutServiceContextProcessor
    {
        private readonly ICommerceAnalyticsService analyticsRepository;

        private readonly ICatalogService catalogService;

        public ProductContextExtension(
            ICatalogService catalogService,
            ICommerceAnalyticsService analyticsRepository,
            IConfigurationResolver configurationResolver)
            : base(configurationResolver)
        {
            this.catalogService = catalogService;
            this.analyticsRepository = analyticsRepository;
        }

        protected override void DoProcessSafe(GetLayoutServiceContextArgs args, AppConfiguration application)
        {
            var result = this.catalogService.GetCurrentProduct();

            if (result.Success && result.Data != null)
            {
                this.analyticsRepository.RaiseProductVisitedEvent(result.Data);
            }

            args.ContextData.Add("product", result.Data);
        }
    }
}