using System.Linq;
using AutoTests.HCA.Core.API;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Account;
using AutoTests.HCA.Core.API.Services.HcaService;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    [Description("Cart Tests")]
    [ApiTest]
    public class CreateAccountTests : HcaApiTest
    {
        private readonly IHcaApiService _hcaService = CreateHcaApiClient();

        public const string EMAIL = "postman@gmail.com";
        public const string FIRST_NAME = "FName";
        public const string LAST_NAME = "LName";
        public const string PASSWORD = "Password";

        [Test]
        [Description("Create account with invalid parameters.")]
        [TestCase(null, null, null, null, "The Email field is required.",
            "The FirstName field is required.", "The LastName field is required.", "The Password field is required.")]
        [TestCase(EMAIL, null, null, PASSWORD, "The FirstName field is required.", "The LastName field is required.")]
        [TestCase(EMAIL, FIRST_NAME, LAST_NAME, PASSWORD, "Email is in use.")]
        [TestCase(EMAIL, null, LAST_NAME, PASSWORD, "The FirstName field is required.")]
        [TestCase(EMAIL, FIRST_NAME, null, PASSWORD, "The LastName field is required.")]
        [TestCase(EMAIL, FIRST_NAME, LAST_NAME, null, "The Password field is required.")]
        public void _01_CreateAccountWithInvalidParamsTest(string email, string firstName, string lastName,
            string password,
            params string[] expMessages)
        {
            // Arrange
            var user = new CreateAccountRequest
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password
            };

            // Act
            var response = _hcaService.CreateUserAccount(user);

            // Assert
            Assert.False(response.IsSuccessful, "The GetProducts POST request is passed.");
            var dataResult = response.Errors;
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HcaStatus.Error, dataResult.Status);
                Assert.AreEqual(expMessages.First(), dataResult.Error,
                    $"Expected {nameof(dataResult.Error)} text: {expMessages}. Actual:{dataResult.Error}");
                if (expMessages.Length > 1)
                    Assert.That(!expMessages.Except(dataResult.Errors).Any(),
                        "The error list does not contain all validation errors");
            });
        }
    }
}