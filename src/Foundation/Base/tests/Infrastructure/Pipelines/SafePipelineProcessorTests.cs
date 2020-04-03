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

namespace Wooli.Foundation.Base.Tests.Infrastructure.Pipelines
{
    using System;

    using Base.Infrastructure.Pipelines;

    using Models.Logging;

    using NSubstitute;

    using Services.Logging;

    using Sitecore.Pipelines;

    using Xunit;

    public class SafePipelineProcessorTests
    {
        private readonly ILogService<CommonLog> logService;

        public SafePipelineProcessorTests()
        {
            this.logService = Substitute.For<ILogService<CommonLog>>();
        }

        [Fact]
        public void
            Process_IfSafeProcessThrowsException_ShouldWriteExceptionToLogNotThrowExceptionAddMessageToArgsAbortPipeline()
        {
            // arrange
            var processor = new ThrowsExceptionProcessor(this.logService);
            var args = new PipelineArgs();

            // act
            var exception = Record.Exception(() => processor.Process(args));

            // assert
            Assert.Null(exception);
            this.logService.Received(1).Error(Arg.Any<string>());
            Assert.True(args.Aborted);
            Assert.NotEmpty(args.GetMessages(PipelineMessageFilter.Errors));
        }

        private class ThrowsExceptionProcessor : SafePipelineProcessor<PipelineArgs>
        {
            public ThrowsExceptionProcessor(ILogService<CommonLog> logService)
                : base(logService)
            {
            }

            protected override void SafeProcess(PipelineArgs args)
            {
                throw new NotImplementedException();
            }
        }
    }
}