using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.UI;
using AutoTests.HCA.Core.UI.ConstantsAndEnums;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.UITests
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(BrowserType.Chrome)]
    [UiTest]
    internal class SignUpTests : HcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
            _hcaWebSite.NavigateToMain();
            _hcaWebSite.GoToPageWithDefaultParams(PagePrefix.AccountSignUp, TestsData.HcaTestsData);
        }

        public SignUpTests(BrowserType browserType) : base(browserType)
        {
        }

        private HcaWebSite _hcaWebSite;

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
        public void SignUpTests_01_BlankFormTest()
        {
            Assert.False(_hcaWebSite.SignUpPage.SignUpIsClickable());
        }

        [Test]
        public void SignUpTests_02_ConfirmPasswordDoesNotMatchTest()
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
        public void SignUpTests_03_AddExistsUserTest()
        {
            _hcaWebSite.SignUpPage.FillFieldByName("First Name", "FName");
            _hcaWebSite.SignUpPage.FillFieldByName("Last Name", "LName");
            var user = TestsData.UserLogin;
            _hcaWebSite.SignUpPage.FillFieldByName("Email", user.Email);
            _hcaWebSite.SignUpPage.FillFieldByName("Password", "password");
            _hcaWebSite.SignUpPage.FillFieldByName("Confirm Password", "password");
            _hcaWebSite.SignUpPage.VerifyFieldError("Email", "Email is already in use!");
        }

        [Test]
        public void SignUpTests_04_InvalidEmailTest()
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
        public void SignUpTests_05_SuccessTest()
        {
            var firstName = StringHelpers.RandomString(10);
            _hcaWebSite.SignUpPage.FillFieldByName("First Name", firstName);
            var lastName = StringHelpers.RandomString(10);
            _hcaWebSite.SignUpPage.FillFieldByName("Last Name", lastName);
            _hcaWebSite.SignUpPage.FillFieldByName("Email", $"{firstName}@mail.com");
            _hcaWebSite.SignUpPage.FillFieldByName("Password", "password");
            _hcaWebSite.SignUpPage.FillFieldByName("Confirm Password", "password");
            _hcaWebSite.SignUpPage.ClickSignUp();
            _hcaWebSite.SignUpPage.WaitAccountSignUpSuccessMessage();
        }
    }
}