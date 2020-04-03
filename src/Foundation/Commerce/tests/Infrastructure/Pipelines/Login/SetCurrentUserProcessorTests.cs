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

namespace Wooli.Foundation.Commerce.Tests.Infrastructure.Pipelines.Login
{
    using Account.Infrastructure.Pipelines.Login;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using Commerce.Infrastructure.Pipelines.Login;
    using Commerce.Mappers.Users;

    using Context;

    using Models.Entities.Users;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    public class SetCurrentUserProcessorTests
    {
        private readonly IFixture fixture;

        private readonly ILogService<CommonLog> logService;

        private readonly SetCurrentUserProcessor processor;

        private readonly IUserMapper userMapper;

        private readonly IVisitorContext visitorContext;

        public SetCurrentUserProcessorTests()
        {
            this.fixture = new Fixture();
            this.logService = Substitute.For<ILogService<CommonLog>>();
            this.visitorContext = Substitute.For<IVisitorContext>();
            this.userMapper = Substitute.For<IUserMapper>();

            this.processor = new SetCurrentUserProcessor(this.visitorContext, this.userMapper, this.logService);
        }

        [Fact]
        public void Process_IfArgsIsNotNull_ShouldCallUserMapperMap()
        {
            // arrange
            var args = new LoginPipelineArgs();

            // act
            this.processor.Process(args);

            // assert
            this.userMapper.Received(1).Map<LoginPipelineArgs, User>(args);
        }

        [Fact]
        public void Process_IfArgsIsNotNull_ShouldSetAnonymousContactIdInArgs()
        {
            // arrange
            var args = new LoginPipelineArgs();
            var contactId = this.fixture.Create<string>();
            this.visitorContext.ContactId.Returns(contactId);

            // act
            this.processor.Process(args);

            // assert
            Assert.Equal(contactId, args.AnonymousContactId);
        }

        [Fact]
        public void Process_IfMapperReturnsUSer_ShouldSetVisitorContextCurrentUser()
        {
            // arrange
            var args = new LoginPipelineArgs();
            var user = this.fixture.Create<User>();
            this.userMapper.Map<LoginPipelineArgs, User>(args).Returns(user);

            // act
            this.processor.Process(args);

            // assert
            this.visitorContext.Received(1).CurrentUser = user;
        }
    }
}