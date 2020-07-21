using System;
using HCA.Pages.CommonElements;
using HCA.Pages.ConstantsAndEnums;
using HCA.Pages.Pages;
using HCA.Pages.Pages.Checkout;
using HCA.Pages.Pages.MyAccount;
using NUnit.Framework;
using UIAutomationFramework;
using UIAutomationFramework.Core;
using UIAutomationFramework.Entities.WebSiteEntities;
using UIAutomationFramework.Utils;

namespace HCA.Pages
{
    public class HcaWebSite : WebSiteEntity
    {
        private static readonly string EnvironmentName = "HCAEnvironment";
        private static HcaWebSite _hcaWebSite;

        protected HcaWebSite() : base(Configuration.GetEnvironmentUri(EnvironmentName))
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
            HideUserMenu();
            OpenUserMenu();
            LoginForm.VerifyLoggedUser();
            HideUserMenu();
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
            var firstName = StringUtils.RandomString(10);
            MyAccountNewAddressSection.FillFieldByName("First Name", firstName);
            var lastName = StringUtils.RandomString(10);
            MyAccountNewAddressSection.FillFieldByName("Last Name", lastName);
            var addressLine = StringUtils.RandomString(10);
            MyAccountNewAddressSection.FillFieldByName("Address Line", addressLine);
            var city = StringUtils.RandomString(10);
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
            var firstName = StringUtils.RandomString(10);
            SignUpPage.FillFieldByName("First Name", firstName);
            var lastName = StringUtils.RandomString(10);
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
            int tryCount = 0;
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
            int tryCount = 0;
            while (!LoginForm.IsFormPresent() || tryCount==2)
            {
                HeaderControl.ClickUserButton();
                LoginForm.WaitForPresentForm(10, false);
                tryCount++;
            }

            LoginForm.VerifyFormPresent();
        }

        public void AddProductToCart(int id)
        {
            NavigateToPage(ProductPage.GetPath() + $"/{id}");
            ProductPage.WaitForOpened();
            ProductPage.AddToCartButtonClick();
            HeaderControl.WaitForPresentProductsQuantity();
            //TODO investigate problem with blank after Product adding
            ProductPage.AddToCartButtonClick();
        }

        public void AddProductAndGoToCheckoutShippingPage(int productId)
        {
            AddProductToCart(productId);
            HeaderControl.ClickCartButton();
            CartPage.VerifyOpened();
            CartPage.WaitForProductsLoaded();
            CartPage.ClickChekoutButton();
        }

        public void AddProductAndGoToCheckoutBillingPage(int productId)
        {
            AddProductAndGoToCheckoutShippingPage(productId);
            CheckoutShippingPage.GoToTheNextPage();
        }

        public void AddProductAndGoToCheckoutPaymentPage(int productId)
        {
            AddProductAndGoToCheckoutBillingPage(productId);
            CheckoutBillingPage.GoToTheNextPage();
        }

        public void AddProductAndGoToCheckoutConfirmationPage(int productId)
        {
            AddProductAndGoToCheckoutPaymentPage(productId);
            CheckoutPaymentPage.GoToTheNextPage();
        }

        public void GoToPageWithDefaultParams(PagePrefix pagePrefix)
        {
            const int productId = 6042347;
            switch (pagePrefix)
            {
                case PagePrefix.CheckoutBilling:
                    _hcaWebSite.AddProductAndGoToCheckoutBillingPage(productId);
                    break;
                case PagePrefix.CheckoutPayment:
                    _hcaWebSite.AddProductAndGoToCheckoutPaymentPage(productId);
                    break;
                case PagePrefix.CheckoutConfirmation:
                    _hcaWebSite.AddProductAndGoToCheckoutConfirmationPage(productId);
                    break;
                case PagePrefix.Account:
                case PagePrefix.AccountOrderHistory:
                    var user = Configuration.GetDefaultUserLogin();
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