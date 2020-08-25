using System.Linq;
using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.BaseTests;
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
                var user = TestsData.GetUser();
                var userManager = TestsHelper.CreateUserManagerHelper(user);
                //ToDo clean promocode
                userManager.CleanCart();
                _hcaWebSite.OpenHcaAndLogin(user.Credentials);
            }
        }

        public CartTests(BrowserType browserType, HcaUserRole hcaUserRole) : base(browserType)
        {
            _hcaUserRole = hcaUserRole;
        }

        private HcaWebSite _hcaWebSite;
        private readonly HcaUserRole _hcaUserRole;

        [Test]
        public void AddProductToACartTest()
        {
            var product = TestsData.GetProduct();
            _hcaWebSite.AddProductToCart(product.ProductId);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyProductPresent(product.ProductName);
            _hcaWebSite.CartPage.VerifyProductQty(product.ProductName, 1);
            //TODO delete workaround
            if (_hcaWebSite.CartPage.IsAdjustmentPresent(TestsData.GetDefaultDiscount().DiscountCouponName))
            {
                _hcaWebSite.CartPage.DeleteAjustment(TestsData.GetDefaultDiscount().DiscountCouponName);
                _hcaWebSite.CartPage.WaitProductForNonPresent(TestsData.GetDefaultDiscount().DiscountCouponName);
            }
            _hcaWebSite.CartPage.VerifyCartSum(false);
        }

        [Test]
        public void AddSecondProductToACartTest()
        {
            var products = TestsData.GetProducts(2);
            _hcaWebSite.AddProductsToCartFromTestData(products);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyProducts(products);
            //TODO delete workaround
            if (_hcaWebSite.CartPage.IsAdjustmentPresent(TestsData.GetDefaultDiscount().DiscountCouponName))
            {
                _hcaWebSite.CartPage.DeleteAjustment(TestsData.GetDefaultDiscount().DiscountCouponName);
                _hcaWebSite.CartPage.WaitProductForNonPresent(TestsData.GetDefaultDiscount().DiscountCouponName);
            }
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
            //TODO delete workaround
            if (_hcaWebSite.CartPage.IsAdjustmentPresent(TestsData.GetDefaultDiscount().DiscountCouponName))
            {
                _hcaWebSite.CartPage.DeleteAjustment(TestsData.GetDefaultDiscount().DiscountCouponName);
                _hcaWebSite.CartPage.WaitProductForNonPresent(TestsData.GetDefaultDiscount().DiscountCouponName);
            }
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
            //TODO delete workaround
            if (_hcaWebSite.CartPage.IsAdjustmentPresent(TestsData.GetDefaultDiscount().DiscountCouponName))
            {
                _hcaWebSite.CartPage.DeleteAjustment(TestsData.GetDefaultDiscount().DiscountCouponName);
                _hcaWebSite.CartPage.WaitProductForNonPresent(TestsData.GetDefaultDiscount().DiscountCouponName);
            }
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
            //TODO delete workaround
            if (_hcaWebSite.CartPage.IsAdjustmentPresent(TestsData.GetDefaultDiscount().DiscountCouponName))
            {
                _hcaWebSite.CartPage.DeleteAjustment(TestsData.GetDefaultDiscount().DiscountCouponName);
                _hcaWebSite.CartPage.WaitProductForNonPresent(TestsData.GetDefaultDiscount().DiscountCouponName);
            }
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
            //TODO delete workaround
            if (_hcaWebSite.CartPage.IsAdjustmentPresent(TestsData.GetDefaultDiscount().DiscountCouponName))
            {
                _hcaWebSite.CartPage.DeleteAjustment(TestsData.GetDefaultDiscount().DiscountCouponName);
                _hcaWebSite.CartPage.WaitProductForNonPresent(TestsData.GetDefaultDiscount().DiscountCouponName);
            }
            _hcaWebSite.CartPage.FillDiscountField(TestsData.GetDefaultDiscount().DiscountCouponCode);
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
            //TODO delete workaround
            if (_hcaWebSite.CartPage.IsAdjustmentPresent(TestsData.GetDefaultDiscount().DiscountCouponName))
            {
                _hcaWebSite.CartPage.DeleteAjustment(TestsData.GetDefaultDiscount().DiscountCouponName);
                _hcaWebSite.CartPage.WaitProductForNonPresent(TestsData.GetDefaultDiscount().DiscountCouponName);
            }
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
            //TODO delete workaround
            if (_hcaWebSite.CartPage.IsAdjustmentPresent(TestsData.GetDefaultDiscount().DiscountCouponName))
            {
                _hcaWebSite.CartPage.DeleteAjustment(TestsData.GetDefaultDiscount().DiscountCouponName);
                _hcaWebSite.CartPage.WaitProductForNonPresent(TestsData.GetDefaultDiscount().DiscountCouponName);
            }
            _hcaWebSite.CartPage.FillDiscountField(TestsData.GetDefaultDiscount().DiscountCouponCode);
            _hcaWebSite.CartPage.ClickApplyButton();
            _hcaWebSite.CartPage.WaitForPresentAdjustment(TestsData.GetDefaultDiscount().DiscountCouponName);
            _hcaWebSite.CartPage.VerifyCartSum(true);
            _hcaWebSite.CartPage.DeleteAjustment(TestsData.GetDefaultDiscount().DiscountCouponName);
            _hcaWebSite.CartPage.WaitFoDeleteAdjustment(TestsData.GetDefaultDiscount().DiscountCouponName);
        }

        [Test]
        public void ApplyPromoCodeForLowSumTest()
        {
            var product = TestsData.GetProduct();
            _hcaWebSite.AddProductToCart(product.ProductId);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.WaitForProductsLoaded();
            //TODO delete workaround
            if (_hcaWebSite.CartPage.IsAdjustmentPresent(TestsData.GetDefaultDiscount().DiscountCouponName))
            {
                _hcaWebSite.CartPage.DeleteAjustment(TestsData.GetDefaultDiscount().DiscountCouponName);
                _hcaWebSite.CartPage.WaitProductForNonPresent(TestsData.GetDefaultDiscount().DiscountCouponName);
            }
            _hcaWebSite.CartPage.FillDiscountField(TestsData.GetDefaultDiscount().DiscountCouponCode);
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
