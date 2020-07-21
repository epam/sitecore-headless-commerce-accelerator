using HCA.Pages.ConstantsAndEnums;
using OpenQA.Selenium;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.Pages
{
    public class CartPage : CommonPage
    {
        private static CartPage _cartPage;

        private readonly WebElement _cartProductList =
            new WebElement("Cart product list", ByCustom.XPath("//ul[@class= 'cartList']"));

        private readonly WebButton _checkoutButton =
            new WebButton("Checkout button", ByCustom.XPath("//a[text() = 'Checkout']"));

        private readonly WebButton _discountApplyButton =
            new WebButton("Discount apply button", ByCustom.XPath("//button[text() = 'Apply']"));

        private readonly WebTextField _discountField =
            new WebTextField("Discount text field", ByCustom.XPath("//input[@id = 'promo-code']"));

        private readonly WebLabel _title = new WebLabel("Title", ByCustom.XPath("//h1[@class = 'title']"));

        public static CartPage Instance => _cartPage ??= new CartPage();

        public void VerifyOpened()
        {
            _title.WaitForPresent();
            _title.WaitForText("Shopping Cart");
        }

        public void WaitForProductsLoaded() =>
            _cartProductList.WaitForChildElements(By.XPath("//li[@class='cartList-product']"));

        public override string GetPath() => PagePrefix.Cart.GetPrefix();

        public bool ProductIsPresent(string productName) =>
            FindProductByName(productName).IsPresent();

        public int GetProductQty(string productName) =>
            int.Parse(FindProductQtyByName(productName).GetText());

        public void SetQtyForProduct(string productName, int qty)
        {
            var qtyElement = FindProductQtyByName(productName);
            qtyElement.Clear();
            qtyElement.Type(qty);
        }

        public void FillDiscountField(string value) =>
            _discountField.Type(value);

        public void ClickApplyButton() =>
            _discountApplyButton.Click();

        public void ClickChekoutButton() =>
            _checkoutButton.Click();

        private WebElement FindProductByName(string productName) =>
            new WebElement($"Product {productName}", ByCustom.XPath($".//div[@class = 'heading-productTitle']" +
                $"[translate(text(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz') = translate('{productName}', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')]"))
                .GetParent(7);

        private WebTextField FindProductQtyByName(string productName) =>
            new WebTextField($"Qty for {productName}", ByCustom.XPath(".//div[@class = 'product-qty']/input"),
                FindProductByName(productName));
    }
}