﻿//    Copyright 2020 EPAM Systems, Inc.
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

    using Foundation.Base.Controllers;
    using Foundation.Base.Services.Tracking;
    using Foundation.Commerce.Context.Visitor;
    using Foundation.Commerce.Models.Entities.Addresses;
    using Foundation.Commerce.Services.Account;
    using HCA.Foundation.Base.Extensions;
    using HCA.Foundation.Base.Models.Result;
    using HCA.Foundation.Account.Services.Authentication;

    using Mappers;

    using Models.Requests;
    using Sitecore.Diagnostics;

    public class AccountsController : BaseController
    {
        private readonly IAccountService accountService;

        private readonly IAccountMapper mapper;

        private readonly ITrackingService trackingService;

        private readonly IVisitorContext visitorContext;

        public AccountsController(
            IAccountService accountService,
            IAccountMapper accountMapper,
            IAuthenticationService authenticationService,
            IVisitorContext visitorContext,
            ITrackingService trackingService)
        {
            Assert.ArgumentNotNull(accountService, nameof(accountService));
            Assert.ArgumentNotNull(accountMapper, nameof(accountMapper));
            Assert.ArgumentNotNull(visitorContext, nameof(visitorContext));
            Assert.ArgumentNotNull(trackingService, nameof(trackingService));
            Assert.ArgumentNotNull(authenticationService, nameof(authenticationService));

            this.accountService = accountService;
            this.mapper = accountMapper;
            this.visitorContext = visitorContext;
            this.trackingService = trackingService;
        }

        [HttpPost]
        [Authorize]
        [ActionName("address")]
        public ActionResult AddAddress(AddressRequest request)
        {
            return this.Execute(
                () => this.accountService.AddAddress(
                    this.visitorContext.CurrentUser?.UserName,
                    this.mapper.Map<AddressRequest, Address>(request)));
        }

        [HttpPut]
        [Authorize]
        [ActionName("password")]
        public ActionResult ChangePassword(ChangePasswordRequest request)
        {
            return this.Execute(
                () =>
                {
                    this.trackingService.EnsureTracker();
                    return this.accountService.ChangePassword(request.Email, request.NewPassword, request.OldPassword);
                },
                result => result.Success
                    ? this.JsonOk<VoidResult>()
                    : result.Errors.Contains(Foundation.Commerce.Constants.ErrorMessages.IncorrectOldPassword)
                        ? this.JsonError(result.Errors?.ToArray(), HttpStatusCode.BadRequest)
                        : this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError));
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("account")]
        public ActionResult CreateAccount(CreateAccountRequest requests)
        {
            return this.Execute(
                () =>
                {
                    this.trackingService.EnsureTracker();
                    return this.accountService.CreateAccount(
                        requests.Email,
                        requests.FirstName,
                        requests.LastName,
                        requests.Password);
                },
                result =>
                {
                    if (result.Success)
                        return this.JsonOk(result.Data);

                    if (result.Errors.Contains(Foundation.Commerce.Constants.ErrorMessages.EmailInUse))
                        return this.JsonError(Foundation.Commerce.Constants.ErrorMessages.EmailInUse, HttpStatusCode.BadRequest);

                    return this.JsonError(result.Errors?.FirstOrDefault(), HttpStatusCode.InternalServerError);
                });
        }

        [HttpPost]
        [Authorize]
        [ActionName("userImage")]
        public ActionResult UploadUserImage()
        {
            return this.Execute(this.accountService.UploadUserImage);
        }

        [HttpDelete]
        [Authorize]
        [ActionName("userImage")]
        public ActionResult RemoveUserImage()
        {
            return this.Execute(this.accountService.DeleteUserImage);
        }

        [HttpGet]
        [AllowAnonymous]
        [ActionName("address")]
        public ActionResult GetAddresses()
        {
            return this.Execute(() => this.accountService.GetAddresses(this.visitorContext.CurrentUser?.UserName));
        }

        [HttpDelete]
        [Authorize]
        [ActionName("address")]
        public ActionResult RemoveAddress(string externalId)
        {
            return this.Execute(
                () => this.accountService.RemoveAddress(this.visitorContext.CurrentUser?.UserName, externalId));
        }

        [HttpPut]
        [Authorize]
        [ActionName("account")]
        public ActionResult UpdateAccount(UpdateAccountRequest request)
        {
            return this.Execute(
                () => this.accountService.UpdateAccount(this.visitorContext.ExternalId,
                    request.FirstName,
                    request.LastName,
                    request.PhoneNumber,
                    request.DateOfBirth));
        }

        [HttpPut]
        [Authorize]
        [ActionName("address")]
        public ActionResult UpdateAddress(AddressRequest request)
        {
            return this.Execute(
                () => this.accountService.UpdateAddress(
                    this.visitorContext.CurrentUser?.UserName,
                    this.mapper.Map<AddressRequest, Address>(request)));
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("validate")]
        public ActionResult ValidateEmail(ValidateEmailRequest request)
        {
            return this.Execute(() => this.accountService.ValidateEmail(request.Email));
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("password")]
        public ActionResult ConfirmPasswordRecovery(ConfirmPasswordRecoveryRequest request)
        {
            return this.Execute(() => this.accountService.ConfirmPasswordRecovery(request.Email));
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("verifytoken")]
        public ActionResult ResetPassword(ResetPasswordRequest request)
        {
            return this.Execute(() => this.accountService.VerifyRecoveryToken(request.UserName, request.Token));
        }

        [HttpPut]
        [AllowAnonymous]
        [ActionName("recoverpassword")]
        public ActionResult RecoverPassword(RecoverPasswordRequest request)
        {
            return this.Execute(() => this.accountService.ResetPassword(request.UserName, request.NewPassword, request.Token));
        }

        [HttpDelete]
        [Authorize]
        [ActionName("account")]
        public ActionResult DeleteAccount()
        {
            var userId = this.visitorContext.ExternalId;

            return this.Execute(
                () => this.accountService.DeleteAccount(userId),
                result => result.Success ? this.JsonOk(result.Data) : this.JsonError(result.Errors?.FirstOrDefault(), HttpStatusCode.InternalServerError));
        }
    }
}