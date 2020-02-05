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

namespace Wooli.Foundation.Commerce.Infrastructure.Pipelines.HttpRequestBegin
{
    using Context;
    using Extensions.Infrastructure;
    using Providers;
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
            if (visitorContext.CurrentUser != null) return;

            if (!Sitecore.Context.PageMode.IsNormal) return;

            visitorContext.CurrentUser = customerProvider.GetCurrentCommerceUser(args.HttpContext);
        }
    }
}