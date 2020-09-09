using System.Collections.Generic;
using System.Linq;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Cart;
using AutoTests.HCA.Core.Common.Settings.Promotions;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CartTests.Promotions
{
    public class AddPromotionTests : BaseCartApiTest
    {
        public AddPromotionTests(HcaUserRole userRole) : base(userRole)
        {
        }

        [Test(Description = "Checks the applicability of the coupon for the entire basket.")]
        [TestCase(HcaPromotionName.Cart10OffCouponPromotion)]
        [TestCase(HcaPromotionName.Cart5OffExclusiveCouponPromotion)]
        [TestCase(HcaPromotionName.Cart10PctOffCouponPromotion)]
        [TestCase(HcaPromotionName.Cart15PctOffCouponPromotion)]
        [TestCase(HcaPromotionName.Cart5PctOffExclusiveCouponPromotion)]
        public void T1_POSTPromoCodesRequest_PromotionWithAllProductsScope_Successful(HcaPromotionName promotion)
        {
            //SetUp
            ApiHelper.AddProductToCart(AddingProduct.ProductId, 50, AddingProduct.VariantId);
            ApiHelper.CleanPromotions();

            // Arrange
            var expPromotion = TestsData.GetPromotion(promotion);
            var promoCode = new PromoCodeRequest
            {
                PromoCode = expPromotion.Code
            };

            // Act
            var result = ApiContext.Cart.AddPromoCode(promoCode);

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();

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
                VerifyPrice(price, new List<HcaDiscount> {expPromotion.Discount});
            });
        }

        [Test(Description = "Checks the ability to use multiple coupons at once.")]
        public void T2_POSTPromoCodesRequest_SeveralNotExclusivePromotions_AllDiscountsApply()
        {
            //SetUp
            ApiHelper.AddProductToCart(AddingProduct.ProductId, 50, AddingProduct.VariantId);
            ApiHelper.CleanPromotions();

            // Arrange
            var promotions = new List<HcaPromotionTestsDataSettings>
            {
                TestsData.GetPromotion(HcaPromotionName.Cart15PctOffCouponPromotion),
                TestsData.GetPromotion(HcaPromotionName.Cart10PctOffCouponPromotion),
                TestsData.GetPromotion(HcaPromotionName.Cart10OffCouponPromotion)
            };

            // Act
            ApiContext.Cart.AddPromoCode(new PromoCodeRequest {PromoCode = promotions.ElementAt(0).Code})
                .CheckSuccessfulResponse();
            ApiContext.Cart.AddPromoCode(new PromoCodeRequest {PromoCode = promotions.ElementAt(1).Code})
                .CheckSuccessfulResponse();
            var resultAfterAdding3Promo =
                ApiContext.Cart.AddPromoCode(new PromoCodeRequest {PromoCode = promotions.ElementAt(2).Code});

            // Assert
            resultAfterAdding3Promo.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                resultAfterAdding3Promo.VerifyOkResponseData();

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
            ApiHelper.AddProductToCart(AddingProduct.ProductId, 50, AddingProduct.VariantId);
            ApiHelper.CleanPromotions();

            // Arrange
            var nonExclusivePromotion = TestsData.GetPromotion(HcaPromotionName.Cart15PctOffCouponPromotion);
            var exclusivePromotion = TestsData.GetPromotion(HcaPromotionName.Cart5PctOffExclusiveCouponPromotion);
            ApiContext.Cart.AddPromoCode(new PromoCodeRequest {PromoCode = nonExclusivePromotion.Code})
                .CheckSuccessfulResponse();

            // Act
            var addExclusivePromoResult =
                ApiContext.Cart.AddPromoCode(new PromoCodeRequest {PromoCode = exclusivePromotion.Code});

            // Assert
            addExclusivePromoResult.CheckSuccessfulResponse();

            Assert.Multiple(() =>
            {
                addExclusivePromoResult.VerifyOkResponseData();

                // Adjustments
                VerifyAdjustments(addExclusivePromoResult.OkResponseData.Data.Adjustments,
                    new List<HcaPromotionTestsDataSettings> {exclusivePromotion});

                // Price
                VerifyPrice(addExclusivePromoResult.OkResponseData.Data.Price,
                    new List<HcaDiscount> {exclusivePromotion.Discount});
            });
        }

        [Test(Description = "Checks the inadmissibility of using two identical coupons.")]
        public void T4_POSTPromoCodesRequest_TheSamePromotions_BadRequest()
        {
            //SetUp
            ApiHelper.AddProductToCart(AddingProduct.ProductId, 50, AddingProduct.VariantId);
            ApiHelper.CleanPromotions();

            // Arrange
            const string expErrorMessage = "The Item you are trying to add already exists.";
            var promotionCode = TestsData.GetPromotion(HcaPromotionName.Cart10OffCouponPromotion).Code;
            var promoCode = new PromoCodeRequest(promotionCode);

            // Act
            ApiContext.Cart.AddPromoCode(promoCode).CheckSuccessfulResponse();
            var response = ApiContext.Cart.AddPromoCode(promoCode);

            // Assert
            response.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { response.VerifyErrors(expErrorMessage); });
        }

        [Test(Description =
            "Checks the inadmissibility of using a coupon if the total amount of the basket is less than the min amount set for the coupon.")]
        public void T5_POSTPromoCodesRequest_InsufficientBasketAmount_BadRequest()
        {
            //SetUp
            var product = TestsData.GetProduct(productId: "6042062");
            ApiHelper.AddProductToCart(product.ProductId, 1, product.VariantId);
            ApiHelper.CleanPromotions();

            // Arrange
            var expErrorMessage =
                $"You can't use {HcaPromotionName.Cart10OffCouponPromotion} coupon because you have insufficient basket amount.";
            var promoCode =
                new PromoCodeRequest(TestsData.GetPromotion(HcaPromotionName.Cart10OffCouponPromotion).Code);

            // Act
            var response = ApiContext.Cart.AddPromoCode(promoCode);

            // Assert
            response.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { response.VerifyErrors(expErrorMessage); });
        }
    }
}