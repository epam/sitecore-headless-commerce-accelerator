using HCA.Pages.CommonElements;
using HCA.Pages.Pages;
using HCA.Pages.Pages.Checkout;
using HCA.Pages.Pages.MyAccount;
using NUnit.Framework;
using UIAutomationFramework;
using UIAutomationFramework.Entities.WebSiteEntities;
using UIAutomationFramework.Utils;

namespace HCA.Pages
{
    public class HcaWebSite : WebSiteEntity
    {
        private static readonly string EnvironmentName = "HCAEnvironment";
        private static HcaWebSite _hcaiWebSite;

        protected HcaWebSite() : base(Configuration.GetEnvironmentUri(EnvironmentName))
        {
        }

        public static HcaWebSite Instance => _hcaiWebSite ?? (_hcaiWebSite = new HcaWebSite());

        public MainMenuControl MainMenuControl => MainMenuControl.Instance;
        public HeaderControl HeaderControl => HeaderControl.Instance;

        public LoginForm LoginForm => LoginForm.Instance;

        public SignUpPage SignUpPage => SignUpPage.Instance;
        public PhonePage PhonePage => PhonePage.Instance;
        public ProductPage ProductPage => ProductPage.Instance;
        public CartPage CartPage => CartPage.Instance;
        public CheckoutShippingPage CheckoutShippingPage => CheckoutShippingPage.Instance;
        public CheckoutBillingPage CheckoutBillingPage => CheckoutBillingPage.Instance;
        public CheckoutPaymentPage CheckoutPaymentPage => CheckoutPaymentPage.Instance;
        public CheckoutConfirmationPage CheckoutConfirmationPage => CheckoutConfirmationPage.Instance;
        public MyAccountPage MyAccountPage => MyAccountPage.Instance;

        public MyAccountAccountDetailsSection MyAccountAccountDetailsSection =>
            MyAccountAccountDetailsSection.Instance;

        public MyAccountNewAddressSection MyAccountNewAddressSection => MyAccountNewAddressSection.Instance;

        public MyAccountAddressSection MyAccountAddressSection => MyAccountAddressSection.Instance;
        public MyAccountChangePasswordSection MyAccountChangePasswordSection => MyAccountChangePasswordSection.Instance;

        public void OpenHcaAndLogin(string userName, string password)
        {
            NavigateToMain();
            HeaderControl.UserButtonClick();
            LoginForm.WaitForPresentForm();
            LoginForm.LogonToHca(userName, password);
            LoginForm.WaitForNotPresentForm();
            HeaderControl.UserButtonClick();
            LoginForm.VerifyLoggedUser();
            HeaderControl.UserButtonClick();
            LoginForm.WaitForNotPresentForm();
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
            NavigateToMain();
            LogOut();
            NavigateToPage(SignUpPage);
            var firstName = StringUtils.RandomString(10);
            SignUpPage.FillFieldByName("First Name", firstName);
            var lastName = StringUtils.RandomString(10);
            SignUpPage.FillFieldByName("Last Name", lastName);
            SignUpPage.FillFieldByName("Email", $"{firstName}@autotests.com");
            SignUpPage.FillFieldByName("Password", "password");
            SignUpPage.FillFieldByName("Confirm Password", "password");
            SignUpPage.ClickSignUp();
            SignUpPage.WaitAccountSignUpSuccessMessage();
            return firstName;
        }

        public void LogOut()
        {
            if (!LoginForm.IsFormPresent())
            {
                HeaderControl.UserButtonClick();
                LoginForm.WaitForPresentForm();
            }

            if (LoginForm.IsUserLogged())
                LoginForm.SignOutClick();
        }

        public void OpenUserMenu()
        {
            if (!LoginForm.IsFormPresent())
            {
                HeaderControl.UserButtonClick();
                LoginForm.WaitForPresentForm();
            }
        }
    }
}