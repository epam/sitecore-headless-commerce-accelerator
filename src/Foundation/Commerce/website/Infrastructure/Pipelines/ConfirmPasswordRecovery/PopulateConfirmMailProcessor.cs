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
    using System;
    using System.Text;

    using Sitecore.Pipelines.PasswordRecovery;
    using Sitecore.Web;
    using System.Text.RegularExpressions;

    public class PopulateConfirmMailProcessor : PopulateMail
    {
        protected virtual string GenerateConfirmLink(string token, string userName)
        {
            var serverUrl = Sitecore.StringUtil.EnsurePostfix('/', WebUtil.GetServerUrl());
            var resetLink =
                $"{serverUrl}{Constants.PasswordRecovery.ResetPasswordUrl}?username={userName.Replace(Constants.PasswordRecovery.UsersDomain + "\\", "")}&token={token}";

            return resetLink;
        }

        protected override string GetEmailContent(PasswordRecoveryArgs args, string emailContentTemplate)
        {
            if (!DoesStringContainToken(emailContentTemplate, Constants.PasswordRecovery.PasswordRecoveryLink))
            {
                throw new ArgumentException(Constants.ErrorMessages.PasswordRecoveryLinkTokenMissing);
            }

            return Regex.Replace(
                emailContentTemplate, 
                Constants.PasswordRecovery.PasswordRecoveryLink, 
                this.GenerateConfirmLink(args.CustomData[Constants.PasswordRecovery.ConfirmTokenKey] as string, args.Username), 
                RegexOptions.IgnoreCase);
        }

        private static bool DoesStringContainToken(string email, string token)
        {
            return !(email.IndexOf(token, StringComparison.InvariantCultureIgnoreCase) < 0);
        }
    }
}