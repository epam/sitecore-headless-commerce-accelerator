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

namespace Wooli.Foundation.Commerce.Infrastructure.Pipelines.Login
{
    using System.Web.Security;

    using Account.Infrastructure.Pipelines.Login;

    using Base.Infrastructure.Pipelines;
    using Base.Models.Logging;
    using Base.Services.Logging;

    using ModelMappers.Users;

    using Providers;

    using Sitecore.Diagnostics;

    public class GetCommerceUserProcessor : SafePipelineProcessor<LoginPipelineArgs>
    {
        private readonly ICustomerProvider customerProvider;

        private readonly IUserMapper userMapper;

        public GetCommerceUserProcessor(ICustomerProvider customerProvider, IUserMapper userMapper, ILogService<CommonLog> logService)
            : base(logService)
        {
            Assert.ArgumentNotNull(customerProvider, nameof(customerProvider));
            Assert.ArgumentNotNull(userMapper, nameof(userMapper));

            this.customerProvider = customerProvider;
            this.userMapper = userMapper;
        }

        protected override void SafeProcess(LoginPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            var userName = Membership.GetUserNameByEmail(args.Email);
            if (string.IsNullOrWhiteSpace(userName))
            {
                args.AbortPipeline();
                return;
            }

            var user = this.customerProvider.GetCommerceUser(userName);

            if (user == null)
            {
                args.AbortPipeline();
            }
            else
            {
                this.userMapper.MapToLoginPipelineArgs(user, args);
            }
        }
    }
}