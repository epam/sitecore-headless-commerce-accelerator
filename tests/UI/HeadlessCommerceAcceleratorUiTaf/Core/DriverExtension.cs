using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Core
{
    public static class DriverExtension
    {
        public static WebDriverWait GetDriverWait(this IWebDriver driver, int waitTime = -1)
        {
            waitTime = waitTime == -1 ? 40 : waitTime;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));
            wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException), typeof(NoSuchElementException), typeof(StaleElementReferenceException), typeof(ElementNotInteractableException));
            return wait;
        }

        public static void WaitUntilElementAppears(this IWebDriver driver, By by, int waitTime = -1)
        {
            GetDriverWait(driver, waitTime).Until(d => d.FindElement(by).Displayed);
        }

        public static void WaitUntilElementDisappears(this IWebDriver driver, By by, int waitTime = -1)
        {
            GetDriverWait(driver, waitTime).Until(d => !d.FindElement(by).Displayed);
        }

        public static IWebElement GetElementWithWait(this IWebDriver driver, By by)
        {
            WaitUntilElementAppears(driver, by);
            return GetElement(driver, by);
        }

        public static IWebElement GetElement(this IWebDriver driver, By by)
        {
            return driver.FindElement(by);
        }

        public static void ScrollByJs(this IWebDriver driver, By by)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView();", GetElement(driver, by));
        }
    }
}
