using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Driver;
using LogLevel = NLog.LogLevel;

namespace UIAutomationFramework.Core
{
    public static class FrameManager
    {
        private static readonly ThreadLocal<Frame> CurrentFramePath = new ThreadLocal<Frame>();

        private static WebDriverWait FrameWaiter =>
            new WebDriverWait(DriverManager.GetDriver(),
                TimeSpan.FromSeconds(5)); //Wait cycle inside of wait cycle requires it's own timeout

        public static string GetFramePathString()
        {
            return GetFramePath(CurrentFramePath.Value);
        }

        private static string GetFramePath(Frame frame)
        {
            var path = "";
            if (frame == null) return path;

            foreach (var frameLocator in frame.FrameLocators) path = $"{path}{frameLocator}|";

            return path;
        }

        private static void SwitchToDefault()
        {
            TestLogger.Log(LogLevel.Debug, "Switching to Default Content");
            DriverManager.GetDriver().SwitchTo().DefaultContent();
            CurrentFramePath.Value = null;
        }

        internal static void SwitchFrame(Frame frame, bool wait = true)
        {
            SwitchToDefault();

            if (frame != null)
            {
                TestLogger.Log(LogLevel.Debug, $"Switching to Frame: {GetFramePath(frame)}");
                SwitchToFramePath(frame.FrameLocators, wait);
                CurrentFramePath.Value = frame;
            }
        }

        private static void SwitchToFramePath(List<By> frameToSelect, bool wait)
        {
            var finalFrame = GetLastFrameLocator(frameToSelect, wait);
            if (wait) FrameWaiter.Until(x => x.FindElements(finalFrame).Count > 0);

            try
            {
                TestLogger.Log(LogLevel.Debug, $"Switching to Frame: {finalFrame}");
                DriverManager.GetDriver().SwitchTo().Frame(DriverManager.GetDriver().FindElement(finalFrame));
            }
            catch (Exception) when (wait)
            {
                TestLogger.Log(LogLevel.Debug, $"Switching to Frame: {finalFrame}");
                DriverManager.GetDriver().SwitchTo().Frame(DriverManager.GetDriver().FindElement(finalFrame));
            }

            Browser.WaitForPageToLoad();
        }

        public static void WaitForFrameExpanded(Frame frame, int minimumHeight)
        {
            TestLogger.Log(LogLevel.Debug, $"Waiting for frame expanded, {frame}, {minimumHeight}");
            var height = 0;
            DriverManager.GetWaiter().Until(x =>
                {
                    var currentHeight = 0;
                    try
                    {
                        var lastFrameLocator = GetLastFrameLocator(frame.FrameLocators, true);
                        var webElement = DriverManager.GetDriver().FindElement(lastFrameLocator);
                        currentHeight = webElement.Size.Height;
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                    var result = currentHeight > minimumHeight && height == currentHeight;
                    height = currentHeight;
                    return result;
                }
            );
        }

        private static By GetLastFrameLocator(IReadOnlyList<By> frameToSelect, bool wait)
        {
            SwitchToDefault();
            for (var i = 0; i < frameToSelect.Count - 1; i++)
            {
                var frameLocator = frameToSelect[i];
                TestLogger.Log(LogLevel.Debug, $"Switching to Frame Locator: {frameLocator}. Wait: {wait}");

                if (wait) FrameWaiter.Until(d => d.FindElements(frameLocator).Count > 0);

                var frameElement = DriverManager.GetDriver().FindElement(frameLocator);

                try
                {
                    TestLogger.Log(LogLevel.Debug, $"Switching to Frame: {frameElement}");
                    DriverManager.GetDriver().SwitchTo().Frame(frameElement);
                }
                catch (Exception)
                {
                    TestLogger.Log(LogLevel.Debug, $"Switching to Frame: {frameLocator}");
                    DriverManager.GetDriver().SwitchTo().Frame(DriverManager.GetDriver().FindElement(frameLocator));
                }

                Browser.WaitForPageToLoad();
            }

            var lastFrameLocator = frameToSelect.Last();
            return lastFrameLocator;
        }
    }
}