using System;
using System.Collections.Generic;
using System.Text;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;
using UIAutomationFramework.Interfaces;

namespace HCA.Pages.Pages
{
    abstract public class ShopPage : IPage
    {
        private readonly WebElement _productGrid = new WebElement("product grid", ByCustom.ClassName("listing-product-grid"));

        private WebElement FindProductByName(String productName)
        {
           return new WebElement($"Product {productName}", ByCustom.XPath($".//li//h2[contains(text(), '{productName}')]"), _productGrid).GetParent(3);
        }

        private WebElement FindProductByIndex(Int32 index)
        {
            return new WebElement($"Product by index {index}", ByCustom.XPath($".//li[{index}]"), _productGrid);

        }

        private WebElement FindViewProductButton(WebElement productElement, String productName)
        {
            return new WebElement($"View product button for {productName}", ByCustom.XPath(".//a[text() = 'View Product']"), productElement);
        }

        public void ChooseProduct(String productName)
        {
            WebElement productElement = FindProductByName(productName);
            productElement.MouseOver();
            WebElement buttonViewProduct = FindViewProductButton(productElement, productName);
            //buttonViewProduct.WaitForPresent();
            buttonViewProduct.Click();

        }

        public void VerifyOpened()
        {
            _productGrid.WaitForPresent();
        }

        public Uri GetUrl()
        {
            throw new NotImplementedException();
        }

        abstract public string GetPath();
    }
}
