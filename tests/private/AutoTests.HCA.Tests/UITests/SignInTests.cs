using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.UI;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.UITests
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(BrowserType.Chrome)]
    [UiTest]
    internal class SignInTests : HcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
            _hcaWebSite.NavigateToMain();
            _hcaWebSite.OpenUserMenu();
        }

        public SignInTests(BrowserType browserType) : base(browserType)
        {
        }

        private HcaWebSite _hcaWebSite;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //TODO: create user with API helps
        }

        [Test]
        public void SignInTests_01_BlankFormTest()
        {
            _hcaWebSite.LoginForm.VerifySignInButtonNotClickable();
        }

        [Test]
        public void SignInTests_02_BlankPasswordTest()
        {
            var user = TestsData.UserLogin;
            _hcaWebSite.LoginForm.FillUserNameField(user.Email);
            _hcaWebSite.LoginForm.VerifySignInButtonNotClickable();
        }

        [Test]
        public void SignInTests_03_BlankUserNameTest()
        {
            var user = TestsData.UserLogin;
            _hcaWebSite.LoginForm.FillPasswordField(user.Password);
            _hcaWebSite.LoginForm.VerifySignInButtonNotClickable();
        }

        [Test]
        public void SignInTests_04_ClickCreateAccountTest()
        {
            _hcaWebSite.LoginForm.SignUpClick();
            _hcaWebSite.SignUpPage.WaitForOpened();
        }

        [Test]
        public void SignInTests_05_IncorrectPasswordTest()
        {
            var user = TestsData.UserLogin;
            _hcaWebSite.LoginForm.FillUserNameField(user.Email);
            _hcaWebSite.LoginForm.FillPasswordField(user.Password + "_1");
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.VerifyValidationMessage("The email or password you entered is incorrect");
        }

        [Test]
        public void SignInTests_06_SuccessSignInTest()
        {
            var user = TestsData.UserLogin;
            _hcaWebSite.LoginForm.FillUserNameField(user.Email);
            _hcaWebSite.LoginForm.FillPasswordField(user.Password);
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.HideUserMenu();
            _hcaWebSite.OpenUserMenu();
            _hcaWebSite.LoginForm.VerifyLoggedUser();
        }
    }
}