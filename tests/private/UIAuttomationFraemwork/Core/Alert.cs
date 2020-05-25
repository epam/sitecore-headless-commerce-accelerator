
using UIAutomationFramework.Driver;
using UIAutomationFramework.Utils.Extensions;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;
using OpenQA.Selenium;
using LogLevel = NLog.LogLevel;

namespace UIAutomationFramework.Core
{
    public static class Alert
    {
        public static bool IsPresent()
        {
            try
            {
                DriverManager.GetDriver().SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private static void WaitForAlert()
        {
            TestLogger.Log(LogLevel.Debug, "Waiting for alert");
            DriverManager.GetWaiter().Until(ExpectedConditions.AlertIsPresent(), "Alert is not present");
        }

        public static void Accept()
        {
            TestLogger.Log(LogLevel.Debug, $"Accepting alert with text {GetText()}");
            DriverManager.GetDriver().SwitchTo().Alert().Accept();
        }

        public static void VerifyText(string expectedValue, string message = "Alert text is incorrect")
        {
            TestLogger.Log(LogLevel.Debug, $"Verifying text of alert. Expected {expectedValue}");
            Assert.AreEqual(expectedValue, GetText().Trim(), message);
        }

        public static string GetText()
        {
            WaitForAlert();
            TestLogger.Log(LogLevel.Debug, "Getting text from alert");
            return IsPresent() ? DriverManager.GetDriver().SwitchTo().Alert().Text : null;
        }

        public static void Cancel()
        {
            TestLogger.Log(LogLevel.Debug, $"Declining alert with text {GetText()}");
            DriverManager.GetDriver().SwitchTo().Alert().Dismiss();
        }
    }
}
