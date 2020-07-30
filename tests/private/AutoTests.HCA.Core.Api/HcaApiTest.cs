using AutoTests.AutomationFramework.API;
using AutoTests.HCA.Core.API.Services.BraintreeServices;
using AutoTests.HCA.Core.API.Services.HcaService;
using AutoTests.HCA.Core.API.Settings.Api;
using AutoTests.HCA.Core.API.Settings.Braintree;
using NUnit.Framework;

namespace AutoTests.HCA.Core.API
{
    [TestFixture]
    [Description("Base Hca Api Test.")]
    public class HcaApiTest : ApiTest
    {
        protected static readonly HcaApiSettings ApiSettings;
        protected static readonly BraintreeSettings BraintreeSettings;

        static HcaApiTest()
        {
            ApiSettings = Configuration.Get<HcaApiSettings>();
            BraintreeSettings = Configuration.Get<BraintreeSettings>();
        }

        protected static HcaApiService CreateHcaApiClient()
        {
            return new HcaApiService(ApiSettings);
        }

        protected static BraintreeApiService CreateBraintreeClient()
        {
            return new BraintreeApiService(BraintreeSettings);
        }
    }
}