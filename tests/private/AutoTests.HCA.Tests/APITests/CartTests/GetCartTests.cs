using System.Net;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CartTests
{
    [Parallelizable(ParallelScope.None)]
    public class GetCartTests : BaseCartApiTest
    {
        public GetCartTests(HcaUserRole userRole) : base(userRole) { }

        [SetUp]
        public new void SetUp()
        {
            foreach (var product in ProductsCollection)
            {
                HcaService.AddCartLines(product);
            }
        }

        [Test(Description = "The test checks the state of the cart.")]
        public void T1_GETCartRequest_VerifyResponse()
        {
            // Arrange, Act
            var result = HcaService.GetCart();

            // Assert
            Assert.True(result.IsSuccessful, "The GET Cart request isn't passed.");
            Assert.Multiple(() =>
            {
                ExtendedAssert.AreEqual(HttpStatusCode.OK, result.StatusCode, nameof(result.StatusCode));
                ExtendedAssert.NotNull(result.OkResponseData, nameof(result.OkResponseData));
                ExtendedAssert.AreEqual(HcaStatus.Ok, result.OkResponseData.Status, nameof(result.OkResponseData.Status));

                VerifyCartResponse("GET Cart", ProductsCollection, result.OkResponseData.Data);
            });
        }
    }
}