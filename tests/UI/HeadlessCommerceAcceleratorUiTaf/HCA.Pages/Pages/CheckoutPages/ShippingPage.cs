using Core;
using Pages.Pages;

namespace HCA.Pages.Pages.CheckoutPages
{
    public class ShippingPage : BasePage
    {
        private UiElement NewAddress => new UiElement("//label[@for='r1']");
        private UiElement FirstName => new UiElement("//input[@name='@SHIPPING/FIRST_NAME']");
        private UiElement LastName => new UiElement("//input[@name='@SHIPPING/LAST_NAME']");
        private UiElement AddressLine => new UiElement("//input[@name='@SHIPPING/ADDRESS_LINE']");
        private UiElement City => new UiElement("//input[@name='@SHIPPING/CITY']");
        private UiElement Country => new UiElement("//select[@name='@SHIPPING/COUNTRY']");
        private UiElement Province => new UiElement("//select[@name='@SHIPPING/PROVINCE']");
        private UiElement PostalCode => new UiElement("//input[@name='@SHIPPING/POSTAL_CODE']");
        private UiElement Email => new UiElement("//input[@name='@SHIPPING/EMAIL']");
        private UiElement UseForBilling => new UiElement("//label[@for='use-for-billing']");
        private UiElement ShippingMethod => new UiElement("//select[@name='@SHIPPING/SELECTED_SHIPPING_METHOD']");
        private UiElement SaveAndContinueBtn => new UiElement("//button[@type='submit']");

        public ShippingPage FillIn()
        {
            FirstName.TypeText("TaFirstName");
            LastName.TypeText("TaLastName");
            AddressLine.TypeText("Ta address 1");
            City.TypeText("TaCity");
            Country.SeleckByValue("US");
            Province.SeleckByValue("NY");
            PostalCode.TypeText("10002");
            Email.TypeText("test@test.ta");
            UseForBilling.Click();
            ShippingMethod.SeleckByText("Standard", true);
            return this;
        }

        public BillingPage SaveAndContinue()
        {
            SaveAndContinueBtn.Click(true);
            return new BillingPage();
        }
    }
}
