using HCA.Pages.ConsantsAndEnums.ConsantsAndEnums;
using UIAutomationFramework.Utils;

namespace HCA.Pages.ConsantsAndEnums.Footer
{
    public enum FooterSocialNetworkLink
    {
        [Link("twitter", "", "http://twitter.com/")]
        Twitter,

        [Link("facebook", "", "http://facebook.com/")]
        Facebook,

        [Link("youtube", "", "http://youtube.com/")]
        Youtube,

        [Link("instagram", "", "http://www.instagram.com/")]
        Instagram
    }

    public static class FooterSocialNetworkLinkExtensions
    {
        public static string GetLinkName(this FooterSocialNetworkLink sn) =>
            sn.GetAttribute<LinkAttribute>().LinkName;

        public static string GetLinkText(this FooterSocialNetworkLink sn) =>
            sn.GetAttribute<LinkAttribute>().LinkText;

        public static string GetHref(this FooterSocialNetworkLink sn) =>
            sn.GetAttribute<LinkAttribute>().Href;
    }
}
