using System;
using Ui.AutomationFramework.Core;
using Ui.AutomationFramework.Interfaces;
using Ui.AutomationFramework.Utils;

namespace Ui.AutomationFramework.Entities.WebSiteEntities
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

        public void NavigateToPage(IPage page, string parameter)
        {
            Navigate(UriManager.AddPostfix(Uri, page.GetPath() + "/" + parameter));
        }
    }
}