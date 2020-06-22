using HCA.Pages.ConsantsAndEnums.ConsantsAndEnums;
using UIAutomationFramework.Utils;

namespace HCA.Pages.ConsantsAndEnums.Footer
{
    public enum FooterSiteLink
    {
        [Link("Gift Cards", "GIFT CARDS", "http:")]
        GiftCards,

        [Link("Find a Store", "FIND A STORE", "http:")]
        FindAStore,

        [Link("Sign Up for Email", "SIGN UP FOR EMAIL", "http:")]
        SignUpForEmail,

        [Link("Join HCA", "JOIN HCA", "http:")]
        JoinHCA,

        [Link("Get Help", "GET HELP", "http:")]
        GetHelp,

        [Link("Order Status", "ORDER STATUS", "http:")]
        OrderStatus,

        [Link("Shipping and Review", "SHIPPING AND REVIEW", "http:")]
        ShippingAndReview,

        [Link("Returns", "RETURNS", "http:")]
        Returns,

        [Link("Payment Options", "PAYMENT OPTIONS", "http:")]
        PaymentOptions,

        [Link("Contact Us", "CONTACT US", "http:")]
        ContactUs,

        [Link("News", "NEWS", "http:")]
        News,

        [Link("About HCA", "ABOUT HCA", "http:")]
        AboutHCA
    }

    public static class FooterSiteLinkExtensions
    {
        public static string GetLinkName(this FooterSiteLink sl) =>
            sl.GetAttribute<LinkAttribute>().LinkName;

        public static string GetLinkText(this FooterSiteLink sl) =>
            sl.GetAttribute<LinkAttribute>().LinkText;

        public static string GetHref(this FooterSiteLink sl) =>
            sl.GetAttribute<LinkAttribute>().Href;
    }
}
