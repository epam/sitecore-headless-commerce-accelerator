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

namespace HCA.Foundation.Commerce.Infrastructure.Pipelines.ConfirmPasswordRecovery
{
    using Base.Infrastructure.Pipelines;
    using Base.Models.Logging;
    using Base.Services.Logging;
    using Providers;

    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;

    public class VerifyEmailProcessor : SafePipelineProcessor<ConfirmPasswordRecoveryArgs>
    {
        private readonly ICustomerProvider customerProvider;

        public VerifyEmailProcessor(ICustomerProvider customerProvider, ILogService<CommonLog> logService)
           : base(logService)
        {
            Assert.ArgumentNotNull(customerProvider, nameof(customerProvider));
            this.customerProvider = customerProvider;
        }

        protected override void SafeProcess(ConfirmPasswordRecoveryArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            Assert.ArgumentNotNullOrEmpty(args.UserEmail, nameof(args.UserEmail));

            var user = this.customerProvider.GetUser(args.UserEmail);

            if (user == null)
            {
                this.LogService.Debug($"[{nameof(VerifyEmailProcessor)}.{nameof(SafeProcess)}]: {Constants.ErrorMessages.UserNotFoundEmail}");

                args.IsEmailValid = false;
                args.AddMessage(Constants.ErrorMessages.UserNotFoundEmail, PipelineMessageType.Error);
                args.AbortPipeline();
            }
            else
            {
                args.IsEmailValid = true;
                args.Username = user.UserName;
            }
        }
    }
}