using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.UI.ConstantsAndEnums.Common;

namespace AutoTests.HCA.Core.UI.ConstantsAndEnums.Header.MainMenu
{
    public enum MenuItem
    {
        [Link("Cameras", "CAMERAS", "")] Cameras,

        [Link("Computers", "COMPUTERS", "")] Computers,

        [Link("Household", "HOUSEHOLD", "")] HouseHold,

        [Link("Multimedia", "MULTIMEDIA", "")] Multimedia,

        [Link("Phones", "PHONES", "")] Phones
    }

    public static class MenuItemLinkExtensions
    {
        public static string GetLinkName(this MenuItem item)
        {
            return item.GetAttribute<LinkAttribute>().Name;
        }

        public static string GetLinkText(this MenuItem item)
        {
            return item.GetAttribute<LinkAttribute>().LinkText;
        }

        public static string GetHref(this MenuItem item)
        {
            return item.GetAttribute<LinkAttribute>().Href;
        }
    }
}