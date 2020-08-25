using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.UI.ConstantsAndEnums.Common;

namespace AutoTests.HCA.Core.UI.ConstantsAndEnums.Header
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
        public static string GetLinkName(this QuickNavigationLink ul)
        {
            return ul.GetAttribute<LinkAttribute>().Name;
        }

        public static string GetLinkText(this QuickNavigationLink ul)
        {
            return ul.GetAttribute<LinkAttribute>().LinkText;
        }

        public static string GetHref(this QuickNavigationLink ul)
        {
            return ul.GetAttribute<LinkAttribute>().Href;
        }
    }
}