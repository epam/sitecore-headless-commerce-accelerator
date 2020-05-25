using NUnit.Framework;
using OpenQA.Selenium;
using static UIAutomationFramework.Core.TestLogger;
using LogLevel = NLog.LogLevel;

namespace UIAutomationFramework.Controls
{
    public class WebLink : BaseWebUiElement
    {
        public WebLink(string elementName, By locator, BaseWebControl container = null) : base(elementName,
            new Locator(locator), container)
        {
        }

        public WebLink(string elementName, By locator, Frame frame) : base(elementName, locator, frame)
        {
        }

        public WebLink(string elementName, Locator locator, BaseWebControl container = null) : base(elementName,
            locator, container)
        {
        }

        public void VerifyUrl(string url, string message = "Url of the link is incorrect")
        {
            Log(LogLevel.Debug, $"Verifying URL {ElementName}");
            Assert.AreEqual(url, GetAttribute("href"), "{0}: {1}", ElementName, message);
        }
    }
}