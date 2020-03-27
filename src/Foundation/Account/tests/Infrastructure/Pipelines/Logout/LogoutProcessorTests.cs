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

namespace Wooli.Foundation.Account.Tests.Infrastructure.Pipelines.Logout
{
    using Account.Infrastructure.Pipelines.Logout;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using Managers.Authentication;

    using NSubstitute;

    using Xunit;

    public class LogoutProcessorTests
    {
        private readonly IAuthenticationManager authenticationManager;

        private readonly ILogService<CommonLog> logService;

        private readonly LogoutProcessor logoutProcessor;

        public LogoutProcessorTests()
        {
            this.authenticationManager = Substitute.For<IAuthenticationManager>();
            this.logService = Substitute.For<ILogService<CommonLog>>();

            this.logoutProcessor = new LogoutProcessor(this.authenticationManager, this.logService);
        }

        [Fact]
        public void Process_IfArgsNotNull_ShouldCallAuthenticationManagerLogout()
        {
            // arrange
            var loginArgs = new LogoutPipelineArgs();

            // act
            this.logoutProcessor.Process(loginArgs);

            // assert
            this.authenticationManager.Received(1).Logout();
        }
    }
}