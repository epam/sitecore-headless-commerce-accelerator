using System.Collections.Generic;
using System.Linq;
using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Addresses;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [TestFixture(Description = "Add address for user api tests.")]
    public class AddAddressTests : BaseAccountTest
    {
        protected Address DefaultAddress;

        [SetUp]
        public new void SetUp()
        {
            var user = TestsData.GetUser();
            TestsHelper.CreateHcaUserApiHelper(user.Credentials, ApiContext).CleanAddresses();
            DefaultAddress = new Address
            {
                FirstName = StringHelpers.RandomString(10),
                LastName = StringHelpers.RandomString(10),
                Address1 = StringHelpers.GetRandomAddressString(),
                Address2 = StringHelpers.GetRandomAddressString(),
                City = StringHelpers.RandomString(10),
                Country = "United States",
                Email = StringHelpers.GetRandomEmail(),
                CountryCode = "US",
                State = "AL",
                ZipPostalCode = "2335",
                IsPrimary = true
            };
        }

        [Test(Description = "A test that checks the server's response after adding the address to the addresses list.")]
        public void T1_POSTAddressRequest_ValidAddress_VerifyResponse()
        {
            // Arrange, Act
            var response = ApiContext.Account.AddAddress(DefaultAddress);

            //Assert
            response.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                response.VerifyOkResponseData();
                VerifyAddressResponse(new List<Address> { DefaultAddress }, response.OkResponseData.Data, true);
            });
        }

        [Test(Description = "A test that checks if a address has been added to the addresses list.")]
        public void T2_POSTAddressRequest_ValidAddress_AddressHasBeenAddedToAddressesList()
        {
            // Arrange, Act
            var addAddressResponse = ApiContext.Account.AddAddress(DefaultAddress);
            var getAddressesResponse = ApiContext.Account.GetAddresses();

            //Assert
            addAddressResponse.CheckSuccessfulResponse();
            getAddressesResponse.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                addAddressResponse.VerifyOkResponseData();
                getAddressesResponse.VerifyOkResponseData();

                VerifyAddressResponse(new List<Address> { addAddressResponse.OkResponseData.Data.First() },
                    getAddressesResponse.OkResponseData.Data);
            });
        }

        [Test(Description = "A test that checks if a address has been added to the addresses list.")]
        public void T3_POSTAddressRequest_TwoAddressesWithPrimaryStatus_OnlyLastAddressHasPrimaryStatus()
        {
            // Arrange
            var secondNewAddress = new Address
            {
                FirstName = StringHelpers.RandomString(10),
                LastName = StringHelpers.RandomString(10),
                Address1 = StringHelpers.GetRandomAddressString(),
                Address2 = StringHelpers.GetRandomAddressString(),
                City = StringHelpers.RandomString(10),
                Country = "United States",
                Email = StringHelpers.GetRandomEmail(),
                CountryCode = "US",
                State = "AL",
                ZipPostalCode = "2335",
                IsPrimary = true
            };

            // Act
            var addFirstAddressResponse = ApiContext.Account.AddAddress(DefaultAddress);
            var addSecondAddressResponse = ApiContext.Account.AddAddress(secondNewAddress);
            var getAddressesResponse = ApiContext.Account.GetAddresses();

            //Assert
            addFirstAddressResponse.CheckSuccessfulResponse();
            addSecondAddressResponse.CheckSuccessfulResponse();
            getAddressesResponse.CheckSuccessfulResponse();

            Assert.Multiple(() =>
            {
                addFirstAddressResponse.VerifyOkResponseData();
                addFirstAddressResponse.VerifyOkResponseData();
                getAddressesResponse.VerifyOkResponseData();

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