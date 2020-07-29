namespace Api.HCA.Core.Settings.Api
{
    public class GlobalAuthentication
    {
        public BasicAuthenticator BasicAuthenticator { get; set; }
        public CookieData SiteCoreCookie { get; set; }
    }
}