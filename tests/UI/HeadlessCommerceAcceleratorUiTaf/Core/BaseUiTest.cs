using NUnit.Framework;

namespace Core
{
    public class BaseUiTest
    {
        protected BaseUiTest()
        {
        }

        [SetUp]
        protected void SetUpTest()
        {
            Browser.Maximize();
        }

        [TearDown]
        protected void TearDownTest()
        {
            DriverContext.Driver.Quit();
            DriverContext.Driver.Dispose();
        }
    }
}
