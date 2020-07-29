using OpenQA.Selenium;
using UI.AutomationFramework.Controls;

namespace Ui.AutomationFramework.Controls
{
    public class WebImage : BaseWebUiElement
    {
        public WebImage(string elementName, By locator, BaseWebControl container = null) : base(elementName,
            new Locator(locator), container)
        {
        }

        public string GetSource()
        {
            return GetAttribute("src");
        }
    }
}