using HCA.Pages;
using NUnit.Framework;

namespace HCA.Tests
{
    [TestFixture(BrowserType.Chrome)]
    internal class SignInTests : HCAWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HCAWebSite.Instance;
            _hcaWebSite.NavigateToMain();
            _hcaWebSite.HeaderControl.UserButtonClick();
            _hcaWebSite.LoginForm.WaitForPresentForm();
        }

        public SignInTests(BrowserType browserType) : base(browserType)
        {
        }

        private HCAWebSite _hcaWebSite = HCAWebSite.Instance;
        private readonly string userName = "";
        private readonly string password = "";
        private readonly string incorrectPassword = "";

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
            _hcaWebSite.LoginForm.FillUserNameField(userName);
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.VerifyValidationMessage("Please fill the form");
        }

        [Test]
        public void BlankUserNameTest()
        {
            _hcaWebSite.LoginForm.FillPasswordField(password);
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
            _hcaWebSite.LoginForm.FillUserNameField(userName);
            _hcaWebSite.LoginForm.FillPasswordField(incorrectPassword);
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.VerifyValidationMessage("The email or password you entered is incorrect");
        }

        [Test]
        public void SuccessTest()
        {
            _hcaWebSite.LoginForm.FillUserNameField(userName);
            _hcaWebSite.LoginForm.FillPasswordField(password);
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.WaitForPresentForm();
            _hcaWebSite.HeaderControl.UserButtonClick();
            _hcaWebSite.LoginForm.VerifyLoggedUser();
        }
    }
}