using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;

namespace AutoTests.AutomationFramework.UI.Driver
{
    public static class BrowserOptions
    {
        private static FirefoxOptions GetFirefoxOptions()
        {
            var profile = new FirefoxProfile();

            var firefoxOptions = new FirefoxOptions();

            profile.SetPreference("browser.download.dir", AppDomain.CurrentDomain.BaseDirectory);
            profile.SetPreference("browser.download.folderList", 2);
            profile.SetPreference("browser.helperApps.neverAsk.saveToDisk",
                "application/vnd.ms-excel;application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;");
            firefoxOptions.Profile = profile;
            firefoxOptions.AcceptInsecureCertificates = true;

            firefoxOptions.SetPreference("browser.link.open_newwindow", 3);
            return firefoxOptions;
        }

        private static ChromeOptions GetChromeOptions()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory",
                AppDomain.CurrentDomain.BaseDirectory);
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", "false");

            chromeOptions.AddArguments("--no-sandbox");
            //chromeOptions.AddArguments("--lang=de");
            chromeOptions.AddArguments("--start-maximized");
            return chromeOptions;
        }

        public static ChromeOptions GetChromeOptionsWithUserAgent(string userAgent)
        {
            var options = new ChromeOptions();
            options.AddArgument($"--user-agent={userAgent}");
            options.AddArguments("--no-sandbox");
            return options;
        }

        private static ChromeOptions GetChromeMobileOptions()
        {
            var chromeMobileOptions = new ChromeOptions();
            chromeMobileOptions.AddArguments("--no-sandbox");
            chromeMobileOptions.AddArgument(
                "--user-agent=Mozilla/5.0 (iPad; CPU OS 10_2 like Mac OS X) AppleWebKit/602.1.50 (KHTML, like Gecko) CriOS/55.0.2883.79 Mobile/14C92 Safari/602.1");

            return chromeMobileOptions;
        }

        private static ChromeOptions GetChromeMobileEmulatorOptions()
        {
            var chromeMobileOptions = new ChromeOptions();
            chromeMobileOptions.EnableMobileEmulation("iPhone 6/7/8");
            chromeMobileOptions.AddArguments("--no-sandbox");
            return chromeMobileOptions;
        }

        public static DriverOptions GetOptions(BrowserType type)
        {
            DriverOptions driverOptions = null;
            switch (type)
            {
                case BrowserType.Chrome:
                    driverOptions = GetChromeOptions();
                    break;
                case BrowserType.Firefox:
                    driverOptions = GetFirefoxOptions();
                    break;
                case BrowserType.Edge:
                    driverOptions = new EdgeOptions();
                    break;
                case BrowserType.InternetExplorer:
                    driverOptions = new InternetExplorerOptions();
                    break;
                case BrowserType.Opera:
                    driverOptions = new OperaOptions
                    {
                        BinaryLocation = UiConfiguration.TestsSettings.OperaBinaryPath
                    };
                    break;
                case BrowserType.None:
                    break;
                case BrowserType.ChromeMobile:
                    driverOptions = GetChromeMobileOptions();
                    break;
                case BrowserType.ChromeMobileEmulator:
                    driverOptions = GetChromeMobileEmulatorOptions();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return driverOptions;
        }
    }
}