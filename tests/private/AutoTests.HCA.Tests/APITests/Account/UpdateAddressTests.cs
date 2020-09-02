using System.Collections.Generic;
using System.Linq;
using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Addresses;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [TestFixture(Description = "Update address for user api tests.")]
    public class UpdateAddressTests : BaseAccountTest
    {
        [SetUp]
        public new void SetUp()
        {
            var user = TestsData.GetUser();
            UserManager = TestsHelper.CreateUserManagerHelper(user, HcaService);
            UserManager.CleanAddresses();
            var res = HcaService.AddAddress(ProductForUpdate);
            Assert.True(res.IsSuccessful, "Can't add new address.");
            ProductForUpdate = res.OkResponseData.Data.First();
        }

        protected static Address ProductForUpdate = new Address
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
            IsPrimary = true
        };

        [Test(Description = "A test that checks the server's response after updating the address.")]
        public void T1_PUTAddressRequest_ValidProduct_ProductHasBeenUpdated()
        {
            // Arrange
            var prodForUpdate = new Address
            {
                ExternalId = ProductForUpdate.ExternalId,
                Name = ProductForUpdate.Name,
                FirstName = StringHelpers.RandomString(10),
                LastName = StringHelpers.RandomString(10),
                Address1 = StringHelpers.GetRandomAddressString(),
                Address2 = StringHelpers.GetRandomAddressString(),
                City = StringHelpers.RandomString(10),
                Country = "United States",
                CountryCode = "US",
                State = "AL",
                ZipPostalCode = "2335",
                IsPrimary = true
            };

            // Act
            var updateResult = HcaService.UpdateAddress(prodForUpdate);
            var getResult = HcaService.GetAddresses();

            // Assert
            updateResult.CheckSuccessfulResponse();
            getResult.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                updateResult.VerifyOkResponseData();
                getResult.VerifyOkResponseData();

                VerifyAddressResponse(new List<Address> {prodForUpdate}, getResult.OkResponseData.Data);
            });
        }

        [Test(Description = "A test that checks the server's response after updating the address.")]
        public void T1_PUTAddressRequest_ValidProduct_VerifyResponse()
        {
            // Arrange
            var prodForUpdate = new Address
            {
                ExternalId = ProductForUpdate.ExternalId,
                Name = ProductForUpdate.Name,
                FirstName = StringHelpers.RandomString(10),
                LastName = StringHelpers.RandomString(10),
                Address1 = StringHelpers.GetRandomAddressString(),
                Address2 = StringHelpers.GetRandomAddressString(),
                City = StringHelpers.RandomString(10),
                Country = "United States",
                CountryCode = "US",
                State = "AL",
                ZipPostalCode = "2335",
                IsPrimary = true
            };

            // Act
            var updateResult = HcaService.UpdateAddress(prodForUpdate);

            // Assert
            updateResult.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                updateResult.VerifyOkResponseData();

                VerifyAddressResponse(new List<Address> {prodForUpdate}, updateResult.OkResponseData.Data);
            });
        }
    }
}