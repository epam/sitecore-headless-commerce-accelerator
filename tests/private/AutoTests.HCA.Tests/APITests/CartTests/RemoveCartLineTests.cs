using System.Linq;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CartTests
{
    public class RemoveCartLineTests : BaseCartApiTest
    {
        [SetUp]
        public new void SetUp()
        {
            foreach (var product in ProductsCollection) HcaService.AddCartLines(product);
        }

        public RemoveCartLineTests(HcaUserRole userRole) : base(userRole)
        {
        }

        [Test(Description = "The test verifies the correct product has been removed from the cart.")]
        public void T1_DELETECartLinesRequest_ValidProduct_ProductHasBeenDeleted()
        {
            // Arrange
            var productToRemove = ProductsCollection.First();

            // Act
            var result = HcaService.RemoveCartLine(productToRemove.ProductId, productToRemove.VariantId);

            // Assert
            result.VerifyOkResponseData();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();

                VerifyCartResponse("DELETECartLines", ProductsCollection.Where(x =>
                        x.VariantId != productToRemove.VariantId && x.ProductId != productToRemove.ProductId),
                    result.OkResponseData.Data);
            });
        }
    }
}