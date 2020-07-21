using HCA.Pages;
using HCA.Pages.ConstantsAndEnums;
using NUnit.Framework;
using UIAutomationFramework.Driver;
using UIAutomationFramework.Utils;

namespace HCA.Tests.UITests.MyAccountTests
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(BrowserType.Chrome)]
    [UiTest]
    internal class MyAccountNameTests : HcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
            _hcaWebSite.GoToPageWithDefaultParams(PagePrefix.Account);
            _hcaWebSite.MyAccountPage.WaitForOpened();
        }

        public MyAccountNameTests(BrowserType browserType) : base(browserType)
        {
        }

        private HcaWebSite _hcaWebSite;

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //TODO: delete user with API helps
        }

        [Test]
        public void MyAccountNameTests_01_ChangeFirstNameTest()
        {
            _hcaWebSite.MyAccountAccountDetailsSection.GetFieldValue("First Name");
            var newGeneratedName = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountAccountDetailsSection.FillFieldByName("First Name", newGeneratedName);
            _hcaWebSite.MyAccountAccountDetailsSection.SaveChangesClick();
            _hcaWebSite.NavigateToMain();
            _hcaWebSite.NavigateToPage(_hcaWebSite.MyAccountPage);
            _hcaWebSite.MyAccountPage.WaitForOpened();
            _hcaWebSite.MyAccountAccountDetailsSection.VerifyFieldValue("First Name", newGeneratedName);
        }

        [Test]
        public void MyAccountNameTests_02_ChangeLastNameTest()
        {
            _hcaWebSite.MyAccountAccountDetailsSection.GetFieldValue("Last Name");
            var newGeneratedLastName = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountAccountDetailsSection.FillFieldByName("Last Name", newGeneratedLastName);
            _hcaWebSite.MyAccountAccountDetailsSection.SaveChangesClick();
            _hcaWebSite.NavigateToMain();
            _hcaWebSite.NavigateToPage(_hcaWebSite.MyAccountPage);
            _hcaWebSite.MyAccountPage.WaitForOpened();
            _hcaWebSite.MyAccountAccountDetailsSection.VerifyFieldValue("Last Name", newGeneratedLastName);
        }

        [Test]
        public void MyAccountNameTests_03_ChangeAccountDetailsWithoutSaveTest()
        {
            var oldName = _hcaWebSite.MyAccountAccountDetailsSection.GetFieldValue("First Name");
            var oldLastName = _hcaWebSite.MyAccountAccountDetailsSection.GetFieldValue("Last Name");
            var newGeneratedName = StringUtils.RandomString(10);
            var newGeneratedLastName = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountAccountDetailsSection.FillFieldByName("First Name", newGeneratedName);
            _hcaWebSite.MyAccountAccountDetailsSection.FillFieldByName("Last Name", newGeneratedLastName);
            _hcaWebSite.NavigateToMain();
            _hcaWebSite.NavigateToPage(_hcaWebSite.MyAccountPage);
            _hcaWebSite.MyAccountPage.WaitForOpened();
            _hcaWebSite.MyAccountAccountDetailsSection.VerifyFieldValue("First Name", oldName);
            _hcaWebSite.MyAccountAccountDetailsSection.VerifyFieldValue("Last Name", oldLastName);
        }

        [Test]
        public void MyAccountNameTests_04_TryToSaveWithoutFirstName()
        {
            var oldName = _hcaWebSite.MyAccountAccountDetailsSection.GetFieldValue("First Name");
            _hcaWebSite.MyAccountAccountDetailsSection.FillFieldByName("First Name", "");
            _hcaWebSite.MyAccountAccountDetailsSection.SaveChangesClick();
            _hcaWebSite.NavigateToMain();
            _hcaWebSite.NavigateToPage(_hcaWebSite.MyAccountPage);
            _hcaWebSite.MyAccountPage.WaitForOpened();
            _hcaWebSite.MyAccountPage.VerifyFieldValue("First Name", oldName);
        }

        [Test]
        public void MyAccountNameTests_05_TryToSaveWithoutLastName()
        {
            var oldLastName = _hcaWebSite.MyAccountAccountDetailsSection.GetFieldValue("Last Name");
            _hcaWebSite.MyAccountAccountDetailsSection.FillFieldByName("Last Name", "");
            _hcaWebSite.MyAccountAccountDetailsSection.SaveChangesClick();
            _hcaWebSite.NavigateToMain();
            _hcaWebSite.NavigateToPage(_hcaWebSite.MyAccountPage);
            _hcaWebSite.MyAccountPage.WaitForOpened();
            _hcaWebSite.MyAccountPage.VerifyFieldValue("Last Name", oldLastName);
        }
    }
}