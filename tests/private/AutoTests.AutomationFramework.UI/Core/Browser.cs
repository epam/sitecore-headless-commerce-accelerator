using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.AutomationFramework.UI.Extensions;
using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.AutomationFramework.UI.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;
using Cookie = OpenQA.Selenium.Cookie;
using LogLevel = NLog.LogLevel;

namespace AutoTests.AutomationFramework.UI.Core
{
    public static class Browser
    {
        internal static void Navigate(Uri url)
        {
            TestLogger.Log(LogLevel.Debug, $"Navigating by URL to {url}");
            DriverManager.GetDriver().Navigate().GoToUrl(Uri.UnescapeDataString(url.ToString()));
        }

        public static void GoBack()
        {
            TestLogger.Log(LogLevel.Debug, "Navigating back");
            DriverManager.GetDriver().Navigate().Back();
        }

        public static Uri GetCurrentUrl()
        {
            var currentUrl = DriverManager.GetDriver().Url;
            TestLogger.Log(LogLevel.Debug, $"Getting current URL: {currentUrl}");
            return new Uri(currentUrl);
        }

        public static void Maximize()
        {
            TestLogger.Log(LogLevel.Debug, "Maximizing driver window");
            DriverManager.GetDriver().Manage().Window.Maximize();
        }


        /// <summary>
        ///     Opens second browser that should be different from the initial browser of the test.
        ///     To switch from one browser to another user method SwitchTo...
        /// </summary>
        /// <param name="browserType"></param>
        /// <param name="url"></param>
        public static void OpenSecondBrowser(BrowserType browserType, Uri url)
        {
            TestLogger.Log(LogLevel.Debug, $"Opening second browser with type: {browserType} and url:{url}");
            Assert.AreNotEqual(BrowserType.None, browserType);
            SeleniumTestContext.SetSecondBrowserType(browserType);
            DriverManager.InitDriver(browserType);
            Maximize();
            Navigate(url);
        }

        public static void OpenSecondBrowser()
        {
            OpenSecondBrowser(new Uri("about:blank"));
        }

