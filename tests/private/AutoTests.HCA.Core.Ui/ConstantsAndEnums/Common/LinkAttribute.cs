namespace AutoTests.HCA.Core.UI.ConstantsAndEnums.Common
{
    public class LinkAttribute : ElementAttribute
    {
        internal LinkAttribute(string linkName, string linkText, string href) : base(linkName)
        {
            LinkText = linkText;
            Href = href;
        }

        public string LinkText { get; }
        public string Href { get; }
    }
}