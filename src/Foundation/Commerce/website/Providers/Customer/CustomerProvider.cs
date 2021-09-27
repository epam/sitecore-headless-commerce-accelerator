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

namespace HCA.Foundation.Commerce.Providers.Customer
{
    using System.Web;
    using System.Web.Security;

    using Connect.Managers.Account;

    using DependencyInjection;
    using HCA.Foundation.Base.Context;
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
        private readonly ISitecoreContext sitecoreContext;

        public CustomerProvider(IAccountManager accountManager, IUserMapper userMapper, ISitecoreContext sitecoreContext)
        {
            Assert.ArgumentNotNull(accountManager, nameof(accountManager));
            Assert.ArgumentNotNull(userMapper, nameof(userMapper));
            Assert.ArgumentNotNull(sitecoreContext, nameof(sitecoreContext));

            this.accountManager = accountManager;
            this.userMapper = userMapper;
            this.sitecoreContext = sitecoreContext;
        }

        public User GetUser(string email)
        {
            Assert.ArgumentNotNullOrEmpty(email, nameof(email));

            var userName = Membership.GetUserNameByEmail(email);
            return string.IsNullOrWhiteSpace(userName) ? null : this.GetCommerceUser(userName);
        }

        public User GetCurrentCommerceUser(HttpContextBase httpContext)
        {
            var user = Context.Data.User;
            if (user == null)
            {
                return null;
            }

            if (user.IsAuthenticated && sitecoreContext.Site.Domain.IsValidAccountName(user.Profile.UserName))
            {
                return this.GetCommerceUser(user.Profile.UserName);
            }

            var cookie = httpContext.Request.Cookies["SC_ANALYTICS_GLOBAL_COOKIE"];
            if (cookie != null)
            {
                return new User { ExternalId = cookie.Value };
            }

            return null;
        }

        private User GetCommerceUser(string name)
        {
            Assert.ArgumentNotNullOrEmpty(name, nameof(name));

            var getUserResult = this.accountManager.GetUser(name);
            return this.userMapper.Map<CommerceUser, User>(getUserResult.CommerceUser);
        }
    }
}