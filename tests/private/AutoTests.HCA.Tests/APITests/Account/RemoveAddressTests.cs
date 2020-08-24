using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Addresses;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [TestFixture(Description = "Remove address for user api tests.")]
    public class RemoveAddressTests : BaseAccountTest
    {
        public List<Address> AddressesWithExternalIds;

        [SetUp]
        public new void SetUp()
        {
            var user = TestsData.GetUser();
            UserManager = TestsHelper.CreateUserManagerHelper(user, HcaService);
            UserManager.CleanAddresses();
            AddressesWithExternalIds = new List<Address>();
            foreach (var address in AddressesCollection)
            {
                var res = HcaService.AddAddress(address);
                Assert.True(res.IsSuccessful, "Can't add new address.");
                AddressesWithExternalIds.AddRange(res.OkResponseData.Data);
            }
        }

        [Test(Description = "A test that checks the server's response after delete the user's address.")]
        public void T1_DELETEAddressesRequest_ExternalId_VerifyResponse()
        {
            // Arrange, Act
            var result = HcaService.RemoveAddress(AddressesWithExternalIds.First().ExternalId);

            // Assert
            Assert.True(result.IsSuccessful, "The DELETE Addresses request isn't passed.");
            Assert.Multiple(() =>
            {
                ExtendedAssert.AreEqual(HttpStatusCode.OK, result.StatusCode, nameof(result.StatusCode));
                ExtendedAssert.NotNull(result.OkResponseData, nameof(result.OkResponseData));
                ExtendedAssert.AreEqual(HcaStatus.Ok, result.OkResponseData.Status, nameof(result.OkResponseData.Status));

                VerifyAddressResponse(AddressesWithExternalIds.Skip(1), result.OkResponseData.Data);
            });
        }

        [Test(Description = "A test that checks if a address has been deleted to the addresses list.")]
        public void T2_DELETEAddressesRequest_ExternalId_ProductHasBeenDeleted()
        {
            // Arrange, Act
            var removeAddressResult = HcaService.RemoveAddress(AddressesWithExternalIds.First().ExternalId);
            var getAddressesResult = HcaService.GetAddresses();
            
            // Assert
            Assert.True(removeAddressResult.IsSuccessful, "The DELETE Addresses request isn't passed.");
            Assert.True(getAddressesResult.IsSuccessful, "The GET Addresses request isn't passed.");
            Assert.Multiple(() =>
            {
                ExtendedAssert.AreEqual(HttpStatusCode.OK, removeAddressResult.StatusCode, nameof(removeAddressResult.StatusCode));
                ExtendedAssert.NotNull(removeAddressResult.OkResponseData, nameof(removeAddressResult.OkResponseData));
                ExtendedAssert.AreEqual(HcaStatus.Ok, removeAddressResult.OkResponseData.Status, nameof(removeAddressResult.OkResponseData.Status));
                ExtendedAssert.AreEqual(HttpStatusCode.OK, getAddressesResult.StatusCode, nameof(getAddressesResult.StatusCode));
                ExtendedAssert.NotNull(getAddressesResult.OkResponseData, nameof(getAddressesResult.OkResponseData));
                ExtendedAssert.AreEqual(HcaStatus.Ok, getAddressesResult.OkResponseData.Status, nameof(getAddressesResult.OkResponseData.Status));

                VerifyAddressResponse(AddressesWithExternalIds.Skip(1), getAddressesResult.OkResponseData.Data);
            });
        }
    }
}
