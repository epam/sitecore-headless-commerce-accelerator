using System;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.CommonElements
{
    public class LoginForm
    {
        private static LoginForm _loginForm;

        protected static readonly WebElement LoginFormElement =
            new WebElement("Login Form", ByCustom.XPath(".//div[@class = 'login-form']"));

        private readonly WebButton _myAccountButton =
            new WebButton("My Account", ByCustom.XPath(".//a[@href = '/account']"), LoginFormElement);

        private readonly WebButton _orderHistoryButton = new WebButton("Order History",
            ByCustom.XPath(".//a[@href = '/account/order-history']"), LoginFormElement);

        private readonly WebTextField _passwordField = new WebTextField("Password Field",
            ByCustom.XPath(".//input[@name = 'password']"), LoginFormElement);

        private readonly WebButton _signInButton =
            new WebButton("Sign In Button", ByCustom.XPath(".//button"), LoginFormElement);

        private readonly WebButton _signOutButton = new WebButton("Sign Out Button",
            ByCustom.XPath(".//button[text() = 'Sign Out']"), LoginFormElement);

        private readonly WebLink _signUpLink =
            new WebLink("Sign Up link", ByCustom.XPath(".//a[@href = '/account/sign-up']"));

        private readonly WebTextField _userNameField = new WebTextField("User Name Field",
            ByCustom.XPath(".//input[@name = 'email']"), LoginFormElement);

        private readonly WebLabel _validationLabel =
            new WebLabel("Validation message", ByCustom.XPath(".//h5"), LoginFormElement);

        public static LoginForm Instance => _loginForm ??= new LoginForm();

        public void VerifyFormNotPresent() => LoginFormElement.VerifyNotPresent();

        public void WaitForPresentForm()
        {
            LoginFormElement.WaitForPresent();
        }

        public void WaitForNotPresentForm()
        {
            LoginFormElement.WaitForNotPresent();
        }

        public bool IsFormPresent()
        {
            return LoginFormElement.IsPresent();
        }

        public void FillUserNameField(string value)
        {
            _userNameField.Type(value);
        }

        public void FillPasswordField(string value)
        {
            _passwordField.Type(value);
        }

        public void SignInButtonClick()
        {
            _signInButton.Click();
        }

        public void VerifySignInButtonNotClickable()
        {
            _signInButton.VerifyNotClickable();

        }

        public void SignUpClick()
        {
            _signUpLink.Click();
        }

        public void SignOutClick()
        {
            _signOutButton.Click();
        }

        public void MyAccountClick()
        {
            _myAccountButton.Click();
        }

        public void OrderHistory()
        {
            _orderHistoryButton.Click();
        }

        public void VerifyLoggedUser()
        {
            _myAccountButton.WaitForPresent();
        }

        public bool IsUserLogged()
        {
            return _myAccountButton.IsPresent();
        }

        public void VerifyValidationMessage(string text)
        {
            _validationLabel.VerifyText(text);
        }

        public void LogonToHca(string login, string password)
        {
            FillUserNameField(login);
            FillPasswordField(password);
            SignInButtonClick();
        }
    }
}