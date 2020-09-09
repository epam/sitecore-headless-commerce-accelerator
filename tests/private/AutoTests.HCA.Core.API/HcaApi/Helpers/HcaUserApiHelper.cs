using System.Collections.Generic;
using System.Linq;
using AutoTests.AutomationFramework.Shared.Models;
using AutoTests.HCA.Core.API.HcaApi.Context;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Account;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Account.Authentication;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Addresses;

namespace AutoTests.HCA.Core.API.HcaApi.Helpers
{
    public class HcaUserApiHelper : HcaApiHelper
    {
        public HcaUserApiHelper(UserLogin user, IHcaApiContext apiContext) : base(apiContext)
        {
            ApiContext.Auth.Login(new LoginRequest(user.Email, user.Password));
        }

        public HcaUserApiHelper(CreateAccountRequest newUser, IHcaApiContext apiContext) : base(apiContext)
        {
            ApiContext.Account.CreateUserAccount(newUser);
            ApiContext.Auth.Login(new LoginRequest(newUser.Email, newUser.Password));
        }

        public IEnumerable<Address> AddAddress(Address newAddress)
        {
            var res = ApiContext.Account.AddAddress(newAddress);
            res.CheckSuccessfulResponse();
            return res?.OkResponseData.Data;
        }

        public IEnumerable<Address> AddAddresses(IEnumerable<Address> newAddresses)
        {
            IList<Address> addedAddresses = new List<Address>();
            using var enumerator = newAddresses.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var response = ApiContext.Account.AddAddress(enumerator.Current);
                response.CheckSuccessfulResponse();
                addedAddresses.Add(response.OkResponseData.Data.First());
            }

            return addedAddresses;
        }

        public void CleanAddresses()
        {
            var res = ApiContext.Account.GetAddresses();

            res.CheckSuccessfulResponse();

            var addressesListIsNotEmpty = res?.OkResponseData?.Data?.Any();
            if (!addressesListIsNotEmpty.HasValue || !addressesListIsNotEmpty.Value) return;

            foreach (var address in res.OkResponseData.Data)
                ApiContext.Account.RemoveAddress(address.ExternalId).CheckSuccessfulResponse();
        }
    }
}