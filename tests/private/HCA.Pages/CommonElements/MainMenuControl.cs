using System;
using System.Collections.Generic;
using System.Text;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.CommonElements
{
    public class MainMenuControl
    {
        private static MainMenuControl _mainMenuControl;

        public static MainMenuControl Instance =>
            _mainMenuControl ?? (_mainMenuControl = new MainMenuControl());

        private readonly WebElement menuConainer = new WebElement("Menu Container", ByCustom.Id("nav-main"));

        private WebElement FindMenuItemByName(String itemName)
        {
            return new WebElement($"Menu item {itemName}", ByCustom.XPath($"//li[@class ='menu-item']/a[text()='{itemName}']/parent::*"));
        }


        public void MoveMouseTiMenuItem(String itemName)
        {
            FindMenuItemByName(itemName).MouseOver();
        }

        private WebElement FindSubMenuItemByName(String subMenuName, WebElement menuItem)
        {
           return new WebElement($"Submenu item {subMenuName}", ByCustom.XPath($".//li/a[contains(@href, '{subMenuName}')]"), menuItem);
        }

        public void ChooseSubMenuItem(String subMenuName, String itemName)
        {
            WebElement elementMenuItem = FindMenuItemByName(itemName);
            elementMenuItem.MouseOver();
            WebElement subMenuItem = FindSubMenuItemByName(subMenuName, elementMenuItem);
            subMenuItem.WaitForPresent();
            subMenuItem.Click();
        }
    }
}
