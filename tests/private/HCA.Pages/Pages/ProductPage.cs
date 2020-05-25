using System;
using System.Collections.Generic;
using System.Text;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;
using UIAutomationFramework.Interfaces;

namespace HCA.Pages.Pages
{
    public class ProductPage : IPage
    {
        private static ProductPage _productPage;

        public static ProductPage Instance =>
            _productPage ?? (_productPage = new ProductPage());

        private readonly WebButton _addToCartButton = new WebButton("Add to cart", ByCustom.XPath("//button[@title = 'Add to Cart']"));


        public void AddToCartButtonClick()
        {
            _addToCartButton.Click();
        }
        public void VerifyOpened()
        {
            Browser.WaitForUrlContains("product/");
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
