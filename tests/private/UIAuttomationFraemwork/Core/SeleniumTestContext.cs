using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NLog;
using System.Threading.Tasks;

namespace UIAutomationFramework.Core
{
    public static class SeleniumTestContext
    {

        private static readonly ThreadLocal<BrowserType> CurrentBrowserType = new ThreadLocal<BrowserType>();
        private static readonly ThreadLocal<BrowserType> FirstBrowserType = new ThreadLocal<BrowserType>();
        private static readonly ThreadLocal<BrowserType> SecondBrowserType = new ThreadLocal<BrowserType>();
        private static readonly ThreadLocal<bool> ThreadExtendedWaiting = new ThreadLocal<bool>();
        public static readonly ThreadLocal<string> CurrentStep = new ThreadLocal<string>();

        private static readonly ThreadLocal<List<BrowserType>> ThreadLocalAcceptCookies
            = new ThreadLocal<List<BrowserType>>(() => new List<BrowserType>());

        public static void AcceptCookies()
        {
            ThreadLocalAcceptCookies.Value.Add(GetCurrentBrowserType());
        }

        public static bool AreCookiesAccepted()
        {
            return ThreadLocalAcceptCookies.Value.Contains(GetCurrentBrowserType());
        }

        public static void SetExtendedWaiting()
        {
            ThreadExtendedWaiting.Value = true;
        }

        public static bool IsExtendedWaiting()
        {
            return ThreadExtendedWaiting.Value;
        }



        public static BrowserType GetFirstBrowserType()
        {
            var firstBrowserType = FirstBrowserType.Value;
            TestLogger.Log(LogLevel.Debug, $"SeleniumTestContext: First browser type is: {firstBrowserType}");
            return firstBrowserType;
        }

        public static void SetFirstBrowserType(BrowserType type)
        {
            TestLogger.Log(LogLevel.Debug, $"SeleniumTestContext: Setting first browser type: {type}");
            FirstBrowserType.Value = type;
        }

        public static BrowserType GetSecondBrowserType()
        {
            if (!SecondBrowserType.IsValueCreated)
            {
                TestLogger.Log(LogLevel.Debug, $"SeleniumTestContext: Second browser type: {BrowserType.None}");
                SecondBrowserType.Value = BrowserType.None;
            }

            var secondBrowserType = SecondBrowserType.Value;
            TestLogger.Log(LogLevel.Debug, $"SeleniumTestContext: Second browser type: {secondBrowserType}");
            return secondBrowserType;
        }

        public static void SetSecondBrowserType(BrowserType type)
        {
            TestLogger.Log(LogLevel.Debug, $"SeleniumTestContext: Setting second browser type: {type}");
            SecondBrowserType.Value = type;
        }

        public static void SetCurrentStep(string stepInfo)
        {
            CurrentStep.Value = stepInfo;
        }

        public static void ResetEnvironment()
        {
            TestLogger.Common("SeleniumTestContext: Resetting environment");
            ThreadLocalAcceptCookies.Value.Clear();
            CurrentStep.Value = string.Empty;
            ThreadExtendedWaiting.Value = false;
        }

        public static BrowserType GetCurrentBrowserType()
        {
            return CurrentBrowserType.Value;
        }

        public static void SetCurrentBrowser(BrowserType type)
        {
            CurrentBrowserType.Value = type;
        }
    }

}
