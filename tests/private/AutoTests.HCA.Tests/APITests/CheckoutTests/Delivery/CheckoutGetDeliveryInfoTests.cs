using System.Linq;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.HcaApi.Helpers;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CheckoutTests
{
    public class CheckoutGetDeliveryInfoTests : BaseCheckoutTest
    {
        public CheckoutGetDeliveryInfoTests(HcaUserRole userRole) : base(userRole)
        {
        }

        [Test]
        public void T1_GETDeliveryInfoRequest_Successful()
        {
            // Arrange, Act
            var result = TestsHelper.CreateHcaApiContext().Checkout.GetDeliveryInfo();

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() => { result.VerifyOkResponseData(); });
        }

        [Test]
        public void T2_GETDeliveryInfoRequest_CartIsEmpty_VerifyResponse()
        {
            // Arrange
            if (IsUser)
            {
                HcaApiHelper.CleanCart();
            }

            // Act
            var result = ApiContext.Checkout.GetDeliveryInfo();

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();
                var model = result.OkResponseData.Data;
                ExtendedAssert.NotNullOrWhiteSpace(model.NewPartyId, nameof(model.NewPartyId));
                ExtendedAssert.NullOrEmpty(model.ShippingOptions, nameof(model.ShippingOptions));
                ExtendedAssert.Null(model.UserAddresses, nameof(model.UserAddresses));
            });
        }

        [Test]
        public void T3_GETDeliveryInfoRequest_CartIsNotEmpty_VerifyResponse()
        {
            // Arrange
            var expAddress = GetRandomAddress();
            if (IsUser)
            {
                var userHelper = HcaApiHelper as HcaUserApiHelper;
                HcaApiHelper.CleanCart();
                userHelper?.CleanAddresses();
                userHelper?.AddAddress(expAddress);
            }
            FillCart();

            // Act
            var result = ApiContext.Checkout.GetDeliveryInfo();

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();
                var model = result.OkResponseData.Data;
                ExtendedAssert.NotNullOrWhiteSpace(model.NewPartyId, nameof(model.NewPartyId));
                ExtendedAssert.NotNullOrEmpty(model.ShippingOptions, nameof(model.ShippingOptions));
                if (IsUser)
                {
                    var actAddress = model.UserAddresses?.FirstOrDefault();
                    ExtendedAssert.NotNull(actAddress, nameof(model.UserAddresses));
                    ExtendedAssert.AreEqual(expAddress.FirstName, actAddress?.FirstName, nameof(actAddress.FirstName));
                    ExtendedAssert.AreEqual(expAddress.LastName, actAddress?.LastName, nameof(actAddress.LastName));
                    ExtendedAssert.AreEqual(expAddress.Address1, actAddress?.Address1, nameof(actAddress.Address1));
                    ExtendedAssert.AreEqual(expAddress.Address2, actAddress?.Address2, nameof(actAddress.Address2));
                    ExtendedAssert.AreEqual(expAddress.Country, actAddress?.Country, nameof(actAddress.Country));
                    ExtendedAssert.AreEqual(expAddress.CountryCode, actAddress?.CountryCode,
                        nameof(actAddress.CountryCode));
                    ExtendedAssert.AreEqual(expAddress.City, actAddress?.City, nameof(actAddress.City));
                    ExtendedAssert.AreEqual(expAddress.ZipPostalCode, actAddress?.ZipPostalCode,
                        nameof(actAddress.ZipPostalCode));
                    ExtendedAssert.AreEqual(expAddress.State, actAddress?.State, nameof(actAddress.State));
                    ExtendedAssert.AreEqual(expAddress.IsPrimary, actAddress?.IsPrimary, nameof(actAddress.IsPrimary));
                }
                else
                {
                    ExtendedAssert.NullOrEmpty(model.UserAddresses, nameof(model.UserAddresses));
                }
            });
        }
    }
}
