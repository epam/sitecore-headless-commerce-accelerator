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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Foundation.Commerce.Context;
    using Foundation.Commerce.Models;
    using Foundation.Commerce.Models.Account;
    using Foundation.Commerce.Models.Checkout;
    using Foundation.Commerce.Repositories;
    using Foundation.Extensions.Extensions;
    using Sitecore.Analytics;

    public class AccountController : Controller
    {
        private readonly IAccountRepositry accountRepositry;

        private readonly IVisitorContext visitorContext;

        public AccountController(
            IAccountRepositry accountRepositry,
            IVisitorContext visitorContex)
        {
            this.accountRepositry = accountRepositry;
            visitorContext = visitorContex;
        }


        [HttpPost]
        [ActionName("create")]
        public ActionResult CreateAccount(CreateAccountModel createAccountModel)
        {
            try
            {
                EnsureTracker();

                var createAccountResult =
                    accountRepositry.CreateAccount(createAccountModel);

                if (!createAccountResult.Success)
                    return this.JsonError(createAccountResult.Errors.ToArray(), HttpStatusCode.BadRequest);

                return this.JsonOk(createAccountResult.Data);
            }
            catch (Exception ex)
            {
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("validate")]
        public ActionResult ValidateAccount(ValidateAccountModel validateAccountModel)
        {
            try
            {
                EnsureTracker();
                var accountExists =
                    accountRepositry.ValidateAccount(validateAccountModel);

                if (!accountExists.Success)
                    return this.JsonError(accountExists.Errors.ToArray(), HttpStatusCode.BadRequest);

                return this.JsonOk(accountExists.Data);
            }
            catch (Exception ex)
            {
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [ActionName("update")]
        public ActionResult UpdateUser(CommerceUserModel user)
        {
            try
            {
                EnsureTracker();
                var updateUserResult = accountRepositry.UpdateAccountInfo(user);

                if (!updateUserResult.Success)
                    return this.JsonError(updateUserResult.Errors.ToArray(), HttpStatusCode.BadRequest);

                return this.JsonOk(updateUserResult.Data);
            }
            catch (Exception ex)
            {
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [ActionName("change-password")]
        public ActionResult ChangePassword(ChangePasswordModel changePassword)
        {
            try
            {
                EnsureTracker();
                var
                    ñhangePasswordResult = accountRepositry.ChangePassword(changePassword);

                if (!ñhangePasswordResult.Success)
                    return this.JsonError(ñhangePasswordResult.Errors.ToArray(), HttpStatusCode.BadRequest);

                return this.JsonOk(ñhangePasswordResult.Data);
            }
            catch (Exception ex)
            {
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [ActionName("address/add")]
        public ActionResult AddAddress(AddressModel newAddress)
        {
            try
            {
                var contactId = visitorContext.ContactId;

                var addAddressResult =
                    accountRepositry.AddCustomerAddress(contactId, newAddress);

                if (!addAddressResult.Success)
                    return this.JsonError(addAddressResult.Errors.ToArray(), HttpStatusCode.BadRequest);

                return this.JsonOk(addAddressResult.Data);
            }
            catch (Exception ex)
            {
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [ActionName("address/list")]
        public ActionResult GetAddressList()
        {
            try
            {
                var contactId = visitorContext.ContactId;

                var getAddressListResult = accountRepositry.GetAddressList(contactId);

                if (!getAddressListResult.Success)
                    return this.JsonError(getAddressListResult.Errors.ToArray(), HttpStatusCode.BadRequest);

                return this.JsonOk(getAddressListResult.Data);
            }
            catch (Exception ex)
            {
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [ActionName("address/update")]
        public ActionResult UpdateAddress(AddressModel address)
        {
            try
            {
                var contactId = visitorContext.ContactId;

                var updateAddressResult =
                    accountRepositry.UpdateAddress(contactId, address);

                if (!updateAddressResult.Success)
                    return this.JsonError(updateAddressResult.Errors.ToArray(), HttpStatusCode.BadRequest);

                return this.JsonOk(updateAddressResult.Data);
            }
            catch (Exception ex)
            {
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [ActionName("address/remove")]
        public ActionResult RemoveAddress(AddressModel address)
        {
            try
            {
                var contactId = visitorContext.ContactId;

                var removeAddressResult =
                    accountRepositry.RemoveCustomerAddress(contactId, address);

                if (!removeAddressResult.Success)
                    return this.JsonError(removeAddressResult.Errors.ToArray(), HttpStatusCode.BadRequest);

                return this.JsonOk(removeAddressResult.Data);
            }
            catch (Exception ex)
            {
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        private void EnsureTracker()
        {
            if (!Tracker.IsActive) Tracker.StartTracking();
        }
    }
}