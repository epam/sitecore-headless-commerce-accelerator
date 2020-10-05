using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Checkout.Payment;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CheckoutTests
{
    public class CheckoutSetPaymentInfoTests : BaseCheckoutTest
    {
        public CheckoutSetPaymentInfoTests(HcaUserRole userRole) : base(userRole) { }

        [Test]
        public void T1_POSTPaymentInfoRequest_ValidToken_Successful()
        {
            // Arrange
            var paymentInfo = new SetPaymentInfoRequest
            {
                BillingAddress = GetRandomAddress(),
                FederatedPayment = new FederatedPaymentInfo
                {
                    CardToken = GetPaymentToken(),
                }
            };

            // Act
            FillCart();
            SetShippingOptions();
            var result = ApiContext.Checkout.SetPaymentInfo(paymentInfo);

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() => { result.VerifyOkResponseData(); });
        }

        [Test]
        public void T2_POSTPaymentInfoRequest_InvalidToken_BadRequest()
        {
            // Arrange
            var paymentInfo = new SetPaymentInfoRequest
            {
                BillingAddress = GetRandomAddress(),
                FederatedPayment = new FederatedPaymentInfo
                {
                    CardToken = "MyInvalidToken",
                }
            };

            // Act
            FillCart();
            SetShippingOptions();
            var result = ApiContext.Checkout.SetPaymentInfo(paymentInfo);

            // Assert
            result.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { result.VerifyErrors("Token is invalid."); });
        }

        [Test]
        public void T3_POSTPaymentInfoRequest_CartIsEmpty_BadRequest()
        {
            // Arrange
            var paymentInfo = new SetPaymentInfoRequest
            {
                BillingAddress = GetRandomAddress(),
                FederatedPayment = new FederatedPaymentInfo
                {
                    CardToken = GetPaymentToken(),
                }
            };

            // Act
            var result = ApiContext.Checkout.SetPaymentInfo(paymentInfo);

            // Assert
            result.CheckUnSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyErrors($"Cart 'Default{ApiContext.GetHcaGlobalCookie().Value}HCA' has no lines.");
            });
        }

        [Test]
        public void T4_POSTPaymentInfoRequest_ShippingOptionsNotSet_BadRequest()
        {
            // Arrange
            var paymentInfo = new SetPaymentInfoRequest
            {
                BillingAddress = GetRandomAddress(),
                FederatedPayment = new FederatedPaymentInfo
                {
                    CardToken = GetPaymentToken(),
                }
            };

            // Act
            FillCart();
            var result = ApiContext.Checkout.SetPaymentInfo(paymentInfo);

            // Assert
            result.CheckUnSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyErrors($"Cart 'Default{ApiContext.GetHcaGlobalCookie().Value}HCA' has no fulfillment.");
            });
        }

        [Test]
        public void T5_POSTPaymentInfoRequest_BillingAddressIsNull_BadRequest()
        {
            // Arrange
            var paymentInfo = new SetPaymentInfoRequest
            {
                BillingAddress = null,
                FederatedPayment = new FederatedPaymentInfo
                {
                    CardToken = GetPaymentToken(),
                }
            };

            // Act
            FillCart();
            SetShippingOptions();
            var result = ApiContext.Checkout.SetPaymentInfo(paymentInfo);

            // Assert
            result.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { result.VerifyErrors("Value cannot be null.\r\nParameter name: billingAddress"); });
        }

        [Test]
        public void T6_POSTPaymentInfoRequest_FederatedPaymentIsNull_BadRequest()
        {
            // Arrange
            var paymentInfo = new SetPaymentInfoRequest
            {
                BillingAddress = GetRandomAddress(),
                FederatedPayment = null
            };

            // Act
            FillCart();
            SetShippingOptions();
            var result = ApiContext.Checkout.SetPaymentInfo(paymentInfo);

            // Assert
            result.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { result.VerifyErrors("Value cannot be null.\r\nParameter name: federatedPayment"); });
        }
    }
}
