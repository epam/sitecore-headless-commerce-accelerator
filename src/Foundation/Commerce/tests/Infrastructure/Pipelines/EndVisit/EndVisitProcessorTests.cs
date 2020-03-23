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

namespace Wooli.Foundation.Commerce.Tests.Infrastructure.Pipelines.EndVisit
{
    using Account.Infrastructure.Pipelines.Logout;
    using Account.Services.Session;

    using Commerce.Infrastructure.Pipelines.EndVisit;
    using Commerce.Services.Tracking;

    using NSubstitute;

    using Xunit;

    public class EndVisitProcessorTests
    {
        public EndVisitProcessorTests()
        {
            this.trackingService = Substitute.For<ICommerceTrackingService>();
            this.sessionService = Substitute.For<ISessionService>();

            this.endVisitProcessor = new EndVisitProcessor(this.trackingService, this.sessionService);
        }

        private readonly EndVisitProcessor endVisitProcessor;

        private readonly ICommerceTrackingService trackingService;

        private readonly ISessionService sessionService;

        [Fact]
        public void Process_IfArgsNotNull_ShouldCallAuthenticationServiceLogout()
        {
            // arrange
            var loginArgs = new LogoutPipelineArgs();

            // act
            this.endVisitProcessor.Process(loginArgs);

            // assert
            this.trackingService.Received(1).EndVisit(true);
            this.sessionService.Received(1).Abandon();
        }
    }
}