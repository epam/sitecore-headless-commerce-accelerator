using System.Collections.Generic;
using System.Linq;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Cart;
using AutoTests.HCA.Core.Common.Settings.Promotions;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CartTests.Promotions
{
    public class AddPromotionTests : BaseCartApiTest
    {
        public AddPromotionTests(HcaUserRole userRole) : base(userRole) { }

        [Test(Description = "Checks the applicability of the coupon for the entire basket.")]
        [TestCase(HcaPromotionName.Cart10OffCouponPromotion)]
        [TestCase(HcaPromotionName.Cart5OffExclusiveCouponPromotion)]
        [TestCase(HcaPromotionName.Cart10PctOffCouponPromotion)]
        [TestCase(HcaPromotionName.Cart15PctOffCouponPromotion)]
        [TestCase(HcaPromotionName.Cart5PctOffExclusiveCouponPromotion)]
        public void T1_POSTPromoCodesRequest_PromotionWithAllProductsScope_Successful(HcaPromotionName promotion)
        {
            //SetUp
            HcaService.AddCartLines(new CartLinesRequest(AddingProduct.ProductId, 50, AddingProduct.VariantId));
            UserManager.CleanPromotions();

            // Arrange
            var expPromotion = TestsData.GetPromotion(promotion);
            var promoCode = new PromoCodeRequest
            {
                PromoCode = expPromotion.Code
            };

            // Act
            var result = HcaService.AddPromoCode(promoCode);

            // Assert
            Assert.True(result.IsSuccessful, "The POST PromoCodes request isn't passed.");
            Assert.Multiple(() =>
            {
                result.VerifyResponseData();

                // Adjustments
                var adjustments = result.OkResponseData.Data.Adjustments;
                ExtendedAssert.NotNullOrEmpty(adjustments, nameof(result.OkResponseData.Data));
                ExtendedAssert.AreEqual(1, adjustments.Count, nameof(adjustments));
                // Adjustments -> Adjustment
                var adjustment = adjustments.FirstOrDefault();
                ExtendedAssert.NotNullOrWhiteSpace(adjustment, nameof(adjustment));
                ExtendedAssert.AreEqual(expPromotion.DisplayCartText, adjustment, nameof(adjustment));

                // Price
                var price = result.OkResponseData.Data.Price;
                ExtendedAssert.NotNull(price, nameof(result.OkResponseData.Data.Price));
                VerifyPrice(price, new List<HcaDiscount> { expPromotion.Discount });
            });
        }

        [Test(Description = "Checks the ability to use multiple coupons at once.")]
        public void T2_POSTPromoCodesRequest_SeveralNotExclusivePromotions_AllDiscountsApply()
        {
            //SetUp
            HcaService.AddCartLines(new CartLinesRequest(AddingProduct.ProductId, 50, AddingProduct.VariantId));
            UserManager.CleanPromotions();

            // Arrange
            var promotions = new List<HcaPromotionTestsDataSettings>
            {
                TestsData.GetPromotion(HcaPromotionName.Cart15PctOffCouponPromotion),
                TestsData.GetPromotion(HcaPromotionName.Cart10PctOffCouponPromotion),
                TestsData.GetPromotion(HcaPromotionName.Cart10OffCouponPromotion)
            };

            // Act
            HcaService.AddPromoCode(new PromoCodeRequest { PromoCode = promotions.ElementAt(0).Code }).CheckSuccessfulResponse();
            HcaService.AddPromoCode(new PromoCodeRequest { PromoCode = promotions.ElementAt(1).Code }).CheckSuccessfulResponse();
            var resultAfterAdding3Promo = HcaService.AddPromoCode(new PromoCodeRequest { PromoCode = promotions.ElementAt(2).Code });

            // Assert
            resultAfterAdding3Promo.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                resultAfterAdding3Promo.VerifyResponseData();

                // Adjustments
                VerifyAdjustments(resultAfterAdding3Promo.OkResponseData.Data.Adjustments, promotions);

                // Price
                VerifyPrice(resultAfterAdding3Promo.OkResponseData.Data.Price, promotions.Select(x => x.Discount));
            });
        }

        [Test(Description = "Verifies removal of non-exclusive coupon after adding exclusive.")]
        public void T3_POSTPromoCodesRequest_NotExclusiveAndExclusivePromotions_OnlyExclusiveDiscountsApply()
        {
            //SetUp
            HcaService.AddCartLines(new CartLinesRequest(AddingProduct.ProductId, 50, AddingProduct.VariantId));
            UserManager.CleanPromotions();

            // Arrange
            var nonExclusivePromotion = TestsData.GetPromotion(HcaPromotionName.Cart15PctOffCouponPromotion);
            var exclusivePromotion = TestsData.GetPromotion(HcaPromotionName.Cart5PctOffExclusiveCouponPromotion);
            HcaService.AddPromoCode(new PromoCodeRequest { PromoCode = nonExclusivePromotion.Code }).CheckSuccessfulResponse();

            // Act
            var addExclusivePromoResult = HcaService.AddPromoCode(new PromoCodeRequest { PromoCode = exclusivePromotion.Code });

            // Assert
            addExclusivePromoResult.CheckSuccessfulResponse();

            Assert.Multiple(() =>
            {
                addExclusivePromoResult.VerifyResponseData();

                // Adjustments
                VerifyAdjustments(addExclusivePromoResult.OkResponseData.Data.Adjustments,
                    new List<HcaPromotionTestsDataSettings> { exclusivePromotion });

                // Price
                VerifyPrice(addExclusivePromoResult.OkResponseData.Data.Price,
                    new List<HcaDiscount> { exclusivePromotion.Discount });
            });
        }

        [Test(Description = "Checks the inadmissibility of using two identical coupons.")]
        public void T4_POSTPromoCodesRequest_TheSamePromotions_BadRequest()
        {
            //SetUp
            HcaService.AddCartLines(new CartLinesRequest(AddingProduct.ProductId, 50, AddingProduct.VariantId));
            UserManager.CleanPromotions();

            // Arrange
            const string expErrorMessage = "The Item you are trying to add already exists.";
            var promotionCode = TestsData.GetPromotion(HcaPromotionName.Cart10OffCouponPromotion).Code;
            var promoCode = new PromoCodeRequest(promotionCode);

            // Act
            HcaService.AddPromoCode(promoCode);
            var response = HcaService.AddPromoCode(promoCode);

            // Assert
            Assert.False(response.IsSuccessful, "The bad POST PromoCodes Request request is passed.");
            Assert.Multiple(() =>
            {
                var dataResult = response.Errors;
                Assert.AreEqual(HcaStatus.Error, dataResult.Status);
                Assert.AreEqual(expErrorMessage, dataResult.Error,
                    $"Expected {nameof(dataResult.Error)} text: {expErrorMessage}. Actual:{dataResult.Error}");
                Assert.That(dataResult.Errors.All(x => x == expErrorMessage));
            });
        }

        [Test(Description = "Checks the inadmissibility of using a coupon if the total amount of the basket is less than the min amount set for the coupon.")]
        public void T5_POSTPromoCodesRequest_InsufficientBasketAmount_BadRequest()
        {
            //SetUp
            var product = TestsData.GetProduct(productId: "6042062");
            HcaService.AddCartLines(new CartLinesRequest(product.ProductId, 1, product.VariantId));
            UserManager.CleanPromotions();

            // Arrange
            var expErrorMessage = $"You can't use {HcaPromotionName.Cart10OffCouponPromotion} coupon because you have insufficient basket amount.";
            var promoCode = new PromoCodeRequest(TestsData.GetPromotion(HcaPromotionName.Cart10OffCouponPromotion).Code);

            // Act
            var response = HcaService.AddPromoCode(promoCode);

            // Assert
            Assert.False(response.IsSuccessful, "The bad POST PromoCodes Request request is passed.");
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
