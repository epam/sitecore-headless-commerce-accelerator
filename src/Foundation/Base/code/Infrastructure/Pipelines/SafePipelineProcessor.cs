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

namespace HCA.Foundation.Base.Infrastructure.Pipelines
{
    using System;

    using Models.Logging;

    using Services.Logging;

    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;

    public abstract class SafePipelineProcessor<T> : PipelineProcessor<T>
        where T : PipelineArgs
    {
        protected readonly ILogService<CommonLog> LogService;

        protected SafePipelineProcessor(ILogService<CommonLog> logService)
        {
            Assert.ArgumentNotNull(logService, nameof(logService));

            this.LogService = logService;
        }

        public override sealed void Process(T args)
        {
            try
            {
                this.SafeProcess(args);
            }
            catch (Exception e)
            {
                args.AddMessage(e.Message, PipelineMessageType.Error);
                this.LogService.Error(e.Message);
                args.AbortPipeline();
            }
        }

        protected abstract void SafeProcess(T args);
    }
}