using AutoTests.AutomationFramework.UI.Driver;
using AutoTests.HCA.Core.UI;
using AutoTests.HCA.Core.UI.CommonElements;
using AutoTests.HCA.Core.UI.ConstantsAndEnums;
using AutoTests.HCA.Core.UI.ConstantsAndEnums.Header;
using AutoTests.HCA.Core.UI.ConstantsAndEnums.Header.MainMenu;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.UITests
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture(BrowserType.Chrome)]
    [HeaderTest]
    public class HeaderTests : HcaWebTest
    {
        [SetUp]
        public void SetUp()
        {
            _hcaWebSite = HcaWebSite.Instance;
            _mainMenuControl = _hcaWebSite.MainMenuControl;
        }

        private HcaWebSite _hcaWebSite;
        private MainMenuControl _mainMenuControl;
        private readonly string _categoryTitle = "CATEGORY";
        private readonly string _featuredTitle = "FEATURED";

        public HeaderTests(BrowserType browserType) : base(browserType)
        {
        }

        private void CheckFullyMenuItem(MenuItem menuItem, params SubMenuItem[] subMenuItems)
        {
            _mainMenuControl.VerifyMenuItem(menuItem);
            _mainMenuControl.MoveMouseTiMenuItem(menuItem);
            _mainMenuControl.TitleMenuItemIsPresent(_categoryTitle, menuItem);
            _mainMenuControl.TitleMenuItemIsPresent(_featuredTitle, menuItem);
            _mainMenuControl.VerifyMenuItemImage(menuItem);
            _mainMenuControl.VerifySubMenuItems(menuItem, subMenuItems);
        }

        [Test]
        public void HeaderTest([Values] PagePrefix pagePrefix)
        {
            const int expMenuItemCounts = 5;

            _hcaWebSite.GoToPageWithDefaultParams(pagePrefix,
                new DefaultHcaData(TestsData.ProductId, TestsData.DefUserLogin));

            var headerControl = _hcaWebSite.HeaderControl;
            headerControl.HeaderIsPresent();
            Assert.Multiple(() =>
            {
                headerControl.VerifyLogoNavigationLink(_hcaWebSite.Uri.AbsoluteUri);

                headerControl.VerifyQuickNavigationLink(QuickNavigationLink.StoreLocator);
                headerControl.VerifyQuickNavigationLink(QuickNavigationLink.OnlineFlyer);
                headerControl.VerifyQuickNavigationLink(QuickNavigationLink.LanguageAndCurrency);

                headerControl.VerifyUserNavigationLink(UserNavigationLink.WishList);
                headerControl.VerifyUserNavigationLink(UserNavigationLink.ShoppingCart);
                headerControl.VerifyUserNavigationLink(UserNavigationLink.MyAccount);
                _hcaWebSite.OpenUserMenu();
                _hcaWebSite.HideUserMenu();

                _mainMenuControl.MenuContainerIsPresent();
                Assert.AreEqual(expMenuItemCounts, _mainMenuControl.MenuItemsCount,
                    $"Expected Menu Items count: {expMenuItemCounts}. Actual: {_mainMenuControl.MenuItemsCount}");

                CheckFullyMenuItem(MenuItem.Cameras, SubMenuItem.CameraAccessories,
                    SubMenuItem.Camcorders,
                    SubMenuItem.Drones,
                    SubMenuItem.PointAndShoot,
                    SubMenuItem.MirrorLessBundles,
                    SubMenuItem.New,
                    SubMenuItem.Promotions,
                    SubMenuItem.Blog);
                CheckFullyMenuItem(MenuItem.Computers, SubMenuItem.Desktops,
                    SubMenuItem.Laptops,
                    SubMenuItem.Tablets,
                    SubMenuItem.New,
                    SubMenuItem.Promotions,
                    SubMenuItem.Blog);
                CheckFullyMenuItem(MenuItem.HouseHold, SubMenuItem.Gaming,
                    SubMenuItem.HealthAndBeautyAndFitness,
                    SubMenuItem.HealthAndBeautyAndFitness,
                    SubMenuItem.HomeTheater,
                    SubMenuItem.New,
                    SubMenuItem.Promotions,
                    SubMenuItem.Blog);
                CheckFullyMenuItem(MenuItem.Multimedia, SubMenuItem.HomeAudio,
                    SubMenuItem.CarSpeakers,
                    SubMenuItem.Amplifiers,
                    SubMenuItem.HomeTheater,
                    SubMenuItem.New,
                    SubMenuItem.Promotions,
                    SubMenuItem.Blog);
                CheckFullyMenuItem(MenuItem.Phones, SubMenuItem.Phones,
                    SubMenuItem.Audio,
                    SubMenuItem.SmartPhoneCases,
                    SubMenuItem.New,
                    SubMenuItem.Promotions,
                    SubMenuItem.Blog);

                Assert.AreEqual("I'M LOOKING FOR...", headerControl.SearchFieldPlaceholderText);
                headerControl.SearchText("123");
                _hcaWebSite.SearchPage.WaitForOpened();
                _hcaWebSite.SearchPage.VerifyOpened();
            });
        }
    }
}