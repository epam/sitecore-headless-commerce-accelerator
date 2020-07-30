using AutoTests.AutomationFramework.Shared.Configuration;
using NUnit.Framework;

namespace AutoTests.AutomationFramework.API
{
    [TestFixture]
    [Description("Base Test.")]
    public class ApiTest
    {
        [SetUp]
        public virtual void Setup()
        {
        }

        [TearDown]
        public virtual void TearDown()
        {
        }

        protected static readonly ConfigurationManager Configuration = new ConfigurationManager("appsettings.api.json");
    }
}