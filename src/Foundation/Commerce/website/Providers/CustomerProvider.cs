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

namespace HCA.Foundation.Commerce.Providers
{
    using System.Web;
    using System.Web.Security;

    using Connect.Managers;
    using Connect.Managers.Account;

    using DependencyInjection;

    using Mappers.Users;

    using Models.Entities.Users;

    using Sitecore;
    using Sitecore.Commerce.Entities.Customers;
    using Sitecore.Diagnostics;

    [Service(typeof(ICustomerProvider))]
    public class CustomerProvider : ICustomerProvider
    {
        private readonly IAccountManager accountManager;

        private readonly IUserMapper userMapper;

        public CustomerProvider(IAccountManager accountManager, IUserMapper userMapper)
        {
            Assert.ArgumentNotNull(accountManager, nameof(accountManager));
            Assert.ArgumentNotNull(userMapper, nameof(userMapper));

            this.accountManager = accountManager;
            this.userMapper = userMapper;
        }

        public User GetUser(string email)
        {
            Assert.ArgumentNotNullOrEmpty(email, nameof(email));

            var userName = Membership.GetUserNameByEmail(email);
            if (string.IsNullOrWhiteSpace(userName))
            {
                return null;
            }

            return this.GetCommerceUser(userName);
        }

        public User GetCurrentCommerceUser(HttpContextBase httpContext)
        {
            var user = Context.Data.User;
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

        private User GetCommerceUser(string contactIdOrName)
        {
            Assert.ArgumentNotNullOrEmpty(contactIdOrName, nameof(contactIdOrName));

            var getUserResult = this.accountManager.GetUser(contactIdOrName);

            return this.MapToUser(getUserResult.CommerceUser, contactIdOrName);
        }

        private User MapToUser(CommerceUser commerceUser, string contactIdOrEmail)
        {
            if (commerceUser == null)
            {
                return new User
                {
                    ContactId = contactIdOrEmail
                };
            }

            return this.userMapper.Map<CommerceUser, User>(commerceUser);
        }
    }
}