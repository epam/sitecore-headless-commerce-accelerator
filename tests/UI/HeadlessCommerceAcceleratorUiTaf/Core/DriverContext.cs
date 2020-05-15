using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

namespace Core
{
    public static class DriverContext
    {
        private static readonly Dictionary<string, IWebDriver> DriverStorage = new Dictionary<string, IWebDriver>();

        public static IWebDriver Driver
        {
            get
            {
                if (DriverStorage.GetValueOrDefault("Driver") == null)
                {
                    DriverStorage.Add("Driver", new ChromeDriver());
                }
                return DriverStorage["Driver"];
            }
        }
        
        public static IJavaScriptExecutor JSExecutor => (IJavaScriptExecutor)Driver;
    }
}
