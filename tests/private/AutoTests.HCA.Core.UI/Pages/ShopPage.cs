using AutoTests.AutomationFramework.UI.Controls;
using AutoTests.AutomationFramework.UI.Core;
using AutoTests.HCA.Core.UI.ConstantsAndEnums;

namespace AutoTests.HCA.Core.UI.Pages
{
    public class ShopPage : CommonPage
    {
        private static ShopPage _shopPage;

        private static readonly WebElement _productGrid =
            new WebElement("product grid", ByCustom.ClassName("listing-product-grid"));

        public static ShopPage Instance => _shopPage ??= new ShopPage();

        public override string GetPath()
        {
            return PagePrefix.Shop.GetPrefix();
        }

        public override void WaitForOpened()
        {
            _productGrid.WaitForPresent();
        }
    }
}