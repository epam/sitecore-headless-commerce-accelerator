using System;
using System.Collections.Generic;
using System.Text;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;
using UIAutomationFramework.Interfaces;

namespace HCA.Pages.Pages
{
    public class CartPage : IPage
    {
        private static CartPage _cartPage;

        public static CartPage Instance =>
            _cartPage ?? (_cartPage = new CartPage());

        private readonly WebTextField _discountField =  new WebTextField("Discount text field", ByCustom.XPath("//input[@id = 'promo-code']"));

        private readonly WebButton _discountApplyButton = new WebButton("Discount apply button", ByCustom.XPath("//button[text() = 'Apply']"));
        
        private readonly WebButton _checkoutButton = new WebButton("Chekout button", ByCustom.XPath("//a[text() = 'Checkout']"));
        
        private readonly WebLabel _title = new WebLabel("Title", ByCustom.XPath("//h1[@class = 'title']"));
        
        private  readonly WebElement _cartProductList = new WebElement("Cart product list", ByCustom.XPath("//ul[@class= 'cartList']"));

        private WebElement FindProductByName(String productName)
        {
            return new WebElement($"Product {productName}", ByCustom.XPath(
                $".//div[@class = 'heading-productTitle'][text() = '{productName}']")).GetParent(7);
        }

        public void SetQtyForProduct(String productName, Int32 qty)
        {
            var foundProduct = FindProductByName(productName);
            WebTextField qtyElement = new WebTextField($"Qty for {productName}", 
                ByCustom.XPath(".//div[@class = 'product-qty']/input"), foundProduct);
            qtyElement.Clear();
            qtyElement.Type(qty);
        }
        
        public void FillDiscountField(String value)
        {
            _discountField.Type(value);
        }

        public void ClickApplyButton()
        {
            _discountApplyButton.Click();
        }
        public void VerifyOpened()
        {
            _title.WaitForPresent();
            _title.WaitForText("Shopping Cart");
        }

        public void ClickChekoutButton()
        {
            _checkoutButton.Click();
        }

        public Uri GetUrl()
        {
            throw new NotImplementedException();
        }

        public string GetPath()
        {
            throw new NotImplementedException();
        }
    }
}
