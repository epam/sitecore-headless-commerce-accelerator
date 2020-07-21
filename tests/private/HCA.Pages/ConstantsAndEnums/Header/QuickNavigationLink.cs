using HCA.Pages.ConstantsAndEnums.Common;
using UIAutomationFramework.Utils;

namespace HCA.Pages.ConstantsAndEnums.Header
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

    public static class HeaderQuickNavigationExtensions
    {
        public static string GetLinkName(this QuickNavigationLink ul) =>
            ul.GetAttribute<LinkAttribute>().Name;

        public static string GetLinkText(this QuickNavigationLink ul) =>
            ul.GetAttribute<LinkAttribute>().LinkText;

        public static string GetHref(this QuickNavigationLink ul) =>
            ul.GetAttribute<LinkAttribute>().Href;
    }
}
