using Core;
using Pages.Pages;
namespace HCA.Pages.Pages.CheckoutPages
{
    public class BillingPage : BasePage
    {
        private UiElement SameAsShipping => new UiElement("//label[@for='r12']");
        private UiElement SaveAndContinueBtn => new UiElement("//button[@type='submit']");

        public BillingPage SelectSameAsShippingAddress()
        {
            SameAsShipping.Click();
            return this;
        }

        public PaymentPage SaveAndContinue()
        {
            SaveAndContinueBtn.Click();
            return new PaymentPage();
        }
    }
}
