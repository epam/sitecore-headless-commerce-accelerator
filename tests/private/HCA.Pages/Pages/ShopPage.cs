using System;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;
using UIAutomationFramework.Interfaces;

namespace HCA.Pages.Pages
{
    public abstract class ShopPage : IPage
    {
        private readonly WebElement _productGrid =
            new WebElement("product grid", ByCustom.ClassName("listing-product-grid"));

        public void VerifyOpened()
        {
            _productGrid.WaitForPresent();
        }

        public Uri GetUrl()
        {
            throw new NotImplementedException();
        }

        public abstract string GetPath();

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