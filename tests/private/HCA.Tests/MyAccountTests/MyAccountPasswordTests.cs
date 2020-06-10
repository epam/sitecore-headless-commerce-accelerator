using HCA.Pages;
using NUnit.Framework;
using UIAutomationFramework.Core;
using UIAutomationFramework.Driver;
using UIAutomationFramework.Utils;

namespace HCA.Tests.MyAccountTests
{
    [TestFixture(BrowserType.Chrome)]
    [UiTest]
    internal class MyAccountPasswordTests : HcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
            _hcaWebSite.OpenHcaAndLogin(_userName, _password);
            _hcaWebSite.NavigateToPage(_hcaWebSite.MyAccountPage);
            _hcaWebSite.MyAccountPage.WaitForOpened();
        }

        public MyAccountPasswordTests(BrowserType browserType) : base(browserType)
        {
        }

        private HcaWebSite _hcaWebSite;
        private readonly string _userName = "testuser@test.com";
        private readonly string _password = "testuser";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //TODO: create user with API helps
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //TODO: delete user with API helps
        }


        [Test]
        public void _01_ChangePasswordTest()
        {
            Browser.DeleteAllCookies();
            var name = _hcaWebSite.CreateNewUser();
            Browser.DeleteAllCookies();
            _hcaWebSite.OpenHcaAndLogin($"{name}@autotests.com", "password");
            _hcaWebSite.NavigateToPage(_hcaWebSite.MyAccountPage);
            var newGeneratedPassword = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Old Password", "password");
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("New Password", newGeneratedPassword);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Confirm New Password", newGeneratedPassword);
            _hcaWebSite.MyAccountChangePasswordSection.SavePasswordClick();
            _hcaWebSite.LogOut();
            _hcaWebSite.OpenHcaAndLogin($"{name}@autotests.com", newGeneratedPassword);
        }

        [Test]
        public void _02_ChangePasswordWithoutOldPasswordTest()
        {
            var newGeneratedPassword = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("New Password", newGeneratedPassword);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Confirm New Password", newGeneratedPassword);
            Assert.False(_hcaWebSite.MyAccountChangePasswordSection.SavePasswordIsClickable());
        }

        [Test]
        public void _03_ChangePasswordWithoutNewPasswordTest()
        {
            var newGeneratedPassword = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Old Password", newGeneratedPassword);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Confirm New Password", newGeneratedPassword);
            Assert.False(_hcaWebSite.MyAccountChangePasswordSection.SavePasswordIsClickable());
        }

        [Test]
        public void _04_ChangePasswordWithoutConfirmPasswordTest()
        {
            var newGeneratedPassword = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Old Password", _password);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("New Password", newGeneratedPassword);
            Assert.False(_hcaWebSite.MyAccountChangePasswordSection.SavePasswordIsClickable());
        }

        [Test]
        public void _05_ChangePasswordConfirmDoesNotMatchTest()
        {
            var newGeneratedPassword = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Old Password", _password);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("New Password", newGeneratedPassword);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Confirm New Password",
                newGeneratedPassword + "123");
            Assert.False(_hcaWebSite.MyAccountChangePasswordSection.SavePasswordIsClickable());
        }

        [Test]
        public void _06_OldPasswordIncorrectTest()
        {
            var newGeneratedPassword = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Old Password", "123");
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("New Password", newGeneratedPassword);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Confirm New Password", newGeneratedPassword);
            Assert.True(_hcaWebSite.MyAccountChangePasswordSection.SavePasswordIsClickable());
            _hcaWebSite.MyAccountChangePasswordSection.SavePasswordClick();
            _hcaWebSite.MyAccountPage.WaitForOpened();
            _hcaWebSite.MyAccountChangePasswordSection.VerifyErrorLabel("Change password failed");
        }
    }
}