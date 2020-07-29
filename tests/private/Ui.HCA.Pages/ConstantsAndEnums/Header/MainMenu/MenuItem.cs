using Ui.AutomationFramework.Utils;
using Ui.HCA.Pages.ConstantsAndEnums.Common;

namespace Ui.HCA.Pages.ConstantsAndEnums.Header.MainMenu
{
    public enum MenuItem
    {
        [Link("Cameras", "CAMERAS", "")]
        Cameras,

        [Link("Computers", "COMPUTERS", "")]
        Computers,

        [Link("Household", "HOUSEHOLD", "")]
        HouseHold,

        [Link("Multimedia", "MULTIMEDIA", "")]
        Multimedia,

        [Link("Phones", "PHONES", "")]
        Phones,
    }

    public static class MenuItemLinkExtensions
    {
        public static string GetLinkName(this MenuItem item) =>
            item.GetAttribute<LinkAttribute>().Name;

        public static string GetLinkText(this MenuItem item) =>
            item.GetAttribute<LinkAttribute>().LinkText;

        public static string GetHref(this MenuItem item) =>
            item.GetAttribute<LinkAttribute>().Href;
    }
}