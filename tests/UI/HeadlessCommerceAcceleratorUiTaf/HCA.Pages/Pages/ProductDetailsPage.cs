using Core;
using Pages.Pages;

namespace HCA.Pages.Pages
{
    public class ProductDetailsPage : BasePage
    {
        private UiElement StockStatus => new UiElement("//div[contains(@class,'product-stock-status')]/h4");
        private UiElement AddToCartBtn => new UiElement("//button[@class='btn btn-teal btn-add']");


        public ProductDetailsPage AddToCart()
        {
            AddToCartBtn.Click();
            return new ProductDetailsPage();
        }
    }
}
