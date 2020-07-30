using OpenQA.Selenium;

namespace AutoTests.AutomationFramework.UI.Core
{
    public abstract class ByCustom : By
    {
        public static By IdContains(string id)
        {
            return CssSelector($"[id*='{id}']");
        }

        public static By IdEndsWith(string id)
        {
            return CssSelector($"[id$='{id}']");
        }

        public static By ClassEndsWith(string @class)
        {
            return CssSelector($"[_class$='{@class}']");
        }

        public static By ClassStartsWith(string @class)
        {
            return CssSelector($"[class^='{@class}']");
        }

        public static By IdContains(string id, string type)
        {
            return XPath($"//{type}[contains(@id, '{id}')]");
        }

        public static By IdContainsRelative(string id, string type = "*", string path = ".//")
        {
            return XPath($"{path}{type}[contains(@id, '{id}')]");
        }

        public new static By XPath(string xpathToFind)
        {
            return By.XPath(xpathToFind);
        }

        public new static By TagName(string tagNameToFind)
        {
            return By.TagName(tagNameToFind);
        }

        public new static By Id(string idToFind)
        {
            return By.Id(idToFind);
        }

        public new static By ClassName(string classNameToFind)
        {
            return By.ClassName(classNameToFind);
        }

        public new static By Name(string nameToFind)
        {
            return By.Name(nameToFind);
        }

        public new static By CssSelector(string cssSelectorToFind)
        {
            return By.CssSelector(cssSelectorToFind);
        }

        public new static By LinkText(string linkTextToFind)
        {
            return By.LinkText(linkTextToFind);
        }
    }
}