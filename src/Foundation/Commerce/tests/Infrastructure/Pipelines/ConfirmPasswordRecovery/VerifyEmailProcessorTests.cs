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

namespace HCA.Foundation.Commerce.Tests.Infrastructure.Pipelines.ConfirmPasswordRecovery
{
    using Commerce.Infrastructure.Pipelines.ConfirmPasswordRecovery;

    using NSubstitute;

    using System.Web;

    using Base.Tests.Utils;

    using Xunit;

    using Commerce.Providers;
    using Commerce.Providers.Customer;

    using HCA.Foundation.Base.Services.Logging;
    using HCA.Foundation.Base.Models.Logging;

    using Models.Entities.Users;

    using Ploeh.AutoFixture;

    public class VerifyEmailProcessorTests
    {
        private readonly VerifyEmailProcessor verifyEmailProcessor;

        private readonly ICustomerProvider customerProvider;

        private readonly ILogService<CommonLog> logService;

        private readonly IFixture fixture;

        public VerifyEmailProcessorTests()
        {
            this.customerProvider = Substitute.For<ICustomerProvider>();
            this.logService = Substitute.For<ILogService<CommonLog>>();
            this.fixture = new Fixture();

            this.verifyEmailProcessor = new VerifyEmailProcessor(this.customerProvider, this.logService);
        }

        [Fact]
        public void Process_IfArgsNotNull_ShouldCallCustomerProviderGetUser()
        {
            // arrange
            HttpContextFaker.FakeGenericPrincipalContext();

            var args = new ConfirmPasswordRecoveryArgs(HttpContext.Current)
            {
                UserEmail = this.fixture.Create<string>()
            };

            // act
            this.verifyEmailProcessor.Process(args);

            // assert
            this.customerProvider.Received(1).GetUser(args.UserEmail);
        }

        [Fact]
        public void Process_IfEmailDoesNotExists_ShouldAbortPipelineAndSetFalseToIsEmailValid()
        {
            // arrange
            HttpContextFaker.FakeGenericPrincipalContext();

            var args = new ConfirmPasswordRecoveryArgs(HttpContext.Current)
            {
                UserEmail = this.fixture.Create<string>()
            };

            // act
            this.verifyEmailProcessor.Process(args);

            // assert
            Assert.True(args.Aborted);
            Assert.False(args.IsEmailValid);
        }

        [Fact]
        public void Process_IfCustomerProviderGetUserReturnsNull_ShouldAbortPipelineAndSetFalseToIsEmailValid()
        {
            // arrange
            HttpContextFaker.FakeGenericPrincipalContext();
            var args = new ConfirmPasswordRecoveryArgs(HttpContext.Current)
            {
                UserEmail = this.fixture.Create<string>()
            };
            this.customerProvider.GetUser(args.UserEmail).Returns((User)null);

            // act
            this.verifyEmailProcessor.Process(args);

            // assert
            Assert.True(args.Aborted);
            Assert.False(args.IsEmailValid);
        }

        [Fact]
        public void Process_IfCustomerProviderGetUserReturnsUser_ShouldAddUsernameToArgsAndSetTrueToIsEmailValid()
        {
            // arrange
            HttpContextFaker.FakeGenericPrincipalContext();
            var args = new ConfirmPasswordRecoveryArgs(HttpContext.Current)
            {
                UserEmail = this.fixture.Create<string>()
            };
            var user = new User { UserName = this.fixture.Create<string>() };
            this.customerProvider.GetUser(args.UserEmail).Returns(user);

            // act
            this.verifyEmailProcessor.Process(args);

            // assert
            Assert.True(args.IsEmailValid);
            Assert.True(!string.IsNullOrWhiteSpace(args.Username));
        }
    }
}
