using System.Collections.Generic;
using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.AutomationFramework.Shared.Models;
using AutoTests.AutomationFramework.UI;
using AutoTests.AutomationFramework.UI.Core;
using AutoTests.AutomationFramework.UI.Entities;
using AutoTests.HCA.Core.Common.Settings.Products;
using AutoTests.HCA.Core.UI.CommonElements;
using AutoTests.HCA.Core.UI.ConstantsAndEnums;
using AutoTests.HCA.Core.UI.Pages;
using AutoTests.HCA.Core.UI.Pages.Checkout;
using AutoTests.HCA.Core.UI.Pages.MyAccount;
using NUnit.Framework;

namespace AutoTests.HCA.Core.UI
{
    public class HcaWebSite : WebSiteEntity
    {
        private static HcaWebSite _hcaWebSite;

        protected HcaWebSite() : base(UiConfiguration.GetEnvironmentUri("HcaEnvironment"))
        {
        }

        public static HcaWebSite Instance => _hcaWebSite ??= new HcaWebSite();

        public MainMenuControl MainMenuControl => MainMenuControl.Instance;
        public HeaderControl HeaderControl => HeaderControl.Instance;
        public FooterControl FooterControl => FooterControl.Instance;

        public LoginForm LoginForm => LoginForm.Instance;

        public SignUpPage SignUpPage => SignUpPage.Instance;
        public PhonePage PhonePage => PhonePage.Instance;
        public ProductPage ProductPage => ProductPage.Instance;
        public CartPage CartPage => CartPage.Instance;
        public SearchPage SearchPage => SearchPage.Instance;
        public CheckoutShippingPage CheckoutShippingPage => CheckoutShippingPage.Instance;
        public CheckoutBillingPage CheckoutBillingPage => CheckoutBillingPage.Instance;
        public CheckoutPaymentPage CheckoutPaymentPage => CheckoutPaymentPage.Instance;
        public CheckoutConfirmationPage CheckoutConfirmationPage => CheckoutConfirmationPage.Instance;
        public MyAccountPage MyAccountPage => MyAccountPage.Instance;
        public ShopPage ShopPage => ShopPage.Instance;

        public MyAccountAccountDetailsSection MyAccountAccountDetailsSection => MyAccountAccountDetailsSection.Instance;
        public MyAccountNewAddressSection MyAccountNewAddressSection => MyAccountNewAddressSection.Instance;
        public MyAccountAddressSection MyAccountAddressSection => MyAccountAddressSection.Instance;
        public MyAccountChangePasswordSection MyAccountChangePasswordSection => MyAccountChangePasswordSection.Instance;
        public ProductsFilterSection ProductsFilterSection => ProductsFilterSection.Instance;
        public ProductGridSection ProductGridSection => ProductGridSection.Instance;


        public void OpenHcaAndLogin(string userName, string password)
        {
            NavigateToMain();
            OpenUserMenu();
            LoginForm.LogonToHca(userName, password);
            OpenUserMenu();
            LoginForm.VerifyLoggedUser();
            HideUserMenu();
        }

        public void OpenHcaAndLogin(UserLogin userLogin)
        {
            OpenHcaAndLogin(userLogin.Email, userLogin.Password);
        }


        //ToDo create address structure
        public string AddNewAddressForLoggedUser()
        {
            NavigateToPage(MyAccountPage);
            MyAccountPage.WaitForOpened();
            MyAccountAddressSection.WaitForOpenedAdressCard();
            MyAccountPage.AddAddressClick();
            MyAccountNewAddressSection.WaitForOpenedNewAddressForm();
            var newName = MyAccountAddressSection.GetFieldValue("First Name");
            Assert.True(newName == string.Empty);
            var firstName = StringHelpers.RandomString(10);
            MyAccountNewAddressSection.FillFieldByName("First Name", firstName);
            var lastName = StringHelpers.RandomString(10);
            MyAccountNewAddressSection.FillFieldByName("Last Name", lastName);
            var addressLine = StringHelpers.RandomString(10);
            MyAccountNewAddressSection.FillFieldByName("Address Line", addressLine);
            var city = StringHelpers.RandomString(10);
            MyAccountNewAddressSection.FillFieldByName("City", city);
            MyAccountNewAddressSection.SelectValueInTheField("Country", "United States");
            MyAccountNewAddressSection.SelectValueInTheField("State", "New York");
            MyAccountNewAddressSection.FillFieldByName("Postal Code", "10005");
            MyAccountNewAddressSection.ClickSaveAddress();
            MyAccountAddressSection.WaitForOpenedAdressCard();
            return $"{firstName} {lastName}, {addressLine}";
        }

