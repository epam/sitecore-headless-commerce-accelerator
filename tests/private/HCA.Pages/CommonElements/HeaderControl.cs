using OpenQA.Selenium;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.CommonElements
{
    public class HeaderControl
    {
        private static HeaderControl _headerControl;

        private readonly WebButton cartButton = new WebButton("Cart button", By.XPath("//a[@href = '/cart']"));

        private readonly WebElement headerElement = new WebElement("Header", ByCustom.Id("header-main"));

        private readonly WebElement navigationUserPanel =
            new WebElement("Navigation User panel", ByCustom.Id("nav-user"));

        private readonly WebButton userButton = new WebButton("User button", By.XPath("//i[@class = 'fa fa-user']"));

        public static HeaderControl Instance =>
            _headerControl ?? (_headerControl = new HeaderControl());

        public void CartButtonClick()
        {
            cartButton.Click();
        }

        public void UserButtonClick()
        {
            userButton.Click();
        }
    }
}