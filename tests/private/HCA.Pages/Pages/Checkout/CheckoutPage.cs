using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.Pages.Checkout
{
    public abstract class CheckoutPage : CommonPage
    {
        private readonly WebElement _navigationPanel =
            new WebElement("Navigation Panel", ByCustom.XPath("//nav[@class = 'nav-checkout']"));

        private readonly WebElement _saveAndContinueButton =
            new WebElement("Save & continue button", ByCustom.XPath("//button[@type = 'submit']"));

        private readonly WebSelect _shippingMethodSelect = new WebSelect("Shipping method",
            ByCustom.XPath("//select[contains(@name, 'SHIPPING_METHOD')]"));

        public void ClickSubmit()
        {
            _saveAndContinueButton.Click();
        }

        public void SelectShippingMethod(string shippingMethod)
        {
            new WebElement($"Shiiping method {shippingMethod}",
                ByCustom.XPath($"./option[text()='{shippingMethod}']"), _shippingMethodSelect).Click();
        }
    }
}