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

    using Xunit;

    using Account.Managers.User;

    using Base.Tests.Common.Utils;

    using Ploeh.AutoFixture;

    using Sitecore.Security.Accounts;

    public class GenerateTokenProcessorTests
    {
        private readonly GenerateTokenProcessor generateTokenProcessor;

        private readonly IUserManager userManager;

        private readonly IFixture fixture;

        public GenerateTokenProcessorTests()
        {
            this.userManager = Substitute.For<IUserManager>();
            this.generateTokenProcessor = new GenerateTokenProcessor(this.userManager);
            this.fixture = new Fixture();
        }

        [Fact]
        public void Process_IfArgsNotNull_ShouldCallUserManagerGetUserFromName()
        {
            // arrange
            HttpContextFaker.FakeGenericPrincipalContext();
            var args = new ConfirmPasswordRecoveryArgs(HttpContext.Current)
            {
                Username = this.fixture.Create<string>()
            };

            // act
            this.generateTokenProcessor.Process(args);

            // assert
            this.userManager.Received(1).GetUserFromName(args.Username, true);
        }

        [Fact]
        public void Process_IfUserManagerGetUserFromNameReturnsNull_ShouldAbortPipeline()
        {
            // arrange
            HttpContextFaker.FakeGenericPrincipalContext();
            var args = new ConfirmPasswordRecoveryArgs(HttpContext.Current)
            {
                Username = this.fixture.Create<string>()
            };
            this.userManager.GetUserFromName(Arg.Any<string>(), Arg.Any<bool>()).Returns((User)null);

            // act
            this.generateTokenProcessor.Process(args);

            // assert
            Assert.True(args.Aborted);
        }

        [Fact]
        public void Process_IfUserManagerGetUserFromNameReturnsUser_ShouldCallUserManagerAddCustomProperty()
        {
            // arrange
            HttpContextFaker.FakeGenericPrincipalContext();
            var args = new ConfirmPasswordRecoveryArgs(HttpContext.Current)
            {
                Username = this.fixture.Create<string>()
            };
            var user = User.FromName(args.Username, false);
            this.userManager.GetUserFromName(Arg.Any<string>(), Arg.Any<bool>()).Returns(user);

            // act
            this.generateTokenProcessor.Process(args);

            // assert
            this.userManager.Received(1).AddCustomProperty(user, Constants.PasswordRecovery.ConfirmTokenKey, args.CustomData[Constants.PasswordRecovery.ConfirmTokenKey].ToString());
        }

        [Fact]
        public void Process_IfUserManagerGetUserFromNameReturnsUser_ShouldAddTokenToPipelineArgsCustomData()
        {
            // arrange
            HttpContextFaker.FakeGenericPrincipalContext();
            var args = new ConfirmPasswordRecoveryArgs(HttpContext.Current)
            {
                Username = this.fixture.Create<string>()
            };
            this.userManager.GetUserFromName(Arg.Any<string>(), Arg.Any<bool>()).Returns(User.FromName(args.Username, false));

            // act
            this.generateTokenProcessor.Process(args);

            // assert
            Assert.Contains(args.CustomData, a => a.Key == Constants.PasswordRecovery.ConfirmTokenKey);
        }
    }
}
