using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.AutomationFramework.Shared.Models;
using AutoTests.HCA.Core.API;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Account.Authentication;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Authorization
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    [Description("Authorization Tests")]
    [ApiTest]
    public class AuthorizationTests : HcaApiTest
    {
        protected const string AUTHORIZATION_COOKIE_NAME = ".AspNet.Cookies";
        protected static readonly UserLogin DefUser = TestsData.UserLogin;

        [Test]
        [Order(1)]
        public void LoginWithValidUserDataTest()
        {
            // Arrange
            var hcaService = CreateHcaApiClient();
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
        public void LoginWithInvalidEmailTest()
        {
            // Arrange
            const string expErrorMsg = "The Email field is not a valid e-mail address.";
            var hcaService = CreateHcaApiClient();
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

        [TestCaseSource(nameof(GetInvalidLoginRequest))]
        public void LoginWithInvalidUserTest(LoginRequest userLoginRequest)
        {
            // Arrange
            const string expErrorMsg = "Incorrect login or password.";
            var hcaService = CreateHcaApiClient();

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

        public static IEnumerable<LoginRequest> GetInvalidLoginRequest()
        {
            yield return new LoginRequest(DefUser.Email, StringHelpers.RandomString(2));
            yield return new LoginRequest(StringHelpers.RandomString(2) + DefUser.Email, DefUser.Password);
        }

        [Test]
        public void LogoutTest()
        {
            // Arrange
            var hcaService = CreateHcaApiClient();
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
