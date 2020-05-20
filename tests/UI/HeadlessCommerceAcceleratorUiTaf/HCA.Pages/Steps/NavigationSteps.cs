using Core;
using Core.Utils;
using Pages.Pages;

namespace Pages.Steps
{
    public static class NavigationSteps
    {
        public static HomePage OpenHomePage()
        {
            Browser.Navigate(ConfigurationManager.BaseUrl);
            return new HomePage();
        }
    }
}
