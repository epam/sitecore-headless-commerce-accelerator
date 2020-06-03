using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.Pages.MyAccount
{
    public class MyAccountAccountDetailsSection : MyAccountPage
    {
        private static MyAccountAccountDetailsSection _myAccountAccountDetailsSection;

        public new static MyAccountAccountDetailsSection Instance =>
            _myAccountAccountDetailsSection ??= new MyAccountAccountDetailsSection();

        protected override WebElement FieldsContainer => new WebElement("Account Details Section",
            ByCustom.XPath("//div[@class = 'account-details-form']"));
    }
}