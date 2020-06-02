using Core;
using HCA.Pages.Pages;

namespace HCA.Pages.Components
{
    public class ProductTile
    {
        public ProductTile(int sku)
        { 
            this.sku = sku;
        }

        private int sku;

        private UiElement ViewProductBtn => new UiElement($"//a[contains(@class,'viewProduct') and contains(@href,'{sku}')]");
        private UiElement ProductImg => new UiElement($"//img[contains(@src,'{sku}')]");

        public ProductDetailsPage ClickOnviewProduct()
        {
            ProductImg.HoverOver();
            ViewProductBtn.Click();
            return new ProductDetailsPage();
        }
    }
}
