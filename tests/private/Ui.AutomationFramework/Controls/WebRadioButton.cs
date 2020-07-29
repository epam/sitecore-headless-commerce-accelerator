using OpenQA.Selenium;
using UI.AutomationFramework.Controls;
using static Ui.AutomationFramework.Core.TestLogger;
using LogLevel = NLog.LogLevel;

namespace Ui.AutomationFramework.Controls
{
    public class WebRadioButton : BaseWebUiElement
    {
        public WebRadioButton(string elementName, By locator, BaseWebControl container = null) : base(elementName,
            new Locator(locator), container)
        {
        }

        public WebRadioButton(string elementName, Locator locator, BaseWebControl container = null) : base(elementName,
            locator, container)
        {
        }

        public WebRadioButton(string elementName, Locator locator, Frame frame) : base(elementName, locator, frame)
        {
        }

        public WebRadioButton(string elementName, By locator, Frame frame) : base(elementName, locator, frame)
        {
        }

        public void Check()
        {
            Log(LogLevel.Debug, $"Checking {ElementName}");
            Click();
        }
    }
}