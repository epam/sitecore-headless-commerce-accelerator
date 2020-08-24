using System.Linq;
using System.Net;
using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Account;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture(Description = "Tests for 'account/validate' endpoint.'")]
    public class ValidateEmailTests : BaseAccountTest
    {
        [Test(Description = "Validate existing email test.")]
        public void T1_ValidateEmailTests_ExistingEmail_EmailInUse()
        {
            // Arrange
            var email = new ValidateEmailRequest { Email = DefUser.Email };

            // Act
            var response = HcaService.ValidateEmail(email);

            // Assert
            Assert.True(response.IsSuccessful, "The Validate POST request is not passed");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
            Assert.True(response.OkResponseData.Data.InUse);
        }

        [Test(Description = "Validate non-existing email test.")]
        public void T2_ValidateEmailTests_NonExistingEmail_NotInUse()
        {
            // Arrange
            var email = new ValidateEmailRequest { Email = GetRandomEmail() };

            // Act
            var response = HcaService.ValidateEmail(email);

            // Assert
            Assert.True(response.IsSuccessful, "The Validate POST request is not passed");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
            Assert.False(response.OkResponseData.Data.InUse);
        }

        [Test(Description = "Validate invalid email test.")]
        public void T3_ValidateEmailTests_InvalidEmail_InvalidEmailError()
        {
            // Arrange
            const string expMessage = "The Email field is not a valid e-mail address.";
            var email = new ValidateEmailRequest { Email = StringHelpers.RandomString(5) };

            // Act
            var response = HcaService.ValidateEmail(email);

            // Assert
            Assert.False(response.IsSuccessful, "The bad 'account/Validate' POST request is passed");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
                Assert.AreEqual(HcaStatus.Error, response.Errors.Status);
                Assert.AreEqual(expMessage, response.Errors.Error,
                    $"Expected {nameof(response.Errors.Error)} text: {expMessage}. Actual:{response.Errors.Error}.");
                Assert.That(response.Errors.Errors.All(x => x == expMessage), "The error list does not contain all validation errors");
            });
        }
    }
}
