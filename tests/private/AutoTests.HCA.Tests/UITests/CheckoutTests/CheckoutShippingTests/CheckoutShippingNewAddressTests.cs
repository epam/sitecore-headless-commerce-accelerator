using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.BaseTests;
using AutoTests.HCA.Core.Common.Entities.ConstantsAndEnums.Checkout;
using AutoTests.HCA.Core.Common.Entities.ConstantsAndEnums.Fields;
using AutoTests.HCA.Core.Common.Settings.Users;
using AutoTests.HCA.Core.UI;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.UITests.CheckoutTests.CheckoutShippingTests
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(BrowserType.Chrome, HcaUserRole.Guest)]
    [TestFixture(BrowserType.Chrome, HcaUserRole.User)]
    [UiTest]
    internal class CheckoutShippingNewAddressTests : BaseHcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
            if (_hcaUserRole == HcaUserRole.Guest)
            {
                _hcaWebSite.NavigateToMain();
            }
            else
            {
                var user = TestsData.GetUser().Credentials;
                var userManager = TestsHelper.CreateHcaUserApiHelper(user);
                userManager.CleanPromotions();
                userManager.CleanCart();
                _hcaWebSite.OpenHcaAndLogin(user);
            }

            //TODO add products to cart API
            var products = TestsData.GetProducts(2);
            _hcaWebSite.AddProductsToCartFromTestData(products);
            _hcaWebSite.NavigateToPage(_hcaWebSite.CheckoutShippingPage);
            _hcaWebSite.CheckoutShippingPage.WaitForOpened();
            _hcaWebSite.CheckoutShippingPage.SelectOptionByName(AddressOption.NewAddress);
        }

        public CheckoutShippingNewAddressTests(BrowserType browserType, HcaUserRole hcaUserRole) : base(browserType)
        {
            _hcaUserRole = hcaUserRole;
        }

        private HcaWebSite _hcaWebSite;
        private readonly HcaUserRole _hcaUserRole;

        [Test]
        public void T1_CheckoutShippingNewAddress_VerifyBillingAddressFill([Values] bool fillBillingCheckout)
        {
            _hcaWebSite.CheckoutShippingPage.FillAddressByDefault(_hcaUserRole);
            _hcaWebSite.CheckoutShippingPage.SelectShippingMethod(ShippingMethod.Standard);
            if (fillBillingCheckout)
                _hcaWebSite.CheckoutShippingPage.SelectOptionByName(AddressOption.AlsoUseForBillingAddress);
            _hcaWebSite.CheckoutShippingPage.ClickSubmit();
            _hcaWebSite.CheckoutBillingPage.WaitForOpened();
            _hcaWebSite.CheckoutBillingPage.VerifyOption(AddressOption.SameAsShippingAddress, fillBillingCheckout);
        }

        [Test]
        public void T2_CheckoutShippingNewAddress_VerifyCorrectAddress()
        {
            _hcaWebSite.CheckoutShippingPage.FillAddressByDefault(_hcaUserRole);
            _hcaWebSite.CheckoutShippingPage.SelectShippingMethod(ShippingMethod.Standard);
            _hcaWebSite.CheckoutShippingPage.ClickSubmit();
            _hcaWebSite.CheckoutBillingPage.WaitForOpened();
        }

        [Test]
        public void T3_CheckoutShippingNewAddress_VerifyDifferentMethodShipping([Values] ShippingMethod shippingMethod)
        {
            _hcaWebSite.CheckoutShippingPage.FillAddressByDefault(_hcaUserRole);
            if (shippingMethod == ShippingMethod.SelectOption)
            {
                _hcaWebSite.CheckoutShippingPage.VerifySubmitIsNotClickable();
            }
            else
            {
                _hcaWebSite.CheckoutShippingPage.SelectShippingMethod(shippingMethod);
                _hcaWebSite.CheckoutShippingPage.ClickSubmit();
                _hcaWebSite.CheckoutBillingPage.WaitForOpened();
            }
        }

        [Test]
        public void T4_CheckoutShippingNewAddress_VerifyOneUnfilledField([Values] AddressField addressField)
        {
            _hcaWebSite.CheckoutShippingPage.FillAddressByDefault(_hcaUserRole);
            _hcaWebSite.CheckoutShippingPage.ClearField(addressField);
            _hcaWebSite.CheckoutShippingPage.SelectShippingMethod(ShippingMethod.Standard);
            _hcaWebSite.CheckoutShippingPage.VerifySubmitIsNotClickable();
        }
    }
}