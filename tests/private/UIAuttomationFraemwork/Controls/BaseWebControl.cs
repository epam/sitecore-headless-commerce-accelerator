using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using UIAutomationFramework.Core;
using UIAutomationFramework.Driver;
using UIAutomationFramework.Exceptions;
using UIAutomationFramework.Utils.Extensions;
using static UIAutomationFramework.Core.TestLogger;
using LogLevel = NLog.LogLevel;

namespace UIAutomationFramework.Controls
{
    public abstract class BaseWebControl
    {
        private string _elementName;
        protected BaseWebControl Container;
        public Frame Frame;
        protected Locator Locator;

        public string ElementName
        {
            get => FullElementName;
            protected set => _elementName = value;
        }

        private string FullElementName
        {
            get
            {
                if (Container != null) return _elementName + " of " + Container._elementName;

                return _elementName;
            }
        }

        public int GetChildElementsCount(By locator)
        {
            Log(LogLevel.Debug, $"Getting child count of {ElementName} by locator {locator}");
            int childElementsCount;
            try
            {
                childElementsCount = Get().FindElements(locator).Count;
            }
            catch (StaleElementReferenceException exception)
            {
                Log(LogLevel.Debug, "cannot get child elements count", exception);
                childElementsCount = GetChildElementsCount(locator);
            }

            return childElementsCount;
        }

        public IWebElement Get(bool waitForElement = true)
        {
            if (waitForElement)
            {
                IWebElement element = null;
                DriverManager.GetWaiter().Until(d =>
                {
                    var searchContext = GetContainer(true);
                    var locator = Locator.GetLocator();
                    element = searchContext.FindElement(locator);
                    return element;
                }, $"Timeout. Element {ElementName} is not found on the page");
                if (element != null) return element;
            }

            try
            {
                return GetContainer(false).FindElement(Locator.GetLocator());
            }
            catch (UnhandledAlertException e)
            {
                Log(LogLevel.Debug, "Unexpected alert is shown. Alert text is:" + Alert.GetText(), e);
                throw;
            }
            catch (LocatorNotFoundException e)
            {
                Log(LogLevel.Debug, $"Please provide selector for {_elementName} for this environment", e);
                throw;
            }
            catch (Exception e)
            {
                Log(LogLevel.Debug, "Cannot find element " + ElementName, e);
                return null;
            }
        }


        private ISearchContext GetContainer(bool waitForContainer)
        {
            if (Container != null) return Container.Get(waitForContainer);

            FrameManager.SwitchFrame(Frame, waitForContainer);

            return DriverManager.GetDriver();
        }

        public bool IsVisible(bool retry = true)
        {
            return Status(() => Get(false).Displayed);
        }

        public bool IsEnabled()
        {
            return Status(() => Get(false).Enabled);
        }

        public void VerifyPresent(string message = "The element is not on the page", int timeout = -1,
            bool expectedValue = true)
        {
            if (timeout == -1) timeout = Configuration.DefaultTimeout;

            WaitForPresent(timeout, false);

            Assert.AreEqual(expectedValue, IsVisible(), "{0}: {1}", ElementName, message);
        }

        public void VerifyEnabled(string message = "Element is disabled")
        {
            Assert.IsTrue(IsEnabled(), "{0}: {1}", ElementName, message);
        }

        public void VerifyDisabled(string message = "Element is enabled")
        {
            Assert.IsFalse(IsEnabled(), "{0}: {1}", ElementName, message);
        }

        public void DoubleClick()
        {
            var action = new Actions(DriverManager.GetDriver());
            action.DoubleClick(Get()).Perform();
        }

        public void Click()
        {
            Execute<object>(() =>
            {
                Log(LogLevel.Debug, $"Clicking {ElementName}");
                WaitForPresent();
                DriverManager.GetWaiter().Until(ExpectedConditions.ElementToBeClickable(Get()),
                    $"Timeout. Element {ElementName} is not clickable");
                Get().Click();
                Thread.Sleep(90);
                return null;
            });
        }

        protected T Execute<T>(Func<T> action, bool isRetry = true, int scrollCount = 0)
        {
            try
            {
                return action();
            }
            catch (StaleElementReferenceException e) when (isRetry)
            {
                Log(LogLevel.Debug, "Staled. Retry;", e);
                Thread.Sleep(500);
                return Execute(action, false);
            }
            catch (WebDriverException e) when ((e.Message.Contains("Other element would receive the click") ||
                                                e.Message.Contains("obscures it")) &&
                                               isRetry)
            {
                if (Frame != null || Container?.Frame != null)
                    if (DriverManager.JsExecutor.ExecuteScript("return document.activeElement.tagName;").ToString()
                        .ToLower() != "body")
                    {
                        DriverManager.GetDriver().SwitchTo().ActiveElement().SendKeys(Keys.Tab);
                        DriverManager.GetDriver().SwitchTo().ActiveElement().SendKeys(Keys.PageDown);
                    }

                Browser.Scroll(100);
                scrollCount++;
                var retryScroll = scrollCount < 3;

                Log(LogLevel.Debug, "Covered by scroll to top. Retry;", e);
                Thread.Sleep(500);
                return Execute(action, retryScroll, scrollCount);
            }
            catch (ElementClickInterceptedException e) when (isRetry)
            {
                Log(LogLevel.Debug, "Covered by something. Retry;", e);
                Thread.Sleep(500);
                return Execute(action, false);
            }
            catch (ElementNotInteractableException e) when (isRetry)
            {
                Log(LogLevel.Debug, "Not Interactable. Retry;", e);
                Thread.Sleep(500);
                return Execute(action, false);
            }
        }

