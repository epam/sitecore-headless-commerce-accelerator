using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.Pages.MyAccount
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