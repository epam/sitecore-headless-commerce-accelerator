﻿using System.Linq;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Cart;
using AutoTests.HCA.Core.API.Services.HcaService;
using AutoTests.HCA.Core.BaseTests;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CartTests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture(Description = "Cart Tests")]
    [ApiTest]
    public class CartTests : BaseHcaApiTest
    {
        private readonly IHcaApiService _hcaService = TestsHelper.CreateHcaApiClient();

        public const string PRODUCT_ID = "6042079";
        public const string VARIANT_ID = "56042079";

        [Test]
        [Description("Check that the basket is empty")]
        [TestCase("151235", 10, VARIANT_ID, "Invalid productId")]
        [TestCase(PRODUCT_ID, 110, VARIANT_ID,
            "Invalid quantity. Quantity must be greater than '0' and less than '100.'")]
        [TestCase(PRODUCT_ID, 110, "FDFDF", "Invalid variantId")]
        public void _01_AddNonExistentProductToCartLinesTest(string productId, int quantity, string variantId,
            string expErrorMessage)
        {
            // Arrange
            var cartLines = new AddCartLinesRequest
            {
                ProductId = productId,
                Quantity = quantity,
                VariantId = variantId
            };

            // Act
            var response = _hcaService.AddCartLines(cartLines);

            // Assert
            Assert.False(response.IsSuccessful, "The GetProducts POST request is passed.");
            var dataResult = response.Errors;
            Assert.AreEqual(HcaStatus.Error, dataResult.Status);
            Assert.AreEqual(expErrorMessage, dataResult.Error,
                $"Expected {nameof(dataResult.Error)} text: {expErrorMessage}. Actual:{dataResult.Error}");
            Assert.AreEqual(expErrorMessage, dataResult.ExceptionMessage,
                $"Expected {nameof(dataResult.ExceptionMessage)} text: {expErrorMessage}. Actual:{dataResult.ExceptionMessage}");
            Assert.That(dataResult.Errors.Any(x => x == expErrorMessage));
        }
    }
}