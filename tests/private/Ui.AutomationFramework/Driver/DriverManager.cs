using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Ui.AutomationFramework.Core;
using static Ui.AutomationFramework.Core.TestLogger;
using LogLevel = NLog.LogLevel;

namespace Ui.AutomationFramework.Driver
{
    public static class DriverManager
    {
        private static readonly ThreadLocal<IDictionary<BrowserType, IWebDriver>> ThreadDrivers
            = new ThreadLocal<IDictionary<BrowserType, IWebDriver>>(() => new Dictionary<BrowserType, IWebDriver>());

        private static readonly ThreadLocal<IWebDriver> Driver = new ThreadLocal<IWebDriver>();
        private static readonly ThreadLocal<WebDriverWait> Waiter = new ThreadLocal<WebDriverWait>();
        public static IJavaScriptExecutor JsExecutor => (IJavaScriptExecutor) GetDriver();

        public static void InitDriver(BrowserType type, DriverOptions driverOptions = null)
        {
            Log(LogLevel.Debug, $"Initializing driver with type: {type}");
            ResetWaiter();

            if (ThreadDrivers.Value.ContainsKey(type))
            {
                Log(LogLevel.Debug, $"Browser with type {type} is already exist");
                ThreadDrivers.Value.TryGetValue(type, out var driver);
                Driver.Value = driver;
                SeleniumTestContext.SetCurrentBrowser(type);
                return;
            }

            if (driverOptions == null) driverOptions = BrowserOptions.GetOptions(type);
            //TODO make driver without grid
            var remoteWebDriver = GetRemoteDriver(type, driverOptions);

            if (remoteWebDriver != null)
            {
                Driver.Value = remoteWebDriver;
                SeleniumTestContext.SetCurrentBrowser(type);
            }
            else
            {
                throw new Exception("Cannot initialize remote driver");
            }

            ThreadDrivers.Value.Add(type, Driver.Value);
            Log(LogLevel.Debug, "Driver initialized: " + type + " Configuration: " + Configuration.GridAddress);
        }

        private static IWebDriver GetRemoteDriver(BrowserType type, DriverOptions driverOptions, bool retry = true)
        {
            Log(LogLevel.Debug, "Trying to get remoteDriver...");

            var gridAddress = Configuration.GridAddress;
            Log(LogLevel.Debug, "Grid address: " + gridAddress);
            try
            {
                return new RemoteWebDriver(gridAddress, driverOptions?.ToCapabilities(),
                    TimeSpan.FromSeconds(180));
            }
            catch (Exception e) when (retry)
            {
                Log(LogLevel.Debug, "Getting Remote Driver failed. Retrying.", e);
                return GetRemoteDriver(type, driverOptions, false);
            }
        }

        public static WebDriverWait GetWaiter()
        {
            return GetWaiter(Configuration.DefaultTimeout, TimeSpan.FromMilliseconds(500));
        }

        public static IWebDriver GetDriver()
        {
            return Driver.Value;
        }

        public static WebDriverWait GetWaiter(double timeout)
        {
            return GetWaiter(timeout, TimeSpan.FromMilliseconds(500));
        }

        public static WebDriverWait GetWaiter(double timeout, TimeSpan pollingInterval)
        {
            if (Waiter.Value == null)
            {
                Waiter.Value =
                    new WebDriverWait(Driver.Value, TimeSpan.FromSeconds(timeout))
                    {
                        PollingInterval = pollingInterval
                    };
            }
            else
            {
                Waiter.Value.Timeout = TimeSpan.FromSeconds(timeout);
                Waiter.Value.PollingInterval = pollingInterval;
            }

            return Waiter.Value;
        }

        public static void StopDriverProcesses(bool retry = true)
        {
            Log(LogLevel.Debug, "Stopping all drivers...");
            try
            {
                foreach (var key in ThreadDrivers.Value.Keys)
                {
                    Log(LogLevel.Debug, "Driver type: " + key);
                    ThreadDrivers.Value[key].Close();
                    Log(LogLevel.Debug, "Closed");
                    ThreadDrivers.Value[key].Quit();
                    Log(LogLevel.Debug, "Quit");

                    //if (Configuration.ContinuousIntegration)
                    //{
                    //    if (key == BrowserType.Opera)
                    //    {
                    //        CloseOpera(ThreadDrivers.Value[key]);
                    //    }
                    //}
                }
            }
            catch (UnhandledAlertException exception)
            {
                Log(LogLevel.Debug, "Cannot stop driver. Alert opened. Cancelling alert.", exception);
                Alert.Cancel();
                StopDriverProcesses(false);
            }
            catch (Exception exception)
            {
                Log(LogLevel.Debug,
                    $"Cannot stop driver. Exception Text: {exception.Message}. ExceptionType: {exception.GetType()}",
                    exception);
                Log(LogLevel.Error,
                    $"Cannot stop driver. Exception Text: {exception.Message}. ExceptionType: {exception.GetType()}",
                    exception);
            }
            finally
            {
                if (retry)
                {
                    ThreadDrivers.Value.Clear();
                    Log(LogLevel.Debug, "Thread driver cleared");
                    ResetWaiter();
                    Log(LogLevel.Debug, "Waiter reset");
                    Driver.Value = null;
                    Log(LogLevel.Debug, "Driver value set to null");
                    SeleniumTestContext.SetCurrentBrowser(BrowserType.None);
                    Log(LogLevel.Debug, "Current browser is set to NONE");
                }
            }
        }

        private static void CloseOpera(IWebDriver operaDriver)
        {
            try
            {
                operaDriver.Navigate().GoToUrl("about:blank");
                var guid = Guid.NewGuid();
                operaDriver.ExecuteJavaScript($"document.title = '{guid}'");
                var operaProcess = Process.GetProcessesByName("opera")
                    .First(p => p.MainWindowTitle.Contains(guid.ToString()));
                operaProcess.Kill();
                Log(LogLevel.Debug, "Opera Killed");
            }
            catch (Exception e)
            {
                Log(LogLevel.Debug, "Opera process has been already killed", e);
            }
        }

        private static void ResetWaiter()
        {
            Waiter.Value = null;
        }

        public static void CloseDriver(BrowserType type)
        {
            Log(LogLevel.Debug, $"Closing driver {type}");
            if (!ThreadDrivers.Value.ContainsKey(type)) return;

            ThreadDrivers.Value[type].Close();
            ThreadDrivers.Value[type].Quit();
            ThreadDrivers.Value.Remove(type);
        }
    }
}