using Core;
using Pages.Pages;

namespace Pages.Components
{
    public class CategoryPopUp
    {
        private UiElement SubCategory(string subCategoryName) => new UiElement($"//div[@class='dropdown']//a[contains(@href,'{subCategoryName}')]");

        public SubCategoryPage NavigateToSubCategoryPage(string subCategoryName)
        {
            SubCategory(subCategoryName).Click();
            return new SubCategoryPage();
        }
    }
}
