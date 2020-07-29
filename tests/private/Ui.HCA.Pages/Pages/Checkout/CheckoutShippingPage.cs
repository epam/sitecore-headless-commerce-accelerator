using Ui.HCA.Pages.ConstantsAndEnums;

namespace Ui.HCA.Pages.Pages.Checkout
{
    public class CheckoutShippingPage : CheckoutPage
    {
        private static CheckoutShippingPage _checkoutShippingPage;

        public static CheckoutShippingPage Instance => _checkoutShippingPage ??= new CheckoutShippingPage();

        public override string GetPath() =>
            PagePrefix.CheckoutShipping.GetPrefix();

        protected override void FillFieldsByDefault()
        {
            SelectOptionByName("A New Address");
            FillFieldByName("First Name", "John");
            FillFieldByName("Last Name", "Smith");
            FillFieldByName("Address Line 1", "5th Ave");
            FillFieldByName("City", "New York");
            SelectValueInTheField("Country", "United States");
            SelectValueInTheField("Province", "New York");
            FillFieldByName("Postal Code", "10005");
            FillFieldByName("Email Address", "test@test.com");
            SelectOptionByName("Also use for billing address");
            SelectShippingMethod("Standard");
        }
    }
}