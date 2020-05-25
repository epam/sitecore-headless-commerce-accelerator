using System;
using System.Collections.Generic;
using System.Text;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.Pages.Checkout
{
    public class CheckoutPaymentPage : CheckoutPage
    {
        private static CheckoutPaymentPage _checkoutPaymentPage;

        public static CheckoutPaymentPage Instance =>
            _checkoutPaymentPage ?? (_checkoutPaymentPage = new CheckoutPaymentPage());

        private readonly WebTextField _cardNumberField =
            new WebTextField("Card Number Field", ByCustom.XPath("//input[@class = 'card-number']"));

        public void FillCardNumber(String value)
        {
            _cardNumberField.Type(value);
        }
        
        public override string GetPath()
        {
            return "Checkout/Payment";
        }
    }
}
