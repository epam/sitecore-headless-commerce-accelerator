using HCA.Pages.HCAElements;

namespace HCA.Pages.Pages.Checkout
{
    public class CheckoutConfirmationPage : CheckoutPage
    {
        private static CheckoutConfirmationPage _checkoutConfirmationPage;

        public static CheckoutConfirmationPage Instance => _checkoutConfirmationPage ??= new CheckoutConfirmationPage();

        public override string GetPath() =>
            PagePrefix.CheckoutConfirmation.GetPrefix();
    }
}