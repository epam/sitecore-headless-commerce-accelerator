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

namespace HCA.Foundation.Account.Tests.Infrastructure.Pipelines.Login
{
    using Account.Infrastructure.Pipelines.Login;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using Managers.Authentication;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    public class LoginProcessorTests
    {
        private readonly IAuthenticationManager authenticationManager;

        private readonly IFixture fixture;

        private readonly ILogService<CommonLog> logService;

        private readonly LoginProcessor processor;

        public LoginProcessorTests()
        {
            this.authenticationManager = Substitute.For<IAuthenticationManager>();
            this.logService = Substitute.For<ILogService<CommonLog>>();
            this.fixture = new Fixture();

            this.processor = new LoginProcessor(this.authenticationManager, this.logService);
        }

        [Fact]
        public void Process_IfArgsNotNull_ShouldCallAuthenticationManagerLogin()
        {
            // arrange
            var args = new LoginPipelineArgs
            {
                UserName = this.fixture.Create<string>(),
                Password = this.fixture.Create<string>()
            };

            // act
            this.processor.Process(args);

            // assert
            this.authenticationManager.Received(1).Login(args.UserName, args.Password);
        }

        [Fact]
        public void Process_IfLoginReturnsFalse_ShouldAbortPipelineSetTrueToIsInvalidCredentials()
        {
            // arrange
            var args = new LoginPipelineArgs();
            this.authenticationManager.Login(Arg.Any<string>(), Arg.Any<string>()).Returns(false);

            // act
            this.processor.Process(args);

            // assert
            Assert.True(args.Aborted);
            Assert.True(args.IsInvalidCredentials);
        }

        [Fact]
        public void Process_IfLoginReturnsTrue_ShouldSetFalseToIsInvalidCredentials()
        {
            // arrange
            var args = new LoginPipelineArgs();
            this.authenticationManager.Login(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            // act
            this.processor.Process(args);

            // assert
            Assert.False(args.IsInvalidCredentials);
        }
    }
}