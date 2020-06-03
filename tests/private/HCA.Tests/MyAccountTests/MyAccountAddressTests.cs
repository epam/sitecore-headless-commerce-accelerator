using HCA.Pages;
using NUnit.Framework;
using UIAutomationFramework.Core;
using UIAutomationFramework.Driver;
using UIAutomationFramework.Utils;

namespace HCA.Tests.MyAccountTests
{
    [TestFixture(BrowserType.Chrome)]
    internal class MyAccountAddressTests : HcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
            _hcaWebSite.OpenHcaAndLogin(_userName, _password);
            _hcaWebSite.NavigateToPage(_hcaWebSite.MyAccountPage);
            _hcaWebSite.MyAccountPage.WaitForOpened();
        }

        public MyAccountAddressTests(BrowserType browserType) : base(browserType)
        {
        }

        private HcaWebSite _hcaWebSite = HcaWebSite.Instance;
        private readonly string _userName = "testuser@test.com";
        private readonly string _password = "testuser";

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //TODO: delete user with API helps
        }

        [Test]
        public void _01_AddAddressClickTest()
        {
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountPage.AddAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            var newName = _hcaWebSite.MyAccountAddressSection.GetFieldValue("First Name");
            Assert.True(newName == string.Empty);
        }

        [Test]
        public void _02_EditAddressClickTest()
        {
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountPage.EditAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            var newName = _hcaWebSite.MyAccountAddressSection.GetFieldValue("First Name");
            Assert.True(newName != string.Empty);
        }

        //[Test]
        //public void _03_DeleteAllAddresses()
        //{
        //}

        [Test]
        public void _04_AddressAddTest()
        {
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountNewAddressSection.AddAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            var newName = _hcaWebSite.MyAccountAddressSection.GetFieldValue("First Name");
            Assert.True(newName == string.Empty);
            var firstName = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("First Name", firstName);
            var lastName = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("Last Name", lastName);
            var addressLine = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("Address Line", addressLine);
            var city = StringUtils.RandomString(10);
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
        public void _05_DeleteAddressClickWithConfirmTest()
        {
            var newAddress = _hcaWebSite.AddNewAddressForLoggedUser();
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", newAddress);
            _hcaWebSite.MyAccountAddressSection.DeleteAddressClick();
            Alert.Accept();
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.SelectHasNoValue("Addresses", newAddress);
        }

        [Test]
        public void _06_DeleteAddressClickWithoutConfirmTest()
        {
            var newAddress = _hcaWebSite.AddNewAddressForLoggedUser();
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", newAddress);
            _hcaWebSite.MyAccountAddressSection.DeleteAddressClick();
            Alert.Cancel();
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", newAddress);
        }


        [Test]
        public void _07_EditAddress()
        {
            var newAddress = _hcaWebSite.AddNewAddressForLoggedUser();
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", newAddress);
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountPage.EditAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            var firstName = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("First Name", firstName);
            var addressString = _hcaWebSite.MyAccountNewAddressSection.ReturnStringForSelectField();
            _hcaWebSite.MyAccountNewAddressSection.ClickSaveAddress();
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.SelectHasNoValue("Addresses", newAddress);
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", addressString);
        }

        [Test]
        public void _08_CancelEditAddressTest()
        {
            var newAddress = _hcaWebSite.AddNewAddressForLoggedUser();
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", newAddress);
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountPage.EditAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            var firstName = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("First Name", firstName);
            var addressString = _hcaWebSite.MyAccountNewAddressSection.ReturnStringForSelectField();
            _hcaWebSite.MyAccountNewAddressSection.ClickCancel();
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.SelectHasNoValue("Addresses", addressString);
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", newAddress);
        }

        [Test]
        public void _09_CancelAddNewAddressTest()
        {
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.AddAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            var newName = _hcaWebSite.MyAccountAddressSection.GetFieldValue("First Name");
            Assert.True(newName == string.Empty);
            var firstName = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("First Name", firstName);
            var lastName = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("Last Name", lastName);
            var addressLine = StringUtils.RandomString(10);
            _hcaWebSite.MyAccountNewAddressSection.FillFieldByName("Address Line", addressLine);
            var city = StringUtils.RandomString(10);
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
        public void _10_TryToSaveBlankFieldsTest()
        {
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.AddAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            Assert.False(_hcaWebSite.MyAccountNewAddressSection.SaveAddressIsClickable());
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
        }

        //[Test]
        //public void _11_TryToSaveOneBlankFieldTest()
        //{ }
    }
}