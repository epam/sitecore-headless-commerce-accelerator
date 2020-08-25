using AutoTests.AutomationFramework.UI.Controls;
using AutoTests.AutomationFramework.UI.Core;

namespace AutoTests.HCA.Core.UI.Pages.MyAccount
{
    public class MyAccountChangePasswordSection : MyAccountPage
    {
        private static MyAccountChangePasswordSection _myAccountChangePasswordSection;

        public new static MyAccountChangePasswordSection Instance =>
            _myAccountChangePasswordSection ??= new MyAccountChangePasswordSection();

        protected override WebElement FieldsContainer => new WebElement("Change Password Section",
            ByCustom.XPath("//form[@class = 'change-password-form__main']"));
    }
}