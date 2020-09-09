using System.Collections.Generic;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Cart;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CartTests
{
    public class UpdateCartLineTests : BaseCartApiTest
    {
        [SetUp]
        public new void SetUp()
        {
            ApiHelper.AddProductToCart(AddingProduct.ProductId, AddingProduct.Quantity, AddingProduct.VariantId);
        }

        protected static CartLinesRequest UpdatedProduct = new CartLinesRequest
        {
            ProductId = AddingProduct.ProductId,
            Quantity = 1,
            VariantId = AddingProduct.VariantId
        };

        public UpdateCartLineTests(HcaUserRole userRole) : base(userRole)
        {
        }

        [Test(Description =
            "A test that checks the server's response after updating the product to the shopping cart.")]
        public void T1_PUTCartLinesRequest_ValidProduct_VerifyResponse()
        {
            // Arrange, Act
            var updateResult = ApiContext.Cart.UpdateCartLines(UpdatedProduct);

            // Assert
            updateResult.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                updateResult.VerifyOkResponseData();
                VerifyCartResponse("PUTCartLinesRequest", new List<CartLinesRequest> {UpdatedProduct},
                    updateResult.OkResponseData.Data);
            });
        }

        [Test(Description =
            "The test checks if the quantity of the product in the cart has been updated after changing it.")]
        public void T2_PUTCartLinesRequest_ValidProduct_ProductHasBeenUpdate()
        {
            // Arrange, Act
            var updateResult = ApiContext.Cart.UpdateCartLines(UpdatedProduct);
            var getCartAfterUpdate = ApiContext.Cart.GetCart();

            // Assert
            updateResult.CheckSuccessfulResponse();
            getCartAfterUpdate.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                // UpdateRequestResult
                updateResult.VerifyOkResponseData();

                // GetCartAfterUpdateResult
                getCartAfterUpdate.VerifyOkResponseData();

                VerifyCartResponse("PUTCartLinesRequest", new List<CartLinesRequest> {UpdatedProduct},
                    getCartAfterUpdate.OkResponseData.Data);
            });
        }
    }
}