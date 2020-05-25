using System;
using System.Collections.Generic;
using System.Text;

namespace HCA.Pages.Pages.Checkout
{
    public class CheckoutShippingPage : CheckoutPage
    {
        private static CheckoutShippingPage _checkoutShippingPage;

        public static CheckoutShippingPage Instance =>
            _checkoutShippingPage ?? (_checkoutShippingPage = new CheckoutShippingPage());
        public override string GetPath()
        {
            return "Checkout/Shipping";
        }
    }
}
