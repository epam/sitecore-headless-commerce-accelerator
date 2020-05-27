using System;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;
using UIAutomationFramework.Interfaces;

namespace HCA.Pages.Pages
{
    public class ProductPage : CommonPage
    {
        private static ProductPage _productPage;

        private readonly WebButton _addToCartButton =
            new WebButton("Add to cart", ByCustom.XPath("//button[@title = 'Add to Cart']"));

        public static ProductPage Instance =>
            _productPage ?? (_productPage = new ProductPage());

        public override string GetPath()
        {
            throw new NotImplementedException();
        }

        public override void WaitForOpened()
        {
           Browser.WaitForUrlContains("product/");
        }

        public void AddToCartButtonClick()
        {
            _addToCartButton.Click();
        }
    }
}