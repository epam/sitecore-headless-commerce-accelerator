using HCA.Pages;
using NUnit.Framework;
using UIAutomationFramework;
using UIAutomationFramework.Driver;

namespace HCA.Tests.UITests
{
    [Parallelizable(ParallelScope.All)]
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
        public void _01_BlankFormTest()
        {
            _hcaWebSite.LoginForm.VerifySignInButtonNotClickable();
        }

        [Test]
        public void _02_BlankPasswordTest()
        {
            var user = Configuration.GetDefaultUserLogin();
            _hcaWebSite.LoginForm.FillUserNameField(user.Email);
            _hcaWebSite.LoginForm.VerifySignInButtonNotClickable();
        }

        [Test]
        public void _03_BlankUserNameTest()
        {
            var user = Configuration.GetDefaultUserLogin();
            _hcaWebSite.LoginForm.FillPasswordField(user.Password);
            _hcaWebSite.LoginForm.VerifySignInButtonNotClickable();
        }

        [Test]
        public void _04_ClickCreateAccountTest()
        {
            _hcaWebSite.LoginForm.SignUpClick();
            _hcaWebSite.SignUpPage.WaitForOpened();
        }

        [Test]
        public void _05_IncorrectPasswordTest()
        {
            var user = Configuration.GetDefaultUserLogin();
            _hcaWebSite.LoginForm.FillUserNameField(user.Email);
            _hcaWebSite.LoginForm.FillPasswordField(user.Password + "_1");
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.VerifyValidationMessage("The email or password you entered is incorrect");
        }

        [Test]
        public void _06_SuccessSignInTest()
        {
            var user = Configuration.GetDefaultUserLogin();
            _hcaWebSite.LoginForm.FillUserNameField(user.Email);
            _hcaWebSite.LoginForm.FillPasswordField(user.Password);
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.WaitForPresentForm();
            _hcaWebSite.HeaderControl.ClickUserButton();
            _hcaWebSite.LoginForm.VerifyLoggedUser();
        }
    }
}