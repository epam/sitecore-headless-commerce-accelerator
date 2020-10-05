using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CheckoutTests
{
    public class CheckoutGetShippingInfoTests : BaseCheckoutTest
    {
        public CheckoutGetShippingInfoTests(HcaUserRole userRole) : base(userRole)
        {
        }

        [Test]
        public void T1_GETShippingInfoRequest_Successful()
        {
            // Arrange, Act
            var result = ApiContext.Checkout.GetShippingInfo();

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();
            });
        }

        [Test]
        public void T2_GETShippingInfoRequest_VerifyResponse()
        {
            // Arrange, Act
            var result = ApiContext.Checkout.GetShippingInfo();

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();
                foreach (var method in result.OkResponseData.Data.ShippingMethods)
                {
                    ExtendedAssert.NotNullOrWhiteSpace(method.Description, nameof(method.Description));
                    ExtendedAssert.NotNullOrWhiteSpace(method.ExternalId, nameof(method.ExternalId));
                    ExtendedAssert.NotNullOrWhiteSpace(method.Name, nameof(method.Name));
                    ExtendedAssert.Null(method.PartyId, nameof(method.PartyId));
                    ExtendedAssert.Null(method.ShippingPreferenceType, nameof(method.ShippingPreferenceType));
                    ExtendedAssert.Null(method.LineIds, nameof(method.LineIds));
                }
            });
        }
    }
}
