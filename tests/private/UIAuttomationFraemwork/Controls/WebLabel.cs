using UIAutomationFramework.Driver;
using NUnit.Framework;
using OpenQA.Selenium;
using static UIAutomationFramework.Core.TestLogger;
using LogLevel = NLog.LogLevel;

namespace UIAutomationFramework.Controls
{
    public class WebLabel : BaseWebUiElement
    {
        public WebLabel(string elementName, By locator, BaseWebControl container = null) : base(elementName,
            new Locator(locator), container)
        {
        }

        public WebLabel(string elementName, Locator locator, Frame frame) : base(elementName, locator, frame)
        {
        }

        public WebLabel(string elementName, By locator, Frame frame) : base(elementName, locator, frame)
        {
        }

        public WebLabel(string elementName, Locator locator) : base(elementName, locator)
        {
        }

        public WebLabel(string elementName, Locator locator, BaseWebControl container) : base(elementName,
            locator, container)
        {
        }

        public new void VerifyText(string text, string message = "The value in label is incorrect")
        {
            Log(LogLevel.Debug, $"Verifying text {text} of {ElementName}");
            WaitForPresent();
            var fullMessage = GetText();
            Assert.AreEqual(text, fullMessage,
                ElementName + $": {message}. The full text: '{fullMessage}' doesn't match '{text}'");
        }

        public new void VerifyTextContains(string text, string message = "The text is not presented in the label")
        {
            Log(LogLevel.Debug, $"Verifying text contains {text} of {ElementName}");
            WaitForPresent();
            var fullMessage = GetText();
            Assert.IsTrue(fullMessage.Contains(text),
                ElementName + $": {message}. The full text: '{fullMessage}' doesn't contain '{text}'");
        }

        public void WaitForText(string text)
        {
            Log(LogLevel.Debug, "Waiting for text");
            DriverManager.GetWaiter().Until(x => GetText().Equals(text));
        }

        public void WaitForTextEndsWith(string text)
        {
            Log(LogLevel.Debug, "Waiting for text endsWith");
            DriverManager.GetWaiter().Until(x => GetText().EndsWith(text));
        }
    }
}