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

namespace HCA.Foundation.Commerce.Tests.Infrastructure.Pipelines.Login
{
    using Account.Infrastructure.Pipelines.Login;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using Commerce.Infrastructure.Pipelines.Login;
    using Commerce.Services.Tracking;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    public class IdentifyCommerceUserProcessorTests
    {
        private readonly ICommerceTrackingService commerceTrackingService;

        private readonly IFixture fixture;

        private readonly ILogService<CommonLog> logService;

        private readonly IdentifyCommerceUserProcessor processor;

        public IdentifyCommerceUserProcessorTests()
        {
            this.logService = Substitute.For<ILogService<CommonLog>>();
            this.commerceTrackingService = Substitute.For<ICommerceTrackingService>();
            this.fixture = new Fixture();

            this.processor = new IdentifyCommerceUserProcessor(this.commerceTrackingService, this.logService);
        }

        [Fact]
        public void Process_IfArgsNotNull_ShouldCallCommerceTrackingServiceIdentifyAs()
        {
            // arrange
            var args = new LoginPipelineArgs
            {
                UserName = this.fixture.Create<string>()
            };

            // act
            this.processor.Process(args);

            // assert
            this.commerceTrackingService.Received(1).IdentifyAs(Constants.Login.CommerceUserSource, args.UserName);
        }
    }
}