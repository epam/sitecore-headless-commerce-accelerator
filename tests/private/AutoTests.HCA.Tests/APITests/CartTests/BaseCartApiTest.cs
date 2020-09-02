using System.Collections.Generic;
using System.Linq;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.Helpers;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Cart;
using AutoTests.HCA.Core.API.Services.HcaService;
using AutoTests.HCA.Core.BaseTests;
using AutoTests.HCA.Core.Common.Settings.Products;
using AutoTests.HCA.Core.Common.Settings.Promotions;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CartTests
{
    [TestFixture(HcaUserRole.Guest, Description = "Cart Tests for Guest")]
    [TestFixture(HcaUserRole.User, Description = "Cart Tests for User")]
    [ApiTest]
    [Parallelizable(ParallelScope.None)]
    public class BaseCartApiTest : BaseHcaApiTest
    {
        [SetUp]
        public void SetUp()
        {
            User = TestsData.GetUser(_userRole);
            HcaService = TestsHelper.CreateHcaApiClient();
            UserManager = TestsHelper.CreateUserManagerHelper(User, HcaService);
            UserManager.CleanCart();
        }

        [TearDown]
        public override void TearDown()
        {
            UserManager.CleanCart();
        }

        protected static readonly ProductTestsDataSettings Product = TestsData.GetProduct();

        protected static readonly CartLinesRequest AddingProduct = new CartLinesRequest
        {
            ProductId = Product.ProductId,
            Quantity = 1,
            VariantId = Product.VariantId
        };

        private readonly HcaUserRole _userRole;

        protected IHcaApiService HcaService;
        protected UserManagerHelper UserManager;
        protected HcaUserTestsDataSettings User;

        protected readonly IEnumerable<CartLinesRequest> ProductsCollection;

        public BaseCartApiTest(HcaUserRole userRole)
        {
            _userRole = userRole;

            var prod1 = TestsData.GetProduct();
            var prod2 = TestsData.GetProduct();
            var prod3 = TestsData.GetProduct();

            ProductsCollection = new List<CartLinesRequest>
            {
                new CartLinesRequest
                {
                    ProductId = prod1.ProductId,
                    Quantity = 1,
                    VariantId = prod1.VariantId
                },
                new CartLinesRequest
                {
                    ProductId = prod2.ProductId,
                    Quantity = 1,
                    VariantId = prod2.VariantId
                },
                new CartLinesRequest
                {
                    ProductId = prod3.ProductId,
                    Quantity = 1,
                    VariantId = prod3.VariantId
                }
            };
        }

        protected void VerifyCartResponse(string methodName, IEnumerable<CartLinesRequest> expectedCartLines,
            CartResult cartResult)
        {
            var expectedCart = expectedCartLines.GroupBy(x => x.VariantId).ToList();

            // Data
            Assert.NotNull(cartResult,
                $"The '{methodName}' response should contain information about the current state of the cart.");
            ExtendedAssert.NotNull(cartResult.Id, nameof(cartResult.Id));
            if (User.Role == HcaUserRole.User)
            {
                ExtendedAssert.NotNull(cartResult.Addresses, $"{nameof(cartResult.Addresses)} for {User.Role}.");
                ExtendedAssert.NotNull(cartResult.Email, $"{nameof(cartResult.Email)} for {User.Role}.");
            }
            else
            {
                ExtendedAssert.Empty(cartResult.Addresses, $"{nameof(cartResult.Addresses)} for {User.Role}.");
                ExtendedAssert.Empty(cartResult.Email, $"{nameof(cartResult.Email)} for {User.Role}.");
            }

            // Data -> CartLines
            var cartLines = cartResult.CartLines;
            if (expectedCart.Any())
            {
                if (cartLines == null || !cartLines.Any())
                {
                    Assert.Fail($"{nameof(cartLines)} can't be empty.");
                }
                else
                {
                    Assert.AreEqual(expectedCart.Count, cartLines.Count,
                        "The cart doesn't contain all expected products.");

                    foreach (var cartItem in cartLines)
                    {
                        // Data -> CartLines -> CartItem -> Product, Quantity, Variant
                        ExtendedAssert.NotNull(cartItem, nameof(cartItem));
                        ExtendedAssert.NotNull(cartItem.Product, nameof(cartItem.Product));
                        ExtendedAssert.NotNull(cartItem.Variant, nameof(cartItem.Variant));

                        var expProduct = expectedCart.First(x => x.Key == cartItem.Variant.VariantId);

                        ExtendedAssert.AreEqual(expProduct.First().ProductId, cartItem?.Product.ProductId,
                            nameof(cartItem.Product.ProductId));
                        ExtendedAssert.AreEqual(expProduct.Sum(x => x.Quantity), cartItem.Quantity,
                            nameof(cartItem.Quantity));
                        ExtendedAssert.AreEqual(expProduct.Key, cartItem.Variant.VariantId, nameof(cartItem.Variant));

                        // Data -> CartLines -> CartItem -> Price
                        ExtendedAssert.NotNull(cartItem.Price, nameof(cartItem.Price));
                        ExtendedAssert.NotNull(cartItem.Price.CurrencyCode, nameof(cartItem.Price.CurrencyCode));
                        ExtendedAssert.NotNull(cartItem.Price.CurrencySymbol, nameof(cartItem.Price.CurrencySymbol));
                        ExtendedAssert.NotNull(cartItem.Price.HandlingTotal, nameof(cartItem.Price.HandlingTotal));
                        ExtendedAssert.NotNull(cartItem.Price.ShippingTotal, nameof(cartItem.Price.ShippingTotal));
                        ExtendedAssert.NotNull(cartItem.Price.Subtotal, nameof(cartItem.Price.Subtotal));
                        ExtendedAssert.NotNull(cartItem.Price.TaxTotal, nameof(cartItem.Price.TaxTotal));
                        ExtendedAssert.NotNull(cartItem.Price.Total, nameof(cartItem.Price.Total));
                        ExtendedAssert.NotNull(cartItem.Price.TotalSavings, nameof(cartItem.Price.TotalSavings));
                        ExtendedAssert.AreEqual(cartItem.Product.AdjustedPrice * cartItem.Quantity,
                            cartItem.Price.Subtotal, nameof(cartItem.Price.Subtotal));
                    }
                }
            }
            else
            {
                ExtendedAssert.Empty(cartLines, nameof(cartLines));
            }

            ExtendedAssert.AreEqual(cartLines.Sum(x => x.Price.Subtotal), cartResult.Price.Subtotal,
                nameof(cartResult.Price.Subtotal));
            ExtendedAssert.AreEqual(cartResult.Price.Subtotal - cartResult.Price.TotalSavings, cartResult.Price.Total,
                nameof(cartResult.Price.Total));
        }

        protected void VerifyPrice(TotalPrice totalPrice, IEnumerable<HcaDiscount> discounts)
        {
            ExtendedAssert.NotNull(totalPrice, nameof(totalPrice));
            ExtendedAssert.NotNull(totalPrice.CurrencyCode, nameof(totalPrice.CurrencyCode));
            ExtendedAssert.NotNull(totalPrice.CurrencySymbol, nameof(totalPrice.CurrencySymbol));
            ExtendedAssert.NotNull(totalPrice.HandlingTotal, nameof(totalPrice.HandlingTotal));
            ExtendedAssert.NotNull(totalPrice.ShippingTotal, nameof(totalPrice.ShippingTotal));
            ExtendedAssert.NotNull(totalPrice.Subtotal, nameof(totalPrice.Subtotal));
            ExtendedAssert.NotNull(totalPrice.TaxTotal, nameof(totalPrice.TaxTotal));
            ExtendedAssert.NotNull(totalPrice.Total, nameof(totalPrice.Total));
            ExtendedAssert.NotNull(totalPrice.TotalSavings, nameof(totalPrice.TotalSavings));

            if (discounts == null || !discounts.Any())
            {
                ExtendedAssert.AreEqual(0, totalPrice.TotalSavings, nameof(totalPrice.TotalSavings));
                Assert.True(totalPrice.Subtotal == totalPrice.Total,
                    $"{nameof(totalPrice.Subtotal)} should be equal {nameof(totalPrice.Total)}");
            }
            else
            {
                var expTotalPrice = totalPrice.Subtotal.Value;
                foreach (var discount in discounts)
                    if (discount.DiscountValueType == HcaDiscountValueType.InDollars)
                        expTotalPrice -= discount.Discount;
                    else if (discount.DiscountValueType == HcaDiscountValueType.InPercents)
                        expTotalPrice -= expTotalPrice * (discount.Discount / 100m);

                expTotalPrice = expTotalPrice.RoundUpMoney();

                ExtendedAssert.AreEqual(totalPrice.Subtotal.Value - expTotalPrice, totalPrice.TotalSavings.Value,
                    nameof(totalPrice.TotalSavings));
                ExtendedAssert.AreEqual(expTotalPrice, totalPrice.Total.Value, nameof(totalPrice.Total));
            }
        }

        protected void VerifyAdjustments(IEnumerable<string> adjustments,
            IEnumerable<HcaPromotionTestsDataSettings> promotions)
        {
            // Adjustments
            ExtendedAssert.NotNullOrEmpty(adjustments, nameof(adjustments));
            ExtendedAssert.AreEqual(promotions.Count(), adjustments.Count(), nameof(adjustments));

            // Adjustments -> Adjustment
            foreach (var adjustment in adjustments)
            {
                ExtendedAssert.NotNullOrWhiteSpace(adjustment, nameof(adjustment));
                ExtendedAssert.NotNull(promotions.Any(x => x.DisplayCartText == adjustment), nameof(adjustment));
            }
        }
    }
}