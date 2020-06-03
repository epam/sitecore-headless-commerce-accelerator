using NUnit.Framework;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.Pages.MyAccount
{
    public class MyAccountAddressSection : MyAccountPage
    {
        private static MyAccountAddressSection _myAccountAddressSection;

        private readonly WebLabel _addressManagerCard = new WebLabel("Address manager card",
            ByCustom.XPath("//div[@class = 'address-manager__card']"));

        public new static MyAccountAddressSection Instance =>
            _myAccountAddressSection ??= new MyAccountAddressSection();

        protected override WebElement FieldsContainer => new WebElement("Address Section",
            ByCustom.XPath("//div[@class = 'address-manager__main']"));

        public string GetStringForSelect()
        {
            var name = new WebLabel("Name string", ByCustom.XPath("./span[2]"), _addressManagerCard).GetText();
            var address = new WebLabel("Address string", ByCustom.XPath("./span[3]"), _addressManagerCard).GetText();
            return $"{name}, {address}";
        }

        public void VerifySavedAddress(string name, string addressString, string location, string postalCode)
        {
            Assert.Multiple(
                () =>
                {
                    new WebLabel("Name string", ByCustom.XPath("./span[2]"), _addressManagerCard).VerifyText(name);
                    new WebLabel("Address string", ByCustom.XPath("./span[3]"), _addressManagerCard).VerifyText(
                        addressString);
                    new WebLabel("Location string", ByCustom.XPath("./span[4]"), _addressManagerCard).VerifyText(
                        location);
                    new WebLabel("Postal code string", ByCustom.XPath("./span[5]"), _addressManagerCard).VerifyText(
                        postalCode);
                });
        }

        public void WaitForOpenedAdressCard()
        {
            _addressManagerCard.WaitForPresent();
        }
    }
}