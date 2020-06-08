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

namespace HCA.Foundation.Commerce.Infrastructure.Pipelines.Login
{
    using Account.Infrastructure.Pipelines.Login;

    using Base.Infrastructure.Pipelines;
    using Base.Models.Logging;
    using Base.Services.Logging;

    using Services.Cart;

    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;

    public class MergeCartsProcessor : SafePipelineProcessor<LoginPipelineArgs>
    {
        private readonly ICartService cartService;

        public MergeCartsProcessor(ICartService cartService, ILogService<CommonLog> logService)
            : base(logService)
        {
            Assert.ArgumentNotNull(cartService, nameof(cartService));

            this.cartService = cartService;
        }

        protected override void SafeProcess(LoginPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            if (args.AnonymousContactId != null)
            {
                var result = this.cartService.MergeCarts(args.AnonymousContactId);

                if (!result.Success)
                {
                    foreach (var resultError in result.Errors)
                    {
                        args.AddMessage(resultError, PipelineMessageType.Warning);
                    }

                    args.AbortPipeline();
                }
            }
        }
    }
}