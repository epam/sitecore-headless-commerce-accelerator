using NUnit.Framework;
using OpenQA.Selenium;
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

        private string GetButtonName()
        {
            return Execute(() => Get().GetAttribute("Value"));
        }
    }
}