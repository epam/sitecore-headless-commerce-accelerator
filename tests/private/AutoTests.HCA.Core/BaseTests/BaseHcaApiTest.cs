using AutoTests.AutomationFramework.API;

namespace AutoTests.HCA.Core.BaseTests
{
    public class BaseHcaApiTest : ApiTest
    {
        protected static readonly IHcaTestsCore TestsHelper = new HcaTestsCore();
    }
}