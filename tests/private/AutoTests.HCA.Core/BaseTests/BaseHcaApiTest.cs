using AutoTests.AutomationFramework.API;
using NUnit.Framework;

namespace AutoTests.HCA.Core.BaseTests
{
    [TestFixture(Description = "Base Hca Api Test.")]
    public class BaseHcaApiTest : ApiTest
    {
        protected static readonly IHcaTestsCore TestsHelper = new HcaTestsCore();
    }
}