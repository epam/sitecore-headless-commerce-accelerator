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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Security;

    using Base.Models.Result;

    using Connect.Context.Storefront;
    using Connect.Managers.Account;

    using DependencyInjection;

    using Mappers.Account;

    using Models.Entities.Account;
    using Models.Entities.Addresses;
    using Models.Entities.Users;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Customers;
    using Sitecore.Commerce.Services.Customers;
    using Sitecore.Diagnostics;

    [Service(typeof(IAccountService), Lifetime = Lifetime.Singleton)]
    public class AccountService : IAccountService
    {
        private readonly IAccountManager accountManager;

        private readonly IAccountMapper mapper;

        private readonly IStorefrontContext storefrontContext;

        public AccountService(
            IAccountManager accountManager,
            IAccountMapper accountMapper,
            IStorefrontContext storefrontContext)
        {
            Assert.ArgumentNotNull(accountManager, nameof(accountManager));
            Assert.ArgumentNotNull(accountMapper, nameof(accountMapper));
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));

            this.accountManager = accountManager;
            this.mapper = accountMapper;
            this.storefrontContext = storefrontContext;
        }

        public Result<IEnumerable<Address>> AddAddress(string userName, Address address)
        {
            Assert.ArgumentNotNullOrEmpty(userName, nameof(userName));
            Assert.ArgumentNotNull(address, nameof(address));

            var result = new Result<IEnumerable<Address>>();

            var customerResult = this.GetCustomerByName(userName);

            if (customerResult.Success)
            {
                var partyId = Guid.NewGuid().ToString("N");
                address.PartyId = partyId;
                address.ExternalId = partyId;
                address.Name = partyId;

                var commerceParty = this.mapper.Map<Address, CommerceParty>(address);

                commerceParty.RegionName = this.GetSubdivisionModelName(address);

                var addPartiesResult = this.accountManager.AddParties(
                    customerResult.Data,
                    new List<Party>
                    {
                        commerceParty
                    });

                if (addPartiesResult.Success)
                {
                    result.SetResult(
                        addPartiesResult.Parties.Select(party => this.mapper.Map<Party, Address>(party)).ToList());
                }
                else
                {
                    result.SetErrors(addPartiesResult.SystemMessages.Select(sm => sm.Message).ToList());
                }
            }
            else
            {
                result.SetErrors(customerResult.Errors);
            }

            return result;
        }

        // TODO: Create provider for Membership and cover with unit tests.
        public Result<VoidResult> ChangePassword(string email, string newPassword, string oldPassword)
        {
            Assert.ArgumentNotNullOrEmpty(email, nameof(email));
            Assert.ArgumentNotNullOrEmpty(newPassword, nameof(newPassword));
            Assert.ArgumentNotNullOrEmpty(oldPassword, nameof(oldPassword));

            var result = new Result<VoidResult>();

            var userName = Membership.GetUserNameByEmail(email);

            if (!string.IsNullOrWhiteSpace(userName) && Membership.ValidateUser(userName, oldPassword))
            {
                var sitecoreUser = Membership.GetUser(userName);

                if (sitecoreUser != null)
                {
                    sitecoreUser.ChangePassword(sitecoreUser.ResetPassword(), newPassword);
                }
                else
                {
                    result.SetError("User was not found.");
                }
            }
            else
            {
                result.SetError("Incorrect old password.");
            }

            return result;
        }

        public Result<User> CreateAccount(string email, string firstName, string lastName, string password)
        {
            Assert.ArgumentNotNullOrEmpty(email, nameof(email));
            Assert.ArgumentNotNullOrEmpty(firstName, nameof(firstName));
            Assert.ArgumentNotNullOrEmpty(lastName, nameof(lastName));
            Assert.ArgumentNotNullOrEmpty(password, nameof(password));

            var result = new Result<User>();

            var validateEmailResult = this.ValidateEmail(email);

            if (validateEmailResult.Success && validateEmailResult.Data.InUse)
            {
                result.Success = false;
                result.SetError("Email is in use.");
                return result;
            }

            var createUserResult = this.accountManager.CreateUser(
                $"{firstName}{lastName}{Guid.NewGuid():N}",
                email,
                password,
                this.storefrontContext.ShopName);

            if (createUserResult.Success)
            {
                var enableUserResult = this.accountManager.EnableUser(createUserResult.CommerceUser);

                var user = enableUserResult.Success
                    ? enableUserResult.CommerceUser
                    : createUserResult.CommerceUser;

                user.FirstName = firstName;
                user.LastName = lastName;

                var updateResult = this.accountManager.UpdateUser(user);

                if (updateResult.Success)
                {
                    result.SetResult(this.mapper.Map<CommerceUser, User>(updateResult.CommerceUser));
                }
                else
                {
                    result.SetErrors(updateResult.SystemMessages.Select(sm => sm.Message).ToList());
                }
            }
            else
            {
                result.SetErrors(createUserResult.SystemMessages.Select(sm => sm.Message).ToList());
            }

            return result;
        }

        public Result<IEnumerable<Address>> GetAddresses(string userName)
        {
            Assert.ArgumentNotNullOrEmpty(userName, nameof(userName));

            return this.ExecuteWithParties<IEnumerable<Address>>(
                userName,
                (_, parties, result) =>
                {
                    result.SetResult(parties.Select(party => this.mapper.Map<Party, Address>(party)).ToList());
                    return result;
                });
        }

        public Result<IEnumerable<Address>> RemoveAddress(string userName, string externalId)
        {
            Assert.ArgumentNotNullOrEmpty(userName, nameof(userName));
            Assert.ArgumentNotNullOrEmpty(externalId, nameof(externalId));

            return this.ExecuteWithParties<IEnumerable<Address>>(
                userName,
                (commerceCustomer, parties, result) =>
                {
                    var removePartyResult = this.accountManager.RemoveParties(
                        commerceCustomer,
                        new List<Party>
                        {
                            parties.FirstOrDefault(party => party.ExternalId == externalId)
                        });

                    if (removePartyResult.Success)
                    {
                        return this.GetAddresses(userName);
                    }

                    result.SetErrors(removePartyResult.SystemMessages.Select(sm => sm.Message).ToList());
                    return result;
                });
        }

        public Result<VoidResult> UpdateAccount(string contactId, string firstName, string lastName)
        {
            Assert.ArgumentNotNullOrEmpty(contactId, nameof(contactId));
            Assert.ArgumentNotNullOrEmpty(firstName, nameof(firstName));
            Assert.ArgumentNotNullOrEmpty(lastName, nameof(lastName));

            var result = new Result<VoidResult>();

            var getUserResult = this.accountManager.GetUser(contactId);

            if (getUserResult.Success)
            {
                getUserResult.CommerceUser.FirstName = firstName;
                getUserResult.CommerceUser.LastName = lastName;

                var userUpdateResult = this.accountManager.UpdateUser(getUserResult.CommerceUser);

                if (!userUpdateResult.Success)
                {
                    result.SetErrors(userUpdateResult.SystemMessages.Select(sm => sm.Message).ToList());
                }
            }
            else
            {
                result.SetErrors(getUserResult.SystemMessages.Select(sm => sm.Message).ToList());
            }

            return result;
        }

        public Result<IEnumerable<Address>> UpdateAddress(string userName, Address address)
        {
            Assert.ArgumentNotNullOrEmpty(userName, nameof(userName));
            Assert.ArgumentNotNull(address, nameof(address));

            return this.ExecuteWithParties<IEnumerable<Address>>(
                userName,
                (commerceCustomer, parties, result) =>
                {
                    var party = parties.FirstOrDefault(p => p.ExternalId == address.ExternalId);

                    if (party != null)
                    {
                        address.ExternalId = party.ExternalId;
                        address.PartyId = party.PartyId;

                        var commerceParty = this.mapper.Map<Address, CommerceParty>(address);

                        commerceParty.RegionName = this.GetSubdivisionModelName(address);

                        var updatePartyResponse = this.accountManager.UpdateParties(
                            commerceCustomer,
                            new List<Party>
                            {
                                commerceParty
                            });

                        if (updatePartyResponse.Success)
                        {
                            return this.GetAddresses(userName);
                        }

                        result.SetErrors(updatePartyResponse.SystemMessages.Select(sm => sm.Message).ToList());
                    }
                    else
                    {
                        result.SetError("The address with the current external Id was not found.");
                    }

                    return result;
                });
        }

        public Result<ValidateEmailResult> ValidateEmail(string email)
        {
            Assert.ArgumentNotNullOrEmpty(email, nameof(email));

            var result = new Result<ValidateEmailResult>(new ValidateEmailResult());

            var getUserResult = this.accountManager.GetUsers(
                new UserSearchCriteria
                {
                    Email = email
                });

            if (getUserResult.CommerceUsers.FirstOrDefault() != null)
            {
                result.Data.InUse = true;
            }

            return result;
        }

        private Result<T> ExecuteWithParties<T>(
            string userName,
            Func<CommerceCustomer, IReadOnlyCollection<Party>, Result<T>, Result<T>> action)
            where T : class
        {
            var result = new Result<T>();

            var customerResult = this.GetCustomerByName(userName);

            if (customerResult.Success)
            {
                var getPartiesResult = this.accountManager.GetParties(customerResult.Data);

                if (getPartiesResult.Success)
                {
                    return action(customerResult.Data, getPartiesResult.Parties, result);
                }

                result.SetErrors(getPartiesResult.SystemMessages.Select(sm => sm.Message).ToList());
            }
            else
            {
                result.SetErrors(customerResult.Errors);
            }

            return result;
        }

        private Result<CommerceCustomer> GetCustomerByName(string userName)
        {
            Assert.ArgumentNotNullOrEmpty(userName, nameof(userName));

            var result = new Result<CommerceCustomer>();

            var userResult = this.accountManager.GetUser(userName);

            if (userResult.Success)
            {
                result.SetResult(
                    new CommerceCustomer
                    {
                        ExternalId = userResult.CommerceUser.ExternalId
                    });
            }
            else
            {
                result.SetErrors(userResult.SystemMessages.Select(sm => sm.Message).ToList());
            }

            return result;
        }

        private string GetSubdivisionModelName(Address address)
        {
            return this.storefrontContext
                .StorefrontConfiguration
                .CountriesRegionsSettings
                .CountryRegionsValues
                .FirstOrDefault(c => c.CountryCode == address.CountryCode)
                ?.Subdivisions
                .FirstOrDefault(s => s.Code == address.State)
                ?.Name;
        }
    }
}