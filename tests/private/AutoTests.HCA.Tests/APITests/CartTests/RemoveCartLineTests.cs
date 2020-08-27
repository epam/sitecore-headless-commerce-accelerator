using System.Linq;
using System.Net;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CartTests
{
    public class RemoveCartLineTests : BaseCartApiTest
    {
        public RemoveCartLineTests(HcaUserRole userRole) : base(userRole) { }

        [SetUp]
        public new void SetUp()
        {
            foreach (var product in ProductsCollection)
            {
                HcaService.AddCartLines(product);
            }
        }

        [Test(Description = "The test verifies the correct product has been removed from the cart.")]
        public void T1_DELETECartLinesRequest_ValidProduct_ProductHasBeenDeleted()
        {
            // Arrange
            var productToRemove = ProductsCollection.First();

            // Act
            var result = HcaService.RemoveCartLine(productToRemove.ProductId, productToRemove.VariantId);

            // Assert
            Assert.True(result.IsSuccessful, "The 'DELETECartLines' Request isn't passed.");
            Assert.Multiple(() =>
            {
                ExtendedAssert.AreEqual(HttpStatusCode.OK, result.StatusCode, nameof(result.StatusCode));
                ExtendedAssert.NotNull(result.OkResponseData, nameof(result.OkResponseData));
                ExtendedAssert.AreEqual(HcaStatus.Ok, result.OkResponseData.Status, nameof(result.OkResponseData.Status));

                VerifyCartResponse("DELETECartLines", ProductsCollection.Where(x => 
                    x.VariantId != productToRemove.VariantId && x.ProductId != productToRemove.ProductId), 
                    result.OkResponseData.Data);
            });
        }
    }
}
