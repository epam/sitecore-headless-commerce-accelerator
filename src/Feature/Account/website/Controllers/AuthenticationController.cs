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
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Foundation.Account.Services.Authentication;
    using Foundation.Base.Controllers;
    using Foundation.Base.Extensions;
    using Foundation.Base.Models.Result;

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

        [HttpPost]
        [ActionName("login")]
        public ActionResult Login(LoginRequest request)
        {
            return this.Execute(
                () => this.authenticationService.Login(request.Email, request.Password),
                result => result.Success
                    ? this.JsonOk<VoidResult>()
                    : result.Data != null && result.Data.IsInvalidCredentials
                        ? this.JsonError(result.Errors?.ToArray(), HttpStatusCode.BadRequest)
                        : this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError));
        }

        [HttpPost]
        [ActionName("logout")]
        public ActionResult Logout()
        {
            return this.Execute(() => this.authenticationService.Logout());
        }
    }
}