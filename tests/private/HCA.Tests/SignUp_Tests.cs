using System;
using System.Collections.Generic;
using System.Text;
using HCA.Pages;
using NUnit.Framework;

namespace HCA.Tests
{
    [TestFixture(BrowserType.Chrome)]
    class SignUp_Tests : HCAWebTest
    {
        public SignUp_Tests(BrowserType browserType) : base(browserType)
        { }
        HCAWebSite _hcaWebSite = HCAWebSite.Instance;

        private string _existsUser = "mail@mail.com";

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

        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HCAWebSite.Instance;
            _hcaWebSite.NavigateToMain();
            _hcaWebSite.HeaderControl.UserButtonClick();
            _hcaWebSite.LoginForm.WaitForPresentForm();
            _hcaWebSite.LoginForm.SignUpClick();
            _hcaWebSite.SignUpPage.WaitForOpened();
        }


        [Test]
        public void SignUp_Success()
        {
            _hcaWebSite.SignUpPage.FillFieldByName("First Name", "FName");
            _hcaWebSite.SignUpPage.FillFieldByName("Last Name", "LName");
            //TODO: create generation email function
            _hcaWebSite.SignUpPage.FillFieldByName("Email", "mail@mail.com");
            _hcaWebSite.SignUpPage.FillFieldByName("Password", "password");
            _hcaWebSite.SignUpPage.FillFieldByName("Confirm Password", "password");
            _hcaWebSite.SignUpPage.ClickSignUp();
            _hcaWebSite.SignUpPage.WaitAccountSignUpSuccessMessage();
        }

        [Test]
        public void SignUp_ExistsUser()
        {
            _hcaWebSite.SignUpPage.FillFieldByName("First Name", "FName");
            _hcaWebSite.SignUpPage.FillFieldByName("Last Name", "LName");
            //TODO: create generation email function
            _hcaWebSite.SignUpPage.FillFieldByName("Email", "mail@mail.com");
            _hcaWebSite.SignUpPage.FillFieldByName("Password", "password");
            _hcaWebSite.SignUpPage.FillFieldByName("Confirm Password", "password");
            _hcaWebSite.SignUpPage.VerifyFieldError("Email", "Email is already in use!");
            Assert.False(_hcaWebSite.SignUpPage.SignUpIsClickable());
        }

        [Test]
        public void SignUp_BlankForm()
        {
            Assert.False(_hcaWebSite.SignUpPage.SignUpIsClickable());
        }

        [Test]
        public void SignUp_InvalidEmal()
        {
            _hcaWebSite.SignUpPage.FillFieldByName("First Name", "FName");
            _hcaWebSite.SignUpPage.FillFieldByName("Last Name", "LName");
            //TODO: create generation email function
            _hcaWebSite.SignUpPage.FillFieldByName("Email", "123");
            _hcaWebSite.SignUpPage.FillFieldByName("Password", "password");
            _hcaWebSite.SignUpPage.FillFieldByName("Confirm Password", "password");
            Assert.False(_hcaWebSite.SignUpPage.SignUpIsClickable());
        }

        [Test]
        public void SignUp_PasswordDoesNotMatchConfirmPassword()
        {
            _hcaWebSite.SignUpPage.FillFieldByName("First Name", "FName");
            _hcaWebSite.SignUpPage.FillFieldByName("Last Name", "LName");
            //TODO: create generation email function
            _hcaWebSite.SignUpPage.FillFieldByName("Email", "mail@mail.com");
            _hcaWebSite.SignUpPage.FillFieldByName("Password", "password");
            _hcaWebSite.SignUpPage.FillFieldByName("Confirm Password", "password1");
            Assert.False(_hcaWebSite.SignUpPage.SignUpIsClickable());
        }
    }
}
