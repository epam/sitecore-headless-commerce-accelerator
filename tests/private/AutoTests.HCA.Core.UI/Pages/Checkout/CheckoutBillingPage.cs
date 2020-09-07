using AutoTests.HCA.Core.Common.Settings.Checkout;
using AutoTests.HCA.Core.UI.ConstantsAndEnums;

namespace AutoTests.HCA.Core.UI.Pages.Checkout
{
    public class CheckoutBillingPage : CheckoutPage
    {
        private static CheckoutBillingPage _checkoutBillingPage;

        public static CheckoutBillingPage Instance => _checkoutBillingPage ??= new CheckoutBillingPage();

        public override string GetPath()
        {
            return PagePrefix.CheckoutBilling.GetPrefix();
        }

        protected override void FillFieldsByDefault()
        {
            SelectOptionByName(AddressOption.SameAsShippingAddress);
        }
    }
}