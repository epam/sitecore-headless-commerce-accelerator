using OpenQA.Selenium;
using UI.AutomationFramework.Controls;

namespace Ui.AutomationFramework.Controls
{
    public class WebElement : BaseWebUiElement
    {
        public WebElement(string elementName, By locator, BaseWebControl container = null) : base(elementName,
            new Locator(locator), container)
        {
        }

        protected WebElement(By locator, BaseWebControl container = null) : base("", new Locator(locator), container)
        {
        }

        public WebElement(string elementName, Locator locator) : base(elementName, locator)
        {
        }

        public WebElement(string elementName, Locator locator, BaseWebControl container = null) : base(elementName,
            locator, container)
        {
        }

        public WebElement(string elementName, By locator, Frame frame) : base(elementName, locator, frame)
        {
        }
    }
}