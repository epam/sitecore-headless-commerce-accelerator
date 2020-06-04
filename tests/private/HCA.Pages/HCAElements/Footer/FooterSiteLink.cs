using UIAutomationFramework.Utils;

namespace HCA.Pages.HCAElements.Footer
{
    public enum FooterSiteLink
    {
        [FooterLink("Gift Cards", "GIFT CARDS", "http:")]
        GiftCards,

        [FooterLink("Find a Store", "FIND A STORE", "http:")]
        FindAStore,

        [FooterLink("Sign Up for Email", "SIGN UP FOR EMAIL", "http:")]
        SignUpForEmail,

        [FooterLink("Join HCA", "JOIN HCA", "http:")]
        JoinHCA,

        [FooterLink("Get Help", "GET HELP", "http:")]
        GetHelp,

        [FooterLink("Order Status", "ORDER STATUS", "http:")]
        OrderStatus,

        [FooterLink("Shipping and Review", "SHIPPING AND REVIEW", "http:")]
        ShippingAndReview,

        [FooterLink("Returns", "RETURNS", "http:")]
        Returns,

        [FooterLink("Payment Options", "PAYMENT OPTIONS", "http:")]
        PaymentOptions,

        [FooterLink("Contact Us", "CONTACT US", "http:")]
        ContactUs,

        [FooterLink("News", "NEWS", "http:")]
        News,

        [FooterLink("About HCA", "ABOUT HCA", "http:")]
        AboutHCA
    }

    public static class FooterSiteLinkExtensions
    {
        public static string GetLinkName(this FooterSiteLink sl) =>
            sl.GetAttribute<FooterLinkAttribute>().LinkName;

        public static string GetLinkText(this FooterSiteLink sl) =>
            sl.GetAttribute<FooterLinkAttribute>().LinkText;

        public static string GetHref(this FooterSiteLink sl) =>
            sl.GetAttribute<FooterLinkAttribute>().Href;
    }
}
