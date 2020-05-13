using Core;
using Pages.Pages;

namespace Pages.Steps
{
    public static class NavigationSteps
    {
        public static T GoToPage<T>() where T : BasePage, new()
        {
            Browser.Navigate(PagesUrls.Urls[new T().GetType().Name]);
            return new T();
        }
    }
}
