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

namespace HCA.Foundation.Commerce.Infrastructure.Pipelines.Login
{
    using Account.Infrastructure.Pipelines.Login;

    using Base.Infrastructure.Pipelines;
    using Base.Models.Logging;
    using Base.Services.Logging;

    using Services.Tracking;

    using Sitecore.Diagnostics;

    public class IdentifyCommerceUserProcessor : SafePipelineProcessor<LoginPipelineArgs>
    {
        private readonly ICommerceTrackingService commerceTrackingService;

        public IdentifyCommerceUserProcessor(
            ICommerceTrackingService commerceTrackingService,
            ILogService<CommonLog> logService)
            : base(logService)
        {
            Assert.ArgumentNotNull(commerceTrackingService, nameof(commerceTrackingService));

            this.commerceTrackingService = commerceTrackingService;
        }

        protected override void SafeProcess(LoginPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            this.commerceTrackingService.IdentifyAs(Constants.Login.CommerceUserSource, args.UserName);
        }
    }
}