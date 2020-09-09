using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CartTests
{
    public class GetCartTests : BaseCartApiTest
    {
        [SetUp]
        public new void SetUp()
        {
            ApiHelper.AddProductsToCart(ProductsCollection);
        }

        public GetCartTests(HcaUserRole userRole) : base(userRole)
        {
        }

        [Test(Description = "The test checks the state of the cart.")]
        public void T1_GETCartRequest_VerifyResponse()
        {
            // Arrange, Act
            var result = ApiContext.Cart.GetCart();

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();
                VerifyCartResponse("GET Cart", ProductsCollection, result.OkResponseData.Data);
            });
        }
    }
}