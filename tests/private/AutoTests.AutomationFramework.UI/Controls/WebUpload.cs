using OpenQA.Selenium;
using static AutoTests.AutomationFramework.UI.Core.TestLogger;
using LogLevel = NLog.LogLevel;

namespace AutoTests.AutomationFramework.UI.Controls
{
    internal class WebUpload : BaseWebUiElement
    {
        public WebUpload(string elementName, Locator locator, BaseWebControl container = null) : base(elementName,
            locator, container)
        {
        }

        public WebUpload(string elementName, By locator, BaseWebControl container = null) : base(elementName,
            new Locator(locator), container)
        {
        }

        public WebUpload(string elementName, Locator locator, Frame frame) : base(elementName, locator, frame)
        {
        }

        public WebUpload(string elementName, By locator, Frame frame) : base(elementName, locator, frame)
        {
        }

        private void Upload(string filename)
        {
            Log(LogLevel.Debug, $"Uploading {filename} to {ElementName}");
            Execute<object>(() =>
            {
                Get().SendKeys(filename);
                return null;
            });
        }

        public void Upload(params string[] fileNames)
        {
            var filePaths = string.Join("\n", fileNames);
            Upload(filePaths);
        }
    }
}