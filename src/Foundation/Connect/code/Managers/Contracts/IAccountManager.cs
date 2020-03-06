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

namespace Wooli.Foundation.Connect.Managers
{
    using System.Collections.Generic;

    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Customers;
    using Sitecore.Commerce.Services.Customers;

    public interface IAccountManager
    {
        ManagerResponse<CustomerPartiesResult, IEnumerable<CustomerParty>> AddCustomerParties(
            CommerceCustomer customer,
            IEnumerable<CustomerParty> parties);

        ManagerResponse<AddPartiesResult, IEnumerable<Party>> AddParties(
            CommerceCustomer customer,
            IEnumerable<Party> parties);

        ManagerResponse<CreateCustomerResult, CommerceCustomer> CreateCustomer(CommerceCustomer commerceCustomer);

        ManagerResponse<CreateUserResult, CommerceUser> CreateUser(
            string userName,
            string email,
            string password,
            string shopName);

        ManagerResponse<DisableUserResult, CommerceUser> DisableUser(CommerceUser commerceUser);

        ManagerResponse<EnableUserResult, CommerceUser> EnableUser(CommerceUser commerceUser);

        ManagerResponse<GetPartiesResult, IEnumerable<Party>> GetCurrentCustomerParties(
            string shopName,
            string contactId);

        ManagerResponse<GetCustomerResult, CommerceCustomer> GetCustomer(string extenalId);

        ManagerResponse<GetPartiesResult, IEnumerable<Party>> GetParties(CommerceCustomer customer);

        ManagerResponse<GetUserResult, CommerceUser> GetUser(string userName);

        /// <summary>
        /// Gets User by Email
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns></returns>
        ManagerResponse<GetUsersResult, CommerceUser> GetUserByEmail(string email);

        ManagerResponse<GetUsersResult, IList<CommerceUser>> GetUsers(UserSearchCriteria userSearchCriteria);

        ManagerResponse<CustomerPartiesResult, IEnumerable<CustomerParty>> RemoveCustomerParties(
            CommerceCustomer customer,
            IEnumerable<CustomerParty> parties);

        ManagerResponse<CustomerResult, IEnumerable<Party>> RemoveParties(
            CommerceCustomer customer,
            IEnumerable<Party> parties);

        ManagerResponse<UpdateCustomerResult, CommerceCustomer> UpdateCustomer(CommerceCustomer commerceCustomer);

        ManagerResponse<CustomerPartiesResult, IEnumerable<CustomerParty>> UpdateCustomerParties(
            CommerceCustomer customer,
            IEnumerable<CustomerParty> parties);

        ManagerResponse<CustomerResult, IEnumerable<Party>> UpdateParties(
            CommerceCustomer customer,
            IEnumerable<Party> parties);

        ManagerResponse<UpdateUserResult, CommerceUser> UpdateUser(CommerceUser updatedCommerceUser);
    }
}