using Ui.AutomationFramework.Controls;
using Ui.AutomationFramework.Core;

namespace Ui.HCA.Pages.Pages.MyAccount
{
    public class MyAccountNewAddressSection : MyAccountPage
    {
        private static MyAccountNewAddressSection _myAccountNewAddressSection;

        private static readonly WebElement NewAddressForm =
            new WebElement("New Address Form", ByCustom.XPath("//form[@class = 'address-form']"));

        private readonly WebElement _cancelSaveAddressButton = new WebElement("Cancel save address button",
            ByCustom.XPath("//button[text()='Cancel']"), NewAddressForm);

        private readonly WebElement _saveAddressButton = new WebElement("Save address button",
            ByCustom.XPath("//button[text()='Submit']"), NewAddressForm);

        public static new MyAccountNewAddressSection Instance =>
            _myAccountNewAddressSection ??= new MyAccountNewAddressSection();

        protected override WebElement FieldsContainer =>
            new WebElement("New Address Section", ByCustom.XPath("//form[@class = 'address-form']"));

        public void WaitForOpenedNewAddressForm()
        {
            NewAddressForm.WaitForPresent();
        }

        public void ClickSaveAddress()
        {
            _saveAddressButton.Click();
        }

        public bool SaveAddressIsClickable()
        {
            return _saveAddressButton.IsEnabled();
        }

        public void ClickCancel()
        {
            _cancelSaveAddressButton.Click();
        }

        public string ReturnStringForSelectField()
        {
            var firstName = GetFieldValue("First Name");
            var lastName = GetFieldValue("Last Name");
            var addressLine = GetFieldValue("Address Line");
            return $"{firstName} {lastName}, {addressLine}";
        }
    }
}