using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Account.Authentication;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture(Description = "Authorization Tests")]
    public class AuthorizationTests : BaseAccountTest
    {
        private static IEnumerable<TestCaseData> T3_POSTLoginRequest_InvalidUser_TestCaseData()
        {
            yield return new TestCaseData(new LoginRequest(DefUser.Email, "0000"));
            yield return new TestCaseData(new LoginRequest("12@mail.com", DefUser.Password));
        }

        [Test]
        public void T1_POSTLoginRequest_ValidUser_VerifyCookies()
        {
            // Arrange
            var hcaService = TestsHelper.CreateHcaApiClient();
            var user = new LoginRequest(DefUser.Email, DefUser.Password);

            // Act
            var result = hcaService.Login(user);

            // Assert
            Assert.True(result.IsSuccessful, "The Login POST request is passed.");
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.OkResponseData);
            Assert.AreEqual(HcaStatus.Ok, result.OkResponseData.Status);
            var cookies = hcaService.GetClientCookies().FirstOrDefault(x => x.Name == AUTHORIZATION_COOKIE_NAME);
            Assert.NotNull(cookies, "The response of '/login' doesn't contain authorization cookies.");
            Assert.False(string.IsNullOrWhiteSpace(cookies.Value), "Returned cookies contain empty value.");
        }

        [Test]
        public void T2_POSTLoginRequest_InvalidEmail_BadRequest()
        {
            // Arrange
            const string expErrorMsg = "The Email field is not a valid e-mail address.";
            var hcaService = TestsHelper.CreateHcaApiClient();
            var user = new LoginRequest(StringHelpers.RandomString(2), DefUser.Password);

            // Act
            var result = hcaService.Login(user);

            // Assert
            Assert.False(result.IsSuccessful, "The Login POST request isn't passed.");
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual(HcaStatus.Error, result.Errors.Status);
            Assert.NotNull(result.Errors);
            Assert.Multiple(() =>
            {
                Assert.True(result.Errors.Errors.All(x => x == expErrorMsg));
                Assert.AreEqual(expErrorMsg, result.Errors.Error);
                var cookies = hcaService.GetClientCookies().FirstOrDefault(x => x.Name == AUTHORIZATION_COOKIE_NAME);
                Assert.Null(cookies, "The filed response of '/login' contains authorization cookies.");
            });
        }

        [TestCaseSource(nameof(T3_POSTLoginRequest_InvalidUser_TestCaseData))]
        public void T3_POSTLoginRequest_InvalidUser_BadRequest(LoginRequest userLoginRequest)
        {
            // Arrange
            const string expErrorMsg = "Incorrect login or password.";
            var hcaService = TestsHelper.CreateHcaApiClient();

            // Act
            var result = hcaService.Login(userLoginRequest);

            // Assert
            Assert.False(result.IsSuccessful, "The Login POST request isn't passed.");
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual(HcaStatus.Error, result.Errors.Status);
            Assert.NotNull(result.Errors);
            Assert.Multiple(() =>
            {
                Assert.True(result.Errors.Errors?.All(x => x == expErrorMsg));
                Assert.AreEqual(expErrorMsg, result.Errors.Error);
                var cookies = hcaService.GetClientCookies().FirstOrDefault(x => x.Name == AUTHORIZATION_COOKIE_NAME);
                Assert.Null(cookies, "The filed response of '/login' contains authorization cookies.");
            });
        }

        [Test]
        public void T4_POSTLogoutRequest_VerifyResponse()
        {
            // Arrange
            var hcaService = TestsHelper.CreateHcaApiClient();
            var user = new LoginRequest(DefUser.Email, DefUser.Password);

            // Act
            hcaService.Login(user);
            Assert.NotNull(hcaService.GetClientCookies().FirstOrDefault(x => x.Name == AUTHORIZATION_COOKIE_NAME),
                "The response of '/logout' doesn't contain authorization cookies.");
            var logoutResult = hcaService.Logout();

            // Assert
            Assert.True(logoutResult.IsSuccessful, "The Logout POST request is passed.");
            Assert.AreEqual(HttpStatusCode.OK, logoutResult.StatusCode);
            Assert.AreEqual(HcaStatus.Ok, logoutResult.OkResponseData.Status);
            Assert.Null(hcaService.GetClientCookies().FirstOrDefault(x => x.Name == AUTHORIZATION_COOKIE_NAME),
                "The response of '/logout' contains authorization cookies.");
        }
    }
}