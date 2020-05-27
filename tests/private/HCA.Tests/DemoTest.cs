using HCA.Pages;
using NUnit.Framework;

namespace HCA.Tests
{
    [TestFixture(BrowserType.Chrome)]
    internal class DemoTest : HCAWebTest
    {
        public DemoTest(BrowserType browserType) : base(browserType)
        {
        }

        [Test]
        public void Test1()
        {
            var hcaWebSite = HCAWebSite.Instance;
            hcaWebSite.NavigateToMain();
            hcaWebSite.MainMenuControl.ChooseSubMenuItem("Phones", "Phones");
            hcaWebSite.PhonePage.WaitForOpened();
            hcaWebSite.PhonePage.ChooseProduct("Habitat Athletica Sports Armband Case");
            hcaWebSite.ProductPage.WaitForOpened();
            hcaWebSite.ProductPage.AddToCartButtonClick();
            hcaWebSite.HeaderControl.CartButtonClick();
            hcaWebSite.CartPage.VerifyOpened();
            hcaWebSite.CartPage.SetQtyForProduct("Habitat Athletica Sports Armband Case", 4);
            hcaWebSite.CartPage.FillDiscountField("HABRTRNC15P");
            hcaWebSite.CartPage.ClickApplyButton();
            hcaWebSite.CartPage.ClickChekoutButton();
            hcaWebSite.CheckoutShippingPage.WaitForOpened();
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
            hcaWebSite.CheckoutBillingPage.WaitForOpened();
            hcaWebSite.CheckoutBillingPage.ClickSubmit();
            hcaWebSite.CheckoutPaymentPage.WaitForOpened();
            hcaWebSite.CheckoutPaymentPage.FillCardNumber("4111111111111111");
            hcaWebSite.CheckoutPaymentPage.FillFieldByName("Security Code", "123");
            hcaWebSite.CheckoutPaymentPage.ClickSubmit();
            hcaWebSite.CheckoutConfirmationPage.WaitForOpened();
        }
    }
}