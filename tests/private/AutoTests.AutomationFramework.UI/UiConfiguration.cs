using System;
using System.Collections.Generic;
using System.Linq;
using AutoTests.AutomationFramework.Shared.Configuration;
using AutoTests.AutomationFramework.UI.Entities;

namespace AutoTests.AutomationFramework.UI
{
    public static class UiConfiguration
    {
        public static readonly TestsExecutionSettings TestsSettings;
        public static readonly IEnumerable<EnvironmentSetting> Environments;

        static UiConfiguration()
        {
            var configuration = new ConfigurationManager("appsettings.json");
            var uiConfiguration = configuration.Get<UiSettings>();
            TestsSettings = uiConfiguration.UiTestsInfo;
            Environments = uiConfiguration.Environments;
        }

        public static Uri GetEnvironmentUri(string name)
        {
            return new Uri(Environments.First(x => x.Name == name).Uri);
        }
    }
}