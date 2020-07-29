using Api.AutomationFramework;
using Api.HCA.Core.Services.BraintreeServices;
using Api.HCA.Core.Services.HcaService;
using Api.HCA.Core.Settings.Api;
using Api.HCA.Core.Settings.Braintree;
using NUnit.Framework;

namespace Api.HCA.Core
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