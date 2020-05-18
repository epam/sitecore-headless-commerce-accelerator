using Pages.Components.BasePageComponents;

namespace Pages.Pages
{
    public class BasePage
    {
        protected BasePage()
        {
            Core.Browser.WaitForPageToLoad();
        }

        public Header Header => new Header();
        public Footer Footer => new Footer();
    }
}
