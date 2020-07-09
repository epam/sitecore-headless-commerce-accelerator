using NLog;
using NLog.Config;
using NLog.Targets;
using NUnit.Framework;
using System;
using System.Threading;
using UIAutomationFramework.Driver;

namespace UIAutomationFramework.Core
{
    public class TestLogger
    {
        private static readonly ThreadLocal<TestLogger> ThreadTestLogger = new ThreadLocal<TestLogger>();
        private static readonly ThreadLocal<string> RepeatingLogMessage = new ThreadLocal<string>();
        private static readonly ThreadLocal<LogLevel> RepeatingMessageLogLevel = new ThreadLocal<LogLevel>();
        private static readonly ThreadLocal<string> RepeatingLogMessageTimings = new ThreadLocal<string>();

        private readonly string _logFileNameDebug;
        private readonly string _logFileNameInfo;
        private readonly LoggingRule _loggingRuleDebug;
        private readonly LoggingRule _loggingRuleInfo;
        private readonly FileTarget _targetDebug;
        private readonly FileTarget _targetInfo;
        private readonly string _testName;

        public TestLogger(BrowserType browserType, string postfix = "")
        {
            _testName = GetTestName(browserType, postfix);

            _logFileNameDebug = $"{_testName}Debug.log";
            _targetDebug = new FileTarget(_testName)
            {
                FileName = "${basedir}\\Report\\" + _logFileNameDebug,
                Layout = "${longdate} | ${message} | ${exception}"
            };
            _loggingRuleDebug =
                new LoggingRule(_testName + "Debug", LogLevel.Debug, LogLevel.Debug, _targetDebug);

            _logFileNameInfo = $"{_testName}Info.log";
            _targetInfo = new FileTarget(_testName)
            {
                FileName = "${basedir}\\Report\\" + _logFileNameInfo,
                Layout = "${longdate} | ${message}"
            };
            _loggingRuleInfo = new LoggingRule(_testName + "Info", LogLevel.Info, LogLevel.Info, _targetInfo);
            LogManager.Configuration.AddTarget(_targetDebug);
            LogManager.Configuration.AddTarget(_targetInfo);

            ThreadTestLogger.Value = this;
        }

        private string GetTestName(BrowserType browserType, string postfix)
        {
            var testName = browserType + postfix;
            var isTimeStampAdded = false;
            foreach (var testArgument in TestContext.CurrentContext.Test.Arguments)
                if (testArgument is string || testArgument is string[])
                {
                    if (isTimeStampAdded) continue;

                    testName += DateTime.Now.ToString("yyyy_MM_ddTHH_mm_ss") + "_Thread_" +
                                Thread.CurrentThread.ManagedThreadId;
                    isTimeStampAdded = true;
                }
                else
                {
                    if (testArgument.GetType().Name != "Missing") testName += testArgument;
                }

            return testName;
        }

        public void Init()
        {
            LogManager.Configuration.LoggingRules.Add(_loggingRuleDebug);
            LogManager.Configuration.LoggingRules.Add(_loggingRuleInfo);
            LogManager.ReconfigExistingLoggers();
            Log(LogLevel.Debug, "Logger initialized");
        }

        private static TestLogger GetThreadLogger()
        {
            return ThreadTestLogger.Value;
        }

        private Logger GetLogger(string namePostfix = "")
        {
            return LogManager.GetLogger(_testName + namePostfix);
        }

