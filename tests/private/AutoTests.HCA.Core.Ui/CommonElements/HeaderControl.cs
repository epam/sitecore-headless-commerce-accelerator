using AutoTests.AutomationFramework.UI.Controls;
using AutoTests.AutomationFramework.UI.Core;
using AutoTests.HCA.Core.UI.ConstantsAndEnums.Header;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AutoTests.HCA.Core.UI.CommonElements
{
    public class HeaderControl
    {
        private static HeaderControl _headerControl;

        private static readonly WebElement _userNavigation = new WebElement("Navigation User panel",
            ByCustom.XPath("//nav[@class = 'user-navigation']"));

        private static readonly WebElement _productsQuantity =
            new WebElement("Quantity Products in Cart", By.ClassName("quantity"),
                FindUserNavigationLinkByName(UserNavigationLink.ShoppingCart.GetLinkName()));

        private static readonly WebTextField _searchField = new WebTextField("SearchTextField",
            By.XPath("//form[@class= 'navigation-search']/input[@type='search']"));

        private readonly WebElement _headerElement = new WebElement("Header", ByCustom.Id("header-main"));

        private readonly WebLink _logoLink = new WebLink("HCA Logo Link",
            ByCustom.ClassName("navigation-logo"));

        private readonly WebElement _quickNavigation = new WebElement("Quick Navigation panel",
            ByCustom.XPath("//nav[@class = 'quick-navigation']"));

        public static HeaderControl Instance => _headerControl ??= new HeaderControl();

        public string SearchFieldPlaceholderText => _searchField.GetAttribute("placeholder");

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

        public bool HeaderIsPresent()
        {
            return _headerElement.IsPresent();
        }

        public void ClickLogoLink()
        {
            _logoLink.Click();
        }

        public void ClickStoreLocatorButton()
        {
            FindQuickNavigationLinkByName(QuickNavigationLink.StoreLocator.GetLinkName()).Click();
        }

        public void ClickOnlineFlyerButton()
        {
            FindQuickNavigationLinkByName(QuickNavigationLink.OnlineFlyer.GetLinkName()).Click();
        }

        public void ClickLanguageAndCurrencyButton()
        {
            FindQuickNavigationLinkByName(QuickNavigationLink.LanguageAndCurrency.GetLinkName()).Click();
        }

        public void ClickWishButton()
        {
            FindUserNavigationLinkByName(UserNavigationLink.WishList.GetLinkName()).Click();
        }

        public void ClickCartButton()
        {
            FindUserNavigationLinkByName(UserNavigationLink.ShoppingCart.GetLinkName()).Click();
        }

        public void ClickUserButton()
        {
            FindUserNavigationLinkByName(UserNavigationLink.MyAccount.GetLinkName()).Click();
        }

        public void SearchText(string text)
        {
            _searchField.IsPresent();
            _searchField.Type(text);
            _searchField.Submit();
        }

        public void WaitForPresentProductsQuantity()
        {
            _productsQuantity.WaitForPresent();
        }

        public void VerifyLogoNavigationLink(string href)
        {
            VerifyLinkWithoutText(_logoLink, href);
        }

        public void VerifyUserNavigationLink(UserNavigationLink userNavigationLink)
        {
            VerifyLinkWithoutText(FindUserNavigationLinkByName(userNavigationLink.GetLinkName()),
                userNavigationLink.GetHref());
        }

        public void VerifyQuickNavigationLink(QuickNavigationLink utilityLink)
        {
            VerifyLink(FindQuickNavigationLinkByName(utilityLink.GetLinkName()), utilityLink.GetLinkText(),
                utilityLink.GetHref());
        }

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

        private static WebLink FindUserNavigationLinkByName(string navigationLinkName)
        {
            return new WebLink($"Navigation User Link {navigationLinkName}",
                By.XPath($".//span[text() = '{navigationLinkName}']/parent::*"),
                _userNavigation);
        }

        private WebLink FindQuickNavigationLinkByName(string linkName)
        {
            return new WebLink($"Quick Navigation Link {linkName}", By.XPath($".//a[text() = '{linkName}']"),
                _quickNavigation);
        }
    }
}