using System;
using System.Collections.Generic;
using System.Text;

namespace HCA.Pages.Pages.Checkout
{
    public class CheckoutConfirmationPage : CheckoutPage
    {
        private static CheckoutConfirmationPage _checkoutConfirmationPage;

        public static CheckoutConfirmationPage Instance =>
            _checkoutConfirmationPage ?? (_checkoutConfirmationPage = new CheckoutConfirmationPage());
        public override string GetPath()
        {
            return "Checkout/Confirmation";
        }
    }
}
