﻿//    Copyright 2020 EPAM Systems, Inc.
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

namespace HCA.Feature.Navigation.Infrastructure.Pipelines.GetLayoutServiceContext
{
    using Foundation.ReactJss.Infrastructure;

    using Services.Breadcrumb;

    using Sitecore.Diagnostics;
    using Sitecore.JavaScriptServices.Configuration;
    using Sitecore.LayoutService.ItemRendering.Pipelines.GetLayoutServiceContext;

    public class BreadcrumbContextExtension : BaseSafeJssGetLayoutServiceContextProcessor
    {
        private readonly IBreadcrumbService breadcrumbService;

        public BreadcrumbContextExtension(
            IBreadcrumbService breadcrumbService,
            IConfigurationResolver configurationResolver) :
            base(configurationResolver)
        {
            Assert.ArgumentNotNull(breadcrumbService, nameof(breadcrumbService));

            this.breadcrumbService = breadcrumbService;
        }

        protected override void DoProcessSafe(GetLayoutServiceContextArgs args, AppConfiguration application)
        {
            args.ContextData.Add("breadcrumb", this.breadcrumbService.GetCurrentPageBreadcrumbs());
        }
    }
}