using AutoTests.HCA.Core.UI.ConstantsAndEnums;

namespace AutoTests.HCA.Core.UI.Pages
{
    public class PhonePage : ShopPage
    {
        private static PhonePage _phonePage;

        public static PhonePage Instance => _phonePage ??= new PhonePage();

        public override string GetPath()
        {
            return PagePrefix.PhoneShop.GetPrefix();
        }
    }
}