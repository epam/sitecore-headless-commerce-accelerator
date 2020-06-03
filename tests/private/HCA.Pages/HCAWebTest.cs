using OpenQA.Selenium;
using UIAutomationFramework;
using UIAutomationFramework.Driver;

namespace HCA.Pages
{
    public class HcaWebTest : SeleniumTest
    {
        private static readonly string EnvironmentName = "HCAEnvironment";

        public HcaWebTest(BrowserType browserType) : base(browserType, EnvironmentName)
        {
        }

        public HcaWebTest() : base(EnvironmentName)
        {
        }

        public HcaWebTest(BrowserType browserType, DriverOptions driverOptions) : base(browserType, driverOptions,
            EnvironmentName)
        {
        }
    }
}