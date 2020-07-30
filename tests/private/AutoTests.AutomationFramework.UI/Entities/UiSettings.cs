using System.Collections.Generic;

namespace AutoTests.AutomationFramework.UI.Entities
{
    public class UiSettings
    {
        public IEnumerable<EnvironmentSetting> Environments { get; set; }

        public TestsExecutionSettings UiTestsInfo { get; set; }
    }
}