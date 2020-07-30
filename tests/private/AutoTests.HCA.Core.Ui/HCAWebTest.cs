using AutoTests.AutomationFramework.UI;
using AutoTests.AutomationFramework.UI.Driver;
using OpenQA.Selenium;

namespace AutoTests.HCA.Core.UI
{
    public class HcaWebTest : SeleniumTest
    {
        private static readonly string EnvironmentName = "HCAEnvironment";

        public HcaWebTest() : base(EnvironmentName)
        {
        }

        public HcaWebTest(BrowserType browserType) : base(EnvironmentName, browserType)
        {
        }

        public HcaWebTest(BrowserType browserType, DriverOptions driverOptions) :
            base(EnvironmentName, browserType, driverOptions)
        {
        }
    }
}