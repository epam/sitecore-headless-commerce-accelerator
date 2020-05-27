using System;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;
using UIAutomationFramework.Interfaces;

namespace HCA.Pages.Pages
{
    public class CartPage : CommonPage
    {
        private static CartPage _cartPage;

        private readonly WebElement _cartProductList =
            new WebElement("Cart product list", ByCustom.XPath("//ul[@class= 'cartList']"));

        private readonly WebButton _checkoutButton =
            new WebButton("Chekout button", ByCustom.XPath("//a[text() = 'Checkout']"));

        private readonly WebButton _discountApplyButton =
            new WebButton("Discount apply button", ByCustom.XPath("//button[text() = 'Apply']"));

        private readonly WebTextField _discountField =
            new WebTextField("Discount text field", ByCustom.XPath("//input[@id = 'promo-code']"));

        private readonly WebLabel _title = new WebLabel("Title", ByCustom.XPath("//h1[@class = 'title']"));

        public static CartPage Instance =>
            _cartPage ?? (_cartPage = new CartPage());

        public void VerifyOpened()
        {
            _title.WaitForPresent();
            _title.WaitForText("Shopping Cart");
        }

        public override string GetPath()
        {
            return "/cart";
        }

        private WebElement FindProductByName(string productName)
        {
            return new WebElement($"Product {productName}", ByCustom.XPath(
                $".//div[@class = 'heading-productTitle'][text() = '{productName}']")).GetParent(7);
        }

        public void SetQtyForProduct(string productName, int qty)
        {
            var foundProduct = FindProductByName(productName);
            var qtyElement = new WebTextField($"Qty for {productName}",
                ByCustom.XPath(".//div[@class = 'product-qty']/input"), foundProduct);
            qtyElement.Clear();
            qtyElement.Type(qty);
        }

        public void FillDiscountField(string value)
        {
            _discountField.Type(value);
        }

        public void ClickApplyButton()
        {
            _discountApplyButton.Click();
        }

        public void ClickChekoutButton()
        {
            _checkoutButton.Click();
        }
    }
}