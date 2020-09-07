using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.BaseTests;
using AutoTests.HCA.Core.Common.Settings.Checkout;
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
            _hcaWebSite.ProductGridSection.ChooseProduct(TestsData.GetDefaultProduct().ProductName);
            _hcaWebSite.ProductPage.WaitForOpened();
            _hcaWebSite.ProductPage.AddToCartButtonClick();
            _hcaWebSite.HeaderControl.ClickCartButton();
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.SetQtyForProduct(TestsData.GetDefaultProduct().ProductName, 4);
            _hcaWebSite.CartPage.FillDiscountField(TestsData.GetDefaultPromotion().Code);
            _hcaWebSite.CartPage.ClickApplyButton();
            _hcaWebSite.CartPage.VerifyCartSum(true);
            _hcaWebSite.CartPage.ClickCheckoutButton();
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
            _hcaWebSite.CheckoutShippingPage.SelectOptionByName(AddressOption.NewAddress);
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("First Name", "John");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("Last Name", "Smith");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("Address Line 1", "5th Ave");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("City", "New York");
            _hcaWebSite.CheckoutShippingPage.SelectValueInTheField("Country", "United States");
            _hcaWebSite.CheckoutShippingPage.SelectValueInTheField("Province", "New York");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("Postal Code", "10005");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("Email Address", "test@test.com");
            _hcaWebSite.CheckoutShippingPage.SelectOptionByName(AddressOption.AlsoUseForBillingAddress);
            _hcaWebSite.CheckoutShippingPage.SelectShippingMethod(ShippingMethod.Standard);
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
            var user = TestsData.GetUser().Credentials;
            _hcaWebSite.OpenHcaAndLogin(user.Email, user.Password);
            _hcaWebSite.MainMenuControl.ChooseSubMenuItem(MenuItem.Phones, SubMenuItem.Phones);
            _hcaWebSite.PhonePage.WaitForOpened();
            _hcaWebSite.ProductGridSection.ChooseProduct(TestsData.GetDefaultProduct().ProductName);
            _hcaWebSite.ProductPage.WaitForOpened();
            _hcaWebSite.ProductPage.AddToCartButtonClick();
            _hcaWebSite.HeaderControl.ClickCartButton();
            _hcaWebSite.CartPage.VerifyOpened();
            _hcaWebSite.CartPage.SetQtyForProduct(TestsData.GetDefaultProduct().ProductName, 2);
            _hcaWebSite.CartPage.FillDiscountField(TestsData.GetDefaultPromotion().Code);
            _hcaWebSite.CartPage.ClickApplyButton();
            _hcaWebSite.CartPage.VerifyCartSum(true);
            _hcaWebSite.CartPage.ClickCheckoutButton();
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
            _hcaWebSite.CheckoutShippingPage.SelectOptionByName(AddressOption.NewAddress);
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("First Name", "John");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("Last Name", "Smith");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("Address Line 1", "5th Ave");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("City", "New York");
            _hcaWebSite.CheckoutShippingPage.SelectValueInTheField("Country", "United States");
            _hcaWebSite.CheckoutShippingPage.SelectValueInTheField("Province", "New York");
            _hcaWebSite.CheckoutShippingPage.FillFieldByName("Postal Code", "10005");
            _hcaWebSite.CheckoutShippingPage.SelectOptionByName(AddressOption.AlsoUseForBillingAddress);
            _hcaWebSite.CheckoutShippingPage.SelectShippingMethod(ShippingMethod.Standard);
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