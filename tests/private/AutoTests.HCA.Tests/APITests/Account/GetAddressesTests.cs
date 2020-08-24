using System.Collections.Generic;
using System.Net;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Addresses;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [TestFixture(Description = "Get addresses for user api tests.")]
    public class GetAddressesTests : BaseAccountTest
    {
        public List<Address> AddressesWithExternalIds = new List<Address>();

        [SetUp]
        public new void SetUp()
        {
            var user = TestsData.GetUser();
            UserManager = TestsHelper.CreateUserManagerHelper(user, HcaService);
            UserManager.CleanAddresses();
            foreach (var address in AddressesCollection)
            {
               var res =  HcaService.AddAddress(address);
               Assert.True(res.IsSuccessful, "Can't add new address.");
               AddressesWithExternalIds.AddRange(res.OkResponseData.Data);
            }
        }

        [Test(Description = "The test checks the state of the user addresses collection.")]
        public void T1_GETAddressesRequest_VerifyResponse()
        {
            // Arrange, Act
            var result = HcaService.GetAddresses();

            // Assert
            Assert.True(result.IsSuccessful, "The GET Addresses request isn't passed.");
            Assert.Multiple(() =>
            {
                ExtendedAssert.AreEqual(HttpStatusCode.OK, result.StatusCode, nameof(result.StatusCode));
                ExtendedAssert.NotNull(result.OkResponseData, nameof(result.OkResponseData));
                ExtendedAssert.AreEqual(HcaStatus.Ok, result.OkResponseData.Status, nameof(result.OkResponseData.Status));

                VerifyAddressResponse(AddressesWithExternalIds, result.OkResponseData.Data);
            });
        }
    }
}
