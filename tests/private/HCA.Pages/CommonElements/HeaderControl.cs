using OpenQA.Selenium;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.CommonElements
{
    public class HeaderControl
    {
        private static HeaderControl _headerControl;

        private static readonly WebButton _cartButton = new WebButton("Cart button", By.XPath("//a[@href = '/cart']"));

        private static readonly WebElement _quantityProducts =
            new WebElement("Quantity Products in Cart", By.ClassName("quantity"), _cartButton);

        private readonly WebElement _headerElement = new WebElement("Header", ByCustom.Id("header-main"));

        private readonly WebElement _navigationUserPanel =
            new WebElement("Navigation User panel", ByCustom.Id("nav-user"));

        private readonly WebButton _userButton = new WebButton("User button", By.XPath("//i[@class = 'fa fa-user']"));

        public static HeaderControl Instance => _headerControl ??= new HeaderControl();

        public void CartButtonClick()
        {
            _cartButton.Click();
        }

        public void UserButtonClick()
        {
            _userButton.WaitForClickable();
            _userButton.Click();
        }

        public void WaitForPresentProductsQuantity() =>
            _quantityProducts.WaitForPresent();
    }
}