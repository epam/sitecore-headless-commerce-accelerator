using System.Collections.Generic;
using System.Linq;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Addresses;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [TestFixture(Description = "Get addresses for user api tests.")]
    public class GetAddressesTests : BaseAccountTest
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

        [Test(Description = "The test checks the state of the user addresses collection.")]
        public void T1_GETAddressesRequest_VerifyResponse()
        {
            // Arrange, Act
            var result = ApiContext.Account.GetAddresses();

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();

                VerifyAddressResponse(AddressesWithExternalIds, result.OkResponseData.Data);
            });
        }
    }
}