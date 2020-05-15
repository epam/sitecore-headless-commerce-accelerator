using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Core
{
    public class UiElement
    {
        public UiElement(string xpath)
        {
            Locator = By.XPath(xpath);
        }

        public By Locator { get; private set; }

        public string TagName => DriverContext.Driver.GetElement(Locator).TagName;

        public string Text => DriverContext.Driver.GetElement(Locator).Text.Trim();

        public bool Selected => DriverContext.Driver.GetElement(Locator).Selected;

        public bool Displayed
        {
            get
            {
                try
                {
                    return DriverContext.Driver.GetElement(Locator).Displayed;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public void Clear()
        {
            DriverContext.Driver.GetElementWithWait(Locator).Clear();
        }

        public void TypeText(string text)
        {
            DriverContext.Driver.GetElementWithWait(Locator).SendKeys(text);
        }

        public void Click(bool withScroll = false)
        {
            if (withScroll)
            {
                DriverContext.Driver.ScrollByJs(Locator);
            }
            DriverContext.Driver.GetElementWithWait(Locator).Click();
        }

        public void HoverOver()
        {
            new Actions(DriverContext.Driver).MoveToElement(DriverContext.Driver.GetElementWithWait(Locator)).Build().Perform();
        }

        public string GetAttribute(string attributeName)
        {
            return DriverContext.Driver.GetElement(Locator).GetAttribute(attributeName);
        }

        public string GetProperty(string propertyName)
        {
            return DriverContext.Driver.GetElement(Locator).GetProperty(propertyName);
        }

        public string GetCssValue(string propertyName)
        {
            return DriverContext.Driver.GetElement(Locator).GetCssValue(propertyName);
        }

        public void SeleckByValue(string value)
        {
            new SelectElement(DriverContext.Driver.FindElement(Locator)).SelectByValue(value);
        }

        public void SeleckByText(string text, bool withScroll = false)
        {
            if (withScroll)
            {
                DriverContext.Driver.ScrollByJs(Locator);
            }
            new SelectElement(DriverContext.Driver.FindElement(Locator)).SelectByText(text);
        }
    }
}
