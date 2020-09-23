using System;
using AutoTests.AutomationFramework.UI.Controls;
using AutoTests.AutomationFramework.UI.Core;
using AutoTests.AutomationFramework.UI.Interfaces;
using AutoTests.HCA.Core.Common.Entities.ConstantsAndEnums.Checkout;

namespace AutoTests.HCA.Core.UI
{
    public abstract class CommonPage : IPage
    {
        //public virtual void VerifyOpened()
        //{
        //    throw new NotImplementedException();
        //}
        protected readonly WebElement _spinner = new WebElement("Spinner", ByCustom.XPath("//div[@class= 'loading']"));

        protected virtual WebElement FieldsContainer { get; }

        public void VerifyOpened()
        {
            throw new NotImplementedException();
        }

        public Uri GetUrl()
        {
            throw new NotImplementedException();
        }

        public abstract string GetPath();

        public string GetTitleText()
        {
            // TODO:  create overloaded method
            return new WebElement("Page Title", ByCustom.XPath("//title")).GetAttribute("innerHTML");
        }

        public virtual void WaitForOpened()
        {
            Browser.WaitForUrlContains(GetPath());
            SpinnerWaitForNotPresent();
        }

        private WebCheckBox FindOptionByName(string nameOption)
        {
            return new WebCheckBox($"{nameOption} select",
                ByCustom.XPath($"//ul[@class= 'options']//label[text() = '{nameOption}']"));
        }

        public void SelectOptionByName(AddressOption nameOption)
        {
            FindOptionByName(nameOption.GetValue()).Check();
        }

        public void VerifyOption(AddressOption nameOption, bool selected)
        {
            var nameOptionString = nameOption.GetValue();
            new WebCheckBox($"Selector for {nameOptionString}", ByCustom.XPath("./preceding-sibling::*"),
                FindOptionByName(nameOptionString)).Verify(selected);
        }

        protected virtual WebTextField FindFieldContainer(string nameField)
        {
            return new WebTextField($"Container field {nameField}",
                ByCustom.XPath($".//label[contains(text(), '{nameField}')]/parent::*"), FieldsContainer);
        }

        protected virtual WebTextField GetInputField(string nameField)
        {
            var fieldContainer = FindFieldContainer(nameField);
            return new WebTextField($"Text field {nameField}",
                ByCustom.XPath("./input"), fieldContainer);
        }

        public void FillFieldByName(string nameField, string value)
        {
            GetInputField(nameField).Type(value);
        }

        public string GetFieldValue(string nameField)
        {
            return GetInputField(nameField).GetText();
        }

        public void VerifyFieldValue(string nameField, string value)
        {
            GetInputField(nameField).VerifyText(value);
        }

        public void SelectValueInTheField(string nameField, string value)
        {
            var fieldContainer = FindFieldContainer(nameField);
            new WebElement($"Value {value}", ByCustom.XPath($".//option[text()='{value}']"), fieldContainer).Click();
        }

        public void SelectHasNoValue(string nameField, string value)
        {
            var fieldContainer = FindFieldContainer(nameField);
            new WebElement($"Value {value}", ByCustom.XPath($".//option[text()='{value}']"), fieldContainer)
                .WaitForNotEnabled();
        }

        public void VerifyFieldError(string nameField, string text)
        {
            var fieldContainer = FindFieldContainer(nameField);
            var errorMessage = new WebLabel($"Error message {nameField}",
                ByCustom.XPath("./span[@class = 'form-field-error-message']"), fieldContainer);
            errorMessage.IsPresent();
            errorMessage.VerifyText(text);
        }

        public void SpinnerWaitForNotPresent()
        {
            _spinner.WaitForPresent(5, false);
            _spinner.WaitForNotPresent();
        }
    }
}