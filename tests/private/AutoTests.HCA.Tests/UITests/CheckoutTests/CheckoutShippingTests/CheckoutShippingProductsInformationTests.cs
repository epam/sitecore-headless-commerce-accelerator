using System.Collections.Generic;
using System.Linq;
using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.BaseTests;
using AutoTests.HCA.Core.Common.Settings.Products;
using AutoTests.HCA.Core.Common.Settings.Users;
using AutoTests.HCA.Core.UI;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.UITests.CheckoutTests.CheckoutShippingTests
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(BrowserType.Chrome, HcaUserRole.Guest)]
    [TestFixture(BrowserType.Chrome, HcaUserRole.User)]
    [CheckoutTest]
    [UiTest]
    internal class CheckoutShippingProductsInformationTests : BaseHcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
            if (_hcaUserRole == HcaUserRole.Guest)
            {
                _hcaWebSite.NavigateToMain();
            }
            else
            {
                var user = TestsData.GetUser().Credentials;
                var userHelper = TestsHelper.CreateHcaUserApiHelper(user);
                userHelper.CleanPromotions();
                userHelper.CleanCart();
                _hcaWebSite.OpenHcaAndLogin(user);
            }
        }

        public CheckoutShippingProductsInformationTests(BrowserType browserType, HcaUserRole hcaUserRole) : base(
            browserType)
        {
            _hcaUserRole = hcaUserRole;
        }

        private static readonly IEnumerable<ProductTestsDataSettings> _products = TestsData.GetProducts(5).ToList();

        private HcaWebSite _hcaWebSite;
        private readonly HcaUserRole _hcaUserRole;

        [Test]
        public void T1_CheckoutShippingProductsInformation_BlankProductInformation()
        {
            _hcaWebSite.NavigateToPage(_hcaWebSite.CheckoutShippingPage);
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
            _hcaWebSite.CheckoutShippingPage.VerifyProductCount(0);
        }

        [Test]
        public void T2_CheckoutShippingProductsInformation_VerifyOneProduct()
        {
            var product = _products.First();
            _hcaWebSite.AddProductToCart(product);

            _hcaWebSite.NavigateToPage(_hcaWebSite.CheckoutShippingPage);
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
            _hcaWebSite.CheckoutShippingPage.VerifyProductCount(1);
            _hcaWebSite.CheckoutShippingPage.VerifyCartSum(false);
            _hcaWebSite.CheckoutShippingPage.VerifyProductPresent(product.ProductName);
        }


        [Test]
        public void T3_CheckoutShippingProductsInformation_VerifySameProductWithDifferentVariant()
        {
            var products = TestsData.GetDifferentVariantProducts().ToList();
            _hcaWebSite.AddProductsToCartFromTestData(products);

            _hcaWebSite.NavigateToPage(_hcaWebSite.CheckoutShippingPage);
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
            _hcaWebSite.CheckoutShippingPage.VerifyProductCount(products.Count());
            _hcaWebSite.CheckoutShippingPage.VerifyCartSum(false);
            _hcaWebSite.CheckoutShippingPage.VerifyProducts(products);
        }

        [Test]
        public void T4_CheckoutShippingProductsInformation_VerifySumWithDiscount()
        {
            _hcaWebSite.AddProductsToCartFromTestData(_products);
            //TODO apply discount with API
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.WaitForProductsLoaded();
            _hcaWebSite.CartPage.FillDiscountField(TestsData.GetDefaultPromotion().Code);
            _hcaWebSite.CartPage.ClickApplyButton();


            _hcaWebSite.NavigateToPage(_hcaWebSite.CheckoutShippingPage);
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
            _hcaWebSite.CheckoutShippingPage.VerifyProductCount(_products.Count());
            _hcaWebSite.CheckoutShippingPage.VerifyCartSum(true);
            _hcaWebSite.CheckoutShippingPage.VerifyProducts(_products);
        }

        [Test]
        public void T5_CheckoutShippingProductsInformation_VerifySumWithoutDiscount()
        {
            _hcaWebSite.AddProductsToCartFromTestData(_products);

            _hcaWebSite.NavigateToPage(_hcaWebSite.CheckoutShippingPage);
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
            _hcaWebSite.CheckoutShippingPage.VerifyProductCount(_products.Count());
            _hcaWebSite.CheckoutShippingPage.VerifyCartSum(false);
            _hcaWebSite.CheckoutShippingPage.VerifyProducts(_products);
        }

        [Test]
        public void T6_CheckoutShippingProductsInformation_VerifyTwoSameProduct()
        {
            var product = _products.First();
            _hcaWebSite.AddProductToCart(product);
            _hcaWebSite.AddProductToCart(product);

            _hcaWebSite.NavigateToPage(_hcaWebSite.CheckoutShippingPage);
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
            _hcaWebSite.CheckoutShippingPage.VerifyProductCount(1);
            _hcaWebSite.CheckoutShippingPage.VerifyCartSum(false);
            _hcaWebSite.CheckoutShippingPage.VerifyProductPresent(product.ProductName);
        }
    }
}