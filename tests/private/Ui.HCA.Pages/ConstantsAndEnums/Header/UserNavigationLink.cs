using Ui.AutomationFramework;
using Ui.AutomationFramework.Utils;
using Ui.HCA.Pages.ConstantsAndEnums.Common;

namespace Ui.HCA.Pages.ConstantsAndEnums.Header
{
    public enum UserNavigationLink
    {
        [Link("Wishlist", null, "wishlist")]
        WishList,



        [Link("Shopping Cart", null, "cart")]
        ShoppingCart,



        [Link("My Account", null, null)]
        MyAccount
    }

    public static class UserNavigationLinkExtensions
    {
        public static string GetLinkName(this UserNavigationLink sl) =>
            sl.GetAttribute<LinkAttribute>().Name;

        public static string GetHref(this UserNavigationLink sl)
        {
            var href = sl.GetAttribute<LinkAttribute>().Href;
            return (href == null) ? null : UriManager.AddPostfix(Configuration.GetEnvironmentUri("HCAEnvironment"), href).AbsoluteUri;
        }
    }
}
