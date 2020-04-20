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

namespace HCA.Foundation.Connect.Tests.Managers
{
    using Base.Models.Logging;
    using Base.Services.Logging;

    using Connect.Managers;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Sitecore.Commerce.Services;

    using Xunit;

    public class BaseManagerTests
    {
        private readonly IFixture fixture;

        private readonly ILogService<CommonLog> logService;

        private readonly TestManager manager;

        public BaseManagerTests()
        {
            this.logService = Substitute.For<ILogService<CommonLog>>();
            this.fixture = new Fixture();

            this.manager = new TestManager(this.logService);
        }

        [Fact]
        public void Execute_IfFunctionReturnsFailResult_ShouldCallLogServiceError()
        {
            // arrange
            var failResult = this.fixture.Build<ServiceProviderResult>()
                .With(result => result.Success, false)
                .Create();
            failResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            var request = this.fixture.Create<ServiceProviderRequest>();

            // act
            this.manager.Execute(request, providerRequest => failResult);

            // assert
            this.logService.Received(failResult.SystemMessages.Count).Error(Arg.Any<string>());
        }

        [Fact]
        public void Execute_IfFunctionReturnsSuccessfulResult_ShouldNotCallLogServiceError()
        {
            // arrange
            var successResult = this.fixture.Build<ServiceProviderResult>()
                .With(result => result.Success, true)
                .Create();
            var request = this.fixture.Create<ServiceProviderRequest>();

            // act
            this.manager.Execute(request, providerRequest => successResult);

            // assert
            this.logService.DidNotReceive().Error(Arg.Any<string>());
        }

        private class TestManager : BaseManager
        {
            public TestManager(ILogService<CommonLog> logService) : base(logService)
            {
            }
        }
    }
}