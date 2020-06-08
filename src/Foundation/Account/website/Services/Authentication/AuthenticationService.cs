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

namespace HCA.Foundation.Account.Services.Authentication
{
    using System;
    using System.Linq;
    using System.Web.Security;

    using Base.Models.Result;
    using Base.Services.Pipeline;

    using DependencyInjection;

    using Infrastructure.Pipelines.Login;
    using Infrastructure.Pipelines.Logout;

    using Models.Authentication;

    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;

    [Service(typeof(IAuthenticationService), Lifetime = Lifetime.Transient)]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPipelineService pipelineService;

        public AuthenticationService(IPipelineService pipelineService)
        {
            Assert.ArgumentNotNull(pipelineService, nameof(pipelineService));

            this.pipelineService = pipelineService;
        }

        public Result<LoginResult> Login(string email, string password)
        {
            var args = new LoginPipelineArgs
            {
                Email = email,
                Password = password
            };

            this.pipelineService.RunPipeline(Constants.Pipelines.Login, args);

            return this.ResolveResult(
                args,
                pipelineArgs => new LoginResult
                {
                    IsInvalidCredentials = pipelineArgs.IsInvalidCredentials
                });
        }

        public Result<VoidResult> Logout()
        {
            var args = new LogoutPipelineArgs();
            this.pipelineService.RunPipeline(Constants.Pipelines.Logout, args);

            return this.ResolveResult<VoidResult, LogoutPipelineArgs>(args);
        }

        public bool ValidateUser(string email, string password)
        {
            var userName = Membership.GetUserNameByEmail(email);
            if (!string.IsNullOrWhiteSpace(userName))
            {
                return Membership.ValidateUser(userName, password);
            }

            return false;
        }

        private Result<TResult> ResolveResult<TResult, TArgs>(TArgs args, Func<TArgs, TResult> function = null)
            where TResult : class
            where TArgs : PipelineArgs
        {
            var errorMessages = args.GetMessages(PipelineMessageFilter.Errors);
            var result = function?.Invoke(args);
            return args.Aborted
                ? new Result<TResult>(result, errorMessages.Select(message => message.Text).ToList())
                {
                    Success = false
                }
                : new Result<TResult>(result);
        }
    }
}