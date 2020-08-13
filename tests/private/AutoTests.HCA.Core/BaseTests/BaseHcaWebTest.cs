using AutoTests.AutomationFramework.UI;
using AutoTests.AutomationFramework.UI.Driver;
using OpenQA.Selenium;

namespace AutoTests.HCA.Core.BaseTests
{
    public class BaseHcaWebTest : SeleniumTest
    {
        private static readonly string EnvironmentName = "HcaEnvironment";

        protected static readonly IHcaTestsCore TestsHelper = new HcaTestsCore();

        public BaseHcaWebTest() : base(EnvironmentName)
        {
        }

        public BaseHcaWebTest(BrowserType browserType) : base(EnvironmentName, browserType)
        {
        }

        public BaseHcaWebTest(BrowserType browserType, DriverOptions driverOptions) :
            base(EnvironmentName, browserType, driverOptions)
        {
        }
    }
}