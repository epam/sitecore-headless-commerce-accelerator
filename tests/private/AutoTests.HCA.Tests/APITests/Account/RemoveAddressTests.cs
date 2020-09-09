using System.Collections.Generic;
using System.Linq;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Addresses;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [TestFixture(Description = "Remove address for user api tests.")]
    public class RemoveAddressTests : BaseAccountTest
    {
        [SetUp]
        public new void SetUp()
        {
            var user = TestsData.GetUser();
            var userHelper = TestsHelper.CreateHcaUserApiHelper(user.Credentials, ApiContext);
            userHelper.CleanAddresses();
            AddressesWithExternalIds = userHelper.AddAddresses(AddressesCollection).ToList();
        }

        public IEnumerable<Address> AddressesWithExternalIds;

        [Test(Description = "A test that checks the server's response after delete the user's address.")]
        public void T1_DELETEAddressesRequest_ExternalId_VerifyResponse()
        {
            // Arrange, Act
            var result = ApiContext.Account.RemoveAddress(AddressesWithExternalIds.First().ExternalId);

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();
                VerifyAddressResponse(AddressesWithExternalIds.Skip(1), result.OkResponseData.Data);
            });
        }

        [Test(Description = "A test that checks if a address has been deleted to the addresses list.")]
        public void T2_DELETEAddressesRequest_ExternalId_ProductHasBeenDeleted()
        {
            // Arrange, Act
            var removeAddressResult = ApiContext.Account.RemoveAddress(AddressesWithExternalIds.First().ExternalId);
            var getAddressesResult = ApiContext.Account.GetAddresses();

            // Assert
            removeAddressResult.CheckSuccessfulResponse();
            getAddressesResult.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                removeAddressResult.VerifyOkResponseData();
                getAddressesResult.VerifyOkResponseData();

                VerifyAddressResponse(AddressesWithExternalIds.Skip(1), getAddressesResult.OkResponseData.Data);
            });
        }
    }
}