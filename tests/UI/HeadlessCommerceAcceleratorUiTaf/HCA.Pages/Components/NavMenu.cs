using Core;

namespace Pages.Components
{
    public class NavMenu
    {
        public CategoryPopUp SubCategoryMenu => new CategoryPopUp();
        private UiElement Category(string categoryName) => new UiElement($"//li[contains(@class,'menu-item')]/a[text()='{categoryName}']");

        public CategoryPopUp OpenCategoryPopUp(string categoryName)
        {
            Category(categoryName).HoverOver();
            return new CategoryPopUp();
        }
    }
}
