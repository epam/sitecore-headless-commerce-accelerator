using HCA.Pages.ConsantsAndEnums;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.Pages
{
    public abstract class ShopPage : CommonPage
    {
        private readonly WebElement _productGrid = new WebElement("product grid", ByCustom.ClassName("listing-product-grid"));

        public override string GetPath() =>
            PagePrefix.Shop.GetPrefix();

        public override void WaitForOpened()
        {
            _productGrid.WaitForPresent();
        }

        private WebElement FindProductByName(string productName)
        {
            return new WebElement($"Product {productName}",
                ByCustom.XPath($".//li//h2[contains(text(), '{productName}')]"), _productGrid).GetParent(3);
        }

        private WebElement FindProductByIndex(int index)
        {
            return new WebElement($"Product by index {index}", ByCustom.XPath($".//li[{index}]"), _productGrid);
        }

        private WebElement FindViewProductButton(WebElement productElement, string productName)
        {
            return new WebElement($"View product button for {productName}",
                ByCustom.XPath(".//a[text() = 'View Product']"), productElement);
        }

        public void ChooseProduct(string productName)
        {
            var productElement = FindProductByName(productName);
            productElement.MouseOver();
            var buttonViewProduct = FindViewProductButton(productElement, productName);
            //buttonViewProduct.WaitForPresent();
            buttonViewProduct.Click();
        }
    }
}