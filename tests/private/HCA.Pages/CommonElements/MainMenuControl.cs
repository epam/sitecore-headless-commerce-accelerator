using System.Linq;
using HCA.Pages.ConstantsAndEnums.Header.MainMenu;
using NUnit.Framework;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.CommonElements
{
    public class MainMenuControl
    {
        private static MainMenuControl _mainMenuControl;
        public static MainMenuControl Instance => _mainMenuControl ??= new MainMenuControl();

        private static readonly WebElement _menuContainer = new WebElement("Menu Container", ByCustom.ClassName("navigation-menu"));

        public int MenuItemsCount =>
            _menuContainer.GetChildElementsCount(ByCustom.ClassStartsWith("menu-item"));

        public bool MenuContainerIsPresent() => 
            _menuContainer.IsPresent();

        public void MoveMouseTiMenuItem(MenuItem menuItem) => 
            FindMenuLinkByName(menuItem).MouseOver();

        public bool TitleMenuItemIsPresent(string title, MenuItem menuItem) => 
            FindTitleSubMenuItemByName(title, FindMenuLinkByName(menuItem)).IsPresent();

        public void VerifyMenuItemImage(MenuItem menuItem)
        {
            var imageElement = new WebImage($"Image of {menuItem} Menu Item", ByCustom.TagName("img"),
                FindMenuLinkByName(menuItem).GetParent());
            imageElement.IsPresent();

            Assert.IsFalse(string.IsNullOrWhiteSpace(imageElement.GetSource()),
                $"{menuItem} Menu Item src image isn't present or contains white space");
        }

        public void VerifyMenuItem(MenuItem menuItem) =>
            VerifyLink(FindMenuLinkByName(menuItem), menuItem.GetLinkText(), menuItem.GetHref());

        public void VerifySubMenuItems(MenuItem menuItem, params SubMenuItem[] subMenuItems)
        {
            var menuLink = FindMenuLinkByName(menuItem);
            if (subMenuItems.Any())
                foreach (var subMenuItem in subMenuItems)
                {
                    var subMenuLink = FindSubMenuLinkByName(subMenuItem, menuLink);
                    VerifyLink(subMenuLink, subMenuItem.GetLinkText(), subMenuItem.GetHref());
                }
        }

        public void ChooseSubMenuItem(MenuItem menuItemName, SubMenuItem subMenuItemName)
        {
            var elementMenuItem = FindMenuLinkByName(menuItemName);
            elementMenuItem.MouseOver();
            var subMenuItemElement = FindSubMenuLinkByName(subMenuItemName, elementMenuItem);
            subMenuItemElement.WaitForPresent();
            subMenuItemElement.Click();
        }

        private static void VerifyLink(WebLink linkElement, string linkText, string linkHref)
        {
            linkElement.WaitForPresent();
            linkElement.VerifyText(linkText);
            linkElement.VerifyUrl(linkHref);
            linkElement.IsClickable();
        }

        private static WebLink FindMenuLinkByName(MenuItem menuItem)
        {
            var name = menuItem.GetLinkName();
            return new WebLink($"Menu item {name}", ByCustom.XPath($"//a[text()='{name}']"));
        }

        private static WebLink FindSubMenuLinkByName(SubMenuItem subMenuItem, WebLink menuItemLink)
        {
            var subMenuItemName = subMenuItem.GetLinkName();
            return new WebLink($"Submenu item {subMenuItemName}", ByCustom.XPath($".//li/a[text()='{subMenuItemName}']"),
                menuItemLink.GetParent());
        }

        private static WebLink FindTitleSubMenuItemByName(string titleName, WebLink menuItemLink)
        {
            return new WebLink($"Submenu title {titleName}",
                ByCustom.XPath($".//li/span[text()='{titleName}' and @class='title']"),
                menuItemLink.GetParent());
        }
    }
}