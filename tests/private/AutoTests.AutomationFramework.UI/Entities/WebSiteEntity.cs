using System;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.AutomationFramework.UI.Core;
using AutoTests.AutomationFramework.UI.Interfaces;

namespace AutoTests.AutomationFramework.UI.Entities
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

        private static void Navigate(Uri uri)
        {
            Browser.Navigate(uri);
        }

        public void NavigateToPage(string postfix = "")
        {
            Navigate(Uri.AddPostfix(postfix));
        }

        public void NavigateToPage(IPage page)
        {
            Navigate(Uri.AddPostfix(page.GetPath()));
        }

        public void NavigateToPage(IPage page, string parameter)
        {
            Navigate(Uri.AddPostfix(page.GetPath() + "/" + parameter));
        }
    }
}