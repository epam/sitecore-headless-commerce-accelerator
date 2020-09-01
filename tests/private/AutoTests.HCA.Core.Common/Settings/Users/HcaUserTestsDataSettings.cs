using AutoTests.AutomationFramework.Shared.Models;

namespace AutoTests.HCA.Core.Common.Settings.Users
{
    public class HcaUserTestsDataSettings : BaseHcaEntityTestsDataSettings
    {
        public HcaUserRole Role { get; set; }

        public UserLogin Credentials { get; set; }
    }
}