using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using AutoTests.AutomationFramework.UI.Core;
using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.AutomationFramework.UI.Entities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using static AutoTests.AutomationFramework.UI.Core.TestLogger;
using LogLevel = NLog.LogLevel;

namespace AutoTests.AutomationFramework.UI
{
    [TestFixture]
    [Order(1)]
    public class SeleniumTest
    {
        [SetUp]
        public void Setup()
        {
            testTime.Reset();
            testTime.Start();
            CustomOneTimeSetUp(BrowserType, TestContext.CurrentContext.Test.MethodName);
        }

        [TearDown]
        public void TearDown()
        {
            Log(LogLevel.Debug, "Tearing down...");
            if (TestContext.CurrentContext.CurrentRepeatCount > 0)
            {
                Log(LogLevel.Debug, "Test was rerun!");
                TestContext.Out.WriteLine("[TestReRunKey]");
            }

            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed) TearDownIfFailed();

            if (DriverManager.GetDriver() != null && Alert.IsPresent()) Alert.Cancel();

            DriverManager.StopDriverProcesses();


            SeleniumTestContext.ResetEnvironment();

            Log(LogLevel.Debug, "SeleniumTest TearDown() completed");
            Dispose();
            testTime.Stop();
            Console.WriteLine($"testTime {testTime.Elapsed}");
        }

        private static readonly TestsExecutionSettings _testsSettings = UiConfiguration.TestsSettings;

        public SeleniumTest(string environmentName, BrowserType browserType = BrowserType.Chrome)
        {
            BrowserType = browserType;
            EnvironmentName = environmentName;
        }

        public SeleniumTest(string environmentName, BrowserType browserType, DriverOptions driverOptions) :
            this(environmentName, browserType)
        {
            _driverOptions = driverOptions;
        }

        private readonly Stopwatch testTime = new Stopwatch();

        private string EnvironmentName { get; }

        private const string ScreenshotExt = ".png";

        protected static void TearDownIfFailed()
        {
            TestContext.Out.WriteLine($"[StepFailed]{SeleniumTestContext.CurrentStep}###");


            Log(LogLevel.Debug, $"Current Frame is: {FrameManager.GetFramePathString()}");

            var screenshotFilePath = DoScreenshot(GetScriptId());
            if (!string.IsNullOrEmpty(screenshotFilePath))
                TestContext.AddTestAttachment(screenshotFilePath, "Screenshot");


            Log(LogLevel.Info, "***[FAILED]: " + TestContext.CurrentContext.Result.Message);

            try
            {
                Log(LogLevel.Debug, $"Current url is:  {Browser.GetCurrentUrl()}");
            }
            catch (Exception exception)
            {
                Log(LogLevel.Error, "Cannot get current url", exception);
            }
        }

        private static string MakeWindowsScreenShot()
        {
            Log(LogLevel.Debug, "Creating windows screenshot");
            var screenshotFilePath = GetScreenshotName(GetScriptId()) + ScreenshotExt;
            try
            {
                var bounds = Screen.GetBounds(Point.Empty);
                using (var bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (var g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                    }

                    bitmap.Save(screenshotFilePath, ImageFormat.Png);
                }

                TestContext.AddTestAttachment(screenshotFilePath, "Screenshot");
            }
            catch (Exception screenshotError)
            {
                Log(LogLevel.Error, "Error during making windows screenshot.", screenshotError);
            }

            return screenshotFilePath;
        }


        private static readonly object Sync = new object();

        private void CustomOneTimeSetUp(BrowserType browser, string name = "")
        {
            SeleniumTestContext.SetCurrentStep("Precondition");
            new TestLogger(browser, name).Init();
            Log(LogLevel.Info, $"Test '{name}' SetUp() is started");
            DriverManager.InitDriver(browser, _driverOptions);
            SeleniumTestContext.SetFirstBrowserType(browser);
            if (browser != BrowserType.ChromeMobile && browser != BrowserType.Chrome) Browser.Maximize();

            Log(LogLevel.Info, $"{name} SetUp() completed");
        }

