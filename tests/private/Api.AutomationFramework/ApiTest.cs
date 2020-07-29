using NUnit.Framework;

namespace Api.AutomationFramework
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

        protected static readonly ApiConfiguration Configuration = new ApiConfiguration("appsettings.api.json");
    }
}