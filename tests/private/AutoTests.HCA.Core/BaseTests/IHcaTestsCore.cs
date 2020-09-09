using AutoTests.AutomationFramework.Shared.Models;
using AutoTests.HCA.Core.API.BraintreeApi.Services;
using AutoTests.HCA.Core.API.HcaApi.Context;
using AutoTests.HCA.Core.API.HcaApi.Helpers;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Account;

namespace AutoTests.HCA.Core.BaseTests
{
    public interface IHcaTestsCore
    {
        IHcaApiContext CreateHcaApiContext();

        HcaUserApiHelper CreateHcaUserApiHelper(UserLogin user, IHcaApiContext apiContext = null);

        HcaUserApiHelper CreateHcaUserApiHelper(CreateAccountRequest user, IHcaApiContext apiContext = null);

        HcaGuestApiHelper CreateHcaGuestApiHelper(CookieData cookie);

        HcaGuestApiHelper CreateHcaGuestApiHelper(IHcaApiContext apiContext = null);

        IBraintreeApiService CreateBraintreeClient();
    }
}