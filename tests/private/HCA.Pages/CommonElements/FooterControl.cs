using System.Linq;
using HCA.Pages.HCAElements.Footer;
using NUnit.Framework;
using OpenQA.Selenium;
using UIAutomationFramework.Controls;
using UIAutomationFramework.Core;

namespace HCA.Pages.CommonElements
{
    public class FooterControl
    {
        private static FooterControl _footerControl;

        public static FooterControl Instance => _footerControl ??= new FooterControl();

        private static readonly WebElement _footerElement = new WebElement("Footer", ByCustom.Id("footer-main"));

        private static readonly WebElement _footerLinksListElement =
            new WebElement("Footer Links List", By.XPath("//ul[@class ='footer-links-list']"));

        private static readonly WebLabel _socialNetworksTitle =
            new WebLabel("Footer Social Title", By.XPath("//h2[@class = 'social-title']"));

        private static readonly WebElement _footerSocialListElement =
            new WebElement("Footer Social Networks List", By.XPath("//ul[@class ='social-list']"));

        private static readonly WebElement _copyright = new WebElement("Footer Copyright",
            ByCustom.XPath("//div[@class='footer-copyright']"));

        public bool FooterElementIsPresent() =>
            _footerElement.IsPresent();

        public int SiteLinksCount
        {
            get
            {
                _footerLinksListElement.IsPresent();
                return _footerLinksListElement.GetChildElementsCount(By.XPath("//li[@class = 'footer-list-item']"));
            }
        }

        public string SocialNetworksTitleText
        {
            get
            {
                _socialNetworksTitle.IsPresent();
                return _socialNetworksTitle.GetText();
            }
        }

        public int SocialNetworksCount
        {
            get
            {
                _footerSocialListElement.IsPresent();
                return _footerSocialListElement.GetChildElementsCount(By.XPath("//li[@class = 'social-item']"));
            }
        }

        public string CopyrightText
        {
            get
            {
                _copyright.IsPresent();
                return _copyright.GetText();
            }
        }

        public void VerifyForExtraFooters() =>
            Assert.AreEqual(1, new WebElement("App", By.Id("app")).GetChildElements(_footerElement).Count(),
                "There should be one footer on the page");

        public void VerifySiteLinksAndNetworksLinksContainers()
        {
            var columns = new WebElement("Footer links columns", ByCustom.ClassName("footer-columns"))
                    .GetChildElements(ByCustom.ClassStartsWith("col-md-")).ToArray();

            Assert.AreEqual(2, columns.Length, 
                "Site Links and Social Network Links containers not found or displayed incorrectly");
        }

        public void VerifySiteLink(FooterSiteLink siteLink) =>
            VerifyLink(FindSiteLinkItemByName(siteLink.GetLinkName()), siteLink.GetLinkText(), siteLink.GetHref());

        public void VerifySocialNetworksLink(FooterSocialNetworkLink socialNetwork) =>
            VerifyLink(FindSocialNetworkLinkByName(socialNetwork.GetLinkName()), socialNetwork.GetLinkText(), socialNetwork.GetHref());

        public void ChooseSiteLinkItem(string siteLinkName) => FindSiteLinkItemByName(siteLinkName).Click();

        public void ChooseSocialNetworksItem(string socialNetworkName) => FindSocialNetworkLinkByName(socialNetworkName).Click();

        private static void VerifyLink(WebLink linkContainer, string text, string url)
        {
            linkContainer.IsPresent();
            linkContainer.VerifyText(text);
            linkContainer.VerifyUrl(url);
        }

        private WebLink FindSiteLinkItemByName(string name) =>
            new WebLink($"Footer site link {name}",
                ByCustom.XPath($"//li[@class ='footer-list-item']/a[text()='{name}']"),
                _footerLinksListElement);

        private WebLink FindSocialNetworkLinkByName(string name) =>
            new WebLink($"Footer social link {name}",
                ByCustom.XPath($".//a[@class = 'social-link']/i[@class = 'fa fa-{name}']/parent::*"),
                _footerSocialListElement);
    }
}
