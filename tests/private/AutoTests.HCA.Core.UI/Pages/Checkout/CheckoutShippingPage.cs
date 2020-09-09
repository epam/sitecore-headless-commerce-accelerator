using System;
using System.Collections.Generic;
using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.AutomationFramework.UI.Controls;
using AutoTests.AutomationFramework.UI.Core;
using AutoTests.HCA.Core.Common.Entities.ConstantsAndEnums.Checkout;
using AutoTests.HCA.Core.Common.Entities.ConstantsAndEnums.Fields;
using AutoTests.HCA.Core.Common.Settings.Products;
using AutoTests.HCA.Core.Common.Settings.Users;
using AutoTests.HCA.Core.UI.ConstantsAndEnums;
using NUnit.Framework;

namespace AutoTests.HCA.Core.UI.Pages.Checkout
{
    public class CheckoutShippingPage : CheckoutPage
    {
        private static CheckoutShippingPage _checkoutShippingPage;

        private readonly WebCheckBox _saveAddress = new WebCheckBox("Checkbox save Address to My Account",
            ByCustom.XPath("//label[@for='save-to-account']"));

        private readonly WebElement _sectionCart =
            new WebElement("Cart section", ByCustom.XPath("//section[@class='toggle open']"));

        private readonly WebElement _sectionPricing =
            new WebElement("Pricing section", ByCustom.XPath("//section[@class='no-border']"));

        public static CheckoutShippingPage Instance => _checkoutShippingPage ??= new CheckoutShippingPage();

        public override string GetPath()
        {
            return PagePrefix.CheckoutShipping.GetPrefix();
        }

        public void SetSaveAddress(bool select)
        {
            _saveAddress.Set(select);
        }

        public void FillAddressByDefault(HcaUserRole role)
        {
            FillFieldByName(AddressField.FirstName.GetValue(), "John");
            FillFieldByName(AddressField.LastName.GetValue(), "Smith");
            FillFieldByName(AddressField.AddressLine1.GetValue(), "5th Ave");
            FillFieldByName(AddressField.City.GetValue(), "New York");
            SelectValueInTheField(AddressField.Country.GetValue(), "United States");
            SelectValueInTheField(AddressField.Province.GetValue(), "New York");
            FillFieldByName(AddressField.PostalCode.GetValue(), "10005");
            if (role == HcaUserRole.Guest)
                FillFieldByName(AddressField.EmailAddress.GetValue(), "test@test.com");
        }

        public string CreateNewAddress(HcaUserRole role)
        {
            var firstName = StringHelpers.RandomString(10);
            FillFieldByName(AddressField.FirstName.GetValue(), firstName);
            var lastName = StringHelpers.RandomString(10);
            FillFieldByName(AddressField.LastName.GetValue(), lastName);
            var addressLine = StringHelpers.RandomString(10);
            FillFieldByName(AddressField.AddressLine1.GetValue(), addressLine);
            FillFieldByName(AddressField.City.GetValue(), "New York");
            SelectValueInTheField(AddressField.Country.GetValue(), "United States");
            SelectValueInTheField(AddressField.Province.GetValue(), "New York");
            FillFieldByName(AddressField.PostalCode.GetValue(), "10005");
            if (role == HcaUserRole.Guest)
                FillFieldByName(AddressField.EmailAddress.GetValue(), "test@test.com");
            return $"{firstName} {lastName}, {addressLine}";
        }

        public void ClearField(AddressField addressField)
        {
            if (addressField == AddressField.Country)
                SelectValueInTheField(AddressField.Country.GetValue(), "Not Selected");
            else if (addressField == AddressField.Province)
                SelectValueInTheField(AddressField.Province.GetValue(), "Not Selected");
            else
                FillFieldByName(AddressField.PostalCode.GetValue(), "");
        }

        protected override void FillFieldsByDefault()
        {
            SelectOptionByName(AddressOption.NewAddress);
            FillAddressByDefault(HcaUserRole.Guest);
            SelectOptionByName(AddressOption.AlsoUseForBillingAddress);
            SelectShippingMethod(ShippingMethod.Standard);
        }

        public void VerifyProductCount(int expectedCount)
        {
            if (expectedCount > 0)
                WaitFirstProductLoad();
            Assert.AreEqual(expectedCount, GetProductsCount());
        }

        private int GetProductsCount()
        {
            return _sectionCart.GetChildElementsCount(ByCustom.XPath("./ul/li"));
        }

        private double GetProductSum()
        {
            double sum = 0;
            var productCount = GetProductsCount();
            for (var i = 1; i <= productCount; i++)
            {
                var product = new WebElement($"product number {i}", ByCustom.XPath($"./ul/li[{i}]"), _sectionCart);
                var text = new WebLink($"Product total for {i}",
                    ByCustom.XPath(".//span[@class = 'price']"), product).GetText().Replace("$", "");
                sum += Convert.ToDouble(text);
            }

            return Math.Round(sum, 2);
        }

        private double FindCartSumByText(string text)
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
            if (discount) savingsDetails = FindCartSumByText("Savings (Details):");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(productSum, merchandiseSubtotal, "Merchandise Subtotal:");
                Assert.AreEqual(productSum, Math.Round(estimatedTotal - savingsDetails, 2), "Estimated Total:");
            });
        }

        public void VerifyProducts(IEnumerable<ProductTestsDataSettings> products)
        {
            foreach (var product in products) VerifyProductPresent(product.ProductName);
        }

        public void VerifyProductPresent(string productName)
        {
            FindProductByName(productName).VerifyPresent();
        }

        private WebElement FindProductByName(string productName)
        {
            return new WebElement($"Product {productName}", ByCustom.XPath("//li//figcaption/span[1]" +
                                                                           $"[translate(text(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz') = translate('{productName}', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')]"))
                .GetParent(3);
        }

        private void WaitFirstProductLoad()
        {
            new WebElement("First Product", ByCustom.XPath("//li//figcaption/span[1]")).WaitForPresent();
        }
    }
}