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

    using Mappers.Users;

    using Providers;

    using Sitecore.Diagnostics;

    public class GetCommerceUserProcessor : SafePipelineProcessor<LoginPipelineArgs>
    {
        private readonly ICustomerProvider customerProvider;

        private readonly IUserMapper userMapper;

        public GetCommerceUserProcessor(
            ICustomerProvider customerProvider,
            IUserMapper userMapper,
            ILogService<CommonLog> logService)
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

            var user = this.customerProvider.GetUser(args.Email);

            if (user == null)
            {
                args.IsInvalidCredentials = true;
                args.AbortPipeline();
            }
            else
            {
                this.userMapper.MapToLoginPipelineArgs(user, args);
            }
        }
    }
}