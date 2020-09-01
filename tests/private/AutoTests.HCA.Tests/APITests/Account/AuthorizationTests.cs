using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Account.Authentication;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(Description = "Authorization Tests")]
    public class AuthorizationTests : BaseAccountTest
    {
        [Test(Description = "Checks that an existing user is logged in.")]
        public void T1_POSTLoginRequest_ValidUser_VerifyCookies()
        {
            // Arrange
            var user = new LoginRequest(DefUser.Email, DefUser.Password);

            // Act
            var result = HcaService.Login(user);

            // Assert
            Assert.True(result.IsSuccessful, "The Login POST request is passed.");
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.OkResponseData);
            Assert.AreEqual(HcaStatus.Ok, result.OkResponseData.Status);
            var cookies = HcaService.GetClientCookies().FirstOrDefault(x => x.Name == AUTHORIZATION_COOKIE_NAME);
            Assert.NotNull(cookies, "The response of '/login' doesn't contain authorization cookies.");
            Assert.False(string.IsNullOrWhiteSpace(cookies.Value), "Returned cookies contain empty value.");
        }

        [Test(Description = "Checks that the server returns an error if the email is not valid.")]
        public void T2_POSTLoginRequest_InvalidEmail_BadRequest()
        {
            // Arrange
            const string expErrorMsg = "The Email field is not a valid e-mail address.";
            var user = new LoginRequest(StringHelpers.RandomString(2), DefUser.Password);

            // Act
            var result = HcaService.Login(user);

            // Assert
            Assert.False(result.IsSuccessful, "The Login POST request isn't passed.");
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual(HcaStatus.Error, result.Errors.Status);
            Assert.NotNull(result.Errors);
            Assert.Multiple(() =>
            {
                Assert.True(result.Errors.Errors.All(x => x == expErrorMsg));
                Assert.AreEqual(expErrorMsg, result.Errors.Error);
                var cookies = HcaService.GetClientCookies().FirstOrDefault(x => x.Name == AUTHORIZATION_COOKIE_NAME);
                Assert.Null(cookies, "The filed response of '/login' contains authorization cookies.");
            });
        }

        [Test(Description = "Checks that the server returns an error if the email is non-existing.")]
        public void T3_POSTLoginRequest_NonExistingEmail_BadRequest()
        {
            // Arrange
            const string expErrorMsg = "Incorrect login or password.";
            var userLoginRequest = new LoginRequest("12@mail.com", DefUser.Password);

            // Act
            var result = HcaService.Login(userLoginRequest);

            // Assert
            Assert.False(result.IsSuccessful, "The Login POST request isn't passed.");
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual(HcaStatus.Error, result.Errors.Status);
            Assert.NotNull(result.Errors);
            Assert.Multiple(() =>
            {
                Assert.True(result.Errors.Errors?.All(x => x == expErrorMsg));
                Assert.AreEqual(expErrorMsg, result.Errors.Error);
                var cookies = HcaService.GetClientCookies().FirstOrDefault(x => x.Name == AUTHORIZATION_COOKIE_NAME);
                Assert.Null(cookies, "The filed response of '/login' contains authorization cookies.");
            });
        }

        [Test(Description = "Checks that the server returns an error if the password is invalid.")]
        public void T4_POSTLoginRequest_InvalidPassword_BadRequest()
        {
            // Arrange
            const string expErrorMsg = "Incorrect login or password.";
            var userLoginRequest = new LoginRequest(DefUser.Email, "0000");

            // Act
            var result = HcaService.Login(userLoginRequest);

            // Assert
            Assert.False(result.IsSuccessful, "The Login POST request isn't passed.");
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual(HcaStatus.Error, result.Errors.Status);
            Assert.NotNull(result.Errors);
            Assert.Multiple(() =>
            {
                Assert.True(result.Errors.Errors?.All(x => x == expErrorMsg));
                Assert.AreEqual(expErrorMsg, result.Errors.Error);
                var cookies = HcaService.GetClientCookies().FirstOrDefault(x => x.Name == AUTHORIZATION_COOKIE_NAME);
                Assert.Null(cookies, "The filed response of '/login' contains authorization cookies.");
            });
        }

        [Test(Description = "Checks that the authorized user is logged out.")]
        public void T5_POSTLogoutRequest_VerifyResponse()
        {
            // Arrange
            var user = new LoginRequest(DefUser.Email, DefUser.Password);

            // Act
            HcaService.Login(user);
            Assert.NotNull(HcaService.GetClientCookies().FirstOrDefault(x => x.Name == AUTHORIZATION_COOKIE_NAME),
                "The response of '/logout' doesn't contain authorization cookies.");
            var logoutResult = HcaService.Logout();

            // Assert
            Assert.True(logoutResult.IsSuccessful, "The Logout POST request is passed.");
            Assert.AreEqual(HttpStatusCode.OK, logoutResult.StatusCode);
            Assert.AreEqual(HcaStatus.Ok, logoutResult.OkResponseData.Status);
            Assert.Null(HcaService.GetClientCookies().FirstOrDefault(x => x.Name == AUTHORIZATION_COOKIE_NAME),
                "The response of '/logout' contains authorization cookies.");
        }
    }
}