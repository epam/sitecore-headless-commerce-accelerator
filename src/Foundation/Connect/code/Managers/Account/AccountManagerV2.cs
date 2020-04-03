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

namespace Wooli.Foundation.Connect.Managers.Account
{
    using System.Collections.Generic;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using DependencyInjection;

    using Providers.Contracts;

    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Customers;
    using Sitecore.Commerce.Services.Customers;
    using Sitecore.Diagnostics;

    [Service(typeof(IAccountManagerV2), Lifetime = Lifetime.Singleton)]
    public class AccountManagerV2 : BaseManager, IAccountManagerV2
    {
        private readonly CustomerServiceProvider customerServiceProvider;

        public AccountManagerV2(IConnectServiceProvider connectServiceProvider, ILogService<CommonLog> logService)
            : base(logService)
        {
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));

            this.customerServiceProvider = connectServiceProvider.GetCustomerServiceProvider();
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

        public GetPartiesResult GetParties(CommerceCustomer customer)
        {
            Assert.ArgumentNotNull(customer, nameof(customer));

            return this.Execute(new GetPartiesRequest(customer), this.customerServiceProvider.GetParties);
        }

        public GetUserResult GetUser(string userName)
        {
            Assert.ArgumentNotNullOrEmpty(userName, nameof(userName));

            return this.Execute(new GetUserRequest(userName), this.customerServiceProvider.GetUser);
        }
    }
}