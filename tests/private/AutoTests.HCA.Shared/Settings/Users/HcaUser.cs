using AutoTests.AutomationFramework.Shared.Models;

namespace AutoTests.HCA.Common.Settings.Users
{
    public class HcaUser
    {
        public HcaUserType Type { get; set; }

        public UserLogin Credentials { get; set; }
    }
}
