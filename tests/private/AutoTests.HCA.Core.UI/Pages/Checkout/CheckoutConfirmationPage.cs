using AutoTests.HCA.Core.UI.ConstantsAndEnums;

namespace AutoTests.HCA.Core.UI.Pages.Checkout
{
    public class CheckoutConfirmationPage : CheckoutPage
    {
        private static CheckoutConfirmationPage _checkoutConfirmationPage;

        public static CheckoutConfirmationPage Instance => _checkoutConfirmationPage ??= new CheckoutConfirmationPage();

        public override string GetPath()
        {
            return PagePrefix.CheckoutConfirmation.GetPrefix();
        }
    }
}