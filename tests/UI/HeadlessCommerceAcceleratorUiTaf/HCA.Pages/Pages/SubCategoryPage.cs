using HCA.Pages.Components;
using HCA.Pages.Pages;

namespace Pages.Pages
{
    public class SubCategoryPage : BasePage
    {
        private ProductTile ProductTile(int sku) => new ProductTile(sku);

        public ProductDetailsPage OpenProductDetailsPage(int sku)
        {
            return ProductTile(sku).ClickOnviewProduct();
        }
    }
}
