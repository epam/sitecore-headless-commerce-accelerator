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

namespace HCA.Foundation.Commerce.Infrastructure.Pipelines.Logout
{
    using Account.Infrastructure.Pipelines.Logout;
    using Account.Services.Session;

    using Base.Infrastructure.Pipelines;

    using Services.Tracking;

    using Sitecore.Diagnostics;

    public class EndVisitProcessor : PipelineProcessor<LogoutPipelineArgs>
    {
        private readonly ISessionService sessionService;

        private readonly ICommerceTrackingService trackingService;

        public EndVisitProcessor(ICommerceTrackingService trackingService, ISessionService sessionService)
        {
            Assert.ArgumentNotNull(trackingService, nameof(trackingService));
            Assert.ArgumentNotNull(sessionService, nameof(sessionService));

            this.trackingService = trackingService;
            this.sessionService = sessionService;
        }

        public override void Process(LogoutPipelineArgs args)
        {
            this.trackingService.EndVisit(true);
            this.sessionService.Abandon();
        }
    }
}