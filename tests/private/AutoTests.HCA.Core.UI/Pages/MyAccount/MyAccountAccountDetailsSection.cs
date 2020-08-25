using AutoTests.AutomationFramework.UI.Controls;
using AutoTests.AutomationFramework.UI.Core;

namespace AutoTests.HCA.Core.UI.Pages.MyAccount
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