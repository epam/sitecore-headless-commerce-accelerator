using System.Collections.Generic;
using AutoTests.AutomationFramework.API.Services.RestService;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Account;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Addresses;
using AutoTests.HCA.Core.API.HcaApi.Models.RequestResult;
using RestSharp;

namespace AutoTests.HCA.Core.API.HcaApi.Services
{
    public class AccountService : BaseHcaService
    {
        public const string ACCOUNT_ENDPOINT = "accounts/account";
        public const string PASSWORD_ENDPOINT = "accounts/password";
        public const string ADDRESS_ENDPOINT = "accounts/address";
        public const string EMAIL_ENDPOINT = "/accounts/validate";

        public AccountService(IHttpClientService httpClientService) : base(httpClientService)
        {
        }

        public HcaVoidResponse ChangePassword(ChangePasswordRequest password)
        {
            return ExecuteJsonRequest(PASSWORD_ENDPOINT, Method.PUT, password);
        }

        public HcaResponse<UserResult> CreateUserAccount(CreateAccountRequest newUser)
        {
            return ExecuteJsonRequest<UserResult>(ACCOUNT_ENDPOINT, Method.POST, newUser);
        }

        public HcaVoidResponse UpdateAccount(UpdateAccountRequest account)
        {
            return ExecuteJsonRequest(ACCOUNT_ENDPOINT, Method.PUT, account);
        }

        public HcaResponse<ValidateEmailResult> ValidateEmail(ValidateEmailRequest email)
        {
            return ExecuteJsonRequest<ValidateEmailResult>(EMAIL_ENDPOINT, Method.POST, email);
        }

        public HcaResponse<IEnumerable<Address>> GetAddresses()
        {
            return ExecuteJsonRequest<IEnumerable<Address>>(ADDRESS_ENDPOINT, Method.GET);
        }

        public HcaResponse<IEnumerable<Address>> RemoveAddress(string externalId)
        {
            return ExecuteJsonRequest<IEnumerable<Address>>(ADDRESS_ENDPOINT + $"?externalid={externalId}",
                Method.DELETE);
        }

        public HcaResponse<IEnumerable<Address>> UpdateAddress(Address newAddress)
        {
            return ExecuteJsonRequest<IEnumerable<Address>>(ADDRESS_ENDPOINT, Method.PUT, newAddress);
        }

        public HcaResponse<IEnumerable<Address>> AddAddress(Address newAddress)
        {
            return ExecuteJsonRequest<IEnumerable<Address>>(ADDRESS_ENDPOINT, Method.POST, newAddress);
        }
    }
}