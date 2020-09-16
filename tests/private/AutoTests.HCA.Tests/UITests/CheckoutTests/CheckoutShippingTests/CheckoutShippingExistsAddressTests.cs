using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.BaseTests;
using AutoTests.HCA.Core.Common.Entities.ConstantsAndEnums.Checkout;
using AutoTests.HCA.Core.Common.Settings.Users;
using AutoTests.HCA.Core.UI;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.UITests.CheckoutTests.CheckoutShippingTests
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(BrowserType.Chrome)]
    [CheckoutTest]
    [UiTest]
    internal class CheckoutShippingExistsAddressTests : BaseHcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
            var user = TestsData.GetDefaultUser().Credentials;
            var userManager = TestsHelper.CreateHcaUserApiHelper(user);
            userManager.CleanPromotions();
            userManager.CleanCart();
            _hcaWebSite.OpenHcaAndLogin(user);
        }

        public CheckoutShippingExistsAddressTests(BrowserType browserType) : base(browserType)
        {
        }

        private HcaWebSite _hcaWebSite;

        [Test]
        public void T1_CheckoutShippingExistsAddress_VerifyAddNewAddress()
        {
            var products = TestsData.GetProducts(2);
            _hcaWebSite.AddProductsToCartFromTestData(products);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CheckoutShippingPage);
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
            _hcaWebSite.CheckoutShippingPage.SelectOptionByName(AddressOption.NewAddress);
            var addressLine = _hcaWebSite.CheckoutShippingPage.CreateNewAddress(HcaUserRole.User);
            _hcaWebSite.CheckoutShippingPage.SetSaveAddress(true);
            _hcaWebSite.CheckoutShippingPage.SelectShippingMethod(ShippingMethod.Standard);
            _hcaWebSite.CheckoutShippingPage.ClickSubmit();
            _hcaWebSite.CheckoutBillingPage.WaitForOpened();
            _hcaWebSite.NavigateToPage(_hcaWebSite.MyAccountPage);
            _hcaWebSite.MyAccountPage.WaitForOpened();
            _hcaWebSite.MyAccountAddressSection.SelectValueInTheField("Addresses",
                $"{addressLine}");
        }

        [Test]
        public void T2_CheckoutShippingExistsAddress_VerifyBillingAddressFillWithSavedAddress(
            [Values] bool fillBillingCheckout)
        {
            var products = TestsData.GetProducts(2);
            _hcaWebSite.AddProductsToCartFromTestData(products);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CheckoutShippingPage);
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
            _hcaWebSite.CheckoutShippingPage.SelectOptionByName(AddressOption.SavedAddress);
            _hcaWebSite.CheckoutShippingPage.SelectFirstShippingAddress();
            if (fillBillingCheckout)
                _hcaWebSite.CheckoutShippingPage.SelectOptionByName(AddressOption.AlsoUseForBillingAddress);
            _hcaWebSite.CheckoutShippingPage.SelectShippingMethod(ShippingMethod.Standard);
            _hcaWebSite.CheckoutShippingPage.ClickSubmit();
            _hcaWebSite.CheckoutBillingPage.WaitForOpened();
            _hcaWebSite.CheckoutBillingPage.VerifyOption(AddressOption.SameAsShippingAddress, fillBillingCheckout);
        }

        [Test]
        public void T3_CheckoutShippingExistsAddress_VerifyChooseSavedAddress()
        {
            var products = TestsData.GetProducts(2);
            _hcaWebSite.AddProductsToCartFromTestData(products);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CheckoutShippingPage);
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
            _hcaWebSite.CheckoutShippingPage.SelectOptionByName(AddressOption.SavedAddress);
            _hcaWebSite.CheckoutShippingPage.SelectFirstShippingAddress();
            _hcaWebSite.CheckoutShippingPage.SelectShippingMethod(ShippingMethod.Standard);
            _hcaWebSite.CheckoutShippingPage.ClickSubmit();
            _hcaWebSite.CheckoutBillingPage.WaitForOpened();
        }
    }
}