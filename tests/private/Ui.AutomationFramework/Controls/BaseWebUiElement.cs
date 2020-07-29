using System;
using System.Drawing;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using Ui.AutomationFramework.Controls;
using Ui.AutomationFramework.Core;
using Ui.AutomationFramework.Driver;
using Ui.AutomationFramework.Utils.Extensions;
using static Ui.AutomationFramework.Core.TestLogger;
using LogLevel = NLog.LogLevel;


namespace UI.AutomationFramework.Controls
{
    public abstract class BaseWebUiElement : BaseWebControl
    {
        internal BaseWebUiElement(string elementName, Locator locator, BaseWebControl container = null)
        {
            if (container != null)
            {
                var locatorString = locator.ToString();
                if (locatorString != null && locatorString.StartsWith("By.XPath: ") &&
                    !locatorString.StartsWith("By.XPath: ./"))
                    throw new Exception("Relative element must start from .");
            }

            ElementName = GetNameWithType(elementName);
            Locator = locator;
            Container = container;
        }

        internal BaseWebUiElement(string elementName, Locator locator, Frame frame)
        {
            ElementName = elementName;
            Locator = locator;
            Frame = frame;
        }

        internal BaseWebUiElement(string elementName, By locator, Frame frame)
        {
            ElementName = elementName;
            Locator = new Locator(locator);
            Frame = frame;
        }

        private string GetNameWithType(string elementName)
        {
            var nameWithType = $"{GetType().Name}";
            if (!string.IsNullOrEmpty(elementName)) nameWithType = nameWithType + $" '{elementName}'";

            return nameWithType;
        }

        /// <summary>
        ///     Submit element
        /// </summary>
        public void Submit()
        {
            Log(LogLevel.Debug, $"Submitting {ElementName}");
            Execute<object>(() =>
            {
                WaitForPresent();
                Get().Submit();
                return null;
            });
        }

        internal void SendKeys(string keysToSend)
        {
            Execute<object>(() =>
            {
                Log(LogLevel.Debug, $"Sending text: {keysToSend} to {ElementName}");
                Get().SendKeys(keysToSend);
                return null;
            });
        }

        public string GetAttribute(string attributeName)
        {
            return Execute(() =>
            {
                Log(LogLevel.Debug, $"Getting attribute: {attributeName} from {ElementName}");
                return Get().GetAttribute(attributeName);
            });
        }

        public string GetText()
        {
            Log(LogLevel.Debug, $"Getting text from {ElementName}");
            return Execute(() => Get().Text);
        }

        public string GetTrimmedText()
        {
            Log(LogLevel.Debug, $"Getting trimmed text from {ElementName}");
            return Execute(() => GetText().Trim());
        }

        public bool IsPresent()
        {
            Log(LogLevel.Debug, $"Getting IsPresent condition from {ElementName}");
            return Get(false) != null && IsVisible();
        }

        public bool IsNotPresent()
        {
            Log(LogLevel.Debug, $"Getting IsPresent condition from {ElementName}");
            return Get(false) != null && !IsVisible();
        }

        public void UnFocus()
        {
            Log(LogLevel.Debug, $"Unfocusing {ElementName}");
            DriverManager.JsExecutor.ExecuteScript(
                "var tmp=document.createElement('input');document.body.appendChild(tmp),tmp.focus(),document.body.removeChild(tmp);");
        }

        public bool IsClickable()
        {
            return Execute(() => Get().Displayed && Get().Enabled);
        }

        public void VerifyText(string text, string message = "The text of element is not as expected")
        {
            Log(LogLevel.Debug, $"Verifying text, {ElementName}");
            Assert.AreEqual(text, GetText().Trim(), "{0}: {1}", ElementName, message);
        }

        public WebElement GetParent(int depth = 1)
        {
            Log(LogLevel.Debug, $"Getting parent from {ElementName} with depth {depth}");
            return new WebElement($"Parent of '{ElementName}'", ByCustom.XPath("ancestor::*[" + depth + "]"), this);
        }

        public void VerifyClickable(string message = "The element is not click able")
        {
            Log(LogLevel.Debug, $"Verifying clickable, {ElementName}");
            Assert.IsTrue(IsClickable(), message);
        }

