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

namespace HCA.Foundation.Account.Services.Authentication
{
    using System;

    using Base.Models.Result;

    using Models.Authentication;

    /// <summary>
    /// Performs main authentication operations
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Performs user login
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="password">User's password</param>
        /// <returns>Result of login</returns>
        Result<LoginResult> Login(string email, string password);

        /// <summary>
        /// Performs current active user logout
        /// </summary>
        /// <returns>Result of logout</returns>
        Result<VoidResult> Logout();

        /// <summary>
        /// Validates user existence
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <returns>True if user exists, otherwise - false</returns>
        [Obsolete("Will be removed")]
        bool ValidateUser(string email, string password);
    }
}