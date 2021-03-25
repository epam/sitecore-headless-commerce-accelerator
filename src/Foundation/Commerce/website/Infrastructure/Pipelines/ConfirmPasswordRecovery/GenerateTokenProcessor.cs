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
    using Account.Managers.User;

    using Sitecore.Data;
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines.PasswordRecovery;
    using Sitecore.Security.Accounts;
    using Sitecore.Shell.Applications.ContentManager.ReturnFieldEditorValues;

    public class GenerateTokenProcessor : PasswordRecoveryProcessor
    {
        private readonly IUserManager userManager;

        public GenerateTokenProcessor(IUserManager userManager)
        {
            Assert.ArgumentNotNull(userManager, nameof(userManager));

            this.userManager = userManager;
        }

        public override void Process(PasswordRecoveryArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            Assert.ArgumentNotNullOrEmpty(args.Username, nameof(args.Username));

            var user = this.userManager.GetUserFromName(args.Username, true);

            if (user == null)
            {
                args.AbortPipeline();
                return;
            }

            var token = ID.NewID.ToShortID().ToString();

            this.userManager.AddCustomProperty(user, Constants.PasswordRecovery.ConfirmTokenKey, token);

            args.CustomData.Add(Constants.PasswordRecovery.ConfirmTokenKey, token);
        }
    }
}