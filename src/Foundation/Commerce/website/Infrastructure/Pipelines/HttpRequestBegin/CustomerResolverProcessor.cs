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

namespace HCA.Foundation.Commerce.Infrastructure.Pipelines.HttpRequestBegin
{
    using Base.Infrastructure.Pipelines;

    using Context;

    using Providers;

    using Sitecore;
    using Sitecore.Pipelines.HttpRequest;

    public class CustomerResolverProcessor : SiteSpecificPipelineProcessor
    {
        private readonly ICustomerProvider customerProvider;

        private readonly IVisitorContext visitorContext;

        public CustomerResolverProcessor(IVisitorContext visitorContext, ICustomerProvider customerProvider)
        {
            this.visitorContext = visitorContext;
            this.customerProvider = customerProvider;
        }

        protected override void DoProcess(HttpRequestArgs args)
        {
            if (this.visitorContext.CurrentUser != null)
            {
                return;
            }

            if (!Context.PageMode.IsNormal)
            {
                return;
            }

            this.visitorContext.CurrentUser = this.customerProvider.GetCurrentCommerceUser(args.HttpContext);
        }
    }
}