        /// <summary>
        ///     That method is solving log duplication.
        /// </summary>
        /// <param name="logLevel"> Used to log timings </param>
        /// <param name="message"> Log message </param>
        /// <returns></returns>
        private static string SetRepeatingLogMessage(LogLevel logLevel, string message)
        {
            //There was a repeat in previous step?
            if (!string.IsNullOrEmpty(RepeatingLogMessageTimings.Value))
            {
                //Yes, it was repeat
                //Still repeating?
                if (RepeatingLogMessage.Value.Equals(message))
                {
                    //Yes
                    //Save time of repeating message
                    var dateTime = DateTime.Now;
                    RepeatingLogMessageTimings.Value = RepeatingLogMessageTimings.Value + $"{dateTime:T}. ";
                    //Do not log repeating message
                    return null;
                }

                if (!RepeatingLogMessage.Value.Equals(message))
                {
                    //No, previous step wasn't repeat
                    //Exit from repeating, we need to write timings and count of repeat
                    //Saving to variable to be able to erase timings after write to file
                    var timings = RepeatingLogMessageTimings.Value;
                    //If repeat once
                    if (!string.IsNullOrEmpty(timings))
                    {
                        //Erase timings
                        RepeatingLogMessageTimings.Value = string.Empty;
                        //Write to file
                        Log(RepeatingMessageLogLevel.Value, timings);
                    }

                    //We have received new message, it should be written too
                    //We can't write it before previous message was written
                    SetRepeatingMessage(logLevel, message);
                    return RepeatingLogMessage.Value;
                }
            }
            else
            {
                //No repeats in previous step
                //Will we repeat previous message?
                if (message.Equals(RepeatingLogMessage.Value))
                {
                    //Yes
                    //Set timings for first repeat
                    var dateTime = DateTime.Now;
                    RepeatingLogMessageTimings.Value = RepeatingLogMessageTimings.Value + $"{dateTime:T}. ";
                    //Entering to repeats
                    //No need to log
                    return null;
                }

                //No
                SetRepeatingMessage(logLevel, message);
                return RepeatingLogMessage.Value;
            }

            return null;
        }

        private static void SetRepeatingMessage(LogLevel logLevel, string message)
        {
            RepeatingLogMessage.Value = message;
            RepeatingMessageLogLevel.Value = logLevel;
        }

        public static void Log(LogLevel logLevel, string text, Exception e = null)
        {
            var message = SetRepeatingLogMessage(logLevel, text);

            if (message == null) return;

            Common(message);
            if (GetThreadLogger() == null && TestContext.CurrentContext.Test.MethodName == null)
            {
                Common("There is no logger! Not yet in test.");
                return;
            }

            if (logLevel.Equals(LogLevel.Debug))
            {
                //Console.WriteLine(message);
                GetThreadLogger().GetLogger("Debug").Debug(e, message);
            }
            else if (logLevel.Equals(LogLevel.Info))
            {
                GetThreadLogger().GetLogger("Info").Info(message);
            }
        }

        public static void Dispose()
        {
            Log(LogLevel.Debug, "Disposing logger");
            var debugFilePath = Configuration.ReportFolderPath + GetThreadLogger()._logFileNameDebug;

            //if (Configuration.ContinuousIntegration)
            //{
            //    try
            //    {
            //        var fileName = Path.GetFileName(debugFilePath);
            //        var tempFilePath =
            //            Path.Combine(Configuration.CiReportRuntimeFolder + fileName);
            //        File.Copy(debugFilePath, tempFilePath.Replace(".log", ".txt"));
            //        Console.WriteLine($"Debug: {Configuration.CiReportUrl}{fileName.Replace(".log", ".txt")}");
            //    }
            //    catch (IOException copyError)
            //    {
            //        Log(LogLevel.Error, copyError.Message);
            //    }
            //}

            TestContext.AddTestAttachment(debugFilePath, "Debug");
            TestContext.AddTestAttachment(Configuration.ReportFolderPath + GetThreadLogger()._logFileNameInfo,
                "Info");

            LogManager.Configuration.LoggingRules.Remove(GetThreadLogger()._loggingRuleDebug);
            LogManager.Configuration.LoggingRules.Remove(GetThreadLogger()._loggingRuleInfo);
            LogManager.Configuration.RemoveTarget(GetThreadLogger()._targetDebug.Name);
            LogManager.Configuration.RemoveTarget(GetThreadLogger()._targetInfo.Name);
        }

        public static void Common(string text)
        {
            var testName = "Unknown";
            try
            {
                testName = TestContext.CurrentContext.Test.Name;
            }
            catch
            {
                // ignored.
            }

            LogManager.GetLogger("Common").Debug($"{testName} || {text}");
        }
    }
}