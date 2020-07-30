﻿using System;
using AutoTests.AutomationFramework.UI.Controls;
using AutoTests.AutomationFramework.UI.Core;
using AutoTests.AutomationFramework.UI.Interfaces;

namespace AutoTests.HCA.Core.UI
{
    public abstract class CommonPage : IPage
    {
        //public virtual void VerifyOpened()
        //{
        //    throw new NotImplementedException();
        //}

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
        }

        public void SelectOptionByName(string nameOption)
        {
            new WebCheckBox("New address select",
                ByCustom.XPath($"//ul[@class= 'options']//label[text() = '{nameOption}']")).Check();
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
    }
}