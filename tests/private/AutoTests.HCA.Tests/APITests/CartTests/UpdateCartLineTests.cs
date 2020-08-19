using System.Collections.Generic;
using System.Net;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Cart;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CartTests
{
    [Parallelizable(ParallelScope.None)]
    public class UpdateCartLineTests : BaseCartApiTest
    {
        protected static CartLinesRequest UpdatedProduct = new CartLinesRequest
        {
            ProductId = AddingProduct.ProductId,
            Quantity = 1,
            VariantId = AddingProduct.VariantId
        };

        public UpdateCartLineTests(HcaUserRole userRole) : base(userRole) { }

        [SetUp]
        public new void SetUp()
        {
            HcaService.AddCartLines(AddingProduct);
        }

        [Test(Description = "A test that checks the server's response after updating the product to the shopping cart.")]
        public void T1_PUTCartLinesRequest_ValidProduct_VerifyResponse()
        {
            // Arrange, Act
            var updateResult = HcaService.UpdateCartLines(UpdatedProduct);

            // Assert
            Assert.True(updateResult.IsSuccessful, "The PUTCartLinesRequest request isn't passed.");
            Assert.Multiple(() =>
            {
                ExtendedAssert.AreEqual(HttpStatusCode.OK, updateResult.StatusCode, nameof(updateResult.StatusCode));
                ExtendedAssert.NotNull(updateResult.OkResponseData, nameof(updateResult.OkResponseData));
                ExtendedAssert.AreEqual(HcaStatus.Ok, updateResult.OkResponseData.Status, nameof(updateResult.OkResponseData.Status));

                VerifyCartResponse("PUTCartLinesRequest", new List<CartLinesRequest> { UpdatedProduct }, updateResult.OkResponseData.Data);
            });

        }

        [Test(Description = "The test checks if the quantity of the product in the cart has been updated after changing it.")]
        public void T1_PUTCartLinesRequest_ValidProduct_ProductHasBeenUpdate()
        {
            // Arrange, Act
            var updateResult = HcaService.UpdateCartLines(UpdatedProduct);
            var getCartAfterUpdate = HcaService.GetCart();

            // Assert
            Assert.True(updateResult.IsSuccessful, "The PUTCartLinesRequest request isn't passed.");
            Assert.True(getCartAfterUpdate.IsSuccessful, "The GETCartRequest request isn't passed.");
            Assert.Multiple(() =>
            {
                // UpdateRequestResult
                ExtendedAssert.AreEqual(HttpStatusCode.OK, updateResult.StatusCode, nameof(updateResult.StatusCode));
                ExtendedAssert.NotNull(updateResult.OkResponseData, nameof(updateResult.OkResponseData));
                ExtendedAssert.AreEqual(HcaStatus.Ok, updateResult.OkResponseData.Status, nameof(updateResult.OkResponseData.Status));

                // GetCartAfterUpdateResult
                ExtendedAssert.AreEqual(HttpStatusCode.OK, getCartAfterUpdate.StatusCode, nameof(getCartAfterUpdate.StatusCode));
                ExtendedAssert.NotNull(getCartAfterUpdate.OkResponseData, nameof(getCartAfterUpdate.OkResponseData));
                ExtendedAssert.AreEqual(HcaStatus.Ok, getCartAfterUpdate.OkResponseData.Status, nameof(getCartAfterUpdate.OkResponseData.Status));

                VerifyCartResponse("PUTCartLinesRequest", new List<CartLinesRequest> { UpdatedProduct }, getCartAfterUpdate.OkResponseData.Data);
            });

        }
    }
}
