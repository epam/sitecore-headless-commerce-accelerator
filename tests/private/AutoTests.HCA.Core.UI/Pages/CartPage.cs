using System;
using System.Collections.Generic;
using AutoTests.AutomationFramework.UI.Controls;
using AutoTests.AutomationFramework.UI.Core;
using AutoTests.HCA.Core.Common.Settings.Product;
using AutoTests.HCA.Core.UI.ConstantsAndEnums;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AutoTests.HCA.Core.UI.Pages
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

        private readonly WebLabel _title = 
            new WebLabel("Title", ByCustom.XPath("//h1[@class = 'title']"));
        
        private readonly WebElement _productList = 
            new WebElement("Product list", ByCustom.XPath("//ul[@class='cartList']"));

        private readonly WebLabel _invalidPromoCode = 
            new WebLabel("Invalid promocode", ByCustom.XPath("//p[text()='Invalid promo code']"));
        
        private static readonly WebElement OrderSummary = 
            new WebElement("Order summary section", ByCustom.XPath("//section[@class = 'orderSummary']"));
        
        private readonly WebElement _priceLines = 
            new WebElement("Price lines", ByCustom.XPath("./ul[@class = 'orderSummaryPriceLines orderSummary-list']"), OrderSummary);
        
        public static CartPage Instance => _cartPage ??= new CartPage();

        public new void VerifyOpened()
        {
            _title.WaitForPresent();
            _title.WaitForText("Shopping Cart");
        }

        public void WaitForProductsLoaded()
        {
            _cartProductList.WaitForChildElements(By.XPath("//li[@class='cartList-product']"));
        }

        public void VerifyBlankCart()
        {
            _productList.VerifyNotPresent();
        }

        public int GetProductsCount()
        {
            return _productList.GetChildElementsCount(ByCustom.XPath("./li"));
        }

        public override string GetPath()
        {
            return PagePrefix.Cart.GetPrefix();
        }

        public bool ProductIsPresent(string productName)
        {
            return FindProductByName(productName).IsPresent();
        }

        public void VerifyProductPresent(string productName)
        {
            FindProductByName(productName).VerifyPresent();
        }

        public void VerifyProducts(IEnumerable<ProductTestsDataSettings> products)
        {
            foreach (var product in products)
            {
                VerifyProductPresent(product.ProductName);
                VerifyProductQty(product.ProductName, 1);
            }
        }

        public void WaitProductForNonPresent(string productName)
        {
            FindProductByName(productName).WaitForNotPresent();
        }

        public int GetProductQty(string productName)
        {
            return int.Parse(FindProductQtyByName(productName).GetText());
        }

        public void VerifyProductQty(string productName, int expectedResult)
        {
            Assert.AreEqual(GetProductQty(productName), expectedResult);
        }

        public void SetQtyForProduct(string productName, int qty)
        {
            var qtyElement = FindProductQtyByName(productName);
            qtyElement.MouseOver();
            qtyElement.Click();
            qtyElement.Type(qty);
        }

        public void FillDiscountField(string value)
        {
            _discountField.Type(value);
        }

        public void ClickApplyButton()
        {
            _discountApplyButton.MouseOver();
            _discountApplyButton.Click();
        }

        public void ClickCheckoutButton()
        {
            _checkoutButton.Click();
        }

        private WebElement FindProductByName(string productName)
        {
            return new WebElement($"Product {productName}", ByCustom.XPath("//a[@data-autotests = 'productInfoTitle']" +
                                                                           $"[translate(text(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz') = translate('{productName}', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')]"))
                .GetParent(7);
        }

        private WebTextField FindProductQtyByName(string productName)
        {
            return new WebTextField($"Qty for {productName}", ByCustom.XPath(".//div[@class = 'product-qty']/input"),
                FindProductByName(productName));
        }

        public void DeleteProductFromCart(string productName)
        {
            var productElement = FindProductByName(productName);
            new WebLink($"Delete {productName}", ByCustom.XPath(".//a[@class = 'action action-remove']"), productElement).Click();
        }

        public void DeleteProductsFromCart(IEnumerable<ProductTestsDataSettings> products)
        {
            foreach (var product in products)
            {
                DeleteProductFromCart(product.ProductName);
                WaitProductForNonPresent(product.ProductName);
            }
        }

        //private int GetProductTotalPrice(string productName)
        //{
        //    var productElement = FindProductByName(productName);
        //    return Convert.ToInt32(new WebLink($"Product total for {productName}",
        //        ByCustom.XPath(".//div[@class= 'product-total']/span[@class = 'amount']"), productName).GetText());
        //}

        public void WaitForPresentInvalidPromoCode()
        {
            _invalidPromoCode.WaitForPresent();
        }

        public double GetProductSum()
        {
            double sum = 0;
            var productCount = GetProductsCount();
            for (int i = 1; i <= productCount; i++)
            {
                var product = new WebElement($"product number {i}", ByCustom.XPath($"./li[{i}]"), _productList);
                var text = new WebLink($"Product total for {i}",
                    ByCustom.XPath(".//div[@class= 'product-total']/span[@class = 'amount']"), product).GetText().Replace("$", "");
                sum += Convert.ToDouble(text);
            }
            return Math.Round(sum, 2);
        }

        public double FindCartSumByText(string text)
        {
            return Math.Round(Convert.ToDouble(new WebLink($"Sum {text}",
                ByCustom.XPath($".//span[text()='{text}']/following-sibling::*")).GetText().Replace("$", "")), 2);
        }

        public void VerifyCartSum(bool discount)
        {
            var productSum = GetProductSum();
            var merchandiseSubtotal = FindCartSumByText("Merchandise Subtotal:");
            var estimatedTotal = FindCartSumByText("Estimated Total:");
            var savingsDetails = 0.0;
            if (discount)
            {
                savingsDetails = FindCartSumByText("Savings (Details):");
            }
            Assert.Multiple(() =>
            {
                Assert.AreEqual(productSum, merchandiseSubtotal, "Merchandise Subtotal:");
                Assert.AreEqual(productSum, Math.Round(estimatedTotal - savingsDetails, 2), "Estimated Total:");
            });
        }

        public void DeleteAjustment(string name)
        {
            var adjustmentElement = FindAdjustmentByName(name);
            new WebButton($"Button for delete Ajustment {name}", ByCustom.XPath("./button"), adjustmentElement).Click();
        }

        public void WaitFoDeleteAdjustment(string name)
        {
            FindAdjustmentByName(name).WaitForNotPresent();
        }

        public void WaitForPresentAdjustment(string name)
        {
            FindAdjustmentByName(name).WaitForPresent();
        }

        public bool IsAdjustmentPresent(string name)
        {
            return FindAdjustmentByName(name).IsPresent();
        }

        private static WebElement FindAdjustmentByName(string name)
        {
            return new WebElement($"Ajustment {name}", ByCustom.XPath($"//ul[@class= 'adjustment-list']//li[text()='{name}']")).GetParent();
        }
    }
}
