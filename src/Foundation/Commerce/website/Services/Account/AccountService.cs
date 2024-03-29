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
    using Base.Extensions;
    using Base.Models.Result;
    using Base.Services.Pipeline;
    using Connect.Context.Storefront;
    using Connect.Managers.Account;
    using DependencyInjection;
    using Foundation.Account.Managers.User;
    using HCA.Foundation.Base.Services;
    using HCA.Foundation.Commerce.Services.Cart;
    using HCA.Foundation.ConnectBase.Entities;
    using Infrastructure.Pipelines.ConfirmPasswordRecovery;
    using Mappers.Account;
    using Models.Entities.Account;
    using Models.Entities.Addresses;
    using Sitecore;
    using Sitecore.Analytics;
    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Customers;
    using Sitecore.Commerce.Services.Customers;
    using Sitecore.Configuration;
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;
    using Sitecore.Resources.Media;
    using Sitecore.SecurityModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Constants = Commerce.Constants;
    using User = Models.Entities.Users.User;

    [Service(typeof(IAccountService), Lifetime = Lifetime.Singleton)]
    public class AccountService : IAccountService
    {
        private readonly IAccountManager accountManager;

        private readonly IAccountMapper mapper;

        private readonly IStorefrontContext storefrontContext;

        private readonly IPipelineService pipelineService;

        private readonly IUserManager userManager;

        private readonly IMembershipService membershipService;

        private readonly ICartService cartService;

        public AccountService(
            IAccountManager accountManager,
            IAccountMapper accountMapper,
            IStorefrontContext storefrontContext,
            IPipelineService pipelineService,
            IUserManager userManager,
            IMembershipService membershipService,
            ICartService cartService)
        {
            Assert.ArgumentNotNull(accountManager, nameof(accountManager));
            Assert.ArgumentNotNull(accountMapper, nameof(accountMapper));
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));
            Assert.ArgumentNotNull(pipelineService, nameof(pipelineService));
            Assert.ArgumentNotNull(userManager, nameof(userManager));
            Assert.ArgumentNotNull(membershipService, nameof(membershipService));
            Assert.ArgumentNotNull(cartService, nameof(cartService));

            this.cartService = cartService;
            this.accountManager = accountManager;
            this.mapper = accountMapper;
            this.storefrontContext = storefrontContext;
            this.pipelineService = pipelineService;
            this.userManager = userManager;
            this.membershipService = membershipService;
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
                    result.SetErrors(addPartiesResult.SystemMessages.Select(sm => sm.Message));
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

            var userName = this.membershipService.GetUserNameByEmail(email);

            if (!string.IsNullOrWhiteSpace(userName) && this.membershipService.ValidateUser(userName, oldPassword))
            {
                var sitecoreUser = this.membershipService.GetUser(userName);

                if (sitecoreUser != null)
                {
                    sitecoreUser.ChangePassword(sitecoreUser.ResetPassword(), newPassword);
                }
                else
                {
                    result.SetError(Constants.ErrorMessages.UserNotFoundEmail);
                }
            }
            else
            {
                result.SetError(Constants.ErrorMessages.IncorrectOldPassword);
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
                result.SetError(Constants.ErrorMessages.EmailInUse);
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
                    result.SetResult(parties.Select(party => this.mapper.Map<Party, Address>(party)));
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

        public Result<VoidResult> UpdateAccount(string externalId, string firstName, string lastName, string phone = null, string dateOfBirth = null)
        {
            Assert.ArgumentNotNullOrEmpty(externalId, nameof(externalId));
            Assert.ArgumentNotNullOrEmpty(firstName, nameof(firstName));
            Assert.ArgumentNotNullOrEmpty(lastName, nameof(lastName));

            var result = new Result<VoidResult>();

            var getUserResult = this.accountManager.GetUserById(externalId);

            if (getUserResult.Success)
            {
                getUserResult.CommerceUser.FirstName = firstName;
                getUserResult.CommerceUser.LastName = lastName;
                getUserResult.CommerceUser.SetPropertyValue("Phone", phone ?? "");

                Context.User.Profile.SetCustomProperty("BirthDate", dateOfBirth ?? "");

                Context.User.Profile.Save();

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

        public Result<Dictionary<string, string>> UploadUserImage()
        {
            var result = new Result<Dictionary<string, string>>();

            var fileSizeLimit = Math.Pow(1024, 2); // 1mb
            var allowedExtensions = new List<string> { ".jpg", ".png" };
            var userName = Context.User.LocalName;

            var httpRequest = HttpContext.Current.Request;
            HttpPostedFile postedFile = null;

            if (httpRequest.Files.Count == 0)
            {
                result.SetError("No image provided");
            }

            else
            {
                postedFile = httpRequest.Files[0];
            }

            try
            {
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    var ext = postedFile.FileName
                            .Substring(postedFile.FileName
                            .LastIndexOf('.'))
                            .ToLower();

                    if (!allowedExtensions.Contains(ext))
                    {
                        result.SetError("Please upload image of type .jpg,.png.");
                    }

                    else if (postedFile.ContentLength > fileSizeLimit)
                    {
                        result.SetError("Please upload a file up to 1 mb.");
                    }

                    else
                    {
                        var mediaCreator = new MediaCreator();
                        var database = Factory.GetDatabase("master");
                        var options = new MediaCreatorOptions
                        {
                            Versioned = false,
                            IncludeExtensionInItemName = false,
                            Database = database,
                            Destination = $"/sitecore/media library/HCA/Userpics/{userName}",
                            OverwriteExisting = true
                        };


                        Sitecore.Data.Items.Item item;
                        using (new SecurityDisabler())
                        {
                            item = mediaCreator.CreateFromStream(
                                postedFile.InputStream,
                                postedFile.FileName,
                                options);
                        }

                        var updatedTimeTicks = item.Statistics.Updated.Ticks;
                        var mediaItem = new Sitecore.Data.Items.MediaItem(item);

                        result.SetResult(
                            new Dictionary<string, string>
                            {
                                { "initiator", $"{Context.User.Profile.FullName}" },
                                { "imageUrl", $"{mediaItem.ImageUrl() + $"?{updatedTimeTicks}"}" }
                            });
                    }
                }
            }
            catch (Exception exception)
            {
                result.SetError(exception.Message);
            }

            return result;
        }

        public Result<Dictionary<string, string>> DeleteUserImage()
        {
            var result = new Result<Dictionary<string, string>>();

            var userName = Context.User.LocalName;
            using (new SecurityDisabler())
            {
                try
                {
                    var item = Context.Database.GetItem($"/sitecore/media library/HCA/Userpics/{userName}");
                    if (item == null)
                    {
                        result.SetError("Image not found");
                        return result;
                    }

                    item.Delete();

                    result.SetResult(
                        new Dictionary<string, string>
                        {
                            { "initiator", $"{Context.User.Profile.FullName}" },
                            { "Status", "The image has been removed" }
                        });
                }

                catch (Exception ex)
                {
                    result.SetError(ex.Message);
                }
            }

            return result;
        }


        public Result<VoidResult> DeleteAccount(string userId)
        {
            Assert.ArgumentNotNullOrEmpty(userId, nameof(userId));

            var result = new Result<VoidResult>();

            var getUserResult = this.accountManager.GetUserById(userId);

            if (getUserResult.Success)
            {
                var userName = this.membershipService.GetUserNameByEmail(getUserResult.CommerceUser.Email);
                var sitecoreUser = this.membershipService.GetUser(userName);

                sitecoreUser.ChangePassword(sitecoreUser.ResetPassword(), $"{Guid.NewGuid():N}");

                getUserResult.CommerceUser.FirstName = Constants.DefaultUser.FirstNameDefault;
                getUserResult.CommerceUser.LastName = Constants.DefaultUser.LastNameDefault;
                getUserResult.CommerceUser.Email = $"{Guid.NewGuid():N}@wooli.com";

                Context.User.Profile.SetCustomProperty("BirthDate", Constants.DefaultUser.BirthDateDefault);
                Context.User.Profile.Save();

                var customerService = new CustomerServiceProvider();
                customerService.DisableUser(new DisableUserRequest(getUserResult.CommerceUser));

                cartService.CleanCartLines();

                var userDeleteResult = this.accountManager.UpdateUser(getUserResult.CommerceUser);

                var manager = Factory.CreateObject("tracking/contactManager", true) as Sitecore.Analytics.Tracking.ContactManager;
                manager.RemoveFromSession(Tracker.Current.Contact.ContactId);
                Tracker.Current.Session.Contact = manager.LoadContact(Tracker.Current.Contact.ContactId);

                var args = new Foundation.Account.Infrastructure.Pipelines.Logout.LogoutPipelineArgs();
                this.pipelineService.RunPipeline(Constants.Pipelines.Logout, args);

                if (!userDeleteResult.Success)
                {
                    result.SetErrors(userDeleteResult.SystemMessages.Select(sm => sm.Message).ToList());
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

                        result.SetErrors(updatePartyResponse.SystemMessages.Select(sm => sm.Message));
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

        public Result<ConfirmPasswordRecoveryResult> ConfirmPasswordRecovery(string email)
        {
            Assert.ArgumentNotNullOrEmpty(email, nameof(email));

            var args = new ConfirmPasswordRecoveryArgs(HttpContext.Current)
            {
                UserEmail = email
            };

            this.pipelineService.RunPipeline(Constants.Pipelines.ConfirmPasswordRecovery, args);

            return this.ResolveResult(
                args,
                pipelineArgs => new ConfirmPasswordRecoveryResult
                {
                    IsEmailValid = pipelineArgs.IsEmailValid
                });
        }

        public Result<VerifyRecoveryTokenResult> VerifyRecoveryToken(string userName, string token)
        {
            Assert.ArgumentNotNullOrEmpty(userName, nameof(userName));
            Assert.ArgumentNotNullOrEmpty(token, nameof(token));

            var result = new Result<VerifyRecoveryTokenResult>(new VerifyRecoveryTokenResult
            {
                Token = token,
                UserName = userName
            });

            var user = this.userManager.GetUserFromName(Constants.PasswordRecovery.UsersDomain + "\\" + userName, false);
            if (user == null)
            {
                result.SetError(Constants.ErrorMessages.UserNotFoundName);
            }
            else
            {
                var userToken = user.Profile.GetCustomProperty(Constants.PasswordRecovery.ConfirmTokenKey);
                var isValid = !string.IsNullOrWhiteSpace(token)
                    && !string.IsNullOrWhiteSpace(userToken)
                    && string.Equals(token, userToken);
                result.Data.IsTokenValid = isValid;
                if (!isValid)
                {
                    result.SetError(Constants.ErrorMessages.TokenIsInvalid);
                }
            }

            return result;
        }

        public Result<VoidResult> ResetPassword(string userName, string newPassword, string token)
        {
            Assert.ArgumentNotNullOrEmpty(userName, nameof(userName));
            Assert.ArgumentNotNullOrEmpty(newPassword, nameof(newPassword));
            Assert.ArgumentNotNullOrEmpty(token, nameof(token));

            var result = new Result<VoidResult>();
            var fullUserName = this.EnsureUserFullName(userName, Constants.PasswordRecovery.UsersDomain);
            var sitecoreUser = this.userManager.GetUserFromName(fullUserName, true);
            if (sitecoreUser == null)
            {
                result.SetError(Constants.ErrorMessages.UserNotFoundName);
                return result;
            }

            if (!this.IsResetPasswordTokenValid(sitecoreUser, token))
            {
                result.SetError(Constants.ErrorMessages.TokenIsInvalid);
                return result;
            }

            if (!this.IsResetPasswordTokenLive(sitecoreUser))
            {
                result.SetError(Constants.ErrorMessages.TokenIsExpired);
                return result;
            }

            if (!this.ResetPassword(fullUserName, sitecoreUser, newPassword))
            {
                result.SetError(Constants.ErrorMessages.UnableToChangePassword);
            }

            return result;
        }

        private bool ResetPassword(string userName, Sitecore.Security.Accounts.User sitecoreUser, string newPassword)
        {
            var user = this.membershipService.GetUser(userName);
            var isChangeSuccessful = user?.ChangePassword(user.ResetPassword(), newPassword) ?? false;
            if (isChangeSuccessful)
            {
                this.userManager.RemoveCustomProperty(sitecoreUser, Constants.PasswordRecovery.ConfirmTokenKey);
            }

            return isChangeSuccessful;
        }

        private string EnsureUserFullName(string userName, string domain)
        {
            if (!userName.StartsWith(domain + "\\"))
                return domain + "\\" + userName;
            return userName;
        }

        private bool IsResetPasswordTokenValid(Sitecore.Security.Accounts.User sitecoreUser, string token)
        {
            var userToken = sitecoreUser?.Profile.GetCustomProperty(Constants.PasswordRecovery.ConfirmTokenKey);
            return !string.IsNullOrWhiteSpace(token)
                && !string.IsNullOrWhiteSpace(userToken)
                && string.Equals(token, userToken);
        }

        private bool IsResetPasswordTokenLive(Sitecore.Security.Accounts.User sitecoreUser)
        {
            var now = DateUtil.ToUniversalTime(DateTime.UtcNow);

            var tokenCreationTime = DateUtil.ToServerTime(DateUtil.IsoDateToDateTime(
                sitecoreUser.Profile.GetCustomProperty(Constants.PasswordRecovery.TokenCreationDatePropertyKey)));

            var expirationTime = Sitecore.Configuration.Settings.GetDoubleSetting(Constants.Settings.TokenExpirationTime, 24);

            return (now - tokenCreationTime) < TimeSpan.FromHours(expirationTime);
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



        private Result<TResult> ResolveResult<TResult, TArgs>(TArgs args, Func<TArgs, TResult> function = null)
            where TResult : class
            where TArgs : PipelineArgs
        {
            var errorMessages = args.GetMessages(PipelineMessageFilter.Errors);
            var result = function?.Invoke(args);
            return args.Aborted
                ? new Result<TResult>(result, errorMessages.Select(message => message.Text).ToList())
                {
                    Success = false
                }
                : new Result<TResult>(result);
        }
    }
}