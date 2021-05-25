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

namespace HCA.Foundation.Base.Services
{
    using System.Web.Security;

    /// <summary>
    /// Proxy interface for static Membership class
    /// </summary>
    public interface IMembershipService
    {
        /// <summary>
        /// Gets a user name where the email address for the user matches the specified email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>The user name where the email address for the user matches the specified email address. If no match is found, null is returned.</returns>
        string GetUserNameByEmail(string email);
        /// <summary>
        /// Gets information from the data source for a membership user.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>A MembershipUser object populated with the specified user's information from the data source.</returns>
        MembershipUser GetUser(string userName);
        /// <summary>
        /// Verifies that the supplied user name and password are valid.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <returns>true if the supplied user name and password are valid; otherwise, false.</returns>
        bool ValidateUser(string userName, string oldPassword);
    }
}
