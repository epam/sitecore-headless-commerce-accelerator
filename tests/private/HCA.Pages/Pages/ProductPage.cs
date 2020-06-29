using HCA.Pages.ConsantsAndEnums;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.Pages
{
    public class ProductPage : CommonPage
    {
        private static ProductPage _productPage;

        private readonly WebButton _addToCartButton = 
            new WebButton("Add to cart", ByCustom.XPath("//button[@title = 'Add to Cart']"));

        public static ProductPage Instance => _productPage ??= new ProductPage();

        public override string GetPath() =>
            PagePrefix.Product.GetPrefix();

        public override void WaitForOpened()
        {
            Browser.WaitForUrlContains("product/");
        }

        public void AddToCartButtonClick()
        {
            _addToCartButton.Click();
        }

        public string GetProductTitle() => new WebElement("Product Title", ByCustom.ClassName("product-title")).GetText();
    }
}