        //TODO create userClass for response
        public string CreateNewUser()
        {
            Browser.DeleteAllCookies();
            NavigateToMain();
            NavigateToPage(SignUpPage);
            var firstName = StringHelpers.RandomString(10);
            SignUpPage.FillFieldByName("First Name", firstName);
            var lastName = StringHelpers.RandomString(10);
            SignUpPage.FillFieldByName("Last Name", lastName);
            SignUpPage.FillFieldByName("Email", $"{firstName}@autotests.com");
            SignUpPage.FillFieldByName("Password", "password");
            SignUpPage.FillFieldByName("Confirm Password", "password");
            SignUpPage.ClickSignUp();
            NavigateToPage(SignUpPage);
            SignUpPage.WaitAccountSignUpSuccessMessage();
            return firstName;
        }

        public void LogOut()
        {
            OpenUserMenu();

            if (LoginForm.IsUserLogged())
                LoginForm.SignOutClick();
        }

        public void HideUserMenu()
        {
            var tryCount = 0;
            while (LoginForm.IsFormPresent() || tryCount == 2)
            {
                HeaderControl.ClickUserButton();
                LoginForm.WaitForNotPresentForm(10, false);
                tryCount++;
            }

            LoginForm.VerifyFormNotPresent();
        }

        public void OpenUserMenu()
        {
            var tryCount = 0;
            while (!LoginForm.IsFormPresent() || tryCount == 2)
            {
                HeaderControl.ClickUserButton();
                LoginForm.WaitForPresentForm(10, false);
                tryCount++;
            }

            LoginForm.VerifyFormPresent();
        }

        public void AddProductToCart(ProductTestsDataSettings product)
        {
            NavigateToPage(ProductPage.GetPath() + $"/{product.ProductId}");
            ProductPage.WaitForOpened();
            if (!product.DefaultVariant) ProductPage.ChooseColor(1);
            ProductPage.AddToCartButtonClick();
            HeaderControl.WaitForPresentProductsQuantity();
        }

        public void AddProductsToCartFromTestData(IEnumerable<ProductTestsDataSettings> products)
        {
            foreach (var product in products) AddProductToCart(product);
        }

        public void AddProductAndGoToCheckoutShippingPage(ProductTestsDataSettings product)
        {
            AddProductToCart(product);
            HeaderControl.ClickCartButton();
            CartPage.VerifyOpened();
            CartPage.WaitForProductsLoaded();
            CartPage.ClickCheckoutButton();
        }

        public void AddProductAndGoToCheckoutBillingPage(ProductTestsDataSettings product)
        {
            AddProductAndGoToCheckoutShippingPage(product);
            CheckoutShippingPage.GoToTheNextPage();
        }

        public void AddProductAndGoToCheckoutPaymentPage(ProductTestsDataSettings product)
        {
            AddProductAndGoToCheckoutBillingPage(product);
            CheckoutBillingPage.GoToTheNextPage();
        }

        public void AddProductAndGoToCheckoutConfirmationPage(ProductTestsDataSettings product)
        {
            AddProductAndGoToCheckoutPaymentPage(product);
            CheckoutPaymentPage.GoToTheNextPage();
        }

        public void GoToPageWithDefaultParams(PagePrefix pagePrefix, ProductTestsDataSettings product, UserLogin user)
        {
            switch (pagePrefix)
            {
                case PagePrefix.CheckoutBilling:
                    _hcaWebSite.AddProductAndGoToCheckoutBillingPage(product);
                    break;
                case PagePrefix.CheckoutPayment:
                    _hcaWebSite.AddProductAndGoToCheckoutPaymentPage(product);
                    break;
                case PagePrefix.CheckoutConfirmation:
                    _hcaWebSite.AddProductAndGoToCheckoutConfirmationPage(product);
                    break;
                case PagePrefix.Account:
                case PagePrefix.AccountOrderHistory:
                    _hcaWebSite.OpenHcaAndLogin(user.Email, user.Password);
                    _hcaWebSite.NavigateToPage(pagePrefix.GetPrefix());
                    break;
                default:
                    _hcaWebSite.NavigateToPage(pagePrefix.GetPrefix());
                    break;
            }
        }
    }
}