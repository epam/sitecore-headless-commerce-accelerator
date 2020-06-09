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

namespace HCA.Feature.Account.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Foundation.Account.Services.Authentication;
    using Foundation.Base.Controllers;
    using Foundation.Base.Extensions;
    using Foundation.Commerce.Models.Authentication;

    using Models.Requests;

    using Sitecore.Diagnostics;

    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            Assert.ArgumentNotNull(authenticationService, nameof(authenticationService));

            this.authenticationService = authenticationService;
        }

        // TODO: Add error transfer to FE
        [HttpPost]
        [ActionName("login")]
        public ActionResult Login(LoginRequest request, string returnUrl)
        {
            return this.Execute(
                () => this.authenticationService.Login(request.Email, request.Password),
                result =>
                {
                    if (result.Success)
                    {
                        //TODO: Redirect can be removed
                        return this.RedirectOnAuthentication(returnUrl);
                    }

                    return result.Data != null && result.Data.IsInvalidCredentials
                        ? this.JsonError(result.Errors?.ToArray(), HttpStatusCode.Forbidden)
                        : this.Redirect(Constants.Redirects.Login);
                });
        }

        [HttpPost]
        [ActionName("logout")]
        public ActionResult Logout()
        {
            return this.Execute(
                () => this.authenticationService.Logout(),
                result => this.RedirectOnAuthentication(null));
        }

        // TODO: Delete ValidateCredentials
        [Obsolete("Will be removed")]
        [HttpPost]
        [ActionName("start")]
        public ActionResult ValidateCredentials(LoginRequest request)
        {
            var validateCredentialsResultDto = new ValidateCredentialsResultModel
            {
                HasValidCredentials = this.authenticationService.ValidateUser(request.Email, request.Password)
            };

            return this.JsonOk(validateCredentialsResultDto);
        }

        private ActionResult RedirectOnAuthentication(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                return this.Redirect(Constants.Redirects.CurrentPage);
            }

            return this.Redirect(returnUrl);
        }
    }
}