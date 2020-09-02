using System.Collections.Generic;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Cart;
using AutoTests.HCA.Core.Common.Settings.Promotions;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CartTests.Promotions
{
    public class RemovePromotionTests : BaseCartApiTest
    {
        [SetUp]
        public new void SetUp()
        {
            HcaService.AddCartLines(new CartLinesRequest(AddingProduct.ProductId, 50, AddingProduct.VariantId))
                .CheckSuccessfulResponse();
            UserManager.CleanPromotions();
            HcaService.AddPromoCode(new PromoCodeRequest(DefPromotion.Code));
        }

        public RemovePromotionTests(HcaUserRole userRole) : base(userRole)
        {
        }

        protected readonly HcaPromotionTestsDataSettings DefPromotion =
            TestsData.GetPromotion(HcaPromotionName.Cart15PctOffCouponPromotion);

        [Test(Description = "Checks if a coupon can be deleted.")]
        public void T1_DELETEPromoCodesRequest_ValidPromoCode_Successful()
        {
            //Arrange
            var removablePromoCode = new PromoCodeRequest(DefPromotion.DisplayCartText);

            // Act
            var response = HcaService.RemovePromoCode(removablePromoCode);

            // Assert
            response.CheckSuccessfulResponse();

            Assert.Multiple(() =>
            {
                response.VerifyOkResponseData();

                // Adjustments
                ExtendedAssert.NullOrEmpty(response.OkResponseData.Data.Adjustments,
                    nameof(response.OkResponseData.Data.Adjustments));

                // Price
                VerifyPrice(response.OkResponseData.Data.Price, null);
            });
        }

        [Test(Description = "Verifies that after deleting an exclusive coupon, the previous coupon is restored.")]
        public void T2_DELETEPromoCodesRequest_NonExclusiveAndExclusiveCoupon_NonExclusiveCouponApplied()
        {
            // SetUp
            var excPromotion = TestsData.GetPromotion(HcaPromotionName.Cart5PctOffExclusiveCouponPromotion);
            HcaService.AddPromoCode(new PromoCodeRequest(excPromotion.Code))
                .CheckSuccessfulResponse();

            //Arrange
            var removablePromoCode = new PromoCodeRequest(excPromotion.DisplayCartText);

            // Act
            var response = HcaService.RemovePromoCode(removablePromoCode);

            // Assert
            response.CheckSuccessfulResponse();

            Assert.Multiple(() =>
            {
                response.VerifyOkResponseData();

                // Adjustments
                VerifyAdjustments(response.OkResponseData.Data.Adjustments,
                    new List<HcaPromotionTestsDataSettings> {DefPromotion});

                // Price
                VerifyPrice(response.OkResponseData.Data.Price, new List<HcaDiscount> {DefPromotion.Discount});
            });
        }
    }
}