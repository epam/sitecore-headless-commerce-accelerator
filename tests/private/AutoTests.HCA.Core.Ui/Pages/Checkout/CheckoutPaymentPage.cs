using AutoTests.AutomationFramework.UI.Controls;
using AutoTests.AutomationFramework.UI.Core;
using AutoTests.HCA.Core.UI.ConstantsAndEnums;

namespace AutoTests.HCA.Core.UI.Pages.Checkout
{
    public class CheckoutPaymentPage : CheckoutPage
    {
        private static CheckoutPaymentPage _checkoutPaymentPage;

        private readonly WebTextField _cardNumberField =
            new WebTextField("Card Number Field", ByCustom.XPath("//input[@class = 'card-number']"));

        public static CheckoutPaymentPage Instance => _checkoutPaymentPage ??= new CheckoutPaymentPage();

        public void FillCardNumber(string value)
        {
            _cardNumberField.Type(value);
        }

        public override string GetPath()
        {
            return PagePrefix.CheckoutPayment.GetPrefix();
        }

        protected override void FillFieldsByDefault()
        {
            FillCardNumber("4111111111111111");
            FillFieldByName("Security Code", "123");
        }
    }
}