using System.Collections.Generic;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Cart;
using AutoTests.HCA.Core.Common.Settings.Products;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CartTests
{
    public class AddProductToCartTests : BaseCartApiTest
    {
        public AddProductToCartTests(HcaUserRole userRole) : base(userRole)
        {
        }

        [Test(Description = "A test that checks the server's response after adding the product to the shopping cart.")]
        public void T1_POSTCartLinesRequest_ValidProduct_VerifyResponse()
        {
            // Arrange, Act
            var result = HcaService.AddCartLines(AddingProduct);

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();

                VerifyCartResponse("POST CartLines", new List<CartLinesRequest> {AddingProduct},
                    result.OkResponseData.Data);
            });
        }

        [Test(Description = "A test that checks if a product has been added to the cart.")]
        public void T2_POSTCartLinesRequest_ValidProduct_ProductHasBeenAddedToCart()
        {
            // Arrange, Act
            var addProductResult = HcaService.AddCartLines(AddingProduct);
            var getProductResult = HcaService.GetCart();

            // Assert
            addProductResult.CheckSuccessfulResponse();
            getProductResult.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                addProductResult.VerifyOkResponseData();
                getProductResult.VerifyOkResponseData();

                VerifyCartResponse("POST CartLines", new List<CartLinesRequest> {AddingProduct},
                    getProductResult.OkResponseData.Data);
            });
        }

        [Test(Description = "A test that checks if a product(OutOfStockStatus) has not been added to the cart.")]
        public void T3_POSTCartLinesRequest_ProductWithOutOfStockStatus_ProductHasNotBeenAddedToCart()
        {
            // Arrange
            const string expErrorMessage = "Product which is out of stock can't be added to cart";
            var prod = TestsData.GetProduct(HcaProductStatus.OutOfStock);

            // Act
            var addProductResult = HcaService.AddCartLines(new CartLinesRequest(prod.ProductId, 1, prod.VariantId));
            HcaService.GetCart();
            // Assert
            addProductResult.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { addProductResult.VerifyErrors(expErrorMessage); });
        }

        [Test(Description =
            "The test checks if two identical products are added to the cart if the number of products is updated.")]
        public void T4_POSTCartLinesRequest_TwoIdenticalProducts_ProductsNumberHasBeenUpdated()
        {
            // Arrange, Act
            var addProductResult1 = HcaService.AddCartLines(AddingProduct);
            var addProductResult2 = HcaService.AddCartLines(AddingProduct);
            var getProductResult = HcaService.GetCart();

            // Assert
            addProductResult1.CheckSuccessfulResponse();
            addProductResult2.CheckSuccessfulResponse();
            getProductResult.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                addProductResult1.VerifyOkResponseData();
                addProductResult2.VerifyOkResponseData();
                getProductResult.VerifyOkResponseData();

                VerifyCartResponse("POST CartLines", new List<CartLinesRequest> {AddingProduct, AddingProduct},
                    getProductResult.OkResponseData.Data);
            });
        }

        [Test(Description =
            "The test will check if the correct message is returned by the server with invalid productId.")]
        public void T5_POSTCartLinesRequest_InvalidProductId_BadRequest()
        {
            // Arrange
            const string expErrorMessage = "Product Not Found.";
            var cartLines = new CartLinesRequest
            {
                ProductId = Product.ProductId + 1,
                Quantity = 10,
                VariantId = Product.VariantId
            };

            // Act
            var response = HcaService.AddCartLines(cartLines);

            // Assert
            response.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { response.VerifyErrors(expErrorMessage); });
        }

        [Test(Description =
            "The test will check if the correct message is returned by the server with invalid quantity.")]
        public void T6_POSTCartLinesRequest_InvalidQuantity_BadRequest()
        {
            // Arrange
            const string expErrorMessage = "Invalid quantity. Quantity must be greater than '0' and less than '100.'";
            var cartLines = new CartLinesRequest
            {
                ProductId = Product.ProductId,
                Quantity = 110,
                VariantId = Product.VariantId
            };

            // Act
            var response = HcaService.AddCartLines(cartLines);

            // Assert
            response.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { response.VerifyErrors(expErrorMessage); });
        }

        [Test(Description =
            "The test will check if the correct message is returned by the server with invalid variantId.")]
        public void T7_POSTCartLinesRequest_InvalidVariantId_BadRequest()
        {
            // Arrange
            const string expErrorMessage = "VariantId Not Found.";
            var cartLines = new CartLinesRequest
            {
                ProductId = Product.ProductId,
                Quantity = 10,
                VariantId = Product.VariantId + 1
            };

            // Act
            var response = HcaService.AddCartLines(cartLines);

            // Assert
            response.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { response.VerifyErrors(expErrorMessage); });
        }
    }
}