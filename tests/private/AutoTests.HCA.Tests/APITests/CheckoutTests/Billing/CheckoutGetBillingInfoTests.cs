using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CheckoutTests
{
    public class CheckoutGetBillingInfoTests : BaseCheckoutTest
    {
        public CheckoutGetBillingInfoTests(HcaUserRole userRole) : base(userRole)
        {
        }

        [Test]
        public void T1_GETBillingInfoRequest_Successful()
        {
            // Arrange, Act
            var result = ApiContext.Checkout.GetBillingInfo();

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() => { result.VerifyOkResponseData(); });
        }

        [Test]
        public void T2_GETBillingInfoRequest_CartIsEmpty_VerifyResponse()
        {
            // Arrange

            // Act
            var result = ApiContext.Checkout.GetBillingInfo();

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();
                var model = result.OkResponseData.Data;
                ExtendedAssert.Null(model.PaymentMethods, nameof(model.PaymentMethods));
                ExtendedAssert.Null(model.PaymentClientToken, nameof(model.PaymentClientToken));
                ExtendedAssert.Null(model.PaymentOptions, nameof(model.PaymentOptions));
            });
        }

        [Test]
        public void T3_GETBillingInfoRequest_CartIsNotEmpty_VerifyResponse()
        {
            // Arrange
            FillCart();

            // Act
            var result = ApiContext.Checkout.GetBillingInfo();

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();
                var model = result.OkResponseData.Data;
                ExtendedAssert.NotNullOrWhiteSpace(model.PaymentClientToken, nameof(model.PaymentClientToken));
                ExtendedAssert.NullOrEmpty(model.PaymentMethods, nameof(model.PaymentMethods));
                ExtendedAssert.NotNullOrEmpty(model.PaymentOptions, nameof(model.PaymentOptions));

                foreach (var option in model.PaymentOptions)
                {
                    ExtendedAssert.NotNullOrWhiteSpace(option.Description, nameof(option.Description));
                    ExtendedAssert.NotNullOrWhiteSpace(option.PaymentOptionTypeName, nameof(option.PaymentOptionTypeName));
                }
            });
        }
    }
}
