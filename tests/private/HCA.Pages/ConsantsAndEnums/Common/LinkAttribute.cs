using System;

namespace HCA.Pages.ConsantsAndEnums.ConsantsAndEnums
{
    public class LinkAttribute : Attribute
    {
        public string LinkName { get; }
        public string LinkText { get; }
        public string Href { get; }

        internal LinkAttribute(string nameLink, string linkText, string href)
        {
            LinkName = nameLink;
            LinkText = linkText;
            Href = href;
        }
    }
}
