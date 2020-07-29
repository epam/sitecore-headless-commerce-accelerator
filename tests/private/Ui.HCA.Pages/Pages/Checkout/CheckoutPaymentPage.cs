using Ui.AutomationFramework.Controls;
using Ui.AutomationFramework.Core;
using Ui.HCA.Pages.ConstantsAndEnums;

namespace Ui.HCA.Pages.Pages.Checkout
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

        public override string GetPath() =>
            PagePrefix.CheckoutPayment.GetPrefix();

        protected override void FillFieldsByDefault()
        {
            FillCardNumber("4111111111111111");
            FillFieldByName("Security Code", "123");
        }
    }
}