using AutoTests.AutomationFramework.Shared.Models;

namespace AutoTests.HCA.Core.API.HcaApi.Settings
{
    public class GlobalAuthentication
    {
        public BasicAuthenticator BasicAuthenticator { get; set; }
        public CookieData SiteCoreCookie { get; set; }
    }
}