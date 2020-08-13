using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.AutomationFramework.UI.Core;
using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.BaseTests;
using AutoTests.HCA.Core.UI;
using AutoTests.HCA.Core.UI.ConstantsAndEnums;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.UITests.MyAccountTests
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(BrowserType.Chrome)]
    [UiTest]
    internal class MyAccountAddressTests : BaseHcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
            _hcaWebSite.GoToPageWithDefaultParams(PagePrefix.Account, TestsData.GetDefProduct(), TestsData.GetUser().Credentials);
            _hcaWebSite.MyAccountPage.WaitForOpened();
        }

        public MyAccountAddressTests(BrowserType browserType) : base(browserType)
        {
        }

        private HcaWebSite _hcaWebSite;

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //TODO: delete user with API helps
        }

        [Test]
        public void MyAccountAddressTests_01_AddAddressClickTest()
        {
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountPage.AddAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            var newName = _hcaWebSite.MyAccountAddressSection.GetFieldValue("First Name");
            Assert.IsEmpty(newName);
        }

        [Test]
        public void MyAccountAddressTests_02_EditAddressClickTest()
        {
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountPage.EditAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            var newName = _hcaWebSite.MyAccountAddressSection.GetFieldValue("First Name");
            Assert.IsNotEmpty(newName);
        }

        //[Test]
        //public void _03_DeleteAllAddresses()
        //{
        //}

        [Test]
        public void MyAccountAddressTests_04_AddressAddTest()
        {
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountNewAddressSection.AddAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            var newName = _hcaWebSite.MyAccountAddressSection.GetFieldValue("First Name");
            Assert.IsEmpty(newName);
            var firstName = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("First Name", firstName);
            var lastName = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("Last Name", lastName);
            var addressLine = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("Address Line", addressLine);
            var city = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("City", city);
            _hcaWebSite.MyAccountNewAddressSection.SelectValueInTheField("Country", "United States");
            _hcaWebSite.MyAccountNewAddressSection.SelectValueInTheField("State", "New York");
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("Postal Code", "10005");
            _hcaWebSite.MyAccountNewAddressSection.ClickSaveAddress();
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses",
                $"{firstName} {lastName}, {addressLine}");
            _hcaWebSite.MyAccountAddressSection.VerifySavedAddress($"{firstName} {lastName}", addressLine,
                $"{city}, NY, United States", "10005");
        }

        [Test]
        public void MyAccountAddressTests_05_DeleteAddressClickWithConfirmTest()
        {
            var newAddress = _hcaWebSite.AddNewAddressForLoggedUser();
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", newAddress);
            _hcaWebSite.MyAccountAddressSection.DeleteAddressClick();
            Alert.Accept();
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.SelectHasNoValue("Addresses", newAddress);
        }

        [Test]
        public void MyAccountAddressTests_06_DeleteAddressClickWithoutConfirmTest()
        {
            var newAddress = _hcaWebSite.AddNewAddressForLoggedUser();
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", newAddress);
            _hcaWebSite.MyAccountAddressSection.DeleteAddressClick();
            Alert.Cancel();
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", newAddress);
        }


        [Test]
        public void MyAccountAddressTests_07_EditAddress()
        {
            var newAddress = _hcaWebSite.AddNewAddressForLoggedUser();
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", newAddress);
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountPage.EditAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            var firstName = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("First Name", firstName);
            var addressString = _hcaWebSite.MyAccountNewAddressSection.ReturnStringForSelectField();
            _hcaWebSite.MyAccountNewAddressSection.ClickSaveAddress();
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.SelectHasNoValue("Addresses", newAddress);
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", addressString);
        }

        [Test]
        public void MyAccountAddressTests_08_CancelEditAddressTest()
        {
            var newAddress = _hcaWebSite.AddNewAddressForLoggedUser();
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", newAddress);
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountPage.EditAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            var firstName = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("First Name", firstName);
            var addressString = _hcaWebSite.MyAccountNewAddressSection.ReturnStringForSelectField();
            _hcaWebSite.MyAccountNewAddressSection.ClickCancel();
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.SelectHasNoValue("Addresses", addressString);
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", newAddress);
        }

        [Test]
        public void MyAccountAddressTests_09_CancelAddNewAddressTest()
        {
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.AddAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            var newName = _hcaWebSite.MyAccountAddressSection.GetFieldValue("First Name");
            Assert.IsEmpty(newName);
            var firstName = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("First Name", firstName);
            var lastName = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("Last Name", lastName);
            var addressLine = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("Address Line", addressLine);
            var city = StringHelpers.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("City", city);
            _hcaWebSite.MyAccountNewAddressSection.SelectValueInTheField("Country", "United States");
            _hcaWebSite.MyAccountNewAddressSection.SelectValueInTheField("State", "New York");
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("Postal Code", "10005");
            var addressString = _hcaWebSite.MyAccountNewAddressSection.ReturnStringForSelectField();
            _hcaWebSite.MyAccountNewAddressSection.ClickCancel();
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.SelectHasNoValue("Addresses", addressString);
        }

        [Test]
        public void MyAccountAddressTests_10_TryToSaveBlankFieldsTest()
        {
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.AddAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            Assert.False(_hcaWebSite.MyAccountNewAddressSection.SaveAddressIsClickable(), "SaveAddressIsClickable");
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
        }
    }
}