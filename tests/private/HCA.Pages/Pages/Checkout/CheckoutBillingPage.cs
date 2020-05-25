using System;
using System.Collections.Generic;
using System.Text;

namespace HCA.Pages.Pages.Checkout
{
    public class CheckoutBillingPage : CheckoutPage
    {
        private static CheckoutBillingPage _checkoutBillingPage;

        public static CheckoutBillingPage Instance =>
            _checkoutBillingPage ?? (_checkoutBillingPage = new CheckoutBillingPage());
        public override string GetPath()
        {
            return "Checkout/Billing";
        }
    }
}
