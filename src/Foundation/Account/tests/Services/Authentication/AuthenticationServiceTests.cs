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

namespace Wooli.Foundation.Account.Tests.Services.Authentication
{
    using Account.Infrastructure.Pipelines.Login;
    using Account.Infrastructure.Pipelines.Logout;
    using Account.Services.Authentication;

    using Base.Services.Pipeline;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    public class AuthenticationServiceTests
    {
        private readonly AuthenticationService authenticationService;

        private readonly string email;

        private readonly IFixture fixture;

        private readonly string password;

        private readonly IPipelineService pipelineService;

        public AuthenticationServiceTests()
        {
            this.pipelineService = Substitute.For<IPipelineService>();
            this.fixture = new Fixture();

            this.authenticationService = new AuthenticationService(this.pipelineService);

            this.email = this.fixture.Create<string>();
            this.password = this.fixture.Create<string>();
        }

        [Fact]
        public void Login_IfPipelineAborted_ShouldReturnFailResult()
        {
            // arrange
            this.pipelineService
                .When(x => x.RunPipeline(Constants.Pipelines.Login, Arg.Any<LoginPipelineArgs>()))
                .Do(
                    info =>
                    {
                        var args = info[1] as LoginPipelineArgs;

                        args.AbortPipeline();
                    });

            // act
            var result = this.authenticationService.Login(this.email, this.password);

            // assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Login_IfPipelineAbortedAndCredentialsInvalid_ShouldReturnFailResultWithInvalidCredantialInData()
        {
            // arrange
            this.pipelineService
                .When(x => x.RunPipeline(Constants.Pipelines.Login, Arg.Any<LoginPipelineArgs>()))
                .Do(
                    info =>
                    {
                        var args = info[1] as LoginPipelineArgs;

                        args.IsInvalidCredentials = true;
                        args.AbortPipeline();
                    });

            // act
            var result = this.authenticationService.Login(this.email, this.password);

            // assert
            Assert.False(result.Success);
            Assert.True(result.Data.IsInvalidCredentials);
        }

        [Fact]
        public void Login_IfPipelineNotAborted_ShouldReturnSuccessResult()
        {
            // act
            var result = this.authenticationService.Login(this.email, this.password);

            // assert
            Assert.True(result.Success);
        }

        [Fact]
        public void Login_ShouldRunLoginPipeline()
        {
            // act
            this.authenticationService.Login(this.email, this.password);

            // assert
            this.pipelineService.Received(1).RunPipeline(Constants.Pipelines.Login, Arg.Any<LoginPipelineArgs>());
        }

        [Fact]
        public void Logout_IfPipelineAborted_ShouldReturnFailResult()
        {
            // arrange
            this.pipelineService
                .When(x => x.RunPipeline(Constants.Pipelines.Logout, Arg.Any<LogoutPipelineArgs>()))
                .Do(
                    info =>
                    {
                        var args = info[1] as LogoutPipelineArgs;

                        args.AbortPipeline();
                    });

            // act
            var result = this.authenticationService.Logout();

            // assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Logout_IfPipelineNotAborted_ShouldReturnSuccessResult()
        {
            // act
            var result = this.authenticationService.Logout();

            // assert
            Assert.True(result.Success);
        }

        [Fact]
        public void Logout_ShouldRunLogoutPipeline()
        {
            // act
            this.authenticationService.Logout();

            // assert
            this.pipelineService.Received(1).RunPipeline(Constants.Pipelines.Logout, Arg.Any<LogoutPipelineArgs>());
        }
    }
}