        /// <summary>
        ///     Open browser that is differs from opened one (priority: Chrome, FF)
        ///     Switches directly to opened browser
        /// </summary>
        public static void OpenSecondBrowser(Uri url)
        {
            TestLogger.Log(LogLevel.Debug, "Opening Second Browser");
            BrowserType browserType;
            switch (SeleniumTestContext.GetFirstBrowserType())
            {
                case BrowserType.Chrome:
                case BrowserType.ChromeMobile:
                    browserType = BrowserType.Firefox;
                    break;
                case BrowserType.Firefox:
                case BrowserType.Edge:
                case BrowserType.InternetExplorer:
                case BrowserType.None:
                case BrowserType.Opera:
                    browserType = BrowserType.Chrome;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            OpenSecondBrowser(browserType, url);
        }

        public static void SwitchToSecondBrowser()
        {
            TestLogger.Log(LogLevel.Debug, "Switching to Second Browser");
            Assert.AreNotEqual(BrowserType.None, SeleniumTestContext.GetSecondBrowserType(),
                "Before switching browser you need to Open second browser!");
            DriverManager.InitDriver(SeleniumTestContext.GetSecondBrowserType());
        }

        public static void SwitchToFirstBrowser()
        {
            DriverManager.GetDriver().Manage().Window.Minimize();
            DriverManager.InitDriver(SeleniumTestContext.GetFirstBrowserType());
        }

        public static void CloseSecondBrowser()
        {
            TestLogger.Log(LogLevel.Debug, "Closing secondary browser...");
            DriverManager.CloseDriver(SeleniumTestContext.GetSecondBrowserType());
            Thread.Sleep(1000);
            DriverManager.InitDriver(SeleniumTestContext.GetFirstBrowserType());
            SeleniumTestContext.SetSecondBrowserType(BrowserType.None);
        }

        public static void CloseFirstBrowser()
        {
            throw new NotImplementedException(
                "check if second browser exist - switch second to first and erase the second one");
        }

        public static void WaitForPageToLoad()
        {
            TestLogger.Log(LogLevel.Debug, "Waiting for page loading...");
            DriverManager.GetWaiter().Until(driver =>
                    ((IJavaScriptExecutor) driver).ExecuteScript("return document.readyState;").Equals("complete"),
                "Timeout. Page is not loaded completely");
        }

        public static void SwitchToChildWindow(int index = 1)
        {
            TestLogger.Log(LogLevel.Debug, "Switching to child window with index " + index);
            SwitchToWindowByIndex(index);
        }

        public static void WaitForUrlContains(string text)
        {
            TestLogger.Log(LogLevel.Debug, $"Waiting for URL contains {text}");
            DriverManager.GetWaiter()
                .Until(
                    d => Uri.UnescapeDataString(GetCurrentUrl().ToString().ToLower())
                        .Contains(Uri.UnescapeDataString(text).ToLower()),
                    $"Url '{GetCurrentUrl()}' doesn't contain '{text}'");
        }

        public static void WaitAndRefreshForUrlContains(Uri url, string text, int timeout)
        {
            DriverManager.GetWaiter(timeout, TimeSpan.FromSeconds(2)).Until(x =>
            {
                if (Uri.UnescapeDataString(GetCurrentUrl().ToString()).Contains(Uri.UnescapeDataString(text)))
                    return true;

                Navigate(url);
                return false;
            });
        }

        public static void WaitForUrlEndsWith(string text)
        {
            TestLogger.Log(LogLevel.Debug, $"Waiting for URL ends with {text}");
            DriverManager.GetWaiter()
                .UntilNoException(d => Uri.UnescapeDataString(GetCurrentUrl().ToString()).EndsWith(text));
            Assert.IsTrue(Uri.UnescapeDataString(GetCurrentUrl().ToString()).EndsWith(text),
                $"Url '{GetCurrentUrl()}' doesn't end with '{text}'");
        }

        public static void WaitForUrl(Uri url)
        {
            TestLogger.Log(LogLevel.Debug, $"Waiting for URL {url}");
            DriverManager.GetWaiter()
                .UntilNoException(d =>
                    Uri.UnescapeDataString(GetCurrentUrl().ToString()).Equals(Uri.UnescapeDataString(url.ToString())));
            Assert.AreEqual(Uri.UnescapeDataString(url.ToString()), GetCurrentUrl().ToString());
        }

        public static void WaitForUrlIgnoreParams(string url)
        {
            TestLogger.Log(LogLevel.Debug, $"Waiting for URL ignore params {url}");
            DriverManager.GetWaiter(5).UntilNoException(d =>
                GetCurrentUrl().ToString().Split('?')[0].Equals(Uri.UnescapeDataString(url)));
            Assert.AreEqual(Uri.UnescapeDataString(url), GetCurrentUrl().ToString().Split('?')[0]);
        }

        public static void WaitForUrlIgnoreParamsIgnoreCase(string url)
        {
            TestLogger.Log(LogLevel.Debug, $"Waiting for URL ignore params and case {url}");
            DriverManager.GetWaiter(5).UntilNoException(d =>
                GetCurrentUrl().ToString().Split('?')[0].Equals(Uri.UnescapeDataString(url)));
            StringAssert.AreEqualIgnoringCase(Uri.UnescapeDataString(url), GetCurrentUrl().ToString().Split('?')[0]);
        }

        public static void SwitchToMainWindow()
        {
            TestLogger.Log(LogLevel.Debug, "Switching to main window");
            SwitchToWindowByIndex(0);
        }

        private static void SwitchToWindowByIndex(int index)
        {
            TestLogger.Log(LogLevel.Debug, "Switching window by index: " + index);
            DriverManager.GetWaiter().Until(d => d.WindowHandles.Count > index,
                $"Timeout. There is no window with index {index}");
            DriverManager.GetDriver().SwitchTo()
                .Window(DriverManager.GetDriver().WindowHandles[index]);
            Thread.Sleep(2000);
        }

        public static void Refresh()
        {
            TestLogger.Log(LogLevel.Debug, "Refreshing page");
            DriverManager.GetDriver().Navigate().Refresh();
        }

        public static int GetHistorySize()
        {
            return int.Parse(DriverManager.JsExecutor.ExecuteScript("return window.history.length").ToString());
        }

        internal static void Navigate(IPage page, string postfix = "")
        {
            Navigate(page.GetUrl().AddPostfix(postfix));
        }

        internal static void NavigateToPath(IPage page, string postfix = "")
        {
            var uri = new UriBuilder(GetCurrentUrl()) {Path = page.GetPath()}.Uri;
            Navigate(uri.AddPostfix(postfix));
        }

        internal static void NavigateToPath(Uri uri, string postfix)
        {
            Navigate(uri.AddPostfix(postfix));
        }

        public static void WaitForUrlNotEquals(Uri url)
        {
            TestLogger.Log(LogLevel.Debug, $"Waiting for URL not equals to {url}");
            DriverManager.GetWaiter()
                .UntilNoException(d =>
                    !Uri.UnescapeDataString(GetCurrentUrl().ToString()).Equals(Uri.UnescapeDataString(url.ToString())));
            Assert.AreNotEqual(Uri.UnescapeDataString(url.ToString()), GetCurrentUrl().ToString());
        }

        public static void Close()
        {
            TestLogger.Log(LogLevel.Debug, "Closing driver");
            DriverManager.CloseDriver(SeleniumTestContext.GetCurrentBrowserType());
        }

        public static void CloseTab()
        {
            TestLogger.Log(LogLevel.Debug, "Closing Current tab");
            if (DriverManager.GetDriver().WindowHandles.Count == 1)
            {
                TestLogger.Log(LogLevel.Debug, "There is only one tab opened. Closing Browser");
                Close();
            }

            else
            {
                DriverManager.GetDriver().Close();
                SwitchToMainWindow();
            }
        }

        public static void WaitForUrlStartsWith(string url)
        {
            TestLogger.Log(LogLevel.Debug, $"Waiting for URL starts with {url}");
            DriverManager.GetWaiter().UntilNoException(d =>
                Uri.UnescapeDataString(GetCurrentUrl().ToString()).Contains(Uri.UnescapeDataString(url)));
            Assert.IsTrue(Uri.UnescapeDataString(GetCurrentUrl().ToString()).StartsWith(url),
                $"Url '{GetCurrentUrl()}' doesn't starts with '{url}'");
        }

        public static ICookieJar GetPlanetwinCookies()
        {
            TestLogger.Log(LogLevel.Debug, "Getting PlanetWin cookies");
            var cookieJar = DriverManager.GetDriver().Manage().Cookies;
            return cookieJar;
        }

        public static void SetPlanetwinCookies(List<Cookie> planetwinCookies)
        {
            TestLogger.Log(LogLevel.Debug, "Setting PlanetWin cookies");
            foreach (var cookie in planetwinCookies) DriverManager.GetDriver().Manage().Cookies.AddCookie(cookie);
        }

        public static void DeleteAllCookies()
        {
            TestLogger.Log(LogLevel.Debug, "Deleting all cookies");
            DriverManager.GetDriver().Manage().Cookies.DeleteAllCookies();
        }

        public static void Scroll(int scrollStep)
        {
            TestLogger.Log(LogLevel.Debug, $"Scrolling {scrollStep} pixels");
            DriverManager.GetDriver().SwitchTo().DefaultContent();
            DriverManager.JsExecutor.ExecuteScript(
                $"window.scrollTo(0, document.documentElement.scrollTop + {scrollStep});");
        }

        public static string GetCurrentTabHeader()
        {
            var currentTabHeader = DriverManager.GetDriver().Title;
            TestLogger.Log(LogLevel.Debug, $"Getting current tab header {currentTabHeader}");
            return currentTabHeader;
        }

        public static void Open()
        {
            TestLogger.Log(LogLevel.Debug, "Opening browser");
            DriverManager.InitDriver(SeleniumTestContext.GetCurrentBrowserType());
        }

        public static void VerifyNoErrors()
        {
            var webRequest = (HttpWebRequest) WebRequest
                .Create(GetCurrentUrl());
            webRequest.AllowAutoRedirect = true;
            var response = (HttpWebResponse) webRequest.GetResponse();
            Assert.AreEqual(400, (int) response.StatusCode);
        }

        public static void DeleteCookie(string cookie)
        {
            TestLogger.Log(LogLevel.Debug, $"Deleting cookie {cookie}");
            DriverManager.GetDriver().Manage().Cookies.DeleteCookieNamed(cookie);
        }
    }
}