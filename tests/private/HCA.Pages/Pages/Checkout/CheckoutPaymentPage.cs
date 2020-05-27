using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.Pages.Checkout
{
    public class CheckoutPaymentPage : CheckoutPage
    {
        private static CheckoutPaymentPage _checkoutPaymentPage;

        private readonly WebTextField _cardNumberField =
            new WebTextField("Card Number Field", ByCustom.XPath("//input[@class = 'card-number']"));

        public static CheckoutPaymentPage Instance =>
            _checkoutPaymentPage ?? (_checkoutPaymentPage = new CheckoutPaymentPage());

        public void FillCardNumber(string value)
        {
            _cardNumberField.Type(value);
        }

        public override string GetPath()
        {
            return "Checkout/Payment";
        }
    }
}