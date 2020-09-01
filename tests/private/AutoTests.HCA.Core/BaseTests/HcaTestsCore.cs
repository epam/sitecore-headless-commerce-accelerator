using AutoTests.AutomationFramework.Shared.Configuration;
using AutoTests.HCA.Core.API.Helpers;
using AutoTests.HCA.Core.API.Services.BraintreeServices;
using AutoTests.HCA.Core.API.Services.HcaService;
using AutoTests.HCA.Core.API.Settings.Api;
using AutoTests.HCA.Core.API.Settings.Braintree;
using AutoTests.HCA.Core.Common.Settings.Users;

namespace AutoTests.HCA.Core.BaseTests
{
    public class HcaTestsCore : IHcaTestsCore
    {
        protected static readonly ConfigurationManager Configuration = new ConfigurationManager("appsettings.json");
        protected static readonly HcaApiSettings ApiSettings = Configuration.Get<HcaApiSettings>();
        protected static readonly BraintreeSettings BraintreeSettings = Configuration.Get<BraintreeSettings>();

        public HcaApiService CreateHcaApiClient()
        {
            return new HcaApiService(ApiSettings);
        }

        public UserManagerHelper CreateUserManagerHelper(HcaUserTestsDataSettings user, IHcaApiService hcaApiService = null)
        {
            return new UserManagerHelper(user, hcaApiService ?? CreateHcaApiClient());
        }

        public BraintreeApiService CreateBraintreeClient()
        {
            return new BraintreeApiService(BraintreeSettings);
        }
    }
}