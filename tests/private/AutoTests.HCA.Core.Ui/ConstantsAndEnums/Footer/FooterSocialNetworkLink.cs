using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.UI.ConstantsAndEnums.Common;

namespace AutoTests.HCA.Core.UI.ConstantsAndEnums.Footer
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
        public static string GetLinkName(this FooterSocialNetworkLink sn)
        {
            return sn.GetAttribute<LinkAttribute>().Name;
        }

        public static string GetLinkText(this FooterSocialNetworkLink sn)
        {
            return sn.GetAttribute<LinkAttribute>().LinkText;
        }

        public static string GetHref(this FooterSocialNetworkLink sn)
        {
            return sn.GetAttribute<LinkAttribute>().Href;
        }
    }
}