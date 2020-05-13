using Core;
using HCA.Pages.Pages;

namespace Pages.Components.BasePageComponents
{
    public class Header
    {
        public NavMenu NavMenu => new NavMenu();

        private UiElement Cart => new UiElement("//a[contains(@href,'cart')]");

        public CartPage OpenCartPage()
        {
            Cart.Click();
            return new CartPage();
        }
    }
}
