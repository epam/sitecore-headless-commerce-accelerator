//    Copyright 2019 EPAM Systems, Inc.
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
    using System.Linq;
    using DependencyInjection;
    using Providers.Contracts;
    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Customers;
    using Sitecore.Commerce.Services.Customers;
    using Sitecore.Diagnostics;

    [Service(typeof(IAccountManager))]
    public class AccountManager : IAccountManager
    {
        private readonly ICartManager cartManager;
        private readonly CustomerServiceProvider customerServiceProvider;

        public AccountManager(IConnectServiceProvider connectServiceProvider, ICartManager cartManager)
        {
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));
            Assert.ArgumentNotNull(cartManager, nameof(cartManager));

            customerServiceProvider = connectServiceProvider.GetCustomerServiceProvider();
            this.cartManager = cartManager;
        }

        public ManagerResponse<CreateUserResult, CommerceUser> CreateUser(string userName, string email,
            string password, string shopName)
        {
            Assert.ArgumentNotNullOrEmpty(userName, nameof(userName));
            Assert.ArgumentNotNullOrEmpty(email, nameof(email));
            Assert.ArgumentNotNullOrEmpty(password, nameof(password));
            Assert.ArgumentNotNullOrEmpty(shopName, nameof(shopName));

            // Commerce needs domain name to be presented in the user name
            var fullUserName = $"{Constants.CommerceUsersDomainName}\\{userName}";

            var createUserRequest = new CreateUserRequest(fullUserName, password, email, shopName);

            var createUserResult = customerServiceProvider.CreateUser(createUserRequest);

            if (!createUserResult.Success || createUserResult.CommerceUser == null)
                Log.Warn("User creation failed", GetType());

            return new ManagerResponse<CreateUserResult, CommerceUser>(createUserResult, createUserResult.CommerceUser);
        }

        public ManagerResponse<UpdateUserResult, CommerceUser> UpdateUser(CommerceUser updatedCommerceUser)
        {
            Assert.ArgumentNotNull(updatedCommerceUser, nameof(updatedCommerceUser));

            var updateUserRequest = new UpdateUserRequest(updatedCommerceUser);

            var updateUserResult = customerServiceProvider.UpdateUser(updateUserRequest);

            if (!updateUserResult.Success) Log.Warn("User update failed", GetType());

            return new ManagerResponse<UpdateUserResult, CommerceUser>(updateUserResult, updateUserResult.CommerceUser);
        }

        public ManagerResponse<EnableUserResult, CommerceUser> EnableUser(CommerceUser commerceUser)
        {
            Assert.ArgumentNotNull(commerceUser, nameof(commerceUser));

            var enableUserRequest = new EnableUserRequest(commerceUser);

            var enableUserResult = customerServiceProvider.EnableUser(enableUserRequest);

            if (!enableUserResult.Success) Log.Warn("Enable user failed", GetType());

            return new ManagerResponse<EnableUserResult, CommerceUser>(enableUserResult, enableUserResult.CommerceUser);
        }

        public ManagerResponse<DisableUserResult, CommerceUser> DisableUser(CommerceUser commerceUser)
        {
            Assert.ArgumentNotNull(commerceUser, nameof(commerceUser));

            var disableUserRequest = new DisableUserRequest(commerceUser);

            var disableUserResult = customerServiceProvider.DisableUser(disableUserRequest);

            if (!disableUserResult.Success) Log.Warn("Disable user failed", GetType());

            return new ManagerResponse<DisableUserResult, CommerceUser>(disableUserResult,
                disableUserResult.CommerceUser);
        }

        public ManagerResponse<GetUserResult, CommerceUser> GetUser(string userName)
        {
            Assert.ArgumentNotNullOrEmpty(userName, nameof(userName));

            var user = customerServiceProvider.GetUser(new GetUserRequest(userName));
            if (!user.Success || user.CommerceUser == null) Log.Warn("User Not Found Error", GetType());

            var serviceProviderResult = user;

            return new ManagerResponse<GetUserResult, CommerceUser>(
                serviceProviderResult,
                serviceProviderResult.CommerceUser);
        }

        public ManagerResponse<GetUsersResult, IList<CommerceUser>> GetUsers(
            UserSearchCriteria userSearchCriteria)
        {
            var getUsersRequest = new GetUsersRequest(userSearchCriteria);
            var serviceProviderResult = customerServiceProvider.GetUsers(getUsersRequest);

            if (!serviceProviderResult.Success)
                return new ManagerResponse<GetUsersResult, IList<CommerceUser>>(serviceProviderResult,
                    new List<CommerceUser>());

            return new ManagerResponse<GetUsersResult, IList<CommerceUser>>(serviceProviderResult,
                serviceProviderResult.CommerceUsers);
        }

        public ManagerResponse<GetCustomerResult, CommerceCustomer> GetCustomer(string extenalId)
        {
            Assert.ArgumentNotNullOrEmpty(extenalId, nameof(extenalId));

            var getCustomerRequest = new GetCustomerRequest(extenalId);
            var getCustomerResult = customerServiceProvider.GetCustomer(getCustomerRequest);

            return new ManagerResponse<GetCustomerResult, CommerceCustomer>(getCustomerResult,
                getCustomerResult.CommerceCustomer);
        }

        public ManagerResponse<CreateCustomerResult, CommerceCustomer> CreateCustomer(CommerceCustomer commerceCustomer)
        {
            Assert.ArgumentNotNull(commerceCustomer, nameof(commerceCustomer));

            var createCustomerRequest = new CreateCustomerRequest(commerceCustomer);

            var createCustomerResult =
                customerServiceProvider.CreateCustomer(createCustomerRequest);

            if (!createCustomerResult.Success) Log.Warn("Create customer failed", GetType());

            return new ManagerResponse<CreateCustomerResult, CommerceCustomer>(createCustomerResult,
                createCustomerResult.CommerceCustomer);
        }

        public ManagerResponse<UpdateCustomerResult, CommerceCustomer> UpdateCustomer(CommerceCustomer commerceCustomer)
        {
            Assert.ArgumentNotNull(commerceCustomer, nameof(commerceCustomer));

            var updateCustomerRequest = new UpdateCustomerRequest(commerceCustomer);

            var updateCustomerResult =
                customerServiceProvider.UpdateCustomer(updateCustomerRequest);

            if (!updateCustomerResult.Success) Log.Warn("Update customer failed", GetType());

            return new ManagerResponse<UpdateCustomerResult, CommerceCustomer>(updateCustomerResult,
                updateCustomerResult.CommerceCustomer);
        }

        public ManagerResponse<GetPartiesResult, IEnumerable<Party>> GetCurrentCustomerParties(
            string shopName,
            string contactId)
        {
            var getPartiesResult = new GetPartiesResult();

            var user = GetUser(contactId);
            if (!user.ServiceProviderResult.Success || user.Result == null)
                return new ManagerResponse<GetPartiesResult, IEnumerable<Party>>(getPartiesResult, null);

            var customer = new CommerceCustomer {ExternalId = user.Result.ExternalId};
            return GetParties(customer);
        }

        public ManagerResponse<GetPartiesResult, IEnumerable<Party>> GetParties(CommerceCustomer customer)
        {
            var request = new GetPartiesRequest(customer);
            var parties = customerServiceProvider.GetParties(request);
            IEnumerable<Party> result =
                !parties.Success || parties.Parties == null ? new List<Party>() : parties.Parties;

            return new ManagerResponse<GetPartiesResult, IEnumerable<Party>>(parties, result);
        }

        public ManagerResponse<AddPartiesResult, IEnumerable<Party>> AddParties(CommerceCustomer customer,
            IEnumerable<Party> parties)
        {
            Assert.ArgumentNotNull(customer, nameof(customer));
            Assert.ArgumentNotNull(parties, nameof(parties));

            var addPartiesRequest = new AddPartiesRequest(customer, parties.ToList());

            var addPartiesResult = customerServiceProvider.AddParties(addPartiesRequest);

            return new ManagerResponse<AddPartiesResult, IEnumerable<Party>>(addPartiesResult,
                addPartiesRequest.Parties);
        }

        public ManagerResponse<CustomerResult, IEnumerable<Party>> UpdateParties(CommerceCustomer customer,
            IEnumerable<Party> parties)
        {
            Assert.ArgumentNotNull(customer, nameof(customer));
            Assert.ArgumentNotNull(parties, nameof(parties));

            var updatePartiesRequest = new UpdatePartiesRequest(customer, parties.ToList());

            var updatePartiesResult =
                customerServiceProvider.UpdateParties(updatePartiesRequest);


            return new ManagerResponse<CustomerResult, IEnumerable<Party>>(updatePartiesResult, null);
        }

        public ManagerResponse<CustomerResult, IEnumerable<Party>> RemoveParties(CommerceCustomer customer,
            IEnumerable<Party> parties)
        {
            Assert.ArgumentNotNull(customer, nameof(customer));
            Assert.ArgumentNotNull(parties, nameof(parties));

            var removePartiesRequest = new RemovePartiesRequest(customer, parties.ToList());

            var removePartiesResult = customerServiceProvider.RemoveParties(removePartiesRequest);

            return new ManagerResponse<CustomerResult, IEnumerable<Party>>(removePartiesResult, null);
        }

        public ManagerResponse<CustomerPartiesResult, IEnumerable<CustomerParty>> AddCustomerParties(
            CommerceCustomer customer, IEnumerable<CustomerParty> parties)
        {
            Assert.ArgumentNotNull(customer, nameof(customer));
            Assert.ArgumentNotNull(parties, nameof(parties));

            var addCustomerPartiesRequest = new AddCustomerPartiesRequest(customer, parties.ToList());

            var addCustomerPartiesResult =
                customerServiceProvider.AddCustomerParties(addCustomerPartiesRequest);

            return new ManagerResponse<CustomerPartiesResult, IEnumerable<CustomerParty>>(addCustomerPartiesResult,
                null);
        }

        public ManagerResponse<CustomerPartiesResult, IEnumerable<CustomerParty>> RemoveCustomerParties(
            CommerceCustomer customer, IEnumerable<CustomerParty> parties)
        {
            Assert.ArgumentNotNull(customer, nameof(customer));
            Assert.ArgumentNotNull(parties, nameof(parties));

            var removeCustomerPartiesRequest = new RemoveCustomerPartiesRequest(customer, parties.ToList());

            var removeCustomerPartiesResult =
                customerServiceProvider.RemoveCustomerParties(removeCustomerPartiesRequest);

            return new ManagerResponse<CustomerPartiesResult, IEnumerable<CustomerParty>>(removeCustomerPartiesResult,
                null);
        }

        public ManagerResponse<CustomerPartiesResult, IEnumerable<CustomerParty>> UpdateCustomerParties(
            CommerceCustomer customer, IEnumerable<CustomerParty> parties)
        {
            Assert.ArgumentNotNull(customer, nameof(customer));
            Assert.ArgumentNotNull(parties, nameof(parties));

            var updateCustomerPartiesRequest = new UpdateCustomerPartiesRequest(customer, parties.ToList());

            var updateCustomerPartiesResult =
                customerServiceProvider.UpdateCustomerParties(updateCustomerPartiesRequest);

            return new ManagerResponse<CustomerPartiesResult, IEnumerable<CustomerParty>>(updateCustomerPartiesResult,
                null);
        }
    }
}