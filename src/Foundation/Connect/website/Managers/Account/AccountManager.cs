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

namespace HCA.Foundation.Connect.Managers.Account
{
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using DependencyInjection;

    using Providers;

    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Customers;
    using Sitecore.Commerce.Services.Customers;
    using Sitecore.Diagnostics;

    [Service(typeof(IAccountManager), Lifetime = Lifetime.Singleton)]
    public class AccountManager : BaseManager, IAccountManager
    {
        private readonly CustomerServiceProvider customerServiceProvider;

        public AccountManager(IConnectServiceProvider connectServiceProvider, ILogService<CommonLog> logService)
            : base(logService)
        {
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));

            this.customerServiceProvider = connectServiceProvider.GetCustomerServiceProvider();
        }

        public AddPartiesResult AddParties(CommerceCustomer commerceCustomer, IEnumerable<Party> parties)
        {
            Assert.ArgumentNotNull(commerceCustomer, nameof(commerceCustomer));
            Assert.ArgumentNotNull(parties, nameof(parties));

            return this.Execute(
                new AddPartiesRequest(commerceCustomer, parties.ToList()),
                this.customerServiceProvider.AddParties);
        }

        public CreateUserResult CreateUser(string userName, string email, string password, string shopName)
        {
            Assert.ArgumentNotNullOrEmpty(userName, nameof(userName));
            Assert.ArgumentNotNullOrEmpty(email, nameof(email));
            Assert.ArgumentNotNullOrEmpty(password, nameof(password));
            Assert.ArgumentNotNullOrEmpty(shopName, nameof(shopName));

            // Commerce needs domain name to be presented in the user name
            var commerceUserName = $"{Sitecore.Context.Site?.Domain?.Name ?? Constants.CommerceUsersDomainName}\\{userName}";

            return this.Execute(
                new CreateUserRequest(commerceUserName, password, email, shopName),
                this.customerServiceProvider.CreateUser);
        }

        public EnableUserResult EnableUser(CommerceUser commerceUser)
        {
            Assert.ArgumentNotNull(commerceUser, nameof(commerceUser));

            return this.Execute(
                new EnableUserRequest(commerceUser),
                this.customerServiceProvider.EnableUser);
        }

        public GetCustomerResult GetCustomer(string externalId)
        {
            Assert.ArgumentNotNullOrEmpty(externalId, nameof(externalId));

            return this.Execute(
                new GetCustomerRequest(externalId),
                this.customerServiceProvider.GetCustomer);
        }

        public GetPartiesResult GetCustomerParties(string contactId)
        {
            Assert.ArgumentNotNullOrEmpty(contactId, nameof(contactId));

            var getUserResult = this.GetUser(contactId);

            if (!getUserResult.Success || getUserResult.CommerceUser == null)
            {
                return new GetPartiesResult
                {
                    Parties = new List<Party>()
                };
            }

            var customer = new CommerceCustomer
            {
                ExternalId = getUserResult.CommerceUser.ExternalId
            };

            return this.GetParties(customer);
        }

        public GetPartiesResult GetParties(CommerceCustomer commerceCustomer)
        {
            Assert.ArgumentNotNull(commerceCustomer, nameof(commerceCustomer));

            return this.Execute(new GetPartiesRequest(commerceCustomer), this.customerServiceProvider.GetParties);
        }

        public GetUserResult GetUser(string userName)
        {
            Assert.ArgumentNotNullOrEmpty(userName, nameof(userName));

            return this.Execute(new GetUserRequest(userName), this.customerServiceProvider.GetUser);
        }

        public GetUsersResult GetUsers(UserSearchCriteria userSearchCriteria)
        {
            return this.Execute(
                new GetUsersRequest(userSearchCriteria),
                this.customerServiceProvider.GetUsers);
        }

        public CustomerResult RemoveParties(CommerceCustomer commerceCustomer, IEnumerable<Party> parties)
        {
            Assert.ArgumentNotNull(commerceCustomer, nameof(commerceCustomer));
            Assert.ArgumentNotNull(parties, nameof(parties));

            return this.Execute(
                new RemovePartiesRequest(commerceCustomer, parties.ToList()),
                this.customerServiceProvider.RemoveParties);
        }

        public CustomerResult UpdateParties(CommerceCustomer commerceCustomer, IEnumerable<Party> parties)
        {
            Assert.ArgumentNotNull(commerceCustomer, nameof(commerceCustomer));
            Assert.ArgumentNotNull(parties, nameof(parties));

            return this.Execute(
                new UpdatePartiesRequest(commerceCustomer, parties.ToList()),
                this.customerServiceProvider.UpdateParties);
        }

        public UpdateUserResult UpdateUser(CommerceUser commerceUser)
        {
            Assert.ArgumentNotNull(commerceUser, nameof(commerceUser));

            return this.Execute(
                new UpdateUserRequest(commerceUser),
                this.customerServiceProvider.UpdateUser);
        }
    }
}