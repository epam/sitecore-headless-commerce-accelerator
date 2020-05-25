using System;
using UIAutomationFramework.Core;
using UIAutomationFramework.Interfaces;
using UIAutomationFramework.Utils;

namespace Automation.Selenium.Library.Entities.WebSiteEntities
{
    public abstract class WebSiteEntity
    {
        protected WebSiteEntity(Uri uri)
        {
            Uri = uri;
        }

        public Uri Uri { get; }

        public void NavigateToMain()
        {
            Browser.Navigate(Uri);
        }

        private void Navigate(Uri uri)
        {
            Browser.Navigate(uri);
        }

        public void NavigateToPage(string postfix = "")
        {
            Navigate(UriManager.AddPostfix(Uri, postfix));
        }

        public void NavigateToPage(IPage page)
        {
            Navigate(UriManager.AddPostfix(Uri, page.GetPath()));
        }

    }
}