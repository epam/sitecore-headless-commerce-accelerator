using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.BaseTests;
using AutoTests.HCA.Core.Common.Settings.Users;
using AutoTests.HCA.Core.UI;
using AutoTests.HCA.Core.UI.ConstantsAndEnums.Header.MainMenu;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.UITests
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(BrowserType.Chrome)]
    [UiTest]
    internal class DemoTest : BaseHcaWebTest
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

        [Test]
        public void CheckoutForAnonymousTest()
        {
            _hcaWebSite.NavigateToMain();
            _hcaWebSite.MainMenuControl.ChooseSubMenuItem(MenuItem.Phones, SubMenuItem.Phones);
            _hcaWebSite.PhonePage.WaitForOpened();
            _hcaWebSite.ProductGridSection.ChooseProduct("Habitat Athletica Sports Armband Case");
            _hcaWebSite.ProductPage.WaitForOpened();
            _hcaWebSite.ProductPage.AddToCartButtonClick();
            _hcaWebSite.HeaderControl.ClickCartButton();
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
            var user = TestsData.GetUser(HcaUserRole.User).Credentials;
            _hcaWebSite.OpenHcaAndLogin(user.Email, user.Password);
            _hcaWebSite.MainMenuControl.ChooseSubMenuItem(MenuItem.Phones, SubMenuItem.Phones);
            _hcaWebSite.PhonePage.WaitForOpened();
            _hcaWebSite.ProductGridSection.ChooseProduct("Habitat Athletica Sports Armband Case");
            _hcaWebSite.ProductPage.WaitForOpened();
            _hcaWebSite.ProductPage.AddToCartButtonClick();
            _hcaWebSite.HeaderControl.ClickCartButton();
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