using System;

namespace HCA.Pages.HCAElements.Footer
{
    public class FooterLinkAttribute : Attribute
    {
        public string LinkName { get; }
        public string LinkText { get; }
        public string Href { get; }

        internal FooterLinkAttribute(string nameLink, string linkText, string href)
        {
            LinkName = nameLink;
            LinkText = linkText;
            Href = href;
        }
    }
}
