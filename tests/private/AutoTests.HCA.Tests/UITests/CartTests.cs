using System.Linq;
using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.API.HcaApi.Helpers;
using AutoTests.HCA.Core.BaseTests;
using AutoTests.HCA.Core.Common.Settings.Promotions;
using AutoTests.HCA.Core.Common.Settings.Users;
using AutoTests.HCA.Core.UI;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.UITests
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(BrowserType.Chrome, HcaUserRole.Guest)]
    [TestFixture(BrowserType.Chrome, HcaUserRole.User)]
    [UiTest]
    internal class CartTests : BaseHcaWebTest
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
                ApiHelper = TestsHelper.CreateHcaUserApiHelper(user);
                ApiHelper.CleanPromotions();
                ApiHelper.CleanCart();
                _hcaWebSite.OpenHcaAndLogin(user);
            }
        }

        public IHcaApiHelper ApiHelper;

        public CartTests(BrowserType browserType, HcaUserRole hcaUserRole) : base(browserType)
        {
            _hcaUserRole = hcaUserRole;
            _coupon = TestsData.GetDefaultPromotion();
        }

        private readonly HcaPromotionTestsDataSettings _coupon;
        private readonly HcaUserRole _hcaUserRole;
        private HcaWebSite _hcaWebSite;

        [Test]
        public void T1_Cart_AddProductToACart()
        {
            var product = TestsData.GetProduct();
            _hcaWebSite.AddProductToCart(product);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyProductPresent(product.ProductName);
            _hcaWebSite.CartPage.VerifyProductQty(product.ProductName, 1);
            _hcaWebSite.CartPage.VerifyCartSum(false);
        }

        [Test]
        public void T10_Cart_Checkout()
        {
            var product = TestsData.GetProduct();
            _hcaWebSite.AddProductToCart(product);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.WaitForProductsLoaded();
            _hcaWebSite.CartPage.ClickCheckoutButton();
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
        }

        [Test]
        public void T11_Cart_DeleteAllProductFromCart()
        {
            var products = TestsData.GetProducts(3).ToList();
            _hcaWebSite.AddProductsToCartFromTestData(products);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.WaitForProductsLoaded();

            _hcaWebSite.CartPage.DeleteProductsFromCart(products);
            _hcaWebSite.CartPage.VerifyBlankCart();
        }

        [Test]
        public void T12_Cart_DeleteOneProductFromCart()
        {
            var products = TestsData.GetProducts(2).ToList();
            _hcaWebSite.AddProductsToCartFromTestData(products);

            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.WaitForProductsLoaded();

            var product = products.ElementAt(1);
            _hcaWebSite.CartPage.DeleteProductFromCart(product.ProductName);
            _hcaWebSite.CartPage.WaitProductForNonPresent(product.ProductName);
            _hcaWebSite.CartPage.VerifyCartSum(false);
        }

        [Test]
        public void T13_Cart_OpenBlankCart()
        {
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.VerifyBlankCart();
        }

        [Test]
        public void T2_Cart_AddSameProductToACart()
        {
            var product = TestsData.GetProduct();
            _hcaWebSite.AddProductToCart(product);
            _hcaWebSite.AddProductToCart(product);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.WaitForProductsLoaded();
            _hcaWebSite.CartPage.VerifyProductPresent(product.ProductName);
            _hcaWebSite.CartPage.VerifyProductQty(product.ProductName, 2);
            _hcaWebSite.CartPage.VerifyCartSum(false);
        }

        [Test]
        public void T3_Cart_AddSecondProductToACart()
        {
            var products = TestsData.GetProducts(2).ToList();
            _hcaWebSite.AddProductsToCartFromTestData(products);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyProducts(products);
            _hcaWebSite.CartPage.VerifyCartSum(false);
        }

        [Test]
        public void T4_Cart_AddSomeDifferentProductsToACart()
        {
            var products = TestsData.GetProducts(5).ToList();
            _hcaWebSite.AddProductsToCartFromTestData(products);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.WaitForProductsLoaded();
            _hcaWebSite.CartPage.VerifyProducts(products);
            _hcaWebSite.CartPage.VerifyCartSum(false);
        }

        [Test]
        public void T5_Cart_ApplyCorrectPromoCode()
        {
            _hcaWebSite.AddProductsToCartFromTestData(TestsData.GetProducts(3));
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.WaitForProductsLoaded();
            _hcaWebSite.CartPage.FillDiscountField(_coupon.Code);
            _hcaWebSite.CartPage.ClickApplyButton();
            _hcaWebSite.CartPage.VerifyCartSum(true);
        }

        [Test]
        public void T6_Cart_ApplyIncorrectPromoCode()
        {
            _hcaWebSite.AddProductsToCartFromTestData(TestsData.GetProducts(3));
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.WaitForProductsLoaded();
            _hcaWebSite.CartPage.FillDiscountField("S");
            _hcaWebSite.CartPage.ClickApplyButton();
            _hcaWebSite.CartPage.WaitForPresentInvalidPromoCode();
            _hcaWebSite.CartPage.VerifyCartSum(false);
        }

        [Test]
        public void T7_Cart_ApplyPromoCodeForLowSum()
        {
            var product = TestsData.GetDefaultProduct();
            _hcaWebSite.AddProductToCart(product);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.WaitForProductsLoaded();
            _hcaWebSite.CartPage.FillDiscountField(_coupon.Code);
            _hcaWebSite.CartPage.ClickApplyButton();
            _hcaWebSite.CartPage.ClickApplyButton();
            _hcaWebSite.CartPage.WaitForPresentInvalidPromoCode();
            _hcaWebSite.CartPage.VerifyCartSum(false);
        }

        [Test]
        public void T8_Cart_CancelPromoCode()
        {
            _hcaWebSite.AddProductsToCartFromTestData(TestsData.GetProducts(3));
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.WaitForProductsLoaded();
            _hcaWebSite.CartPage.FillDiscountField(_coupon.Code);
            _hcaWebSite.CartPage.ClickApplyButton();
            _hcaWebSite.CartPage.WaitForPresentAdjustment(_coupon.DisplayCartText);
            _hcaWebSite.CartPage.VerifyCartSum(true);
            _hcaWebSite.CartPage.DeleteAdjustment(_coupon.DisplayCartText);
            _hcaWebSite.CartPage.WaitFoDeleteAdjustment(_coupon.DisplayCartText);
        }

        [Test]
        public void T9_Cart_ChangeQty()
        {
            var product = TestsData.GetProduct();
            _hcaWebSite.AddProductToCart(product);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.WaitForProductsLoaded();
            _hcaWebSite.CartPage.SetQtyForProduct(product.ProductName, 5);
            _hcaWebSite.CartPage.FillDiscountField("");
            _hcaWebSite.CartPage.VerifyProductQty(product.ProductName, 51);
            _hcaWebSite.CartPage.VerifyCartSum(false);
        }
    }
}