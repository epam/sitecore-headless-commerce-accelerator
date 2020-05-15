﻿namespace Core
{
    public static class Browser
    {
        public static string Url => DriverContext.Driver.Url;

        public static void WaitForPageToLoad()
        {
            DriverContext.Driver.GetDriverWait().Until(driver => DriverContext.JSExecutor.ExecuteScript("return document.readyState;").Equals("complete"));
        }

        public static void Navigate(string url)
        {
            DriverContext.Driver.Navigate().GoToUrl(url);
        }

        public static void Maximize()
        {
            DriverContext.Driver.Manage().Window.Maximize();
        }
    }
}
