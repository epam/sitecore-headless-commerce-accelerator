using Ui.AutomationFramework.Controls;
using Ui.AutomationFramework.Core;
using Ui.HCA.Pages.ConstantsAndEnums;

namespace Ui.HCA.Pages.Pages.MyAccount
{
    public class MyAccountPage : CommonPage
    {
        private static MyAccountPage _myAccountPage;

        private readonly WebLabel _addAddressElement =
            new WebLabel("Add address button", ByCustom.XPath("//a[@class = 'add-link']"));

        private readonly WebElement _buttonChangePassword =
            new WebElement("Save changes button", ByCustom.XPath("//button[text()='Change Password']"));

        private readonly WebElement _buttonSaveChanges =
            new WebElement("Save changes button", ByCustom.XPath("//button[text()='Save Changes']"));

        private readonly WebElement _deleteAddressElement =
            new WebElement("Delete address button", ByCustom.XPath("//a[@class = 'delete-link']"));

        private readonly WebElement _editAddressElement =
            new WebElement("Edit address button", ByCustom.XPath("//a[@class = 'edit-link']"));

        private readonly WebLabel _errorMessage =
            new WebLabel("Error message", ByCustom.XPath("//p[@class ='error-message']"));

        public static MyAccountPage Instance =>
            _myAccountPage ??= new MyAccountPage();

        public override string GetPath()
        {
            return PagePrefix.Account.GetPrefix();
        }

        protected override WebTextField GetInputField(string nameField)
        {
            return new WebTextField($"Container field {nameField}",
                ByCustom.XPath($".//label[contains(text(), '{nameField}')]/following-sibling::*"), FieldsContainer);
        }

        public void SaveChangesClick()
        {
            _buttonSaveChanges.Click();
        }

        public bool SaveChangesIsClickable()
        {
            return _buttonSaveChanges.IsClickable();
        }

        public bool SavePasswordIsClickable()
        {
            return _buttonChangePassword.IsClickable();
        }

        public void SavePasswordClick()
        {
            _buttonChangePassword.Click();
        }

        public void AddAddressClick()
        {
            _addAddressElement.Click();
        }

        public void EditAddressClick()
        {
            _editAddressElement.Click();
        }

        public void DeleteAddressClick()
        {
            _deleteAddressElement.Click();
        }

        public void VerifyErrorLabel(string text)
        {
            _errorMessage.IsPresent();
            _errorMessage.VerifyText(text);
        }
    }
}