using Api.HCA.Core;
using Api.HCA.Core.Models.Hca;
using Api.HCA.Core.Services.HcaService;
using NUnit.Framework;

namespace HCA.Tests.APITests.OrderTests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    [Description("Cart Tests")]
    [ApiTest]
    public class GetOrderTests : HcaApiTest
    {
        private readonly IHcaApiService _hcaService = CreateHcaApiClient();

        public const string ORDER_ID = "1H1SZ767SABD59OSJAOW7KMXA";

        [Test]
        [Description("Check that the basket is empty")]
        public void _01_GetOrderTest()
        {
            // Arrange, Act
            var response = _hcaService.GetOrder(ORDER_ID);

            // Assert
            var data = response.OkResponseData;
            Assert.True(response.IsSuccessful, "The GetOrder GET request is not passed.");

            Assert.Multiple(() =>
            {
                Assert.NotNull(data, $"Invalid {nameof(response.OkResponseData)}. Expected: NotNull.");
                Assert.AreEqual(HcaStatus.Ok, data.Status);
                Assert.NotNull(data.Data, $"Invalid {nameof(response.OkResponseData.Data)}. Expected: NotNull.");
                Assert.NotNull(data.Data.OrderId, $"Invalid {nameof(data.Data.OrderId)}. Expected: NotNull.");
                Assert.NotNull(data.Data.OrderDate, $"Invalid {nameof(data.Data.OrderDate)}. Expected: NotNull.");
                Assert.NotNull(data.Data.Status, $"Invalid {nameof(data.Data.Status)}. Expected: NotNull.");
                Assert.NotNull(data.Data.TrackingNumber,
                    $"Invalid {nameof(data.Data.TrackingNumber)}. Expected: NotNull.");
                Assert.NotNull(data.Data.Email, $"Invalid {nameof(data.Data.Email)}. Expected: NotNull.");
                Assert.NotNull(data.Data.Price, $"Invalid {nameof(data.Data.Price)}. Expected: NotNull.");
                Assert.NotNull(data.Data.CartLines, $"Invalid {nameof(data.Data.CartLines)}. Expected: NotNull.");
                Assert.IsNotEmpty(data.Data.CartLines, $"Invalid {nameof(data.Data.CartLines)}. Expected: NotEmpty.");
                Assert.NotNull(data.Data.Addresses, $"Invalid {nameof(data.Data.Addresses)}. Expected: NotNull.");
                Assert.IsNotEmpty(data.Data.Addresses, $"Invalid {nameof(data.Data.Addresses)}. Expected: NotEmpty.");
                Assert.NotNull(data.Data.Shipping, $"Invalid {nameof(data.Data.Shipping)}. Expected: NotNull.");
                Assert.NotNull(data.Data.Payment, $"Invalid {nameof(data.Data.Payment)}. Expected: NotNull.");
                Assert.AreEqual(ORDER_ID, data.Data.TrackingNumber);
            });
        }
    }
}