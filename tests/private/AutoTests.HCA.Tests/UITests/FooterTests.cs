using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.UI;
using AutoTests.HCA.Core.UI.ConstantsAndEnums;
using AutoTests.HCA.Core.UI.ConstantsAndEnums.Footer;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.UITests
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(BrowserType.Chrome)]
    [UiTest]
    public class FooterTests : HcaWebTest
    {
        public FooterTests(BrowserType browserType) : base(browserType)
        {
        }

        [Test]
        public void FooterElementsTest([Values] PagePrefix pagePrefix)
        {
            const int expectedSiteLinksCount = 12;
            const int expectedSocialNetworksCount = 4;
            var hcaWebSite = HcaWebSite.Instance;

            hcaWebSite.GoToPageWithDefaultParams(pagePrefix, TestsData.HcaTestsData);
            var footerControl = hcaWebSite.FooterControl;
            footerControl.FooterElementIsPresent();
            Assert.Multiple(() =>
            {
                footerControl.VerifyForExtraFooters();
                footerControl.VerifySiteLinksAndNetworksLinksContainers();
                footerControl.VerifySiteLink(FooterSiteLink.GiftCards);
                footerControl.VerifySiteLink(FooterSiteLink.FindAStore);
                footerControl.VerifySiteLink(FooterSiteLink.SignUpForEmail);
                footerControl.VerifySiteLink(FooterSiteLink.JoinHCA);
                footerControl.VerifySiteLink(FooterSiteLink.GetHelp);
                footerControl.VerifySiteLink(FooterSiteLink.OrderStatus);
                footerControl.VerifySiteLink(FooterSiteLink.ShippingAndReview);
                footerControl.VerifySiteLink(FooterSiteLink.Returns);
                footerControl.VerifySiteLink(FooterSiteLink.PaymentOptions);
                footerControl.VerifySiteLink(FooterSiteLink.ContactUs);
                footerControl.VerifySiteLink(FooterSiteLink.News);
                footerControl.VerifySiteLink(FooterSiteLink.AboutHCA);
                footerControl.VerifySocialNetworksLink(FooterSocialNetworkLink.Twitter);
                footerControl.VerifySocialNetworksLink(FooterSocialNetworkLink.Facebook);
                footerControl.VerifySocialNetworksLink(FooterSocialNetworkLink.Youtube);
                footerControl.VerifySocialNetworksLink(FooterSocialNetworkLink.Instagram);
                Assert.AreEqual(expectedSiteLinksCount, footerControl.SiteLinksCount,
                    $"Expected site links count: {expectedSiteLinksCount}, " +
                    $"actual: {footerControl.SiteLinksCount}");
                Assert.AreEqual(expectedSocialNetworksCount, footerControl.SocialNetworksCount,
                    $"Expected social networks count: {expectedSocialNetworksCount}," +
                    $"actual: {footerControl.SocialNetworksCount}");
                Assert.AreEqual("SOCIAL NETWORKS", footerControl.SocialNetworksTitleText);
                Assert.AreEqual("© 2020 HEADLESS COMMERCE ACCELERATOR. ALL RIGHTS RESERVED. ",
                    footerControl.CopyrightText);
            });
        }
    }
}