        [OneTimeTearDown]
        public void TearDownFixture()
        {
        }

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            Common("[OneTimeSetUp] started");
            LogConfiguration(EnvironmentName);
            Common("[OneTimeSetUp] completed");
        }

        public static void LogConfiguration(string environmentName)
        {
            Common($"Environment URL: {environmentName}");
            Common("DefaultTimeout: " + _testsSettings.DefaultTimeout);
            Common("GridUrl: " + _testsSettings.GridUrl);
            Common("ReportFolderPath: " + _testsSettings.FullReportFolderPath);
        }

        protected static void StepInfo(string text)
        {
            Log(LogLevel.Info, text);
            SeleniumTestContext.SetCurrentStep(text);
        }

        private static Bitmap GetScrolledImageScreenshot()
        {
            const string javaScriptToFullPageHeight =
                "return Math.max(document.body.scrollHeight, document.body.offsetHeight,  document.documentElement.clientHeight,  document.documentElement.scrollHeight,  document.documentElement.offsetHeight);";

            var scrollHeight =
                Convert.ToInt32(DriverManager.GetDriver().ExecuteJavaScript<object>(javaScriptToFullPageHeight));
            var offset = 0;
            var width = 0;
            var images = new List<Image>();
            while (offset < scrollHeight)
            {
                DriverManager.GetDriver().ExecuteJavaScript($"window.scrollTo(0, {offset});");
                var tempScreenshot = DriverManager.GetDriver().TakeScreenshot().AsByteArray;
                var img = Image.FromStream(new MemoryStream(tempScreenshot));
                if (img.Size.Height == 0)
                {
                    Log(LogLevel.Debug, "Screenshot has height 0px");
                    break;
                }

                offset += img.Size.Height;
                images.Add(img);

                width = img.Size.Width;
            }

            var finalImage = new Bitmap(width, scrollHeight);
            var graphics = Graphics.FromImage(finalImage);
            graphics.Clear(Color.Black);

            var heightPositionToAdd = 0;
            foreach (var image in images)
            {
                var leftSpace = scrollHeight - heightPositionToAdd;
                var pointToAppendImage = leftSpace < image.Height
                    ? new Point(0, heightPositionToAdd + leftSpace - image.Height)
                    : new Point(0, heightPositionToAdd);
                graphics.DrawImage(image, pointToAppendImage);
                heightPositionToAdd += image.Height;
            }

            return finalImage;
        }

        private static string GetScreenshotName(string testId)
        {
            var browserName = SeleniumTestContext.GetFirstBrowserType().ToString();
            var scriptName = TestContext.CurrentContext.Test.MethodName;
            var dateTime = DateTime.Now.ToString("yyyyMMddTHHmmssZ");

            return
                $"{_testsSettings.FullReportFolderPath}{dateTime}{scriptName}_{testId}_{browserName}{Thread.CurrentThread.GetHashCode()}";
        }

        private static string DoScreenshot(string testId)
        {
            var format = "";
            Log(LogLevel.Debug, "Making screenshot...");
            var finalPath = "";
            Bitmap bufferedBitmap = null;

            var path = GetScreenshotName(testId);
            Directory.CreateDirectory(_testsSettings.FullReportFolderPath);
            try
            {
                DriverManager.GetDriver().SwitchTo().DefaultContent();
                if (Alert.IsPresent())
                {
                    Log(LogLevel.Error, "Alert present, Text: " + Alert.GetText());
                    Log(LogLevel.Debug, "Closing alert...");
                    Alert.Cancel();
                }


                bufferedBitmap = GetScrolledImageScreenshot();
            }
            catch (Exception ex)
            {
                Log(LogLevel.Debug, "Native WebDriver screenshot failed.", ex);
                Log(LogLevel.Warn, "Native WebDriver screenshot failed.", ex);
            }

            if (bufferedBitmap != null)
                try
                {
                    var filename = path + format + ScreenshotExt;
                    bufferedBitmap.Save(filename, ImageFormat.Png);
                    finalPath = filename;
                }
                catch (Exception e)
                {
                    Log(LogLevel.Debug, "Saving of image is failed", e);
                    Log(LogLevel.Error, "Saving of image is failed", e);
                }

            Log(LogLevel.Debug, "Path to screenshot: " + finalPath);
            return finalPath;
        }

        private static Bitmap GetScreenshot()
        {
            return new Bitmap(
                Image.FromStream(new MemoryStream(DriverManager.GetDriver().TakeScreenshot().AsByteArray)));
        }

        private static string GetScriptId()
        {
            MethodInfo method = null;
            try
            {
                method = Assembly.Load(_testsSettings.TestProjectAssemblyName)
                    .GetType(TestContext.CurrentContext.Test.ClassName)
                    ?.GetMethod(TestContext.CurrentContext.Test.MethodName);
            }
            catch (Exception e)
            {
                Log(LogLevel.Error, "Method is not found", e);
            }

            return method == null
                ? ""
                : method.Name; //method.GetCustomAttribute<TestIdAttribute>().TestIdValue;
        }

        protected readonly BrowserType BrowserType;
        private readonly DriverOptions _driverOptions;
    }

    public class ExtendedWaitingAttribute : Attribute
    {
        public ExtendedWaitingAttribute()
        {
            SeleniumTestContext.SetExtendedWaiting();
        }
    }

    public class TestIdAttribute : PropertyAttribute
    {
        public readonly string TestIdValue;

        public TestIdAttribute(string id) : base("TestScriptID", id)
        {
            TestIdValue = id;
        }
    }
}