        public void VerifyNotClickable(string message = "The element is click able")
        {
            Log(LogLevel.Debug, $"Verifying not clickable, {ElementName}");
            Assert.IsTrue(!IsClickable(), message);
        }

        public void VerifyTextIgnoreCase(string text, string message = "The text of element is not as expected")
        {
            Log(LogLevel.Debug, $"Verifying text ignoring case, {ElementName}");
            Assert.AreEqual(text.ToLower(), GetText().Trim().ToLower(), "{0}: {1}", ElementName, message);
        }

        public void VerifyTextContainsIgnoreCase(string value,
            string message = "The text of element does not contains expected text.")
        {
            Log(LogLevel.Debug, $"Verifying text contains ignoring case, {ElementName}");
            var actualText = GetTrimmedText();
            Assert.True(actualText.ToLower().Contains(value.ToLower()), "{0}: {1}", ElementName,
                message + $"Actual text is: {actualText} Expected text: {value}");
        }

        public void VerifyTextContains(string value,
            string message = "The text of element does not contains expected text.")
        {
            Log(LogLevel.Debug, $"Verifying text contains {ElementName}");
            var actualText = GetTrimmedText();
            Assert.True(actualText.Contains(value), "{0}: {1}", ElementName,
                message + $"Actual text is: {actualText} Expected text: {value}");
        }

        public void MouseOver()
        {
            Log(LogLevel.Debug, $"MouseOvering {ElementName}");
            var action = new Actions(DriverManager.GetDriver());
            action.MoveToElement(Get()).Perform();
        }

        public void Click(Point point)
        {
            Log(LogLevel.Debug, $"MouseOvering {ElementName} to coordinates: {point.X}x{point.Y} and clicking");
            var action = new Actions(DriverManager.GetDriver());
            action.MoveToElement(Get(), point.X, point.Y).Click().Perform();
        }

        public void JsMouseOver()
        {
            const string javaScript =
                "if(document.createEvent){var evObj = document.createEvent(\'MouseEvents\');evObj.initEvent(\'mouseover\',true, false); arguments[0].dispatchEvent(evObj);} else if(document.createEventObject) { arguments[0].fireEvent(\'onmouseover\');}";
            DriverManager.GetDriver().ExecuteJavaScript(javaScript, Get());
        }

        public void JsClick()
        {
            Log(LogLevel.Debug, $"JsClicking {ElementName}");
            Execute<object>(() =>
                {
                    DriverManager.GetDriver().ExecuteJavaScript("arguments[0].click()", Get());
                    return null;
                }
            );
        }

        public void JsDisplayElement()
        {
            Log(LogLevel.Debug, $"JsDisplaying {ElementName}");
            DriverManager.GetDriver().ExecuteJavaScript("arguments[0].style.display = 'block'", Get());
            WaitForPresent();
        }

        public void WaitForCssTransition()
        {
            Log(LogLevel.Debug, "Waiting for CSS transition of " + ElementName);
            var duration = double.Parse(Get().GetCssValue("transition-duration").Replace("s", ""));
            var delay = double.Parse(Get().GetCssValue("transition-delay").Replace("s", ""));

            Thread.Sleep((int) ((duration + delay) * 1000) + 1000);
        }

        public void WaitForAttribute(string attribute, string expectedAttribute)
        {
            Log(LogLevel.Debug, $"Waiting for attribute {attribute} expected: {expectedAttribute} in {ElementName}");
            DriverManager.GetWaiter().Until(d => Get().GetAttribute(attribute).Equals(expectedAttribute),
                $"Timeout. Attribute '{attribute}' of element {ElementName} still doesn't contain value '{expectedAttribute}'");
        }

        public void VerifyLinkTextBold(string message = "Link text is not BOLD")
        {
            Log(LogLevel.Debug, "Verifying label");
            var font = GetCssValue("font-weight");
            var isBold = font == "bold" || font == "bolder" || short.Parse(font) >= 600;
            Assert.True(isBold, $"{ElementName}: {message}. font-weight is: {font}");
        }

        public string GetCssValue(string propertyType)
        {
            return Execute(() => Get().GetCssValue(propertyType));
        }
    }
}