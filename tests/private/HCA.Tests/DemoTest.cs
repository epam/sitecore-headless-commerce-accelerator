using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using HCA.Pages;

namespace HCA.Tests
{
    [TestFixture(BrowserType.Chrome)]
    internal class DemoTest : HCAWebTest
    {
        public DemoTest(BrowserType browserType) : base(browserType)
        { }

        [Test]
        public void Test1()
        {
            HCAWebSite hcaWebSite = HCAWebSite.Instance;
            hcaWebSite.NavigateToMain();
            hcaWebSite.MainMenuControl.ChooseSubMenuItem("Phones", "Phones");
            hcaWebSite.PhonePage.VerifyOpened();
            hcaWebSite.PhonePage.ChooseProduct("Habitat Athletica Sports Armband Case");
            hcaWebSite.ProductPage.VerifyOpened();
            hcaWebSite.ProductPage.AddToCartButtonClick();
            hcaWebSite.HeaderControl.CartButtonClick();
            hcaWebSite.CartPage.VerifyOpened();
            hcaWebSite.CartPage.SetQtyForProduct("Habitat Athletica Sports Armband Case",4);
            hcaWebSite.CartPage.FillDiscountField("HABRTRNC15P");
            hcaWebSite.CartPage.ClickApplyButton();
            hcaWebSite.CartPage.ClickChekoutButton();
            hcaWebSite.CheckoutShippingPage.VerifyOpened();
            hcaWebSite.CheckoutShippingPage.SelectOptionByName("A New Address");
            hcaWebSite.CheckoutShippingPage.FillFieldByName("First Name", "John");
            hcaWebSite.CheckoutShippingPage.FillFieldByName("Last Name", "Smith");
            hcaWebSite.CheckoutShippingPage.FillFieldByName("Address Line 1", "5th Ave");
            hcaWebSite.CheckoutShippingPage.FillFieldByName("City", "New York");
            hcaWebSite.CheckoutShippingPage.SelectValueInTheField("Country", "United States");
            hcaWebSite.CheckoutShippingPage.SelectValueInTheField("Province", "New York");
            hcaWebSite.CheckoutShippingPage.FillFieldByName("Postal Code", "10005");
            hcaWebSite.CheckoutShippingPage.FillFieldByName("Email Address", "test@test.com");
            hcaWebSite.CheckoutShippingPage.SelectOptionByName("Also use for billing address");
            hcaWebSite.CheckoutShippingPage.SelectShippingMethod("Standard");
            hcaWebSite.CheckoutShippingPage.ClickSubmit();
            hcaWebSite.CheckoutBillingPage.VerifyOpened();
            hcaWebSite.CheckoutBillingPage.ClickSubmit();
            hcaWebSite.CheckoutPaymentPage.VerifyOpened();
            hcaWebSite.CheckoutPaymentPage.FillCardNumber("4111111111111111");
            hcaWebSite.CheckoutPaymentPage.FillFieldByName("Security Code", "123");
            hcaWebSite.CheckoutPaymentPage.ClickSubmit();
            hcaWebSite.CheckoutConfirmationPage.VerifyOpened();

        }
    }
}
