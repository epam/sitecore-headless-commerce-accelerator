using HCA.Pages.ConstantsAndEnums;

namespace HCA.Pages.Pages.Checkout
{
    public class CheckoutBillingPage : CheckoutPage
    {
        private static CheckoutBillingPage _checkoutBillingPage;

        public static CheckoutBillingPage Instance => _checkoutBillingPage ??= new CheckoutBillingPage();

        public override string GetPath() => 
            PagePrefix.CheckoutBilling.GetPrefix();

        protected override void FillFieldsByDefault() => 
            SelectOptionByName("Same As Shipping Address");
    }
}