using HCA.Pages;
using NUnit.Framework;
using UIAutomationFramework.Driver;
using UIAutomationFramework.Utils;

namespace HCA.Tests.UITests
{
    [TestFixture(BrowserType.Chrome)]
    [UiTest]
    internal class SignUpTests : HcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
            _hcaWebSite.NavigateToMain();
            _hcaWebSite.HeaderControl.UserButtonClick();
            _hcaWebSite.LoginForm.WaitForPresentForm();
            _hcaWebSite.LoginForm.SignUpClick();
            _hcaWebSite.SignUpPage.WaitForOpened();
        }

        public SignUpTests(BrowserType browserType) : base(browserType)
        {
        }

        private HcaWebSite _hcaWebSite;

        private readonly string _existsUser = "";

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
        public void BlankFormTest()
        {
            Assert.False(_hcaWebSite.SignUpPage.SignUpIsClickable());
        }

        [Test]
        public void ConfirmPasswordDoesNotMatchTest()
        {
            _hcaWebSite.SignUpPage.FillFieldByName("First Name", "FName");
            _hcaWebSite.SignUpPage.FillFieldByName("Last Name", "LName");
            //TODO: create generation email function
            _hcaWebSite.SignUpPage.FillFieldByName("Email", "mail@mail.com");
            _hcaWebSite.SignUpPage.FillFieldByName("Password", "password");
            _hcaWebSite.SignUpPage.FillFieldByName("Confirm Password", "password1");
            Assert.False(_hcaWebSite.SignUpPage.SignUpIsClickable());
        }

        [Test]
        public void ExistsUserTest()
        {
            _hcaWebSite.SignUpPage.FillFieldByName("First Name", "FName");
            _hcaWebSite.SignUpPage.FillFieldByName("Last Name", "LName");
            //TODO: create generation email function
            _hcaWebSite.SignUpPage.FillFieldByName("Email", _existsUser);
            _hcaWebSite.SignUpPage.FillFieldByName("Password", "password");
            _hcaWebSite.SignUpPage.FillFieldByName("Confirm Password", "password");
            _hcaWebSite.SignUpPage.VerifyFieldError("Email", "Email is already in use!");
            Assert.False(_hcaWebSite.SignUpPage.SignUpIsClickable());
        }

        [Test]
        public void InvalidEmalTest()
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
        public void SuccessTest()
        {
            var firstName = StringUtils.RandomString(10);
            _hcaWebSite.SignUpPage.FillFieldByName("First Name", firstName);
            var lastName = StringUtils.RandomString(10);
            _hcaWebSite.SignUpPage.FillFieldByName("Last Name", lastName);
            //TODO: create generation email function
            _hcaWebSite.SignUpPage.FillFieldByName("Email", $"{firstName}@mail.com");
            _hcaWebSite.SignUpPage.FillFieldByName("Password", "password");
            _hcaWebSite.SignUpPage.FillFieldByName("Confirm Password", "password");
            _hcaWebSite.SignUpPage.ClickSignUp();
            _hcaWebSite.SignUpPage.WaitAccountSignUpSuccessMessage();
        }
    }
}