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
    using Sitecore.Security.Accounts;

    /// <summary>
    /// Executes Sitecore.Security.Accounts.User methods
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Get's a user from a name if exists. Returns null if not
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="isAuthenticated">If set to <c>true</c>, the user has been authenticated.</param>
        /// <returns>User</returns>
        User GetUserFromName(string userName, bool isAuthenticated);

        /// <summary>
        /// Adds custom property to User profile
        /// </summary>
        /// <param name="user">Sitecore user</param>
        /// <param name="key">Property's key</param>
        /// <param name="value">Property's value</param>
        /// <returns></returns>
        void AddCustomProperty(User user, string key, string value);
    }
}