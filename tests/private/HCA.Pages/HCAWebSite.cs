using System;
using System.Collections.Generic;
using System.Text;
using Automation.Selenium.Library.Entities.WebSiteEntities;
using UIAutomationFramework;
using HCA.Pages.CommonElements;
using HCA.Pages.Pages;
using HCA.Pages.Pages.Checkout;

namespace HCA.Pages
{
    public class HCAWebSite : WebSiteEntity
    {
        private static readonly string EnvironmentName = "HCAEnvironment";
        private static HCAWebSite _hcaiWebSite;

        protected HCAWebSite() : base(Configuration.GetEnvironmentUri(EnvironmentName))
        {
        }

        public static HCAWebSite Instance => _hcaiWebSite ?? (_hcaiWebSite = new HCAWebSite());

        public MainMenuControl MainMenuControl => MainMenuControl.Instance;
        public HeaderControl HeaderControl => HeaderControl.Instance;

        public LoginForm LoginForm => LoginForm.Instance;

        public SignUpPage SignUpPage => SignUpPage.Instance;
        public PhonePage PhonePage => PhonePage.Instance;
        public ProductPage ProductPage => ProductPage.Instance;
        public CartPage CartPage => CartPage.Instance;
        public CheckoutShippingPage CheckoutShippingPage => CheckoutShippingPage.Instance;
        public CheckoutBillingPage CheckoutBillingPage => CheckoutBillingPage.Instance;
        public CheckoutPaymentPage CheckoutPaymentPage => CheckoutPaymentPage.Instance;
        public CheckoutConfirmationPage CheckoutConfirmationPage => CheckoutConfirmationPage.Instance;
    }
}
