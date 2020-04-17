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

    using Context;

    using Mappers.Users;

    using Models.Entities.Users;

    using Sitecore.Diagnostics;

    public class SetCurrentUserProcessor : SafePipelineProcessor<LoginPipelineArgs>
    {
        private readonly IUserMapper userMapper;

        private readonly IVisitorContext visitorContext;

        public SetCurrentUserProcessor(
            IVisitorContext visitorContext,
            IUserMapper userMapper,
            ILogService<CommonLog> logService)
            : base(logService)
        {
            Assert.ArgumentNotNull(visitorContext, nameof(visitorContext));
            Assert.ArgumentNotNull(userMapper, nameof(userMapper));

            this.visitorContext = visitorContext;
            this.userMapper = userMapper;
        }

        protected override void SafeProcess(LoginPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            args.AnonymousContactId = this.visitorContext.ContactId;

            var user = this.userMapper.Map<LoginPipelineArgs, User>(args);
            this.visitorContext.CurrentUser = user;
        }
    }
}