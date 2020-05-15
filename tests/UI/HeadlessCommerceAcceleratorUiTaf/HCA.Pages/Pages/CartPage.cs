using Core;
using HCA.Pages.Pages.CheckoutPages;

namespace HCA.Pages.Pages
{
    public class CartPage
    {
        private UiElement Qty => new UiElement("//input[contains(@id,'qty')]");
        private UiElement PromoCode => new UiElement("//input[@id='promo-code']");
        private UiElement ApplyBtn => new UiElement("//button[@class='btn small']");
        private UiElement Savings => new UiElement("//span[text()='Savings (Details):']");
        private UiElement CheckoutBtn => new UiElement("//a[contains(@href,'Checkout') and @class='btn']");

        public CartPage UpdateProductQty(int value)
        {
            Qty.Clear();
            Qty.TypeText(value.ToString());
            return this;
        }

        public CartPage ApplyPromoCode()
        {
            PromoCode.Clear();
            PromoCode.TypeText("HABRTRNC15P");
            ApplyBtn.Click();
            DriverContext.Driver.WaitUntilElementAppears(Savings.Locator);
            return this;
        }

        public ShippingPage CheckoutWithPromoCode()
        {
            ApplyPromoCode();
            CheckoutBtn.Click();
            return new ShippingPage();
        }
    }
}
