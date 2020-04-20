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

namespace HCA.Foundation.Account.Managers.Authentication
{
    /// <summary>
    /// Proxy for static AuthenticationManager
    /// </summary>
    public interface IAuthenticationManager
    {
        /// <summary>
        /// Logs in a user into the system if the <paramref name="password" /> is valid.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns><c>true</c> if user was logged in, otherwise - <c>false</c>.</returns>
        bool Login(string userName, string password);

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        void Logout();
    }
}