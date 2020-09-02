using AutoTests.HCA.Core.API.Models.Hca.Entities.Account;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Account.Authentication;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [TestFixture(Description = "Change Password Tests.")]
    public class ChangePasswordTests : BaseAccountTest
    {
        [SetUp]
        public new void SetUp()
        {
            NewUser = new CreateAccountRequest(GetRandomEmail(), "FirstName123", "LastName123", "123456");
            HcaService.CreateUserAccount(NewUser).CheckSuccessfulResponse();
            HcaService.Login(new LoginRequest(NewUser.Email, NewUser.Password)).CheckSuccessfulResponse();
        }

        public CreateAccountRequest NewUser;

        [TestCase(null, null, new[] {"The NewPassword field is required.", "The OldPassword field is required."})]
        [TestCase("111", "qaz1", new[] {"Incorrect old password."})]
        public void T2_PUTPasswordTest_InvalidOldOrNewPassword_BadRequest(string newPassword, string oldPassword,
            string[] expMessages)
        {
            // Arrange
            var newPasswordModel = new ChangePasswordRequest(NewUser.Email, oldPassword, newPassword);

            // Act
            var response = HcaService.ChangePassword(newPasswordModel);

            // Assert
            response.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { response.VerifyErrors(expMessages); });
        }

        [TestCase(null, "The Email field is required.")]
        [TestCase("InvalidEmail", "The Email field is not a valid e-mail address.")]
        public void T3_PUTPasswordTest_InvalidEmail_BadRequest(string email, string expMessage)
        {
            // Arrange
            var newPasswordModel = new ChangePasswordRequest(email, "a", "a");

            // Act
            var response = HcaService.ChangePassword(newPasswordModel);

            // Assert
            response.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { response.VerifyErrors(expMessage); });
        }

        [Test]
        public void T1_PUTPasswordTest_NewPassword_SuccessfulResult()
        {
            // Arrange
            var newPasswordModel = new ChangePasswordRequest
            {
                Email = NewUser.Email,
                NewPassword = "MyNewPassword",
                OldPassword = NewUser.Password
            };

            // Act, Assert
            HcaService.ChangePassword(newPasswordModel).CheckSuccessfulResponse();
            HcaService.Logout().CheckSuccessfulResponse();
            HcaService.Login(new LoginRequest(newPasswordModel.Email, newPasswordModel.NewPassword))
                .CheckSuccessfulResponse();
        }
    }
}