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
    using Sitecore.JavaScriptServices.Configuration;
    using Sitecore.LayoutService.ItemRendering.Pipelines.GetLayoutServiceContext;

    using Wooli.Foundation.Commerce.Models.Catalog;
    using Wooli.Foundation.Commerce.Repositories;
    using Wooli.Foundation.Commerce.Services.Analytics;
    using Wooli.Foundation.ReactJss.Infrastructure;

    public class ProductContextExtension : BaseSafeJssGetLayoutServiceContextProcessor
    {
        private readonly ICommerceAnalyticsService analyticsRepository;

        private readonly ICatalogRepository catalogRepository;

        public ProductContextExtension(
            ICatalogRepository catalogRepository,
            ICommerceAnalyticsService analyticsRepository,
            IConfigurationResolver configurationResolver)
            : base(configurationResolver)
        {
            this.catalogRepository = catalogRepository;
            this.analyticsRepository = analyticsRepository;
        }

        protected override void DoProcessSafe(GetLayoutServiceContextArgs args, AppConfiguration application)
        {
            ProductModel model = this.catalogRepository.GetCurrentProduct();

            if (model != null) this.analyticsRepository.RaiseProductVisitedEvent(model);
            args.ContextData.Add("product", model);
        }
    }
}