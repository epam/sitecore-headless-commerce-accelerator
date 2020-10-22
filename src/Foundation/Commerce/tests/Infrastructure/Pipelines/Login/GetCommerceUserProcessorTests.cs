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
    using Commerce.Mappers.Users;
    using Commerce.Providers;

    using Models.Entities.Users;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    public class GetCommerceUserProcessorTests
    {
        private readonly ICustomerProvider customerProvider;

        private readonly IFixture fixture;

        private readonly ILogService<CommonLog> logService;

        private readonly GetCommerceUserProcessor processor;

        private readonly IUserMapper userMapper;

        public GetCommerceUserProcessorTests()
        {
            this.fixture = new Fixture();
            this.customerProvider = Substitute.For<ICustomerProvider>();
            this.userMapper = Substitute.For<IUserMapper>();
            this.logService = Substitute.For<ILogService<CommonLog>>();

            this.processor = new GetCommerceUserProcessor(this.customerProvider, this.userMapper, this.logService);
        }

        [Fact]
        public void Process_IfArgsNotNull_ShouldCallCustomerProviderGetUser()
        {
            // arrange
            var args = new LoginPipelineArgs
            {
                Email = this.fixture.Create<string>()
            };

            // act
            this.processor.Process(args);

            // assert
            this.customerProvider.Received(1).GetUser(args.Email);
        }

        [Fact]
        public void Process_IfCustomerProviderReturnsNull_ShouldAbortPipelineAndSetTrueToIsInvalidCredentials()
        {
            // arrange
            var args = new LoginPipelineArgs();
            this.customerProvider.GetUser(Arg.Any<string>()).Returns((User)null);

            // act
            this.processor.Process(args);

            // assert
            Assert.True(args.Aborted);
            Assert.True(args.IsInvalidCredentials);
        }

        [Fact]
        public void Process_IfCustomerProviderReturnsValidUser_ShouldCallMapToLoginPipelineArgs()
        {
            // arrange
            var args = new LoginPipelineArgs();
            var user = new User();
            this.customerProvider.GetUser(Arg.Any<string>()).Returns(user);

            // act
            this.processor.Process(args);

            // assert
            if (System.Web.Security.Membership.ValidateUser(user?.UserName, args.Password))
                this.userMapper.Received(1).MapToLoginPipelineArgs(user, args);
        }
    }
}