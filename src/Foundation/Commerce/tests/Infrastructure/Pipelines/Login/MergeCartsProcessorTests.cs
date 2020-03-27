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
    using System.Linq;

    using Account.Infrastructure.Pipelines.Login;

    using Base.Models;
    using Base.Models.Logging;
    using Base.Services.Logging;

    using Commerce.Infrastructure.Pipelines.Login;
    using Commerce.Services.Cart;

    using Models.Entities.Cart;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Sitecore.Pipelines;

    using Xunit;

    public class MergeCartsProcessorTests
    {
        private readonly MergeCartsProcessor processor;

        private readonly IFixture fixture;

        private readonly ILogService<CommonLog> logService;

        private readonly ICartService cartService;

        public MergeCartsProcessorTests()
        {
            this.fixture = new Fixture();
            this.logService = Substitute.For<ILogService<CommonLog>>();
            this.cartService = Substitute.For<ICartService>();

            this.processor = new MergeCartsProcessor(this.cartService, this.logService);
        }

        [Fact]
        public void Process_IfAnonymousContactIdIsNull_ShouldNotCallCrtServiceMergeCarts()
        {
            // arrange
            var args = new LoginPipelineArgs
            {
                AnonymousContactId = null
            };

            // act
            this.processor.Process(args);

            // assert
            this.cartService.DidNotReceive().MergeCarts(Arg.Any<string>());
        }

        [Fact]
        public void Process_IfAnonymousContactIdIsNotNull_ShouldCallCartServiceMergeCarts()
        {
            // arrange
            var args = new LoginPipelineArgs
            {
                AnonymousContactId = this.fixture.Create<string>()
            };

            // act
            this.processor.Process(args);

            // assert
            this.cartService.Received(1).MergeCarts(args.AnonymousContactId);
        }

        [Fact]
        public void Process_IfCartServiceMergeCartWasSuccess_ShouldNotAbortPipeline()
        {
            // arrange
            var args = new LoginPipelineArgs
            {
                AnonymousContactId = this.fixture.Create<string>()
            };
            var successResult = this.fixture.Build<Result<Cart>>()
                .With(result => result.Success, true)
                .Create();
            this.cartService.MergeCarts(args.AnonymousContactId).Returns(successResult);

            // act
            this.processor.Process(args);

            // assert
            Assert.False(args.Aborted);
        }

        [Fact]
        public void Process_IfCartServiceMergeCartWasNotSuccess_ShouldAbortPipelineAndAddMessages()
        {
            // arrange
            var args = new LoginPipelineArgs
            {
                AnonymousContactId = this.fixture.Create<string>()
            };
            var failResult = this.fixture.Build<Result<Cart>>()
                .With(result => result.Success, false)
                .Create();
            this.cartService.MergeCarts(args.AnonymousContactId).Returns(failResult);

            // act
            this.processor.Process(args);

            // assert
            Assert.True(args.Aborted);
            Assert.Equal(
                failResult.Errors,
                args.GetMessages(PipelineMessageFilter.Warnings).Select(pipelineMessage => pipelineMessage.Text));
        }
    }
}