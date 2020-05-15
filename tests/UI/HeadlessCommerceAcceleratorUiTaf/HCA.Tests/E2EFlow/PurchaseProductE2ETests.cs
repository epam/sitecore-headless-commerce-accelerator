using Core;
using NUnit.Framework;
using Pages;
using Pages.Pages;
using Pages.Steps;

namespace Tests.E2EFlow
{
    [TestFixture]
    public class PurchaseProductE2ETests : BaseUiTest
    {
        [Test]
        public void PurchaseProductE2ETest()
        {
            var confirmationPage = NavigationSteps.OpenHomePage()
                .Header.NavMenu.OpenCategoryPopUp(Categories.Phones)
                .NavigateToSubCategoryPage(SubCategories.Phones)
                .OpenProductDetailsPage(6042347)
                .AddToCart()
                .Header.OpenCartPage()
                .UpdateProductQty(3)
                .CheckoutWithPromoCode()
                .FillIn()
                .SaveAndContinue()
                .SelectSameAsShippingAddress()
                .SaveAndContinue()
                .FillIn()
                .PlaceOrder();
            
            Assert.IsTrue(confirmationPage.IsConfirmationDisplayed, "Confirmation text isn't displayed.");
            Assert.AreEqual(confirmationPage.ConfirmationText, "THANK YOU FOR YOUR ORDER.", "Confirmation text is not equal to \"Thank You For Your Order.\".");
            Assert.IsTrue(confirmationPage.IsOrderDetailsDisplayed, "Order details aren't displayed.");
            Assert.IsTrue(Browser.Url.Contains(confirmationPage.OrderNumber), "There is mismatch in Order Number, that is present in Confirmation Message." +
                "It should be equal to the Order Number from the Browser's address line.");
            Assert.AreEqual(confirmationPage.EmailFromOrderDetails, "test@test.ta", "Confirmation Message should contain email from Order Details.");
        }
    }
}
