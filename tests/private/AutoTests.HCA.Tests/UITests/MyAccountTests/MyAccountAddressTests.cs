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
    [MyAccountTest]
    [UiTest]
    internal class MyAccountAddressTests : BaseHcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
            _hcaWebSite.GoToPageWithDefaultParams(PagePrefix.Account, TestsData.GetProduct(),
                TestsData.GetUser().Credentials);
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
        public void T01_MyAccountAddress_AddAddressClick()
        {
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountPage.AddAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            var newName = _hcaWebSite.MyAccountAddressSection.GetFieldValue("First Name");
            Assert.IsEmpty(newName);
        }

        [Test]
        public void T10_MyAccountAddress_TryToSaveBlankFields()
        {
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.AddAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            Assert.False(_hcaWebSite.MyAccountNewAddressSection.SaveAddressIsClickable(), "SaveAddressIsClickable");
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
        }

        [Test]
        public void T02_MyAccountAddress_EditAddressClick()
        {
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountPage.EditAddressClick();
            _hcaWebSite.MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            var newName = _hcaWebSite.MyAccountAddressSection.GetFieldValue("First Name");
            Assert.IsNotEmpty(newName);
        }

        [Test]
        public void T04_MyAccountAddress_AddressAdd()
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
        public void T05_MyAccountAddress_DeleteAddressClickWithConfirm()
        {
            var newAddress = _hcaWebSite.AddNewAddressForLoggedUser();
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", newAddress);
            _hcaWebSite.MyAccountAddressSection.DeleteAddressClick();
            Alert.Accept();
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.SelectHasNoValue("Addresses", newAddress);
        }

        [Test]
        public void T06_MyAccountAddress_DeleteAddressClickWithoutConfirm()
        {
            var newAddress = _hcaWebSite.AddNewAddressForLoggedUser();
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", newAddress);
            _hcaWebSite.MyAccountAddressSection.DeleteAddressClick();
            Alert.Cancel();
            _hcaWebSite.MyAccountAddressSection.WaitForOpenedAdressCard();
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses", newAddress);
        }


        [Test]
        public void T07_MyAccountAddress_EditAddress()
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
        public void T08_MyAccountAddress_CancelEditAddress()
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
        public void T09_MyAccountAddress_CancelAddNewAddress()
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
    }
}