using System.Linq;
using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.API.Helpers;
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
        public UserManagerHelper UserManager;

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
                var user = TestsData.GetUser();
                UserManager = TestsHelper.CreateUserManagerHelper(user);
                UserManager.CleanPromotions();
                UserManager.CleanCart();
                _hcaWebSite.OpenHcaAndLogin(user.Credentials);
            }
        }

        public CartTests(BrowserType browserType, HcaUserRole hcaUserRole) : base(browserType)
        {
            _hcaUserRole = hcaUserRole;
            _coupon = TestsData.GetDefaultPromotion();
        }

        private readonly HcaPromotionTestsDataSettings _coupon;
        private readonly HcaUserRole _hcaUserRole;
        private HcaWebSite _hcaWebSite;

        [Test]
        public void AddProductToACartTest()
        {
            var product = TestsData.GetProduct();
            _hcaWebSite.AddProductToCart(product.ProductId);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyProductPresent(product.ProductName);
            _hcaWebSite.CartPage.VerifyProductQty(product.ProductName, 1);
            _hcaWebSite.CartPage.VerifyCartSum(false);
        }

        [Test]
        public void AddSecondProductToACartTest()
        {
            var products = TestsData.GetProducts(2);
            _hcaWebSite.AddProductsToCartFromTestData(products);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyProducts(products);
            _hcaWebSite.CartPage.VerifyCartSum(false);
        }

        [Test]
        public void AddSameProductToACartTest()
        {
            var product = TestsData.GetProduct();
            _hcaWebSite.AddProductToCart(product.ProductId);
            _hcaWebSite.AddProductToCart(product.ProductId);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.WaitForProductsLoaded();
            _hcaWebSite.CartPage.VerifyProductPresent(product.ProductName);
            _hcaWebSite.CartPage.VerifyProductQty(product.ProductName, 2);
            _hcaWebSite.CartPage.VerifyCartSum(false);
        }

        [Test]
        public void AddSomeDifferentProductsToACartTest()
        {
            var products = TestsData.GetProducts(5);
            _hcaWebSite.AddProductsToCartFromTestData(products);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.WaitForProductsLoaded();
            _hcaWebSite.CartPage.VerifyProducts(products);
            _hcaWebSite.CartPage.VerifyCartSum(false);
        }

        [Test]
        public void OpenBlankCartTest()
        {
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.VerifyBlankCart();
        }

        [Test]
        public void DeleteOneProductFromCartTest()
        {
            var products = TestsData.GetProducts(2);
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
        public void DeleteAllProductFromCartTest()
        {
            var products = TestsData.GetProducts(3);
            _hcaWebSite.AddProductsToCartFromTestData(products);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.WaitForProductsLoaded();

            _hcaWebSite.CartPage.DeleteProductsFromCart(products);
            _hcaWebSite.CartPage.VerifyBlankCart();
        }

        [Test]
        public void ApplyCorrectPromoCodeTest()
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
        public void ApplyIncorrectPromoCodeTest()
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
        public void CancelPromoCodeTest()
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
        public void ApplyPromoCodeForLowSumTest()
        {
            var product = TestsData.GetProduct();
            _hcaWebSite.AddProductToCart(product.ProductId);
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
        public void ChangeQtyTest()
        {
            var product = TestsData.GetProduct();
            _hcaWebSite.AddProductToCart(product.ProductId);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.WaitForProductsLoaded();
            _hcaWebSite.CartPage.SetQtyForProduct(product.ProductName, 5);
            _hcaWebSite.CartPage.FillDiscountField("");
            _hcaWebSite.CartPage.VerifyProductQty(product.ProductName, 51);
            _hcaWebSite.CartPage.VerifyCartSum(false);
        }

        [Test]
        public void CheckoutTest()
        {
            var product = TestsData.GetProduct();
            _hcaWebSite.AddProductToCart(product.ProductId);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.WaitForProductsLoaded();
            _hcaWebSite.CartPage.ClickCheckoutButton();
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
        }
    }
}
