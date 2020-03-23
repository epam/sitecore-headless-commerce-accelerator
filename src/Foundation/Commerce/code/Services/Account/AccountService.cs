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

namespace Wooli.Foundation.Commerce.Services.Account
{
    using System;
    using System.Collections.Generic;

    using DependencyInjection;

    using Models;
    using Models.Entities.Addresses;
    using Models.Entities.Users;

    using User = Models.Entities.Users.User;

    [Service(typeof(IAccountService), Lifetime = Lifetime.Singleton)]
    public class AccountService : IAccountService
    {
        public Result<IEnumerable<Address>> AddAddress(string userName, Address address)
        {
            throw new NotImplementedException();
        }

        public Result<VoidResult> ChangePassword(string email, string newPassword, string oldPassword)
        {
            throw new NotImplementedException();
        }

        public Result<User> CreateAccount(
            string email,
            string firstName,
            string lastName,
            string password)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<Address>> GetAddress(string userName)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<Address>> RemoveAddress(string userName, Address address)
        {
            throw new NotImplementedException();
        }

        public Result<VoidResult> UpdateAccount(string contactId, string firstName, string lastName)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<Address>> UpdateAddress(string userName, Address address)
        {
            throw new NotImplementedException();
        }

        public Result<VoidResult> ValidateEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}