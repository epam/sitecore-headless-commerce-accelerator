using OpenQA.Selenium;
using UIAutomationFramework;

namespace HCA.Pages
{
    public class HCAWebTest : SeleniumTest
    {
        private static readonly string EnvironmentName = "HCAEnvironment";

        public HCAWebTest(BrowserType browserType) : base(browserType, EnvironmentName)
        {
        }

        public HCAWebTest() : base(EnvironmentName)
        {
        }

        public HCAWebTest(BrowserType browserType, DriverOptions driverOptions) : base(browserType, driverOptions,
            EnvironmentName)
        {
        }
    }
}