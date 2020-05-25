using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using HCA.Pages.CommonElements;
using OpenQA.Selenium;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;
using UIAutomationFramework.Interfaces;

namespace HCA.Pages.Pages.Checkout
{
    abstract public class CheckoutPage : FieldsContainer,IPage
    {
        private readonly WebElement _navigationPanel = new WebElement("Navigation Panel", ByCustom.XPath("//nav[@class = 'nav-checkout']"));

        private readonly WebSelect _shippingMethodSelect= new WebSelect("Shipping method", ByCustom.XPath("//select[contains(@name, 'SHIPPING_METHOD')]"));

        private readonly WebElement _saveAndContinueButton = new WebElement("Save & continue button", ByCustom.XPath("//button[@type = 'submit']"));

        private WebTextField FindFieldByName(String nameField)
        {
            return new WebTextField($"Text field {nameField}", 
                ByCustom.XPath($"//div/label[contains(text(), '{nameField}')]/following-sibling::*"));
        }

        public void ClickSubmit()
        {
            _saveAndContinueButton.Click();
        }
        
        public void SelectShippingMethod(String shippingMethod)
        {
            new WebElement($"Shiiping method {shippingMethod}", 
                ByCustom.XPath($"./option[text()='{shippingMethod}']"), _shippingMethodSelect).Click();
        }
        public void VerifyOpened()
        {
           _navigationPanel.WaitForPresent();
        }

        public Uri GetUrl()
        {
            throw new NotImplementedException();
        }

        abstract public string GetPath();
    }
}
