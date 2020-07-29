using Ui.AutomationFramework.Controls;
using Ui.AutomationFramework.Core;

namespace Ui.HCA.Pages.Pages.MyAccount
{
    public class MyAccountAccountDetailsSection : MyAccountPage
    {
        private static MyAccountAccountDetailsSection _myAccountAccountDetailsSection;

        public static new MyAccountAccountDetailsSection Instance =>
            _myAccountAccountDetailsSection ??= new MyAccountAccountDetailsSection();

        protected override WebElement FieldsContainer => new WebElement("Account Details Section",
            ByCustom.XPath("//div[@class = 'account-details-form']"));
    }
}