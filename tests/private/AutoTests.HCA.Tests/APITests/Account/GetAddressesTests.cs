using System.Collections.Generic;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Addresses;
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
            UserManager = TestsHelper.CreateUserManagerHelper(user, HcaService);
            UserManager.CleanAddresses();
            foreach (var address in AddressesCollection)
            {
                var res = HcaService.AddAddress(address);
                res.CheckSuccessfulResponse();
                AddressesWithExternalIds.AddRange(res.OkResponseData.Data);
            }
        }

        public List<Address> AddressesWithExternalIds = new List<Address>();

        [Test(Description = "The test checks the state of the user addresses collection.")]
        public void T1_GETAddressesRequest_VerifyResponse()
        {
            // Arrange, Act
            var result = HcaService.GetAddresses();

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