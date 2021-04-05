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

namespace HCA.Foundation.Account.Managers.User
{
    using System.Diagnostics.CodeAnalysis;

    using DependencyInjection;
    using Sitecore.Diagnostics;
    using Sitecore.Security.Accounts;

    [Service(typeof(IUserManager), Lifetime = Lifetime.Transient)]
    public class UserManager : IUserManager
    {
        public void AddCustomProperty(User user, string key, string value)
        {
            Assert.ArgumentNotNull(user, nameof(user));
            Assert.ArgumentNotNullOrEmpty(key, nameof(key));
            Assert.ArgumentNotNullOrEmpty(value, nameof(value));

            user.Profile.SetCustomProperty(key, value);
            user.Profile.Save();
        }

        public void RemoveCustomProperty(User user, string key)
        {
            Assert.ArgumentNotNull(user, nameof(user));
            Assert.ArgumentNotNullOrEmpty(key, nameof(key));

            user.Profile.RemoveCustomProperty(key);
            user.Profile.Save();
        }

        public User GetUserFromName(string username, bool isAuthenticated)
        {
            Assert.ArgumentNotNullOrEmpty(username, nameof(username));

            return !User.Exists(username) ? null : User.FromName(username, isAuthenticated);
        }
    }
}