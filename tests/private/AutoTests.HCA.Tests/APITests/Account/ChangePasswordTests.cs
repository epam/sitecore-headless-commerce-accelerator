using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Account;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Account.Authentication;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture(Description = "Change Password Tests.")]
    public class ChangePasswordTests : BaseAccountTest
    {
        public static CreateAccountRequest NewUser =>
            new CreateAccountRequest(GetRandomEmail(), "FirstName123", "LastName123",
            "123456");

        public static IEnumerable<TestCaseData> TestData_ChangePasswordTest_03_InvalidPasswordModel()
        {
            var user = NewUser;
            return new List<TestCaseData>
            {
                new TestCaseData(user, null, null, null,  new List<string> {"The Email field is required.",
                    "The NewPassword field is required.","The OldPassword field is required."}),
                new TestCaseData(user, "InvalidEmail", null, null,  new List<string> {"The Email field is not a valid e-mail address.",
                    "The NewPassword field is required.","The OldPassword field is required."}),
                new TestCaseData(user, user.Email, null, "123",  new List<string> {"The NewPassword field is required.",
                    "The OldPassword field is required."})
            };
        }

        [Test]
        public void T1_PUTPasswordTest_NewPassword_SuccessfulResult()
        {
            // Arrange
            var newUser = NewUser;
            var newPasswordModel = new ChangePasswordRequest()
            {
                Email = newUser.Email,
                NewPassword = "MyNewPassword",
                OldPassword = newUser.Password
            };

            // Act
            HcaService.CreateUserAccount(newUser);
            HcaService.Login(new LoginRequest(newUser.Email, newUser.Password));
            var changePasswordResponse = HcaService.ChangePassword(newPasswordModel);
            HcaService.Logout();
            var loginResponse = HcaService.Login(new LoginRequest(newPasswordModel.Email, newPasswordModel.NewPassword));

            // Assert
            Assert.True(changePasswordResponse.IsSuccessful, "The 'Accounts/password' POST request isn't passed.");
            Assert.True(loginResponse.IsSuccessful, "Login error after changing password.");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, changePasswordResponse.StatusCode);
                Assert.AreEqual(HcaStatus.Ok, changePasswordResponse.OkResponseData.Status);
                Assert.AreEqual(HcaStatus.Ok, loginResponse.OkResponseData.Status);
            });
        }

        [TestCase(null, "The OldPassword field is required.")]
        [TestCase("123", "Incorrect old password.")]
        public void T2_PUTPasswordTest_InvalidOldPassword_BadRequest(string oldPassword, string expMessage)
        {
            // Arrange
            var newUser = NewUser;
            var newPasswordModel = new ChangePasswordRequest(newUser.Email, oldPassword, "MyNewPassword");

            // Act
            HcaService.CreateUserAccount(newUser);
            HcaService.Login(new LoginRequest(newUser.Email, newUser.Password));
            var response = HcaService.ChangePassword(newPasswordModel);

            // Assert
            Assert.False(response.IsSuccessful, "The bad 'Accounts/password' POST request is passed.");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HcaStatus.Error, response.Errors.Status);
                Assert.AreEqual(expMessage, response.Errors.Error,
                    $"Expected {nameof(response.Errors.Error)} text: {expMessage}. Actual:{response.Errors.Error}");
                Assert.True(response.Errors.Errors.All(x => x == expMessage));
            });
        }

        [TestCaseSource(nameof(TestData_ChangePasswordTest_03_InvalidPasswordModel))]
        public void T3_PUTPasswordTest_InvalidPasswordModel_BadRequest(CreateAccountRequest account, string email, 
            string oldPassword, string newPassword, IEnumerable<string> expMessages)
        {
            // Arrange
            var newPasswordModel = new ChangePasswordRequest(email, oldPassword, newPassword);

            // Act
            HcaService.CreateUserAccount(account);
            HcaService.Login(new LoginRequest(account.Email, account.Password));
            var response = HcaService.ChangePassword(newPasswordModel);

            // Assert
            Assert.False(response.IsSuccessful, "The bad 'Accounts/password' POST request is passed.");
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
