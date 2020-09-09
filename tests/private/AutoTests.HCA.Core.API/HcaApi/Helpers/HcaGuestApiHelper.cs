using AutoTests.AutomationFramework.Shared.Models;
using AutoTests.HCA.Core.API.HcaApi.Context;

namespace AutoTests.HCA.Core.API.HcaApi.Helpers
{
    public class HcaGuestApiHelper : HcaApiHelper
    {
        public HcaGuestApiHelper(IHcaApiContext apiContext) : base(apiContext)
        {
        }

        public HcaGuestApiHelper(CookieData guestCookie, IHcaApiContext apiContext) : this(apiContext)
        {
            ApiContext.Client.SetCookieIfNotSet(guestCookie);
        }
    }
}