using System;
using UIAutomationFramework.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using LogLevel = NLog.LogLevel;

namespace UIAutomationFramework.Utils.Extensions
{
    public static class WebDriverWaitExtensions
    {
        public static void UntilNoException<TResult>(this WebDriverWait waiter, Func<IWebDriver, TResult> condition)
        {
            try
            {
                waiter.Until(condition);
            }
            catch (WebDriverTimeoutException exception)
            {
                TestLogger.Log(LogLevel.Debug, "WebDriverTimeoutException was ignored during waiting!", exception);
            }
            catch (Exception exception)
            {
                TestLogger.Log(LogLevel.Debug, "Exception was ignored during waiting!", exception);
            }
        }

        public static void Until<TResult>(this WebDriverWait waiter, Func<IWebDriver, TResult> condition,
            string errorMessage)
        {
            try
            {
                waiter.Until(condition);
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException(errorMessage);
            }
        }
    }
}