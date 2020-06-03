using NUnit.Framework;
using OpenQA.Selenium;
using static UIAutomationFramework.Core.TestLogger;
using LogLevel = NLog.LogLevel;

namespace UIAutomationFramework.Controls
{
    public sealed class WebCheckBox : BaseWebUiElement
    {
        public WebCheckBox(string elementName, By locator, BaseWebControl container = null) : base(
            elementName, new Locator(locator), container)
        {
        }

        public WebCheckBox(string elementName, Locator locator, Frame frame) : base(elementName, locator, frame)
        {
        }

        public WebCheckBox(string elementName, By locator, Frame frame) : base(elementName, locator, frame)
        {
        }

        public void Set(bool isCheck)
        {
            Log(LogLevel.Debug, $"Setting: {isCheck} to {ElementName}");
            if (IsChecked() != isCheck) Click();
        }

        public void Check()
        {
            Set(true);
        }

        public void Uncheck()
        {
            Set(false);
        }

        public bool IsChecked()
        {
            return Execute(() =>
            {
                Log(LogLevel.Debug, $"Getting isChecked state of {ElementName}");
                return Get().Selected;
            });
        }

        public void VerifyChecked(string message = "The checkbox is not checked")
        {
            Log(LogLevel.Debug, $"Verifying checked {ElementName}");
            Assert.True(IsChecked(), "{0}: {1}", ElementName, message);
        }

        public void VerifyUnchecked(string message = "The checkbox is not unchecked")
        {
            Log(LogLevel.Debug, $"Verifying unchecked {ElementName}");
            Assert.False(IsChecked(), "{0}: {1}", ElementName, message);
        }

        public void Verify(bool expectedResult)
        {
            if (expectedResult)
                VerifyChecked();
            else
                VerifyUnchecked();
        }
    }
}