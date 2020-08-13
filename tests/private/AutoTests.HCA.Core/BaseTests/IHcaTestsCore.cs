using AutoTests.AutomationFramework.Shared.Models;
using AutoTests.HCA.Core.API.Helpers;
using AutoTests.HCA.Core.API.Services.BraintreeServices;
using AutoTests.HCA.Core.API.Services.HcaService;

namespace AutoTests.HCA.Core.BaseTests
{
    public interface IHcaTestsCore
    {
        HcaApiService CreateHcaApiClient();

        UserManagerHelper CreateUserManagerHelper(UserLogin user, IHcaApiService hcaApiService = null);

        BraintreeApiService CreateBraintreeClient();
    }
}
