using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.AutomationFramework.UI;
using AutoTests.HCA.Core.UI.ConstantsAndEnums.Common;

namespace AutoTests.HCA.Core.UI.ConstantsAndEnums.Header
{
    public enum UserNavigationLink
    {
        [Link("Wishlist", null, "wishlist")] WishList,


        [Link("Shopping Cart", null, "cart")] ShoppingCart,


        [Link("My Account", null, null)] MyAccount
    }

    public static class UserNavigationLinkExtensions
    {
        public static string GetLinkName(this UserNavigationLink sl)
        {
            return sl.GetAttribute<LinkAttribute>().Name;
        }

        public static string GetHref(this UserNavigationLink sl)
        {
            var href = sl.GetAttribute<LinkAttribute>().Href;
            return href == null
                ? null
                : UiConfiguration.GetEnvironmentUri("HCAEnvironment").AddPostfix(href).AbsoluteUri;
        }
    }
}