using System;
using System.Collections.Generic;
using System.Text;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.CommonElements
{
    public class FieldsContainer
    {
        public void SelectOptionByName(string nameOption)
        {
            new WebCheckBox("New address select", ByCustom.XPath($"//ul[@class= 'options']//label[text() = '{nameOption}']")).Check();
        }

        private WebTextField FindFieldContainer(String nameField)
        {
            return new WebTextField($"Container field {nameField}",
                ByCustom.XPath($"//div/label[contains(text(), '{nameField}')]/parent::*"));
        }

        private WebTextField GetInputField(String nameField)
        {
            var fieldContainer = FindFieldContainer(nameField);
            return new WebTextField($"Text field {nameField}",
                ByCustom.XPath($"./input"), fieldContainer);
        }

        public void FillFieldByName(String nameField, String value)
        {
            GetInputField(nameField).Type(value);
        }

        public void SelectValueInTheField(String nameField, String value)
        {
            var fieldContainer = FindFieldContainer(nameField);
            new WebElement($"Value {value}", ByCustom.XPath($".//option[text()='{value}']"), fieldContainer).Click();
        }

        public void VerifyFieldError(String nameField, String text)
        {
            var fieldContainer = FindFieldContainer(nameField);
            var errorMessage = new WebTextField($"Error message {nameField}",
                ByCustom.XPath($"./span"), fieldContainer);
                errorMessage.IsPresent();
                errorMessage.VerifyText(text);
        }
    }
}
