using System;
using System.IO;

namespace AutoTests.AutomationFramework.UI.Entities
{
    public class TestsExecutionSettings
    {
        public string TestProjectAssemblyName { get; set; }

        public string ReportFolderPath { get; set; }

        public string OperaBinaryPath { get; set; }

        public string GridUrl { get; set; }

        public int DefaultTimeout { get; set; }

        public string LongWaitTimeout { get; set; }

        public string FullReportFolderPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ReportFolderPath);
    }
}