using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.StoreLocator
{
    public class GetLocationsTests : BaseStoreLocatorTest
    {
        [Test(Description = "The test checks the response model.")]
        public void T1_GETGetStoresRequest_VerifyResponse()
        {
            // Arrange, Act
            var result = ApiContext.StoreLocator.GetStores();

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();
                CheckStoresModel(result.OkResponseData.Data.Locations);
            });
        }
    }
}
