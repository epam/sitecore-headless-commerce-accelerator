using HCA.Pages.ConstantsAndEnums.Header;
using NUnit.Framework;
using OpenQA.Selenium;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.CommonElements
{
    public class HeaderControl
    {
        private static HeaderControl _headerControl;

        private readonly WebElement _headerElement = new WebElement("Header", ByCustom.Id("header-main"));

        private readonly WebLink _logoLink = new WebLink("HCA Logo Link",
            ByCustom.ClassName("navigation-logo"));

        private readonly WebElement _quickNavigation = new WebElement("Quick Navigation panel",
            ByCustom.XPath("//nav[@class = 'quick-navigation']"));

        private static readonly WebElement _userNavigation = new WebElement("Navigation User panel",
            ByCustom.XPath("//nav[@class = 'user-navigation']"));

        private static readonly WebElement _productsQuantity =
            new WebElement("Quantity Products in Cart", By.ClassName("quantity"),
                FindUserNavigationLinkByName(UserNavigationLink.ShoppingCart.GetLinkName()));

        private static readonly WebTextField _searchField = new WebTextField("SearchTextField",
            By.XPath("//form[@class= 'navigation-search']/input[@type='search']"));

        public static HeaderControl Instance => _headerControl ??= new HeaderControl();

        public string SearchFieldPlaceholderText => _searchField.GetAttribute("placeholder");

        public bool HeaderIsPresent() => _headerElement.IsPresent();

        public void ClickLogoLink() => _logoLink.Click();

        public void ClickStoreLocatorButton() => FindQuickNavigationLinkByName(QuickNavigationLink.StoreLocator.GetLinkName()).Click();
        public void ClickOnlineFlyerButton() => FindQuickNavigationLinkByName(QuickNavigationLink.OnlineFlyer.GetLinkName()).Click();
        public void ClickLanguageAndCurrencyButton() => FindQuickNavigationLinkByName(QuickNavigationLink.LanguageAndCurrency.GetLinkName()).Click();

        public void ClickWishButton() => FindUserNavigationLinkByName(UserNavigationLink.WishList.GetLinkName()).Click();
        public void ClickCartButton() => FindUserNavigationLinkByName(UserNavigationLink.ShoppingCart.GetLinkName()).Click();
        public void ClickUserButton() => FindUserNavigationLinkByName(UserNavigationLink.MyAccount.GetLinkName()).Click();

        public void SearchText(string text)
        {
            _searchField.IsPresent();
            _searchField.Type(text);
            _searchField.Submit();
        }

        public void WaitForPresentProductsQuantity() =>
            _productsQuantity.WaitForPresent();

        public int ProductsQuantityInCart
        {
            get
            {
                var text = _productsQuantity.GetText();
                if (int.TryParse(text, out var quantity))
                    return quantity;

                Assert.Fail($"Failed to get the number of products in the basket. Actual result: '{text}'");
                return -1;
            }
        }

        public void VerifyLogoNavigationLink(string href) =>
            VerifyLinkWithoutText(_logoLink, href);

        public void VerifyUserNavigationLink(UserNavigationLink userNavigationLink) =>
           VerifyLinkWithoutText(FindUserNavigationLinkByName(userNavigationLink.GetLinkName()), userNavigationLink.GetHref());

        public void VerifyQuickNavigationLink(QuickNavigationLink utilityLink) =>
            VerifyLink(FindQuickNavigationLinkByName(utilityLink.GetLinkName()), utilityLink.GetLinkText(), utilityLink.GetHref());

        private static void VerifyLink(WebLink linkContainer, string text, string url)
        {
            linkContainer.IsPresent();
            linkContainer.VerifyText(text);
            linkContainer.VerifyUrl(url);
            linkContainer.IsClickable();
        }

        private static void VerifyLinkWithoutText(WebLink linkContainer, string url)
        {
            linkContainer.IsPresent();
            linkContainer.VerifyUrl(url);
            linkContainer.IsClickable();
        }

        private static WebLink FindUserNavigationLinkByName(string navigationLinkName) =>
            new WebLink($"Navigation User Link {navigationLinkName}",
                By.XPath($".//span[text() = '{navigationLinkName}']/parent::*"),
                _userNavigation);

        private WebLink FindQuickNavigationLinkByName(string linkName) =>
            new WebLink($"Quick Navigation Link {linkName}", By.XPath($".//a[text() = '{linkName}']"),
                _quickNavigation);
    }
}