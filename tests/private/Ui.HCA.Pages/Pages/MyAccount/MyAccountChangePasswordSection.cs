using Ui.AutomationFramework.Controls;
using Ui.AutomationFramework.Core;

namespace Ui.HCA.Pages.Pages.MyAccount
{
    public class MyAccountChangePasswordSection : MyAccountPage
    {
        private static MyAccountChangePasswordSection _myAccountChangePasswordSection;

        public static new MyAccountChangePasswordSection Instance =>
            _myAccountChangePasswordSection ??= new MyAccountChangePasswordSection();

        protected override WebElement FieldsContainer => new WebElement("Change Password Section",
            ByCustom.XPath("//form[@class = 'change-password-form__main']"));
    }
}