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

namespace Wooli.Foundation.Account.Infrastructure.Pipelines.Login
{
    using Authentication;

    using Base.Infrastructure.Pipelines;
    using Base.Models.Logging;
    using Base.Services.Logging;

    using Sitecore.Diagnostics;

    public class LoginProcessor : SafePipelineProcessor<LoginPipelineArgs>
    {
        private readonly IAuthenticationService authenticationService;

        public LoginProcessor(IAuthenticationService authenticationService, ILogService<CommonLog> logService)
            : base(logService)
        {
            Assert.ArgumentNotNull(authenticationService, nameof(authenticationService));
            this.authenticationService = authenticationService;
        }

        protected override void SafeProcess(LoginPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            if (!this.authenticationService.Login(args.UserName, args.Password))
            {
                args.AbortPipeline();
            }
        }
    }
}