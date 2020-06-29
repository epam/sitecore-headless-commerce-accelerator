﻿using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.CommonElements
{
    public class ProductGridSection
    {
        private static ProductGridSection _listingProductGrid;

        private static readonly WebElement _productGrid = new WebElement("Product Grid", ByCustom.ClassName("listing-product-grid"));

        private static readonly WebElement _productItem = new WebElement("Product Item", ByCustom.ClassName("listing-grid-item"));

        private static readonly WebLink _loadMoreLink = new WebLink("Load More Button", ByCustom.ClassName("btn-load-more"));

        private static readonly WebElement _header = new WebElement("Header Of Products", ByCustom.ClassName("header_stats"));

        private static readonly WebElement _lazyLoadingSpinner = new WebElement("Lazy Load Spinner", ByCustom.ClassName("lazyLoad_spinner"));

        public static ProductGridSection Instance => _listingProductGrid ??= new ProductGridSection();

        public int TotalProducts => int.Parse(GetProductsHeaderText().Split(' ').FirstOrDefault());

        public string GetProductsHeaderText()
        {
            _header.IsPresent();
            return _header.GetText();
        }

        public int GetDisplayedProductsCount()
        {
            _productGrid.WaitForPresent();
            return _productGrid.GetChildElementsCount(_productItem);
        }

        public void WaitForLazyLoadingSpinnerIsNotPresent() =>
            _lazyLoadingSpinner.WaitForNotPresent();

        public void WaitForOpened() =>
            _productGrid.WaitForPresent();

        public void ClickLoadMoreLink() =>
            _loadMoreLink.Click();

        public void VerifyLoadMoreLink()
        {
            if (GetDisplayedProductsCount() == TotalProducts)
                _loadMoreLink.VerifyNotPresent();
            else
            {
                _loadMoreLink.IsPresent();
                _loadMoreLink.VerifyText("LOAD MORE");
            }
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
            buttonViewProduct.WaitForPresent();
            buttonViewProduct.Click();
        }

        private static IEnumerable<IWebElement> GetProductElements() =>
            _productGrid.GetChildElements(_productItem);
    }
}
