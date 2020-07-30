using System.Collections.Generic;
using System.Linq;
using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.UI;
using AutoTests.HCA.Core.UI.CommonElements;
using AutoTests.HCA.Core.UI.ConstantsAndEnums.Product;
using AutoTests.HCA.Core.UI.Pages;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.UITests
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(BrowserType.Chrome)]
    [UiTest]
    public class ProductPageTests : HcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
            _productPage = _hcaWebSite.ProductPage;
            _headerControl = _hcaWebSite.HeaderControl;
            _cartPage = _hcaWebSite.CartPage;
        }

        private HcaWebSite _hcaWebSite;
        private ProductPage _productPage;
        private HeaderControl _headerControl;
        private CartPage _cartPage;

        public const string PRODUCT_ID = "7042136";
        public const string COLOR_PRODUCT_ID = "6042957";
        public const string OUT_OF_STOCK_PRODUCT_ID = "6042175";

        public ProductPageTests(BrowserType browserType) : base(browserType)
        {
        }

        [TestCase(PRODUCT_ID, true, false)]
        [TestCase(COLOR_PRODUCT_ID, true, true)]
        [TestCase(OUT_OF_STOCK_PRODUCT_ID, false, false)]
        public void _01_VerifyProductElementsTest(string productId, bool inStock, bool isColored)
        {
            _hcaWebSite.NavigateToPage(_hcaWebSite.ProductPage, productId);
            _productPage.WaitForOpened();
            Assert.Multiple(() =>
            {
                Assert.IsFalse(string.IsNullOrEmpty(_productPage.BrandText), "Invalid brand");
                Assert.IsFalse(string.IsNullOrEmpty(_productPage.TitleText), "Invalid Title");
                Assert.Less(0, _productPage.Price, "Invalid Price");

                if (inStock)
                {
                    Assert.AreEqual(ProductStatus.InStock, _productPage.Status);
                    Assert.IsFalse(_productPage.AddToCartButtonIsDisabled,
                        "If there is a product in stock, the button for adding an item to the cart must be enabled");
                }
                else
                {
                    Assert.AreEqual(ProductStatus.OutOfStock, _productPage.Status);
                    Assert.IsTrue(_productPage.AddToCartButtonIsDisabled,
                        "If there is no product in stock, the button for adding an item to the cart must be disabled");
                }

                if (isColored) Assert.IsTrue(_productPage.AreColorsPresented, "Product color not shown");

                Assert.IsTrue(_productPage.PrintButtonIsClickable(), "Print button isn't clickable");
                Assert.IsTrue(_productPage.ReadReviewIsClickable(), "ReadReview button isn't clickable");
                Assert.IsTrue(_productPage.WriteReviewIsClickable(), "WriteReview button isn't clickable");
                _productPage.VerifyDescriptionTextSection();
                _productPage.VerifyDescriptionRatingSection();
                _productPage.VerifyDescriptionFeaturesSection();
            });
        }

        [TestCase(COLOR_PRODUCT_ID)]
        public void _02_SelectProductColorTest(string productId)
        {
            _hcaWebSite.NavigateToPage(_hcaWebSite.ProductPage, productId);
            _productPage.WaitForOpened();

            var productName = _productPage.TitleText;
            _productPage.ChooseColor(2);

            Assert.AreNotEqual(productName, _productPage.TitleText);
        }

        [TestCase(PRODUCT_ID, 3, 1, Category = "AddProductToCartTests")]
        public void _03_AddProductToCartTest(string productId, int quantity, int expQuantityInHeader)
        {
            _hcaWebSite.NavigateToPage(_hcaWebSite.ProductPage, productId);
            _productPage.WaitForOpened();
            var productName = _productPage.TitleText;

            for (var i = 0; i < quantity; i++)
            {
                _productPage.AddToCartButtonClick();
                _headerControl.WaitForPresentProductsQuantity();
            }

            Assert.AreEqual(expQuantityInHeader, _headerControl.ProductsQuantityInCart,
                "Invalid products quantity in the cart.");

            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _cartPage.WaitForProductsLoaded();

            Assert.IsTrue(_cartPage.ProductIsPresent(productName),
                $"Product {productName} not found in shopping cart.");
            Assert.AreEqual(quantity, _cartPage.GetProductQty(productName),
                $"Invalid quantity of '{productName}' in the cart.");
        }

        [TestCase(COLOR_PRODUCT_ID, 4)]
        public void _04_AddColorProductToCartTest(string productId, int addedColorsNumber)
        {
            _hcaWebSite.NavigateToPage(_hcaWebSite.ProductPage, productId);
            _productPage.WaitForOpened();
            var expProductsNamesInCart = new List<string>();

            for (var i = 0; i < addedColorsNumber; i++)
            {
                _productPage.ChooseColor(i);
                _productPage.AddToCartButtonClick();
                _headerControl.WaitForPresentProductsQuantity();
                expProductsNamesInCart.Add(_productPage.TitleText);
                Assert.AreEqual(i + 1, _headerControl.ProductsQuantityInCart, "Invalid products quantity in the cart.");
            }

            _hcaWebSite.NavigateToPage(_hcaWebSite.CartPage);
            _cartPage.WaitForProductsLoaded();
            Assert.That(
                () => expProductsNamesInCart.All(x => _cartPage.ProductIsPresent(x) && _cartPage.GetProductQty(x) == 1),
                "Product not found in shopping cart.");
        }
    }
}