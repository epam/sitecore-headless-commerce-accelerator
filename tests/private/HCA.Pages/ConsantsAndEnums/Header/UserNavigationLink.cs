using HCA.Pages.ConsantsAndEnums.ConsantsAndEnums;
using UIAutomationFramework.Utils;

namespace HCA.Pages.ConsantsAndEnums.Header
{
    public enum UserNavigationLink
    {
        [Link("Wishlist", null, "https://int-cd.hca.azure.epmc-stc.projects.epam.com/wishlist")]
        WishList,

        [Link("Shopping Cart", null, "https://int-cd.hca.azure.epmc-stc.projects.epam.com/cart")]
        ShoppingCart,

        [Link("My Account", null, null)]
        MyAccount
    }

    public static class UserNavigationLinkExtensions
    {
        public static string GetLinkName(this UserNavigationLink sl) =>
            sl.GetAttribute<LinkAttribute>().LinkName;

        public static string GetHref(this UserNavigationLink sl) =>
            sl.GetAttribute<LinkAttribute>().Href;
    }
}
