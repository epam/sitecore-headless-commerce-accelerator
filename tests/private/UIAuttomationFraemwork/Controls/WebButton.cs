using NUnit.Framework;
using OpenQA.Selenium;
using System;
using UIAutomationFramework.Driver;
using static UIAutomationFramework.Core.TestLogger;
using LogLevel = NLog.LogLevel;

namespace UIAutomationFramework.Controls
{
    public class WebButton : BaseWebUiElement
    {
        public WebButton(string elementName, By locator, BaseWebControl container = null) : base(
            elementName, new Locator(locator), container)
        {
        }

        public WebButton(string elementName, Locator locator, BaseWebControl container = null) : base(elementName,
            locator, container)
        {
        }

        public WebButton(string elementName, Locator locator, Frame frame) : base(elementName, locator, frame)
        {
        }

        public WebButton(string elementName, By locator, Frame frame) : base(elementName, locator, frame)
        {
        }

        public void VerifyLabel(string label, string message = "Label text is incorrect")
        {
            Log(LogLevel.Debug, "Verifying label");
            Assert.AreEqual(label, GetButtonName(), $"{ElementName}: {message}");
        }

        public void WaitForClickable(double timeout = -1, bool throwIfNotFound = true)
        {
            Log(LogLevel.Debug, $"Waiting for present of {ElementName}");
            if (timeout == -1) timeout = Configuration.DefaultTimeout;

            try
            {
                DriverManager.GetWaiter(timeout).Until(d => IsClickable());
            }
            catch (Exception exception)
            {
                if (throwIfNotFound) throw new NoSuchElementException(ElementName + ": Timed out", exception);
            }
        }

        private string GetButtonName()
        {
            return Execute(() => Get().GetAttribute("Value"));
        }
    }
}