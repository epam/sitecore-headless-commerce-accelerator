using System;
using System.Collections.Generic;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Addresses;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Checkout.Shipping;
using AutoTests.HCA.Core.Common.Settings.Checkout;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CheckoutTests
{
    public class CheckoutSetShippingOptionsTests : BaseCheckoutTest
    {
        public CheckoutSetShippingOptionsTests(HcaUserRole userRole) : base(userRole)
        {
        }

        [Test]
        public void T1_POSTShippingOptionsRequest_VerifyResponse()
        {
            // Arrange
            var model = new SetShippingOptionsRequest
            {
                ShippingAddresses = new List<Address> { GetRandomAddress() },
                OrderShippingPreferenceType = "1",
                ShippingMethods = new List<ShippingMethod> {new ShippingMethod
                {
                    Description = DefaultShippingMethod.Description,
                    ExternalId = DefaultShippingMethod.ExternalId,
                    Name = DefaultShippingMethod.Name.ToString(),
                    PartyId = Guid.NewGuid().ToString("N").ToLower(),
                    LineIds = null,
                } }
            };

            // Act
            FillCart();
            var result = ApiContext.Checkout.SetShippingOptions(model);

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();
            });
        }

        [Test]
        public void T2_POSTShippingOptionsRequest_OrderShippingPreferenceTypeNotSet_BadRequest()
        {
            // Arrange
            var model = new SetShippingOptionsRequest
            {
                ShippingAddresses = new List<Address> { GetRandomAddress() },
                OrderShippingPreferenceType = null,
                ShippingMethods = new List<ShippingMethod> {new ShippingMethod
                {
                    Description = DefaultShippingMethod.Description,
                    ExternalId = DefaultShippingMethod.ExternalId,
                    Name = DefaultShippingMethod.Name.ToString(),
                    PartyId = Guid.NewGuid().ToString("N").ToLower(),
                    LineIds = null,
                } }
            };

            // Act
            FillCart();
            var result = ApiContext.Checkout.SetShippingOptions(model);

            // Assert
            result.CheckUnSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyErrors("Null ids are not allowed.\r\nParameter name: shippingPreferenceType");
            });
        }

        [Test]
        public void T3_POSTShippingOptionsRequest_CartIsEmpty_BadRequest()
        {
            // Arrange
            var model = new SetShippingOptionsRequest
            {
                ShippingAddresses = new List<Address> { GetRandomAddress() },
                OrderShippingPreferenceType = "1",
                ShippingMethods = new List<ShippingMethod> {new ShippingMethod
                {
                    Description = DefaultShippingMethod.Description,
                    ExternalId = DefaultShippingMethod.ExternalId,
                    Name = DefaultShippingMethod.Name.ToString(),
                    PartyId = Guid.NewGuid().ToString("N").ToLower(),
                    LineIds = null,
                } }
            };

            // Act
            var result = ApiContext.Checkout.SetShippingOptions(model);

            // Assert
            result.CheckUnSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyErrors($"Cart 'Default{ApiContext.GetHcaGlobalCookie().Value}HCA' has no lines.");
            });
        }

        [TestCase(null)]
        [TestCase(new object[] { null })]
        public void T4_POSTShippingOptionsRequest_ShippingAddressesNotSet_BadRequest(Address[] addresses)
        {
            // Arrange
            var model = new SetShippingOptionsRequest
            {
                ShippingAddresses = addresses,
                OrderShippingPreferenceType = "1",
                ShippingMethods = new List<ShippingMethod> {new ShippingMethod
                {
                    Description = DefaultShippingMethod.Description,
                    ExternalId = DefaultShippingMethod.ExternalId,
                    Name = DefaultShippingMethod.Name.ToString(),
                    PartyId = Guid.NewGuid().ToString("N").ToLower(),
                    LineIds = null,
                } }
            };

            // Act
            FillCart();
            var result = ApiContext.Checkout.SetShippingOptions(model);

            // Assert
            result.CheckUnSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyErrors("Value cannot be null.\r\nParameter name: shippingAddresses");
            });
        }

        [Test]
        public void T5_POSTShippingOptionsRequest_TwoShippingMethods_BadRequest()
        {
            // Arrange
            var nextDayAirShippingMethod = TestsData.GetShippingMethod(HcaShippingMethod.NextDayAir);
            var model = new SetShippingOptionsRequest
            {
                ShippingAddresses = new List<Address> { GetRandomAddress() },
                OrderShippingPreferenceType = "1",
                ShippingMethods = new List<ShippingMethod> {
                    new ShippingMethod
                    {
                        Description = DefaultShippingMethod.Description,
                        ExternalId = DefaultShippingMethod.ExternalId,
                        Name = DefaultShippingMethod.Name.ToString(),
                        PartyId = Guid.NewGuid().ToString("N").ToLower(),
                        LineIds = null,
                    },
                    new ShippingMethod
                    {
                        Description = nextDayAirShippingMethod.Description,
                        ExternalId = nextDayAirShippingMethod.ExternalId,
                        Name = nextDayAirShippingMethod.Name.ToString(),
                        PartyId = Guid.NewGuid().ToString("N").ToLower(),
                        LineIds = null,
                    }
                }
            };

            // Act
            FillCart();
            var result = ApiContext.Checkout.SetShippingOptions(model);

            // Assert
            result.CheckUnSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyErrors("You can only set one shipping method");
            });
        }

        [TestCase(null)]
        [TestCase(new object[] { null })]
        public void T6_POSTShippingOptionsRequest_ShippingMethodsNotSet_BadRequest(ShippingMethod[] shippingMethods)
        {
            // Arrange
            var model = new SetShippingOptionsRequest
            {
                ShippingAddresses = new List<Address> { GetRandomAddress() },
                OrderShippingPreferenceType = "1",
                ShippingMethods = shippingMethods
            };

            // Act
            FillCart();
            var result = ApiContext.Checkout.SetShippingOptions(model);

            // Assert
            result.CheckUnSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyErrors("Value cannot be null.\r\nParameter name: shippingMethods");
            });
        }
    }
}
