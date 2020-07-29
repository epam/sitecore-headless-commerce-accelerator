using Ui.AutomationFramework.Controls;
using Ui.AutomationFramework.Core;
using Ui.HCA.Pages.ConstantsAndEnums;
using UI.AutomationFramework.Controls;

namespace Ui.HCA.Pages.Pages
{
    public class SignUpPage : CommonPage
    {
        private static SignUpPage _signUpPage;

        private readonly WebButton _signUpButton =
            new WebButton("Sign Up Button", ByCustom.XPath("//button[text()=' Sign up!']"));

        private readonly WebLabel _signUpSuccessMessage = new WebLabel("Success  message",
            ByCustom.XPath("//div[@class = 'account-created-message']"));

        public static SignUpPage Instance => _signUpPage ??= new SignUpPage();

        public override string GetPath() =>
            PagePrefix.AccountSignUp.GetPrefix();

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