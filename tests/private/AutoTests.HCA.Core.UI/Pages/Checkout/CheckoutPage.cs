using AutoTests.AutomationFramework.UI.Controls;
using AutoTests.AutomationFramework.UI.Core;
using AutoTests.HCA.Core.Common.Entities.ConstantsAndEnums.Checkout;
using NUnit.Framework;

namespace AutoTests.HCA.Core.UI.Pages.Checkout
{
    public abstract class CheckoutPage : CommonPage
    {
        private readonly WebElement _navigationPanel =
            new WebElement("Navigation Panel", ByCustom.XPath("//nav[@class = 'nav-checkout']"));

        private readonly WebElement _saveAndContinueButton =
            new WebElement("Save & continue button", ByCustom.XPath("//button[@type = 'submit']"));

        private readonly WebSelect _shippingLocationSelect = new WebSelect("Shipping location",
            ByCustom.XPath("//select[contains(@name, 'SELECTED_ADDRESS')]"));

        private readonly WebSelect _shippingMethodSelect = new WebSelect("Shipping method",
            ByCustom.XPath("//select[contains(@name, 'SHIPPING_METHOD')]"));

        protected virtual void FillFieldsByDefault()
        {
        }

        private WebTextField FindFieldByName(string nameField)
        {
            return new WebTextField($"Text field {nameField}",
                ByCustom.XPath($"//div/label[contains(text(), '{nameField}')]/following-sibling::*"));
        }

        public void ClickSubmit()
        {
            _saveAndContinueButton.Click();
        }

        public void VerifySubmitIsNotClickable()
        {
            _saveAndContinueButton.VerifyNotClickable();
        }

        public void SelectShippingMethod(ShippingMethod shippingMethod)
        {
            var shippingMethodValue = shippingMethod.GetValue();
            new WebElement($"Shipping method {shippingMethodValue}",
                ByCustom.XPath($"./option[text()='{shippingMethodValue}']"), _shippingMethodSelect).Click();
        }

        public void SelectShippingAddress(string shippingAddress)
        {
            new WebElement($"Shipping address {shippingAddress}",
                ByCustom.XPath($"./option[text()='{shippingAddress}']"), _shippingLocationSelect).Click();
        }

        public void SelectFirstShippingAddress()
        {
            if (_shippingLocationSelect.GetChildElementsCount(ByCustom.XPath("./option")) < 2)
                Assert.Fail("Shipping address has no values");
            new WebElement("First Shipping address",
                ByCustom.XPath("./option[2]"), _shippingLocationSelect).Click();
        }

        public void GoToTheNextPage()
        {
            WaitForOpened();
            FillFieldsByDefault();
            ClickSubmit();
        }
    }
}