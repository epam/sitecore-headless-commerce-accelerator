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

namespace HCA.Foundation.Commerce.Services.Account
{
    using System.Collections.Generic;

    using Base.Models.Result;

    using Models.Entities.Account;
    using Models.Entities.Addresses;
    using Models.Entities.Users;

    public interface IAccountService
    {
        Result<IEnumerable<Address>> AddAddress(string userName, Address address);

        Result<VoidResult> ChangePassword(string email, string newPassword, string oldPassword);

        Result<User> CreateAccount(string email, string firstName, string lastName, string password);

        Result<IEnumerable<Address>> GetAddresses(string userName);

        Result<IEnumerable<Address>> RemoveAddress(string userName, string externalId);

        Result<VoidResult> UpdateAccount(string contactId, string firstName, string lastName);

        Result<IEnumerable<Address>> UpdateAddress(string userName, Address address);

        Result<ValidateEmailResult> ValidateEmail(string email);
    }
}