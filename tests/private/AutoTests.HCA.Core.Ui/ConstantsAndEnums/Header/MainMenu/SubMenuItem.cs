using System.IO;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.AutomationFramework.UI;
using AutoTests.HCA.Core.UI.ConstantsAndEnums.Common;

namespace AutoTests.HCA.Core.UI.ConstantsAndEnums.Header.MainMenu
{
    public enum SubMenuItem
    {
        [Link("Gaming", "GAMING", "shop/Gaming")]
        Gaming,

        [Link("Health, Beauty and Fitness", "HEALTH, BEAUTY AND FITNESS", "shop/Health,%20Beauty%20and%20Fitness")]
        HealthAndBeautyAndFitness,

        [Link("Desktops", "DESKTOPS", "shop/Desktops")]
        Desktops,

        [Link("Laptops", "LAPTOPS", "shop/Laptops")]
        Laptops,

        [Link("Tablets", "TABLETS", "shop/Tablets")]
        Tablets,

        [Link("Camera Accessories", "CAMERA ACCESSORIES", "shop/Camera%20Accessories")]
        CameraAccessories,

        [Link("Camcorders", "CAMCORDERS", "shop/Camcorders")]
        Camcorders,

        [Link("Drones", "DRONES", "shop/Drones")]
        Drones,

        [Link("Point and shoot", "POINT AND SHOOT", "shop/Point%20and%20shoot")]
        PointAndShoot,

        [Link("Mirrorless Bundles", "MIRRORLESS BUNDLES", "shop/Mirrorless%20Bundles")]
        MirrorLessBundles,

        [Link("Home Audio", "HOME AUDIO", "shop/Home%20Audio")]
        HomeAudio,

        [Link("Car Speakers", "CAR SPEAKERS", "shop/Car%20Speakers")]
        CarSpeakers,

        [Link("Amplifiers", "AMPLIFIERS", "shop/Amplifiers")]
        Amplifiers,

        [Link("Home Theater", "HOME THEATER", "shop/Home%20Theater")]
        HomeTheater,

        [Link("Phones", "PHONES", "shop/Phones")]
        Phones,

        [Link("Audio", "AUDIO", "shop/Audio")] Audio,

        [Link("Smartphone Cases", "SMARTPHONE CASES", "shop/Smartphone%20Cases")]
        SmartPhoneCases,

        [Link("New", "NEW", "new")] New,

        [Link("Promotions", "PROMOTIONS", "Promitions")]
        Promotions,

        [Link("Blog", "BLOG", "Blog")] Blog
    }

    public static class SubMenuItemExtensions
    {
        public static string GetLinkName(this SubMenuItem item)
        {
            return item.GetAttribute<LinkAttribute>().Name;
        }

        public static string GetLinkText(this SubMenuItem item)
        {
            return item.GetAttribute<LinkAttribute>().LinkText;
        }

        public static string GetHref(this SubMenuItem item)
        {
            return UiConfiguration.GetEnvironmentUri("HcaEnvironment").AddPostfix(item.GetAttribute<LinkAttribute>().Href)
                .AbsoluteUri;
        }
    }
}