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

namespace Wooli.Foundation.Commerce.Providers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Sitecore.Commerce.Entities.Customers;
    using Sitecore.Commerce.Services.Customers;
    using Sitecore.Diagnostics;

    using Wooli.Foundation.Commerce.Models;
    using Wooli.Foundation.Connect.Managers;
    using Wooli.Foundation.DependencyInjection;

    using Constants = Wooli.Foundation.Commerce.Utils.Constants;

    [Service(typeof(ICustomerProvider))]
    public class CustomerProvider : ICustomerProvider
    {
        #region fields

        private readonly IAccountManager accountManager;

        #endregion

        #region constructors

        public CustomerProvider(IAccountManager accountManager)
        {
            Assert.ArgumentNotNull(accountManager, nameof(accountManager));
            this.accountManager = accountManager;

        }

        #endregion

        #region interface methods

        public CommerceUserModel GetCurrentCommerceUser(HttpContextBase httpContext)
        {
            var user = Sitecore.Context.Data.User;
            if (user == null)
            {
                return null;
            }

            if (user.IsAuthenticated)
            {
                return this.GetCommerceUser(user.Profile.UserName);
            }

            var cookie = httpContext.Request.Cookies["SC_ANALYTICS_GLOBAL_COOKIE"];
            if (cookie != null)
            {
                return this.GetCommerceUser(cookie.Value);
            }

            return null;
        }

        public CommerceUserModel GetCommerceUser(string contactIdOrName)
        {
            Assert.ArgumentNotNullOrEmpty(contactIdOrName, nameof(contactIdOrName));

            ManagerResponse<GetUserResult, CommerceUser> commerceUser =
                this.accountManager.GetUser(contactIdOrName);

            return this.MapToCommerceUserModel(commerceUser.Result, contactIdOrName);
        }

        #endregion

        #region private methods

        private string GetCustomerId(IList<string> customers)
        {
            // It is assumed that we have only one commerce customer per commerce user,
            // so we select a first if it exists
            return customers?.FirstOrDefault();
        }

        private string ParseContactId(string externalId)
        {
            // We extract contact id from externalId,
            // it is not returned by current implementation of Sitecore.Commerce.Core
            return externalId?.Replace(Constants.CommereceCustomerIdPrefix, string.Empty);
        }

        private CommerceUserModel MapToCommerceUserModel(CommerceUser commerceUser, string contactIdOrEmail)
        {
            if (commerceUser == null)
            {
                return new CommerceUserModel { ContactId = contactIdOrEmail };
            }

            string customerId = this.GetCustomerId(commerceUser?.Customers);
            string contactId = this.ParseContactId(commerceUser?.ExternalId);
           

            return new CommerceUserModel
            {
                ContactId = contactId,
                CustomerId = customerId,
                Email = commerceUser.Email,
                FirstName = commerceUser.FirstName,
                LastName = commerceUser.LastName,
                UserName = commerceUser.UserName
            };
        }

        #endregion
    }
}
