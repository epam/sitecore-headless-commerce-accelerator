using HCA.Pages;
using NUnit.Framework;
using UIAutomationFramework.Driver;

namespace HCA.Tests
{
    [TestFixture(BrowserType.Chrome)]
    [UiTest]
    internal class SignInTests : HcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
            _hcaWebSite.NavigateToMain();
            _hcaWebSite.HeaderControl.UserButtonClick();
            _hcaWebSite.LoginForm.WaitForPresentForm();
        }

        public SignInTests(BrowserType browserType) : base(browserType)
        {
        }

        private HcaWebSite _hcaWebSite;
        private readonly string _userName = "";
        private readonly string _password = "";
        private readonly string _incorrectPassword = "";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //TODO: create user with API helps
        }

        [Test]
        public void BlankFormTest()
        {
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.VerifyValidationMessage("Please fill the form");
        }

        [Test]
        public void BlankPasswordTest()
        {
            _hcaWebSite.LoginForm.FillUserNameField(_userName);
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.VerifyValidationMessage("Please fill the form");
        }

        [Test]
        public void BlankUserNameTest()
        {
            _hcaWebSite.LoginForm.FillPasswordField(_password);
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.VerifyValidationMessage("Please fill the form");
        }

        [Test]
        public void ClickCreateAccountTest()
        {
            _hcaWebSite.LoginForm.SignUpClick();
            _hcaWebSite.SignUpPage.WaitForOpened();
        }

        [Test]
        public void IncorrectPasswordTest()
        {
            _hcaWebSite.LoginForm.FillUserNameField(_userName);
            _hcaWebSite.LoginForm.FillPasswordField(_incorrectPassword);
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.VerifyValidationMessage("The email or password you entered is incorrect");
        }

        [Test]
        public void SuccessTest()
        {
            _hcaWebSite.LoginForm.FillUserNameField(_userName);
            _hcaWebSite.LoginForm.FillPasswordField(_password);
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.WaitForPresentForm();
            _hcaWebSite.HeaderControl.UserButtonClick();
            _hcaWebSite.LoginForm.VerifyLoggedUser();
        }
    }
}