using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Account;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture(Description = "Tests for 'account/validate' endpoint.'")]
    public class ValidateEmailTests : BaseAccountTest
    {
        [Test(Description = "Validate existing email test.")]
        public void T1_POSTValidateRequest_ExistingEmail_EmailInUse()
        {
            // Arrange
            var email = new ValidateEmailRequest {Email = DefUser.Email};

            // Act
            var response = HcaService.ValidateEmail(email);

            // Assert
            response.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                response.VerifyOkResponseData();

                Assert.True(response.OkResponseData.Data.InUse);
            });
        }

        [Test(Description = "Validate non-existing email test.")]
        public void T2_POSTValidateRequest_NonExistingEmail_NotInUse()
        {
            // Arrange
            var email = new ValidateEmailRequest {Email = GetRandomEmail()};

            // Act
            var response = HcaService.ValidateEmail(email);

            // Assert
            response.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                response.VerifyOkResponseData();

                Assert.False(response.OkResponseData.Data.InUse);
            });
        }

        [Test(Description = "Validate invalid email test.")]
        public void T3_POSTValidateRequest_InvalidEmail_InvalidEmailError()
        {
            // Arrange
            const string expMessage = "The Email field is not a valid e-mail address.";
            var email = new ValidateEmailRequest {Email = StringHelpers.RandomString(5)};

            // Act
            var response = HcaService.ValidateEmail(email);

            // Assert
            response.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { response.VerifyErrors(expMessage); });
        }
    }
}