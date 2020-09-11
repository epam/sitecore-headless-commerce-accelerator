using AutoTests.AutomationFramework.Shared.Configuration;
using AutoTests.AutomationFramework.Shared.Models;
using AutoTests.AutomationFramework.UI.Core;
using AutoTests.HCA.Core.API.BraintreeApi.Services;
using AutoTests.HCA.Core.API.BraintreeApi.Settings;
using AutoTests.HCA.Core.API.HcaApi.Context;
using AutoTests.HCA.Core.API.HcaApi.Helpers;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Account;
using AutoTests.HCA.Core.API.HcaApi.Settings;
using OpenQA.Selenium;

namespace AutoTests.HCA.Core.BaseTests
{
    public class HcaTestsCore : IHcaTestsCore
    {
        protected static readonly ConfigurationManager Configuration = new ConfigurationManager("appsettings.json");
        protected static readonly HcaApiSettings ApiSettings = Configuration.Get<HcaApiSettings>();
        protected static readonly BraintreeSettings BraintreeSettings = Configuration.Get<BraintreeSettings>();

        public IHcaApiContext CreateHcaApiContext()
        {
            return new HcaApiContext(ApiSettings);
        }

        public HcaUserApiHelper CreateHcaUserApiHelper(UserLogin user, IHcaApiContext apiContext = null)
        {
            return new HcaUserApiHelper(user, apiContext ?? CreateHcaApiContext());
        }

        public HcaUserApiHelper CreateHcaUserApiHelper(CreateAccountRequest newUser, IHcaApiContext apiContext = null)
        {
            return new HcaUserApiHelper(newUser, apiContext ?? CreateHcaApiContext());
        }

        public HcaGuestApiHelper CreateHcaGuestApiHelper(CookieData cookie)
        {
            var newSettings = new HcaApiSettings
            {
                GlobalAuthentication = new GlobalAuthentication
                {
                    BasicAuthenticator = ApiSettings.GlobalAuthentication.BasicAuthenticator,
                    SiteCoreCookie = cookie
                },
                HcaApiUri = ApiSettings.HcaApiUri
            };

            return new HcaGuestApiHelper(cookie, new HcaApiContext(newSettings));
        }

        public HcaGuestApiHelper CreateHcaGuestApiHelperWithBrowserCookie()
        {
            var cookie = Browser.GetCookieByName("SC_ANALYTICS_GLOBAL_COOKIE");
            var cookieData = new CookieData(cookie.Name, cookie.Value.Replace("False", "True"));
            return CreateHcaGuestApiHelper(cookieData);
        }
        public HcaGuestApiHelper CreateHcaGuestApiHelper(IHcaApiContext apiContext)
        {
            return new HcaGuestApiHelper(apiContext);
        }

        public IBraintreeApiService CreateBraintreeClient()
        {
            return new BraintreeApiService(BraintreeSettings);
        }
    }
}