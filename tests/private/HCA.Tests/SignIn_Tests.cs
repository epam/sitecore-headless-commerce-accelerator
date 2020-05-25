using System;
using System.Collections.Generic;
using System.Text;
using HCA.Pages;
using NUnit.Framework;

namespace HCA.Tests
{
    [TestFixture(BrowserType.Chrome)]
    internal class SignIn_Tests : HCAWebTest
    {
        public SignIn_Tests(BrowserType browserType) : base(browserType)
        { }
        HCAWebSite _hcaWebSite = HCAWebSite.Instance;
        private String userName;
        private String password;
        private String incorrectPassword;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //TODO: create user with API helps
        }

        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HCAWebSite.Instance;
            _hcaWebSite.NavigateToMain();
            _hcaWebSite.HeaderControl.UserButtonClick();
            _hcaWebSite.LoginForm.WaitForPresentForm();
        }

        [Test]
        public void SignIn_Success()
        {
            _hcaWebSite.LoginForm.FillUserNameField(userName);
            _hcaWebSite.LoginForm.FillPasswordField(password);
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.WaitForPresentForm();
            _hcaWebSite.HeaderControl.UserButtonClick();
            _hcaWebSite.LoginForm.VerifyLoggedUser();
        }

        [Test]
        public void SignIn_BlankForm()
        {
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.VerifyValidationMessage("Please fill the form");
        }

        [Test]
        public void SignIn_BlankUserName()
        {
            _hcaWebSite.LoginForm.FillPasswordField(password);
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.VerifyValidationMessage("Please fill the form");
        }

        [Test]
        public void SignIn_BlankPassword()
        {
            _hcaWebSite.LoginForm.FillUserNameField(userName);
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.VerifyValidationMessage("Please fill the form");
        }

        [Test]
        public void SignIn_IncorrectPassword()
        {
            _hcaWebSite.LoginForm.FillUserNameField(userName);
            _hcaWebSite.LoginForm.FillPasswordField(incorrectPassword);
            _hcaWebSite.LoginForm.SignInButtonClick();
            _hcaWebSite.LoginForm.VerifyValidationMessage("The email or password you entered is incorrect");
        }

        [Test]
        public void SignIn_ClickCreateAccountt()
        {
            _hcaWebSite.LoginForm.SignUpClick();
            _hcaWebSite.SignUpPage.WaitForOpened();
        }
    }
}
