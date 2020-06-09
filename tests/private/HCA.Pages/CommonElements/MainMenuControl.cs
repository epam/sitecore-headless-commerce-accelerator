using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.CommonElements
{
    public class MainMenuControl
    {
        private static MainMenuControl _mainMenuControl;

        private readonly WebElement _menuContainer = new WebElement("Menu Container", ByCustom.Id("nav-main"));

        public static MainMenuControl Instance => _mainMenuControl ??= new MainMenuControl();

        private WebElement FindMenuItemByName(string itemName)
        {
            return new WebElement($"Menu item {itemName}",
                ByCustom.XPath($"//li[contains(@class,'menu-item')]/a[text()='{itemName}']/parent::*"));
        }


        public void MoveMouseTiMenuItem(string itemName) => 
            FindMenuItemByName(itemName).MouseOver();

        private WebElement FindSubMenuItemByName(string subMenuName, WebElement menuItem)
        {
            return new WebElement($"Submenu item {subMenuName}",
                ByCustom.XPath($".//li/a[contains(@href, '{subMenuName}')]"), menuItem);
        }

        public void ChooseSubMenuItem(string subMenuName, string itemName)
        {
            var elementMenuItem = FindMenuItemByName(itemName);
            elementMenuItem.MouseOver();
            var subMenuItem = FindSubMenuItemByName(subMenuName, elementMenuItem);
            subMenuItem.WaitForPresent();
            subMenuItem.Click();
        }
    }
}