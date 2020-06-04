using UIAutomationFramework.Utils;

namespace HCA.Pages.HCAElements.Footer
{
    public enum FooterSocialNetworkLink
    {
        [FooterLink("twitter", "", "http://twitter.com/")]
        Twitter,

        [FooterLink("facebook", "", "http://facebook.com/")]
        Facebook,

        [FooterLink("youtube", "", "http://youtube.com/")]
        Youtube,

        [FooterLink("instagram", "", "http://www.instagram.com/")]
        Instagram
    }

    public static class FooterSocialNetworkLinkExtensions
    {
        public static string GetLinkName(this FooterSocialNetworkLink sn) =>
            sn.GetAttribute<FooterLinkAttribute>().LinkName;

        public static string GetLinkText(this FooterSocialNetworkLink sn) =>
            sn.GetAttribute<FooterLinkAttribute>().LinkText;

        public static string GetHref(this FooterSocialNetworkLink sn) =>
            sn.GetAttribute<FooterLinkAttribute>().Href;
    }
}
