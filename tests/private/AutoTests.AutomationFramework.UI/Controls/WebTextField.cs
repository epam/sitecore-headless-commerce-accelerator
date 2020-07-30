using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using AutoTests.AutomationFramework.UI.Extensions;
using AutoTests.AutomationFramework.UI.Driver;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using static AutoTests.AutomationFramework.UI.Core.TestLogger;
using Keys = OpenQA.Selenium.Keys;
using LogLevel = NLog.LogLevel;

namespace AutoTests.AutomationFramework.UI.Controls
{
    public class WebTextField : BaseWebUiElement
    {
        public WebTextField(string elementName, By locator, BaseWebControl container = null) : base(
            elementName, new Locator(locator), container)
        {
        }

        public WebTextField(string elementName, Locator locator, Frame frame) : base(elementName, locator, frame)
        {
        }

        public WebTextField(string elementName, By locator, Frame frame) : base(elementName, locator, frame)
        {
        }

        public void Type(string keys)
        {
            Log(LogLevel.Debug, $"Filling {ElementName} with {keys}");
            if (keys is null) return;

            DriverManager.GetWaiter().Until(
                x =>
                {
                    var webElement = Get(false);
                    Log(LogLevel.Debug, $"Waiting {ElementName} to be clickable");
                    return ExpectedConditions.ElementToBeClickable(webElement);
                });
            Clear();
            if (!string.IsNullOrEmpty(keys))
            {
                Click();
                SendKeys(keys);
            }
        }

        public void Type(long keys)
        {
            Type(keys.ToString());
        }

        public void Type(decimal keys)
        {
            Type(keys.ToString(CultureInfo.InvariantCulture));
        }

        public void Clear()
        {
            Execute<object>(() =>
            {
                Log(LogLevel.Debug, "Clearing " + ElementName);
                WaitForPresent();
                DriverManager.GetWaiter().Until(ExpectedConditions.ElementToBeClickable(Get()),
                    $"Timeout. Element {ElementName} is not clickable");
                Get().Clear();
                Thread.Sleep(90);
                return null;
            });
        }

        public new string GetText()
        {
            Log(LogLevel.Debug, "Getting value from " + ElementName);
            return GetAttribute("value");
        }

        public new string GetTrimmedText()
        {
            Log(LogLevel.Debug, $"Getting trimmed text from {ElementName}");
            return Execute(() => GetText().Trim());
        }

        public void WaitForTextChangeAfterAction(Action action)
        {
            WaitForAttributeChange("value", action);
        }

        public new void VerifyText(string text, string message = "The value in Text field is incorrect")
        {
            Log(LogLevel.Debug, $"Verifying text of {ElementName}");
            Assert.AreEqual(text, GetText(), "{0}: {1}", ElementName, message);
        }

        public void SetTextViaClipboard(string text)
        {
            Log(LogLevel.Debug, $"Setting text via clipboard: {text}");
            var clipboardText = Clipboard.GetText();

            SetClipboard(text);

            Clear();
            Execute<object>(() =>
            {
                SendKeys(Keys.Control + "v");
                return null;
            });

            if (clipboardText != "")
                SetClipboard(clipboardText);
            else
                ClearClipboard();
        }

        private static void SetClipboard(string text)
        {
            Log(LogLevel.Debug, "Setting text to clipboard");
            var thread = new Thread(() => Clipboard.SetText(text));
            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
            thread.Start();
            thread.Join();
        }

        private static void ClearClipboard()
        {
            Log(LogLevel.Debug, "Setting text to clipboard");
            var thread = new Thread(Clipboard.Clear);
            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
            thread.Start();
            thread.Join();
        }

        public void VerifyPlaceholderText(string text)
        {
            Assert.AreEqual(text, Get().GetAttribute("placeholder"));
        }
    }
}