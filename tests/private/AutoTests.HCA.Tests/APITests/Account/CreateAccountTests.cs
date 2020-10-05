using System.Collections.Generic;
using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Account;
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
            var email = StringHelpers.GetRandomEmail();
            var newUser = new CreateAccountRequest
            {
                Email = email,
                FirstName = StringHelpers.RandomString(5),
                LastName = StringHelpers.RandomString(5),
                Password = StringHelpers.RandomString(5)
            };

            // Act
            var response = ApiContext.Account.CreateUserAccount(newUser);

            // Assert
            response.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                response.VerifyOkResponseData();

                Assert.AreEqual(email, response.OkResponseData.Data.Email);
                Assert.AreEqual(newUser.FirstName, response.OkResponseData.Data.FirstName);
                Assert.AreEqual(newUser.LastName, response.OkResponseData.Data.LastName);
                Assert.True(
                    ApiContext.Account.ValidateEmail(new ValidateEmailRequest {Email = email}).OkResponseData.Data
                        .InUse,
                    $"User {email} wasn't registered");
            });
        }

        [Test(Description = "Create account with invalid user parameters.")]
        [TestCaseSource(nameof(T2_POSTAccountRequest_InvalidUser_TestCaseData))]
        public void T2_POSTAccountRequest_InvalidUser_BadResult(string email, string firstName, string lastName,
            string password,
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
            var response = ApiContext.Account.CreateUserAccount(user);

            // Assert
            response.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { response.VerifyErrors(expMessages); });
        }
    }
}