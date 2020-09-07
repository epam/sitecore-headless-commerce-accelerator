using System;
using System.Collections.Generic;
using System.Text;
using AutoTests.AutomationFramework.UI.Core;
using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.BaseTests;
using AutoTests.HCA.Core.Common.Settings.Checkout;
using AutoTests.HCA.Core.Common.Settings.Users;
using AutoTests.HCA.Core.UI;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.UITests.ChekoutTests.ChekoutShippingTests
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(BrowserType.Chrome)]
    [UiTest]
    internal class CheckoutShippingExistsAddressTests : BaseHcaWebTest
    {
        public CheckoutShippingExistsAddressTests(BrowserType browserType) : base(browserType)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
            var user = TestsData.GetDefaultUser();
            var userManager = TestsHelper.CreateUserManagerHelper(user);
            userManager.CleanPromotions();
            userManager.CleanCart();
            _hcaWebSite.OpenHcaAndLogin(user.Credentials);
        }

        private HcaWebSite _hcaWebSite;

        [Test]
        public void VerifyChooseSavedAddressTest()
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

        [Test]
        public void VerifyAddNewAddressTest()
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
        public void VerifyBillingAddressFillWithSavedAddressTest([Values] bool fillBillingCheckout)
        {
            var products = TestsData.GetProducts(2);
            _hcaWebSite.AddProductsToCartFromTestData(products);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CheckoutShippingPage);
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
            _hcaWebSite.CheckoutShippingPage.SelectOptionByName(AddressOption.SavedAddress);
            _hcaWebSite.CheckoutShippingPage.SelectFirstShippingAddress();
            if (fillBillingCheckout)
            {
                _hcaWebSite.CheckoutShippingPage.SelectOptionByName(AddressOption.AlsoUseForBillingAddress);
            }
            _hcaWebSite.CheckoutShippingPage.SelectShippingMethod(ShippingMethod.Standard);
            _hcaWebSite.CheckoutShippingPage.ClickSubmit();
            _hcaWebSite.CheckoutBillingPage.WaitForOpened();
            _hcaWebSite.CheckoutBillingPage.VerifyOption(AddressOption.SameAsShippingAddress, fillBillingCheckout);
        }

    }
}
