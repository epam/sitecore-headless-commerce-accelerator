using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UI.AutomationFramework.Controls;
using static Ui.AutomationFramework.Core.TestLogger;
using LogLevel = NLog.LogLevel;

namespace Ui.AutomationFramework.Controls
{
    public class WebSelect : BaseWebUiElement
    {
        public WebSelect(string elementName, By locator, BaseWebControl container = null) : base(
            elementName, new Locator(locator), container)
        {
        }

        public WebSelect(string elementName, Locator locator, Frame frame) : base(elementName, locator, frame)
        {
        }

        public WebSelect(string elementName, By locator, Frame frame) : base(elementName, locator, frame)
        {
        }

        private SelectElement Select()
        {
            return new SelectElement(Get());
        }

        private int GetOptionsCount()
        {
            Log(LogLevel.Debug, $"Getting options count of {ElementName}");
            return Select().Options.Count;
        }

        public void Select(string text)
        {
            Log(LogLevel.Debug, $"Selecting '{text}' in {ElementName}");
            if (text != null) Select().SelectByText(text);
        }

        public void SelectByContains(string text)
        {
            Log(LogLevel.Debug, $"Selecting {text} in {ElementName}");
            if (text != null) Select().SelectByText(Select().Options.First(x => x.Text.Contains(text)).Text);
        }

        public void SelectByIndex(int index)
        {
            Log(LogLevel.Debug, $"Selecting by index: {index} in {ElementName}");

            Select().SelectByIndex(index);
        }

        public void SelectByValue(int value)
        {
            SelectByValue(value.ToString());
        }

        public void SelectByValue(string value)
        {
            Log(LogLevel.Debug, $"Selecting by value: {value} in {ElementName}");

            if (value != null) Select().SelectByValue(value);
        }

        public new void VerifyText(string text, string message = "The value in drop-down is not correct")
        {
            Log(LogLevel.Debug, $"Verifying text '{text}' of {ElementName}");
            Assert.AreEqual(text, Select().SelectedOption.Text.Trim(), "{0}: {1}", ElementName, message);
        }

        public void VerifyOptionsContains(IEnumerable<string> options,
            string message = "The values in drop-down list is not correct")
        {
            var selectElementOptions = Select().Options;
            foreach (var option in options)
            {
                var selectWithText = selectElementOptions.ToList().Where(x => x.Text.Equals(option));
                Assert.True(selectWithText.Single().Text.Equals(option), "{0}: {1}", ElementName, message);
            }
        }

        public bool IsOptionsContains(string option)
        {
            Log(LogLevel.Debug, $"Getting if options of {ElementName} contain {option}");
            var selectElementOptions = Select().Options;
            var selectWithText = selectElementOptions.ToList().Where(x => x.Text.Equals(option));
            return selectWithText.Any();
        }

        public void VerifyEmpty(string message = "WebSelect is not empty")
        {
            Log(LogLevel.Debug, $"Verifying empty {ElementName}");
            Assert.AreEqual(0, Select().Options.Count, "{0}: {1}", ElementName, message);
        }

        public void VerifyOptionsNotContains(string option)
        {
            Log(LogLevel.Debug, $"Verifying {ElementName} options not contain {option}");
            var optionsCount = Select().Options.Count;
            var isOptionsExist = false;
            var retry = true;
            for (var i = 0; i < optionsCount; i++)
                try
                {
                    if (Select().Options[i].Text.Equals(option)) isOptionsExist = true;
                }
                catch (Exception) when (retry)
                {
                    i = 0;
                    optionsCount = Select().Options.Count;
                    isOptionsExist = false;
                    retry = false;
                }

            Assert.False(isOptionsExist);
        }

        public void VerifyOptions(List<string> list, bool hasEmptyValue = true)
        {
            Log(LogLevel.Debug, $"Verifying {list.Count} options of {ElementName}");
            var optionsOptions = Select().Options;
            if (hasEmptyValue)
                //TODO possible StaleElementReference because optionsOptions is ListOf IWebElements
                optionsOptions = optionsOptions.Where(x => !string.IsNullOrEmpty(x.Text)).ToList();

            var actualList = optionsOptions.Select(x => x.Text);
            Assert.IsTrue(actualList.SequenceEqual(list),
                $"Expected: {string.Join("\n", list)}.\n Actual: {string.Join("\n", actualList)}");
        }

        public void VerifyNoneSelected()
        {
            Log(LogLevel.Debug, $"Verifying none selected of {ElementName}");
            Assert.AreEqual(0, Select().AllSelectedOptions.Count);
        }

        public void VerifyValueSelected(int value)
        {
            Log(LogLevel.Debug, $"Verifying value by integer {value} of {ElementName}");
            Assert.AreEqual(value, Convert.ToInt32(Select().SelectedOption.GetAttribute("value")));
        }

        public void SelectLast()
        {
            Log(LogLevel.Debug, $"Selectiong last of {ElementName}");
            SelectByIndex(GetOptionsCount() - 1);
        }

        public string GetSelectedItemText()
        {
            return Select().SelectedOption.Text;
        }
    }
}