        private void WaitForChange(int initHashCode)
        {
            Log(LogLevel.Debug, $"Waiting for change of {ElementName}");
            DriverManager.GetWaiter().Until(d => GetHashCode() != initHashCode,
                $"Timeout. Element '{ElementName}' was not changed.");
        }

        public void WaitForDisabled()
        {
            Log(LogLevel.Debug, $"Waiting for disabled {ElementName}");
            DriverManager.GetWaiter().Until(d => !IsEnabled(), $"Timeout. Element '{ElementName}' was not disabled.");
        }

        internal void WaitForAttributeChange(string attributeName, Action action)
        {
            Log(LogLevel.Debug, $"Waiting for attribute change of {ElementName}. Attribute name: {attributeName}");
            var val = Get().GetAttribute(attributeName);
            action();
            DriverManager.GetWaiter().Until(d => !Get().GetAttribute(attributeName).Equals(val),
                $"Timeout. Attribute '{attributeName}' of element '{ElementName}' was not changed after action.");
        }

        internal void WaitForAttributeNotContain(string attributeName, string textNotToContain)
        {
            Log(LogLevel.Debug,
                $"Waiting for attribute not contain {textNotToContain}. Attribute name: {attributeName} of {ElementName}.");
            DriverManager.GetWaiter().Until(d => !Get().GetAttribute(attributeName).Contains(textNotToContain),
                $"Timeout. Attribute '{attributeName}' of element '{ElementName}' still contains '{textNotToContain}'.");
        }

        internal void WaitForAttributeContain(string attributeName, string textToContain)
        {
            Log(LogLevel.Debug,
                $"Waiting for attribute contain {textToContain}. Attribute name: {attributeName} of {ElementName}.");
            DriverManager.GetWaiter().Until(d => Get().GetAttribute(attributeName).Contains(textToContain),
                $"Timeout. Attribute '{attributeName}' of element '{ElementName}' doesn't contain '{textToContain}'. Original value: '{Get().GetAttribute(attributeName)}'");
        }

        public void WaitForChangeAfterAction(Action action)
        {
            Log(LogLevel.Debug, $"Waiting for change after action {action.Method} of element {ElementName}");
            var hashcode = GetHashCode();
            action();
            WaitForChange(hashcode);
        }

        public void WaitForPresent(double timeout = -1, bool throwIfNotFound = true)
        {
            Log(LogLevel.Debug, $"Waiting for present of {ElementName}");
            if (timeout == -1) timeout = Configuration.DefaultTimeout;

            try
            {
                DriverManager.GetWaiter(timeout).Until(d => IsVisible());
            }
            catch (Exception exception)
            {
                if (throwIfNotFound) throw new NoSuchElementException(ElementName + ": Timed out", exception);
            }
        }

        public void WaitForNotPresent(int timeout = -1)
        {
            Log(LogLevel.Debug, $"Waiting for not present of {ElementName}");
            if (timeout == -1) timeout = Configuration.DefaultTimeout;

            DriverManager.GetWaiter(timeout)
                .Until(d => !IsVisible(), $"Timeout. Element {ElementName} is still on the page");
        }

        public void VerifyNotPresent(string message = "The element is on the page")
        {
            Log(LogLevel.Debug, $"Verifying not present {ElementName}");
            Assert.IsFalse(IsVisible(), "{0}: {1}", ElementName, message);
        }

        public IEnumerable<IWebElement> WaitForChildElements(By locator, int n = 1)
        {
            Log(LogLevel.Debug, $"Waiting for {n} child elements of {ElementName}");
            var wait = DriverManager.GetWaiter();
            wait.Until(d => Get().FindElements(locator).Count > n - 1,
                $"Timeout. Element {ElementName} doesn't contain {n} child element(s) with locator {locator}");
            return Get().FindElements(locator);
        }

        public IEnumerable<IWebElement> GetChildElements(By locator)
        {
            Log(LogLevel.Debug, $"Getting child elements of {ElementName}");
            return Get().FindElements(locator);
        }

        private static bool Status(Func<bool> status)
        {
            try
            {
                return status();
            }
            catch (NullReferenceException)
            {
                return false;
            }
            catch (StaleElementReferenceException)
            {
                try
                {
                    return status();
                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }
        }

        public override int GetHashCode()
        {
            var code = -1;
            var webElement = Get(false);
            if (webElement != null) code = webElement.GetHashCode();

            return code;
        }
    }
}