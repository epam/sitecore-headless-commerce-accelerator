using AutoTests.AutomationFramework.Shared.Configuration;
using AutoTests.AutomationFramework.Shared.Models;

namespace AutoTests.HCA.Tests
{
    public static class TestsData
    {
        private static readonly ConfigurationManager _configurationManager = new ConfigurationManager("testsdata.json");

        private static UserLogin _userLogin;
        private static int? _productId;

        public static UserLogin DefUserLogin => _userLogin ??= _configurationManager.Get<UserLogin>("User");
        public static int ProductId => _productId ??= _configurationManager.Get<int>("ProductId");
    }
}