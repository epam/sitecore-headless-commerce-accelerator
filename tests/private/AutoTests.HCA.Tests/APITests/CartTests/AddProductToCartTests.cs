using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Cart;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;
using AutoTests.AutomationFramework.Shared.Extensions;

namespace AutoTests.HCA.Tests.APITests.CartTests
{
    public class AddProductToCartTests : BaseCartApiTest
    {
        public AddProductToCartTests(HcaUserRole userRole) : base(userRole) { }

        private static IEnumerable<TestCaseData> T4_POSTCartLinesRequest_InvalidParameters_TestCaseData()
        {
            yield return new TestCaseData(Product.ProductId + 1, 10, Product.VariantId, "Product Not Found.");
            yield return new TestCaseData(Product.ProductId, 110, Product.VariantId,
                "Invalid quantity. Quantity must be greater than '0' and less than '100.'");
            yield return new TestCaseData(Product.ProductId, 10, Product.VariantId + 1, "VariantId Not Found.");
        }

        [Test(Description = "A test that checks the server's response after adding the product to the shopping cart.")]
        public void T1_POSTCartLinesRequest_ValidProduct_VerifyResponse()
        {
            // Arrange, Act
            var result = HcaService.AddCartLines(AddingProduct);

            // Assert
            Assert.True(result.IsSuccessful, "The POST CartLines request isn't passed.");
            Assert.Multiple(() =>
            {
                ExtendedAssert.AreEqual(HttpStatusCode.OK, result.StatusCode, nameof(result.StatusCode));
                ExtendedAssert.NotNull(result.OkResponseData, nameof(result.OkResponseData));
                ExtendedAssert.AreEqual(HcaStatus.Ok, result.OkResponseData.Status, nameof(result.OkResponseData.Status));

                VerifyCartResponse("POST CartLines", new List<CartLinesRequest> { AddingProduct }, result.OkResponseData.Data);
            });
        }

        [Test(Description = "A test that checks if a product has been added to the cart.")]
        public void T2_POSTCartLinesRequest_ValidProduct_ProductHasBeenAddedToCart()
        {
            // Arrange, Act
            var addProductResult = HcaService.AddCartLines(AddingProduct);
            var getProductResult = HcaService.GetCart();

            // Assert
            Assert.True(addProductResult.IsSuccessful, "The POST CartLines request isn't passed.");
            Assert.True(getProductResult.IsSuccessful, "The GET Cart request isn't passed.");
            Assert.Multiple(() =>
            {
                ExtendedAssert.AreEqual(HttpStatusCode.OK, addProductResult.StatusCode, nameof(addProductResult.StatusCode));
                ExtendedAssert.AreEqual(HttpStatusCode.OK, getProductResult.StatusCode, nameof(getProductResult.StatusCode));
                ExtendedAssert.NotNull(addProductResult.OkResponseData, nameof(addProductResult.OkResponseData));
                ExtendedAssert.NotNull(getProductResult.OkResponseData, nameof(getProductResult.OkResponseData));

                VerifyCartResponse("POST CartLines", new List<CartLinesRequest> { AddingProduct }, getProductResult.OkResponseData.Data);
            });
        }

        [Test(Description = "The test checks if two identical products are added to the cart if the number of products is updated.")]
        public void T3_POSTCartLinesRequest_TwoIdenticalProducts_ProductsNumberHasBeenUpdated()
        {
            // Arrange, Act
            var addProductResult1 = HcaService.AddCartLines(AddingProduct);
            var addProductResult2 = HcaService.AddCartLines(AddingProduct);
            var getProductResult = HcaService.GetCart();

            // Assert
            Assert.True(addProductResult1.IsSuccessful, "The POST CartLines request isn't passed.");
            Assert.True(addProductResult2.IsSuccessful, "The POST CartLines request isn't passed.");
            Assert.True(getProductResult.IsSuccessful, "The GET Cart request isn't passed.");
            Assert.Multiple(() =>
            {
                ExtendedAssert.AreEqual(HttpStatusCode.OK, addProductResult1.StatusCode, nameof(addProductResult1.StatusCode));
                ExtendedAssert.AreEqual(HttpStatusCode.OK, addProductResult2.StatusCode, nameof(addProductResult2.StatusCode));
                ExtendedAssert.AreEqual(HttpStatusCode.OK, getProductResult.StatusCode, nameof(getProductResult.StatusCode));
                ExtendedAssert.NotNull(addProductResult1.OkResponseData, nameof(addProductResult1.OkResponseData));
                ExtendedAssert.NotNull(addProductResult2.OkResponseData, nameof(addProductResult2.OkResponseData));
                ExtendedAssert.NotNull(getProductResult.OkResponseData, nameof(getProductResult.OkResponseData));

                VerifyCartResponse("POST CartLines", new List<CartLinesRequest> { AddingProduct, AddingProduct }, getProductResult.OkResponseData.Data);
            });
        }

        [Test(Description = "The test will check if the correct message is returned by the server with incorrect passed parameters.")]
        [TestCaseSource(nameof(T4_POSTCartLinesRequest_InvalidParameters_TestCaseData))]
        public void T4_POSTCartLinesRequest_InvalidParameters_BadRequest(string productId, int quantity, string variantId,
            string expErrorMessage)
        {
            // Arrange
            var cartLines = new CartLinesRequest
            {
                ProductId = productId,
                Quantity = quantity,
                VariantId = variantId
            };

            // Act
            var response = HcaService.AddCartLines(cartLines);

            // Assert
            Assert.False(response.IsSuccessful, "The bad POST CartLinesRequest request is passed.");
            Assert.Multiple(() =>
            {
                var dataResult = response.Errors;
                Assert.AreEqual(HcaStatus.Error, dataResult.Status);
                Assert.AreEqual(expErrorMessage, dataResult.Error,
                    $"Expected {nameof(dataResult.Error)} text: {expErrorMessage}. Actual:{dataResult.Error}");
                Assert.That(dataResult.Errors.All(x => x == expErrorMessage));
            });
        }
    }
}