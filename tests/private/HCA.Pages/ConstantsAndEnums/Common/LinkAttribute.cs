namespace HCA.Pages.ConstantsAndEnums.Common
{
    public class LinkAttribute : ElementAttribute
    {
        public string LinkText { get; }
        public string Href { get; }

        internal LinkAttribute(string linkName, string linkText, string href):base(linkName)
        {
            LinkText = linkText;
            Href = href;
        }
    }
}
