using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Addresses;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [TestFixture(Description = "Add address for user api tests.")]
    public class AddAddressTests : BaseAccountTest
    {
        [SetUp]
        public override void SetUp()
        {
            HcaService = TestsHelper.CreateHcaApiClient();
            var user = TestsData.GetUser();
            UserManager = TestsHelper.CreateUserManagerHelper(user, HcaService);
            UserManager.CleanAddresses();
        }

        [Test(Description = "A test that checks the server's response after adding the address to the addresses list.")]
        public void T1_POSTAddressRequest_ValidAddress_VerifyResponse()
        {
            // Arrange
            var newAddress = new Address
            {
                FirstName = StringHelpers.RandomString(10),
                LastName = StringHelpers.RandomString(10),
                Address1 = StringHelpers.GetRandomAddressString(),
                Address2 = StringHelpers.GetRandomAddressString(),
                City = StringHelpers.RandomString(10),
                Country = "United States",
                Email = GetRandomEmail(),
                CountryCode = "US",
                State = "AL",
                ZipPostalCode = "2335",
                IsPrimary = true,
            };

            // Act
            var response = HcaService.AddAddress(newAddress);

            //Assert
            Assert.True(response.IsSuccessful, "The 'Accounts/address' POST request isn't passed.");
            Assert.Multiple(() =>
            {
                ExtendedAssert.AreEqual(HttpStatusCode.OK, response.StatusCode, nameof(response.StatusCode));
                ExtendedAssert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status,
                    nameof(response.OkResponseData.Status));
                VerifyAddressResponse(new List<Address> { newAddress }, response.OkResponseData.Data, true);
            });
        }

        [Test(Description = "A test that checks if a address has been added to the addresses list.")]
        public void T2_POSTAddressRequest_ValidAddress_AddressHasBeenAddedToAddressesList()
        {
            // Arrange
            var newAddress = new Address
            {
                FirstName = StringHelpers.RandomString(10),
                LastName = StringHelpers.RandomString(10),
                Address1 = StringHelpers.GetRandomAddressString(),
                Address2 = StringHelpers.GetRandomAddressString(),
                City = StringHelpers.RandomString(10),
                Country = "United States",
                Email = GetRandomEmail(),
                CountryCode = "US",
                State = "AL",
                ZipPostalCode = "2335",
                IsPrimary = true,
            };

            // Act
            var addAddressResponse = HcaService.AddAddress(newAddress);
            var getAddressesResponse = HcaService.GetAddresses();

            //Assert
            Assert.True(addAddressResponse.IsSuccessful, "The 'Accounts/address' POST request isn't passed.");
            Assert.True(getAddressesResponse.IsSuccessful, "The 'Accounts/address' GET request isn't passed.");
            Assert.Multiple(() =>
            {
                ExtendedAssert.AreEqual(HttpStatusCode.OK, addAddressResponse.StatusCode, nameof(addAddressResponse.StatusCode));
                ExtendedAssert.NotNull(addAddressResponse.OkResponseData, nameof(addAddressResponse.OkResponseData));
                ExtendedAssert.AreEqual(HcaStatus.Ok, addAddressResponse.OkResponseData.Status, nameof(addAddressResponse.OkResponseData.Status));

                ExtendedAssert.AreEqual(HttpStatusCode.OK, getAddressesResponse.StatusCode, nameof(getAddressesResponse.StatusCode));
                ExtendedAssert.NotNull(getAddressesResponse.OkResponseData, nameof(getAddressesResponse.OkResponseData));
                ExtendedAssert.AreEqual(HcaStatus.Ok, getAddressesResponse.OkResponseData.Status, nameof(getAddressesResponse.OkResponseData.Status));

                VerifyAddressResponse(new List<Address> { addAddressResponse.OkResponseData.Data.First() }, getAddressesResponse.OkResponseData.Data);
            });
        }

        [Test(Description = "A test that checks if a address has been added to the addresses list.")]
        public void T3_POSTAddressRequest_TwoAddressesWithPrimaryStatus_OnlyLastAddressHasPrimaryStatus()
        {
            // Arrange
            var firstNewAddress = new Address
            {
                FirstName = StringHelpers.RandomString(10),
                LastName = StringHelpers.RandomString(10),
                Address1 = StringHelpers.GetRandomAddressString(),
                Address2 = StringHelpers.GetRandomAddressString(),
                City = StringHelpers.RandomString(10),
                Country = "United States",
                Email = GetRandomEmail(),
                CountryCode = "US",
                State = "AL",
                ZipPostalCode = "2335",
                IsPrimary = true,
            };
            var secondNewAddress = new Address
            {
                FirstName = StringHelpers.RandomString(10),
                LastName = StringHelpers.RandomString(10),
                Address1 = StringHelpers.GetRandomAddressString(),
                Address2 = StringHelpers.GetRandomAddressString(),
                City = StringHelpers.RandomString(10),
                Country = "United States",
                Email = GetRandomEmail(),
                CountryCode = "US",
                State = "AL",
                ZipPostalCode = "2335",
                IsPrimary = true,
            };

            // Act
            var addFirstAddressResponse = HcaService.AddAddress(firstNewAddress);
            var addSecondAddressResponse = HcaService.AddAddress(secondNewAddress);
            var getAddressesResponse = HcaService.GetAddresses();

            //Assert
            Assert.True(addFirstAddressResponse.IsSuccessful, "The 'Accounts/address' POST request isn't passed.");
            Assert.True(addSecondAddressResponse.IsSuccessful, "The 'Accounts/address' POST request isn't passed.");
            Assert.True(getAddressesResponse.IsSuccessful, "The 'Accounts/address' GET request isn't passed.");

            ExtendedAssert.AreEqual(HttpStatusCode.OK, addFirstAddressResponse.StatusCode,
                nameof(addFirstAddressResponse.StatusCode));
            ExtendedAssert.NotNull(addFirstAddressResponse.OkResponseData,
                nameof(addFirstAddressResponse.OkResponseData));
            ExtendedAssert.AreEqual(HcaStatus.Ok, addFirstAddressResponse.OkResponseData.Status,
                nameof(addFirstAddressResponse.OkResponseData.Status));

            ExtendedAssert.AreEqual(HttpStatusCode.OK, addSecondAddressResponse.StatusCode,
                nameof(addSecondAddressResponse.StatusCode));
            ExtendedAssert.NotNull(addSecondAddressResponse.OkResponseData,
                nameof(addSecondAddressResponse.OkResponseData));
            ExtendedAssert.AreEqual(HcaStatus.Ok, addSecondAddressResponse.OkResponseData.Status,
                nameof(addSecondAddressResponse.OkResponseData.Status));

            ExtendedAssert.AreEqual(HttpStatusCode.OK, getAddressesResponse.StatusCode,
                nameof(getAddressesResponse.StatusCode));
            ExtendedAssert.NotNull(getAddressesResponse.OkResponseData, nameof(getAddressesResponse.OkResponseData));
            ExtendedAssert.AreEqual(HcaStatus.Ok, getAddressesResponse.OkResponseData.Status,
                nameof(getAddressesResponse.OkResponseData.Status));

            Assert.Multiple(() =>
            {
                var firstAddressExternalId = addFirstAddressResponse.OkResponseData.Data.First().ExternalId;
                var secondAddressExternalId = addSecondAddressResponse.OkResponseData.Data.First().ExternalId;
                var addresses = getAddressesResponse.OkResponseData.Data;

                foreach (var address in addresses)
                {
                    if (address.ExternalId == firstAddressExternalId)
                        Assert.False(address.IsPrimary);
                    if (address.ExternalId == secondAddressExternalId)
                        Assert.True(address.IsPrimary);
                }
            });
        }
    }
}
