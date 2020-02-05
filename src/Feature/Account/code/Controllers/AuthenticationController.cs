//    Copyright 2019 EPAM Systems, Inc.
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
    using Foundation.Commerce.Context;
    using Foundation.Commerce.Models;
    using Foundation.Commerce.Models.Authentication;
    using Foundation.Commerce.Providers;
    using Foundation.Commerce.Repositories;
    using Foundation.Extensions.Extensions;
    using Sitecore.Commerce;
    using Sitecore.Security.Authentication;

    public class AuthenticationController : Controller
    {
        private readonly ICartRepository cartRepository;
        private readonly ICustomerProvider customerProvider;
        private readonly IVisitorContext visitorContext;

        public AuthenticationController(ICustomerProvider customerProvider, IVisitorContext visitorContext,
            ICartRepository cartRepository)
        {
            this.customerProvider = customerProvider;
            this.visitorContext = visitorContext;
            this.cartRepository = cartRepository;
        }

        [HttpPost]
        [ActionName("start")]
        public ActionResult ValidateCredentials(UserLoginModel userLogin)
        {
            var validateCredentialsResultDto = new ValidateCredentialsResultModel
            {
                HasValidCredentials = ValidateUser(userLogin)
            };

            return this.JsonOk(validateCredentialsResultDto);
        }

        [HttpPost]
        [ActionName("signin")]
        public ActionResult SignIn(UserLoginModel userLogin, string returnUrl)
        {
            bool userLoginResult = LoginUser(userLogin, out CommerceUserModel commerceUserModel);

            if (!userLoginResult || commerceUserModel == null) return Redirect("/signin");

            CompleteAuthentication(commerceUserModel);

            return RedirectOnSignin(returnUrl);
        }

        [HttpPost]
        [ActionName("signout")]
        public ActionResult SignOut()
        {
            visitorContext.CurrentUser = null;

            CommerceTracker.Current.EndVisit(true);
            Session.Abandon();
            AuthenticationManager.Logout();

            return RedirectOnSignin(null);
        }

        private ActionResult RedirectOnSignin(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl)) return Redirect("/");

            return Redirect(returnUrl);
        }

        private void CompleteAuthentication(CommerceUserModel commerceUser)
        {
            string anonymousContact = visitorContext.ContactId;
            visitorContext.CurrentUser = commerceUser;

            cartRepository.MergeCarts(anonymousContact);

            CommerceTracker.Current.IdentifyAs("CommerceUser", commerceUser.UserName);
        }

        private bool ValidateUser(UserLoginModel userLogin)
        {
            string userName = Membership.GetUserNameByEmail(userLogin.Email);
            if (!string.IsNullOrWhiteSpace(userName)) return Membership.ValidateUser(userName, userLogin.Password);

            return false;
        }


        private bool LoginUser(UserLoginModel userLogin, out CommerceUserModel commerceUser)
        {
            string userName = Membership.GetUserNameByEmail(userLogin.Email);
            if (string.IsNullOrWhiteSpace(userName))
            {
                commerceUser = null;
                return false;
            }

            commerceUser = customerProvider.GetCommerceUser(userName);

            if (commerceUser == null) return false;


            return AuthenticationManager.Login(userName, userLogin.Password);
        }
    }
}