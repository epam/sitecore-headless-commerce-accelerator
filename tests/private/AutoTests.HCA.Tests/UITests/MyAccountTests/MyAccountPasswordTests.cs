using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.AutomationFramework.UI.Core;
using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.BaseTests;
using AutoTests.HCA.Core.UI;
using AutoTests.HCA.Core.UI.ConstantsAndEnums;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.UITests.MyAccountTests
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(BrowserType.Chrome)]
    [UiTest]
    internal class MyAccountPasswordTests : BaseHcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
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
        public void T1_MyAccountPassword_ChangePassword()
        {
            var name = _hcaWebSite.CreateNewUser();
            Browser.DeleteAllCookies();
            _hcaWebSite.OpenHcaAndLogin($"{name}@autotests.com", "password");
            _hcaWebSite.NavigateToPage(_hcaWebSite.MyAccountPage);
            var newGeneratedPassword = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Old Password", "password");
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("New Password", newGeneratedPassword);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Confirm New Password", newGeneratedPassword);
            _hcaWebSite.MyAccountChangePasswordSection.SavePasswordClick();
            Browser.DeleteAllCookies();
            _hcaWebSite.OpenHcaAndLogin($"{name}@autotests.com", newGeneratedPassword);
        }

        [Test]
        public void T2_MyAccountPassword_ChangePasswordWithoutOldPassword()
        {
            _hcaWebSite.OpenHcaAndLogin(_userName, _password);
            _hcaWebSite.NavigateToPage(_hcaWebSite.MyAccountPage);
            _hcaWebSite.MyAccountPage.WaitForOpened();
            var newGeneratedPassword = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("New Password", newGeneratedPassword);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Confirm New Password", newGeneratedPassword);
            Assert.False(_hcaWebSite.MyAccountChangePasswordSection.SavePasswordIsClickable(),
                "SavePasswordIsClickable");
        }

        [Test]
        public void T3_MyAccountPassword_ChangePasswordWithoutNewPassword()
        {
            _hcaWebSite.GoToPageWithDefaultParams(PagePrefix.Account, TestsData.GetProduct(),
                TestsData.GetUser().Credentials);
            _hcaWebSite.MyAccountPage.WaitForOpened();
            var newGeneratedPassword = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Old Password", newGeneratedPassword);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Confirm New Password", newGeneratedPassword);
            Assert.False(_hcaWebSite.MyAccountChangePasswordSection.SavePasswordIsClickable(),
                "SavePasswordIsClickable");
        }

        [Test]
        public void T4_MyAccountPassword_ChangePasswordWithoutConfirmPassword()
        {
            _hcaWebSite.GoToPageWithDefaultParams(PagePrefix.Account, TestsData.GetProduct(),
                TestsData.GetUser().Credentials);
            _hcaWebSite.MyAccountPage.WaitForOpened();
            var newGeneratedPassword = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Old Password", _password);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("New Password", newGeneratedPassword);
            Assert.False(_hcaWebSite.MyAccountChangePasswordSection.SavePasswordIsClickable(),
                "SavePasswordIsClickable");
        }

        [Test]
        public void T5_MyAccountPassword_ChangePasswordConfirmDoesNotMatch()
        {
            _hcaWebSite.GoToPageWithDefaultParams(PagePrefix.Account, TestsData.GetProduct(),
                TestsData.GetUser().Credentials);
            _hcaWebSite.MyAccountPage.WaitForOpened();
            var newGeneratedPassword = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Old Password", _password);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("New Password", newGeneratedPassword);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Confirm New Password",
                newGeneratedPassword + "123");
            Assert.False(_hcaWebSite.MyAccountChangePasswordSection.SavePasswordIsClickable(),
                "SavePasswordIsClickable");
        }

        [Test]
        public void T6_MyAccountPassword_OldPasswordIncorrect()
        {
            _hcaWebSite.GoToPageWithDefaultParams(PagePrefix.Account, TestsData.GetProduct(),
                TestsData.GetUser().Credentials);
            _hcaWebSite.MyAccountPage.WaitForOpened();
            var newGeneratedPassword = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Old Password", "123");
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("New Password", newGeneratedPassword);
            _hcaWebSite.MyAccountChangePasswordSection.FillFieldByName("Confirm New Password", newGeneratedPassword);
            Assert.True(_hcaWebSite.MyAccountChangePasswordSection.SavePasswordIsClickable(),
                "SavePasswordIsClickable");
            _hcaWebSite.MyAccountChangePasswordSection.SavePasswordClick();
            _hcaWebSite.MyAccountPage.WaitForOpened();
            _hcaWebSite.MyAccountChangePasswordSection.VerifyErrorLabel("Change password failed");
        }
    }
}