using HCA.Pages;
using HCA.Pages.CommonElements;
using HCA.Pages.Pages;
using NUnit.Framework;
using UIAutomationFramework.Driver;

namespace HCA.Tests.UITests
{
    [UiTest, TestFixture(BrowserType.Chrome)]
    public class ShopPageTests : HcaWebTest
    {
        private readonly HcaWebSite _hcaWebSite;
        private readonly ShopPage _shopPage;
        private readonly ProductsFilterSection _filterSection;
        private readonly ProductGridSection _productGrid;
        public const int DEF_PRODUCTS_COUNT_ON_PAGE = 12;
        public const string DEF_OPTION_NAME = "Aura Home Theater In a Box";
        public const string DEF_PRODUCT_NAME = "Habitat Aura 2.1 Channel Soundbar with Wireless Subwoofer";

        public ShopPageTests(BrowserType browserType) : base(browserType)
        {
            _hcaWebSite = HcaWebSite.Instance;
            _shopPage = _hcaWebSite.ShopPage;
            _filterSection = _hcaWebSite.ProductsFilterSection;
            _productGrid = _hcaWebSite.ProductGridSection;
        }

        [SetUp]
        public void SetUp()
        {
            _hcaWebSite.NavigateToPage(_hcaWebSite.ShopPage, "Home Theater");
            _hcaWebSite.ShopPage.WaitForOpened();
        }

        [Test]
        public void _01_SelectFilterOptionsTest()
        {
            _hcaWebSite.ProductsFilterSection.FilterSectionIsPresent();
            Assert.Multiple(() =>
            {
                _filterSection.SelectFilterOptionByName(DEF_OPTION_NAME);
                _shopPage.WaitForOpened();
                _filterSection.VerifyFilterOptionIsChecked(DEF_OPTION_NAME);
                Assert.AreEqual(_productGrid.TotalProducts, _filterSection.GetProductsCountFromOption(DEF_OPTION_NAME));
            });
        }

        [Test]
        public void _02_DeselectFilterOptionsTest()
        {
            _hcaWebSite.ProductsFilterSection.FilterSectionIsPresent();
            Assert.Multiple(() =>
            {
                var productsInitialQuantity = _productGrid.TotalProducts;
                _filterSection.SelectFilterOptionByName(DEF_OPTION_NAME);
                _shopPage.WaitForOpened();
                _filterSection.DeselectFilter(DEF_OPTION_NAME);
                _shopPage.WaitForOpened();
                _filterSection.VerifyFilterOptionIsUnchecked(DEF_OPTION_NAME);
                Assert.AreEqual(productsInitialQuantity, _productGrid.TotalProducts);
            });
        }

        [Test]
        public void _03_FilterDisableButtonTest()
        {
            _hcaWebSite.ProductsFilterSection.FilterSectionIsPresent();
            Assert.Multiple(() =>
            {
                _filterSection.SelectFilterOptionByName(DEF_OPTION_NAME);
                _shopPage.WaitForOpened();
                _filterSection.ClickFilterDisableButton();
                _shopPage.WaitForOpened();
                _filterSection.VerifyFilterOptionIsUnchecked(DEF_OPTION_NAME);
            });
        }

        [Test]
        public void _04_FilterHideAllButtonTest()
        {
            _hcaWebSite.ProductsFilterSection.FilterSectionIsPresent();
            Assert.Multiple(() =>
            {
                _filterSection.ClickHideOrShowLink();
                _filterSection.VerifyShowLink("SHOW ALL");
                _filterSection.ClickHideOrShowLink();
                _filterSection.VerifyHideLink("HIDE ALL");
            });
        }

        [Test]
        public void _05_ProductHeaderTest()
        {
            var totalProducts = _productGrid.TotalProducts;

            Assert.Multiple(() =>
            {
                for (var i = 1; DEF_PRODUCTS_COUNT_ON_PAGE * i < totalProducts; i++)
                {
                    _productGrid.ClickLoadMoreLink();
                    _productGrid.WaitForLazyLoadingSpinnerIsNotPresent();
                }

                var expProductTitle = _productGrid.GetDisplayedProductsCount() + " PRODUCTS";
                Assert.AreEqual(expProductTitle, _productGrid.GetProductsHeaderText());
            });
        }

        [Test]
        public void _06_LoadProductsTest()
        {
            var totalProducts = _productGrid.TotalProducts;
            var actualProductCount = _productGrid.GetDisplayedProductsCount();
            Assert.LessOrEqual(actualProductCount, DEF_PRODUCTS_COUNT_ON_PAGE,
                $"The number of loaded products at a time should be {DEF_PRODUCTS_COUNT_ON_PAGE}");
            Assert.Multiple(() =>
            {
                for (var i = 1; DEF_PRODUCTS_COUNT_ON_PAGE * i < totalProducts;)
                {
                    _productGrid.ClickLoadMoreLink();
                    _productGrid.WaitForLazyLoadingSpinnerIsNotPresent();
                    actualProductCount = _productGrid.GetDisplayedProductsCount();
                    Assert.LessOrEqual(actualProductCount, DEF_PRODUCTS_COUNT_ON_PAGE * (++i),
                        $"The number of loaded products at a time should be {DEF_PRODUCTS_COUNT_ON_PAGE}");
                }
                Assert.AreEqual(totalProducts, _productGrid.GetDisplayedProductsCount());
                _productGrid.VerifyLoadMoreLink();
            });
        }

        [Test]
        public void _07_ViewProductTest()
        {
            Assert.Multiple(() =>
            {
                _productGrid.ChooseProduct(DEF_PRODUCT_NAME);
                _hcaWebSite.ProductPage.WaitForOpened();
                Assert.AreEqual(DEF_PRODUCT_NAME.ToUpper(),_hcaWebSite.ProductPage.GetProductTitle());
            });
        }
    }
}
