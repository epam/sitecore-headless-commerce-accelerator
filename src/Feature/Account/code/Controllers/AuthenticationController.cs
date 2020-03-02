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

namespace Wooli.Feature.Account.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;

    using Sitecore.Security.Authentication;

    using Wooli.Foundation.Commerce.Context;
    using Wooli.Foundation.Commerce.Models;
    using Wooli.Foundation.Commerce.Models.Authentication;
    using Wooli.Foundation.Commerce.Providers;
    using Wooli.Foundation.Commerce.Repositories;
    using Wooli.Foundation.Commerce.Services.Tracking;
    using Wooli.Foundation.Extensions.Extensions;

    public class AuthenticationController : Controller
    {
        private readonly ICartRepository cartRepository;

        private readonly ICommerceTrackingService commerceTrackingService;

        private readonly ICustomerProvider customerProvider;

        private readonly IVisitorContext visitorContext;

        public AuthenticationController(
            ICustomerProvider customerProvider,
            IVisitorContext visitorContext,
            ICartRepository cartRepository,
            ICommerceTrackingService commerceTrackingService)
        {
            this.customerProvider = customerProvider;
            this.visitorContext = visitorContext;
            this.cartRepository = cartRepository;
            this.commerceTrackingService = commerceTrackingService;
        }

        [HttpPost]
        [ActionName("signIn")]
        public ActionResult SignIn(UserLoginModel userLogin, string returnUrl)
        {
            bool userLoginResult = this.LoginUser(userLogin, out CommerceUserModel commerceUserModel);

            if (!userLoginResult || (commerceUserModel == null)) return this.Redirect("/signIn");

            this.CompleteAuthentication(commerceUserModel);

            return this.RedirectOnSignIn(returnUrl);
        }

        [HttpPost]
        [ActionName("signOut")]
        public ActionResult SignOut()
        {
            this.visitorContext.CurrentUser = null;

            this.commerceTrackingService.EndVisit(true);
            this.Session.Abandon();
            AuthenticationManager.Logout();

            return this.RedirectOnSignIn(null);
        }

        [HttpPost]
        [ActionName("start")]
        public ActionResult ValidateCredentials(UserLoginModel userLogin)
        {
            var validateCredentialsResultDto = new ValidateCredentialsResultModel
                                               {
                                                   HasValidCredentials = this.ValidateUser(userLogin)
                                               };

            return this.JsonOk(validateCredentialsResultDto);
        }

        private void CompleteAuthentication(CommerceUserModel commerceUser)
        {
            string anonymousContact = this.visitorContext.ContactId;
            this.visitorContext.CurrentUser = commerceUser;

            this.cartRepository.MergeCarts(anonymousContact);

            this.commerceTrackingService.IdentifyAs("CommerceUser", commerceUser.UserName);
        }

        private bool LoginUser(UserLoginModel userLogin, out CommerceUserModel commerceUser)
        {
            string userName = Membership.GetUserNameByEmail(userLogin.Email);
            if (string.IsNullOrWhiteSpace(userName))
            {
                commerceUser = null;
                return false;
            }

            commerceUser = this.customerProvider.GetCommerceUser(userName);

            if (commerceUser == null) return false;

            return AuthenticationManager.Login(userName, userLogin.Password);
        }

        private ActionResult RedirectOnSignIn(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl)) return this.Redirect("/");

            return this.Redirect(returnUrl);
        }

        private bool ValidateUser(UserLoginModel userLogin)
        {
            string userName = Membership.GetUserNameByEmail(userLogin.Email);
            if (!string.IsNullOrWhiteSpace(userName)) return Membership.ValidateUser(userName, userLogin.Password);

            return false;
        }
    }
}