using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Account;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture(Description = "Create Account Tests")]
    public class CreateAccountTests : BaseAccountTest
    {
        public const string DEF_FIRST_NAME = "DEFFirstName";
        public const string DEF_LAST_NAME = "DEFFirstName";
        public const string DEF_PASSWORD = "123456";

        public static readonly string Email = DefUser.Email;

        public static IEnumerable<TestCaseData> T2_POSTAccountRequest_InvalidUser_TestCaseData()
        {
            yield return new TestCaseData(null, null, null, null, new List<string>
            {
                "The Email field is required.",
                "The FirstName field is required.", "The LastName field is required.",
                "The Password field is required."
            });
            yield return new TestCaseData(Email, null, null, DEF_PASSWORD,
                new List<string> {"The FirstName field is required.", "The LastName field is required."});
            yield return new TestCaseData(Email, DEF_FIRST_NAME, DEF_LAST_NAME, DEF_PASSWORD,
                new List<string> {"Email is in use."});
            yield return new TestCaseData(Email, null, DEF_LAST_NAME, DEF_PASSWORD,
                new List<string> {"The FirstName field is required."});
            yield return new TestCaseData(Email, DEF_FIRST_NAME, null, DEF_PASSWORD,
                new List<string> {"The LastName field is required."});
            yield return new TestCaseData(Email, DEF_FIRST_NAME, DEF_LAST_NAME, null,
                new List<string> {"The Password field is required."});
        }

        [Test(Description = "Create account with valid user.")]
        public void T1_POSTAccountRequest_ValidNewUser_SuccessfulResult()
        {
            // Arrange
            var email = GetRandomEmail();
            var newUser = new CreateAccountRequest
            {
                Email = email,
                FirstName = StringHelpers.RandomString(5),
                LastName = StringHelpers.RandomString(5),
                Password = StringHelpers.RandomString(5)
            };

            // Act
            var response = HcaService.CreateUserAccount(newUser);

            // Assert
            Assert.True(response.IsSuccessful, "The 'Accounts/account' POST request isn't passed.");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
                Assert.AreEqual(email, response.OkResponseData.Data.Email);
                Assert.AreEqual(newUser.FirstName, response.OkResponseData.Data.FirstName);
                Assert.AreEqual(newUser.LastName, response.OkResponseData.Data.LastName);
                Assert.True(HcaService.ValidateEmail(new ValidateEmailRequest { Email = email }).OkResponseData.Data.InUse,
                    $"User {email} wasn't registered");
            });
        }

        [Test(Description = "Create account with invalid user parameters.")]
        [TestCaseSource(nameof(T2_POSTAccountRequest_InvalidUser_TestCaseData))]
        public void T2_POSTAccountRequest_InvalidUser_BadResult(string email, string firstName, string lastName, string password,
            IEnumerable<string> expMessages)
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
            var response = HcaService.CreateUserAccount(user);

            // Assert
            Assert.False(response.IsSuccessful, "The bad 'accounts/account' POST request is passed.");
            var dataResult = response.Errors;
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
                Assert.AreEqual(HcaStatus.Error, dataResult.Status);
                Assert.AreEqual(expMessages.First(), dataResult.Error,
                    $"Expected {nameof(dataResult.Error)} text: {expMessages}. Actual:{dataResult.Error}");
                if (expMessages.Count() == 1) Assert.That(dataResult.Errors.All(x => x == expMessages.First()));
                else Assert.That(!expMessages.Except(dataResult.Errors).Any(),
                    "The error list does not contain all validation errors");
            });
        }
    }
}