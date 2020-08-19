using AutoTests.HCA.Core.API.Helpers;
using AutoTests.HCA.Core.API.Services.BraintreeServices;
using AutoTests.HCA.Core.API.Services.HcaService;
using AutoTests.HCA.Core.Common.Settings.Users;

namespace AutoTests.HCA.Core.BaseTests
{
    public interface IHcaTestsCore
    {
        HcaApiService CreateHcaApiClient();

        UserManagerHelper CreateUserManagerHelper(HcaUser user, IHcaApiService hcaApiService = null);

        BraintreeApiService CreateBraintreeClient();
    }
}