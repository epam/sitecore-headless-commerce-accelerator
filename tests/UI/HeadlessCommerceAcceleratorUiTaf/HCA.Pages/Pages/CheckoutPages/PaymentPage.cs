using Core;
using Pages.Pages;

namespace HCA.Pages.Pages.CheckoutPages
{
    public class PaymentPage : BasePage
    {
        private UiElement CardNumber => new UiElement("//input[@class='card-number']");
        private UiElement ExpiresMonth => new UiElement("//select[@name='EXPIRES_MONTH']");
        private UiElement ExpiresYear => new UiElement("//select[@name='EXPIRES_YEAR']");
        private UiElement SecurityCode => new UiElement("//input[@class='security-code']");
        private UiElement PlaceOrderBtn => new UiElement("//button[@type='submit']");

        public PaymentPage FillIn()
        {
            CardNumber.TypeText("4111111111111111");
            ExpiresYear.SeleckByValue("2021");
            SecurityCode.TypeText("123");
            return this;
        }

        public ConfirmationPage PlaceOrder()
        {
            PlaceOrderBtn.Click();
            return new ConfirmationPage();
        }
    }
}
