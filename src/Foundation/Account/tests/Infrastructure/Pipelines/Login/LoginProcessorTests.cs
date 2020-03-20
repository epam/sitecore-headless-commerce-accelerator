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

namespace Wooli.Foundation.Account.Tests.Infrastructure.Pipelines.Login
{
    using Account.Infrastructure.Pipelines.Login;

    using Authentication;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    public class LoginProcessorTests
    {
        private readonly IFixture fixture;

        private readonly IAuthenticationService authenticationService;

        private readonly ILogService<CommonLog> logService;

        private readonly LoginProcessor loginProcessor;

        public LoginProcessorTests()
        {
            this.authenticationService = Substitute.For<IAuthenticationService>();
            this.logService = Substitute.For<ILogService<CommonLog>>();
            this.fixture = new Fixture();

            this.loginProcessor = new LoginProcessor(this.authenticationService, this.logService);
        }

        [Fact]
        public void Process_IfArgsNotNull_ShouldCallAuthenticationServiceLogin()
        {
            // arrange
            var loginArgs = new LoginPipelineArgs()
            {
                UserName = this.fixture.Create<string>(),
                Password = this.fixture.Create<string>()
            };

            // act
            this.loginProcessor.Process(loginArgs);

            // assert
            this.authenticationService.Received(1).Login(loginArgs.UserName, loginArgs.Password);
        }

        [Fact]
        public void Process_IfLoginReturnsFalse_ShouldAbortPipeline()
        {
            // arrange
            var loginArgs = new LoginPipelineArgs();
            this.authenticationService.Login(Arg.Any<string>(), Arg.Any<string>()).Returns(false);

            // act
            this.loginProcessor.Process(loginArgs);

            // assert
            Assert.True(loginArgs.Aborted);
        }

        [Fact]
        public void Process_IfLoginReturnsTrue_ShouldNotThrowsException()
        {
            // arrange
            var loginArgs = new LoginPipelineArgs();
            this.authenticationService.Login(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            // act
            var exception = Record.Exception(() => this.loginProcessor.Process(loginArgs));

            // assert
            Assert.Null(exception);
        }
    }
}