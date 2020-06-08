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

namespace HCA.Foundation.Account.Infrastructure.Pipelines.Logout
{
    using Base.Infrastructure.Pipelines;
    using Base.Models.Logging;
    using Base.Services.Logging;

    using Managers.Authentication;

    using Sitecore.Diagnostics;

    public class LogoutProcessor : SafePipelineProcessor<LogoutPipelineArgs>
    {
        private readonly IAuthenticationManager authenticationManager;

        public LogoutProcessor(IAuthenticationManager authenticationManager, ILogService<CommonLog> logService)
            : base(logService)
        {
            Assert.ArgumentNotNull(authenticationManager, nameof(authenticationManager));
            this.authenticationManager = authenticationManager;
        }

        protected override void SafeProcess(LogoutPipelineArgs args)
        {
            this.authenticationManager.Logout();
        }
    }
}