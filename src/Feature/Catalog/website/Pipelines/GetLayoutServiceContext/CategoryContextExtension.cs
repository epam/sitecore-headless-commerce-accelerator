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
    using System.ComponentModel;

    using Foundation.Commerce.Context.Site;
    using Foundation.Commerce.Services.Analytics;
    using Foundation.Commerce.Services.Catalog;
    using Foundation.ReactJss.Infrastructure;

    using Sitecore.Diagnostics;
    using Sitecore.JavaScriptServices.Configuration;
    using Sitecore.LayoutService.ItemRendering.Pipelines.GetLayoutServiceContext;

    public class CategoryContextExtension : BaseSafeJssGetLayoutServiceContextProcessor
    {
        private readonly IAnalyticsService analyticsService;
        private readonly ISiteContext siteContext;

        public CategoryContextExtension(
            ISiteContext siteContext,
            IAnalyticsService analyticsService,
            IConfigurationResolver configurationResolver)
            : base(configurationResolver)
        {
            Assert.ArgumentNotNull(siteContext, nameof(siteContext));
            Assert.ArgumentNotNull(analyticsService, nameof(analyticsService));

            this.siteContext = siteContext;
            this.analyticsService = analyticsService;
        }

        protected override void DoProcessSafe(GetLayoutServiceContextArgs args, AppConfiguration application)
        {
            var category = this.siteContext.CurrentCategory;

            if (category != null)
            {
                this.analyticsService.RaiseCategoryVisitedEvent(category);
            }

            args.ContextData.Add("category", category);
        }
    }
}