using AutoTests.AutomationFramework.Shared.Models;

namespace AutoTests.HCA.Core.Common.Settings.Users
{
    public class HcaUser
    {
        public HcaUserRole Role { get; set; }

        public HcaUserType Type { get; set; }

        public UserLogin Credentials { get; set; }
    }
}