using Core;
using Pages.Pages;
using System.Text.RegularExpressions;

namespace HCA.Pages.Pages.CheckoutPages
{
    public class ConfirmationPage : BasePage
    {
        private UiElement Confirmation => new UiElement("//span[@class='thank-you-text1']");
        private UiElement OrderDetails => new UiElement("//span[@class='thank-you-text2']");

        public bool IsConfirmationDisplayed
        {
            get
            {
                DriverContext.Driver.WaitUntilElementAppears(Confirmation.Locator);
                return Confirmation.Displayed;
            }
        }

        public bool IsOrderDetailsDisplayed => OrderDetails.Displayed;

        public string ConfirmationText => Confirmation.Text;

        public string EmailFromOrderDetails => new Regex(@"[a-zA-Z0-9-_.]+@[a-zA-Z0-9-_.]+").Match(OrderDetails.Text).Value;

        public string OrderNumber => new Regex(@"(?!^Order )([A-Z0-9]{25})").Match(OrderDetails.Text).Value;
    }
}
