using System;
using HCA.Pages.CommonElements;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;
using UIAutomationFramework.Interfaces;

namespace HCA.Pages.Pages
{
    public class SignUpPage : FieldsContainer, IPage
    {
        private static SignUpPage _productPage;

        private readonly WebButton _signUpButton =
            new WebButton("Sign Up Button", ByCustom.XPath("//button[@text=' Sign up!']"));

        private readonly WebLabel _signUpSuccessMessage = new WebLabel("Success  message",
            ByCustom.XPath("//div[@class = 'account-created-message']"));

        public static SignUpPage Instance =>
            _productPage ?? (_productPage = new SignUpPage());

        public void VerifyOpened()
        {
            throw new NotImplementedException();
        }

        public Uri GetUrl()
        {
            throw new NotImplementedException();
        }

        public string GetPath()
        {
            return "/account/sign-up";
        }

        public void WaitForOpened()
        {
            Browser.WaitForUrlContains(GetPath());
        }

        public bool SignUpIsClickable()
        {
            return _signUpButton.IsClickable();
        }

        public void ClickSignUp()
        {
            _signUpButton.Click();
        }

        public void WaitAccountSignUpSuccessMessage()
        {
            _signUpSuccessMessage.WaitForPresent();
        }
    }
}