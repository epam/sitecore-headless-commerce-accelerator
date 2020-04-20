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

namespace HCA.Foundation.ReactJss.Infrastructure
{
    using System;

    using Sitecore.Diagnostics;
    using Sitecore.JavaScriptServices.Configuration;
    using Sitecore.JavaScriptServices.ViewEngine.LayoutService.Pipelines.GetLayoutServiceContext;
    using Sitecore.LayoutService.ItemRendering.Pipelines.GetLayoutServiceContext;

    public abstract class BaseSafeJssGetLayoutServiceContextProcessor : JssGetLayoutServiceContextProcessor
    {
        protected BaseSafeJssGetLayoutServiceContextProcessor(IConfigurationResolver configurationResolver)
            : base(configurationResolver)
        {
        }

        protected override void DoProcess(GetLayoutServiceContextArgs args, AppConfiguration application)
        {
            try
            {
                this.DoProcessSafe(args, application);
            }
            catch (Exception e)
            {
                Log.Error("Unexpected error during JSS context resolving.", e, this.GetType());
            }
        }

        protected abstract void DoProcessSafe(GetLayoutServiceContextArgs args, AppConfiguration application);
    }
}