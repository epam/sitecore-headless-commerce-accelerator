using HCA.Pages;
using NUnit.Framework;
using UIAutomationFramework.Driver;

namespace HCA.Tests
{
    [TestFixture(BrowserType.Chrome)]
    [UiTest]
    internal class DemoTest : HcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
        }

        public DemoTest(BrowserType browserType) : base(browserType)
        {
        }

        private HcaWebSite _hcaWebSite;
        private readonly string _userName = "testuser@test.com";
        private readonly string _password = "testuser";

        [Test]
        public void CheckoutForAnonymousTest()
        {
            _hcaWebSite.NavigateToMain();
            _hcaWebSite.MainMenuControl.ChooseSubMenuItem("Phones", "Phones");
            _hcaWebSite.PhonePage.WaitForOpened();
            _hcaWebSite.PhonePage.ChooseProduct("Habitat Athletica Sports Armband Case");
            _hcaWebSite.ProductPage.WaitForOpened();
            _hcaWebSite.ProductPage.AddToCartButtonClick();
            _hcaWebSite.HeaderControl.CartButtonClick();
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.SetQtyForProduct("Habitat Athletica Sports Armband Case", 4);
            _hcaWebSite.CartPage.FillDiscountField("HABRTRNC15P");
            _hcaWebSite.CartPage.ClickApplyButton();
            _hcaWebSite.CartPage.ClickChekoutButton();
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
            _hcaWebSite.CheckoutShippingPage.SelectOptionByName("A New Address");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("First Name", "John");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("Last Name", "Smith");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("Address Line 1", "5th Ave");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("City", "New York");
            _hcaWebSite.CheckoutShippingPage.SelectValueInTheField("Country", "United States");
            _hcaWebSite.CheckoutShippingPage.SelectValueInTheField("Province", "New York");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("Postal Code", "10005");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("Email Address", "test@test.com");
            _hcaWebSite.CheckoutShippingPage.SelectOptionByName("Also use for billing address");
            _hcaWebSite.CheckoutShippingPage.SelectShippingMethod("Standard");
            _hcaWebSite.CheckoutShippingPage.ClickSubmit();
            _hcaWebSite.CheckoutBillingPage.WaitForOpened();
            _hcaWebSite.CheckoutBillingPage.ClickSubmit();
            _hcaWebSite.CheckoutPaymentPage.WaitForOpened();
            _hcaWebSite.CheckoutPaymentPage.FillCardNumber("4111111111111111");
            _hcaWebSite.CheckoutPaymentPage.FillFieldByName("Security Code", "123");
            _hcaWebSite.CheckoutPaymentPage.ClickSubmit();
            _hcaWebSite.CheckoutConfirmationPage.WaitForOpened();
        }

        [Test]
        public void CheckoutForRegisteredTest()
        {
            _hcaWebSite.OpenHcaAndLogin(_userName, _password);
            _hcaWebSite.MainMenuControl.ChooseSubMenuItem("Phones", "Phones");
            _hcaWebSite.PhonePage.WaitForOpened();
            _hcaWebSite.PhonePage.ChooseProduct("Habitat Athletica Sports Armband Case");
            _hcaWebSite.ProductPage.WaitForOpened();
            _hcaWebSite.ProductPage.AddToCartButtonClick();
            _hcaWebSite.HeaderControl.CartButtonClick();
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.SetQtyForProduct("Habitat Athletica Sports Armband Case", 2);
            _hcaWebSite.CartPage.FillDiscountField("HABRTRNC15P");
            _hcaWebSite.CartPage.ClickApplyButton();
            _hcaWebSite.CartPage.ClickChekoutButton();
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
            _hcaWebSite.CheckoutShippingPage.SelectOptionByName("A New Address");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("First Name", "John");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("Last Name", "Smith");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("Address Line 1", "5th Ave");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("City", "New York");
            _hcaWebSite.CheckoutShippingPage.SelectValueInTheField("Country", "United States");
            _hcaWebSite.CheckoutShippingPage.SelectValueInTheField("Province", "New York");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("Postal Code", "10005");
            _hcaWebSite.CheckoutShippingPage.SelectOptionByName("Also use for billing address");
            _hcaWebSite.CheckoutShippingPage.SelectShippingMethod("Standard");
            _hcaWebSite.CheckoutShippingPage.ClickSubmit();
            _hcaWebSite.CheckoutBillingPage.WaitForOpened();
            _hcaWebSite.CheckoutBillingPage.ClickSubmit();
            _hcaWebSite.CheckoutPaymentPage.WaitForOpened();
            _hcaWebSite.CheckoutPaymentPage.FillCardNumber("4111111111111111");
            _hcaWebSite.CheckoutPaymentPage.FillFieldByName("Security Code", "123");
            _hcaWebSite.CheckoutPaymentPage.ClickSubmit();
            _hcaWebSite.CheckoutConfirmationPage.WaitForOpened();
        }
    }
}