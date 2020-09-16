using AutoTests.AutomationFramework.Shared.Models;
using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.BaseTests;
using AutoTests.HCA.Core.UI;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.UITests
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(BrowserType.Chrome)]
    [SignInSignUpTest]
    [UiTest]
    internal class SignInTests : BaseHcaWebTest
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
        private readonly UserLogin _defUser = TestsData.GetUser().Credentials;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //TODO: create user with API helps
        }

        [Test]
        public void T1_SignInTests_BlankForm()
        {
            _hcaWebSite.LoginForm.VerifySignInButtonNotClickable();
        }

        [Test]
        public void T2_SignInTests_BlankPassword()
        {
            _hcaWebSite.LoginForm.FillUserNameField(_defUser.Email);
            _hcaWebSite.LoginForm.VerifySignInButtonNotClickable();
        }

        [Test]
        public void T3_SignInTests_BlankUserName()
        {
            _hcaWebSite.LoginForm.FillPasswordField(_defUser.Password);
            _hcaWebSite.LoginForm.VerifySignInButtonNotClickable();
        }

        [Test]
        public void T4_SignInTests_ClickCreateAccount()
        {
            _hcaWebSite.LoginForm.SignUpClick();
            _hcaWebSite.SignUpPage.WaitForOpened();
        }

        [Test]
        public void T5_SignInTests_IncorrectPassword()
        {
            _hcaWebSite.LoginForm.FillUserNameField(_defUser.Email);
            _hcaWebSite.LoginForm.FillPasswordField(_defUser.Password + "_1");
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.VerifyValidationMessage("The email or password you entered is incorrect");
        }

        [Test]
        public void T6_SignInTests_SuccessSignIn()
        {
            _hcaWebSite.LoginForm.FillUserNameField(_defUser.Email);
            _hcaWebSite.LoginForm.FillPasswordField(_defUser.Password);
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.HideUserMenu();
            _hcaWebSite.OpenUserMenu();
            _hcaWebSite.LoginForm.VerifyLoggedUser();
        }
    }
}