using HCA.Pages.ConsantsAndEnums.ConsantsAndEnums;
using UIAutomationFramework.Utils;

namespace HCA.Pages.ConsantsAndEnums.Header
{
    public enum QuickNavigationLink
    {
        [Link("Store Locator", "STORE LOCATOR", "")]
        StoreLocator,

        [Link("Online Flyer", "ONLINE FLYER", "")]
        OnlineFlyer,

        [Link("Language/Currency", "LANGUAGE/CURRENCY", "")]
        LanguageAndCurrency
    }

    public static class HeaderUtilityLinkExtensions
    {
        public static string GetLinkName(this QuickNavigationLink ul) =>
            ul.GetAttribute<LinkAttribute>().LinkName;

        public static string GetLinkText(this QuickNavigationLink ul) =>
            ul.GetAttribute<LinkAttribute>().LinkText;

        public static string GetHref(this QuickNavigationLink ul) =>
            ul.GetAttribute<LinkAttribute>().Href;
    }
}
