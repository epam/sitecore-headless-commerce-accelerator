using Ui.HCA.Pages.ConstantsAndEnums;

namespace Ui.HCA.Pages.Pages
{
    public class PhonePage : ShopPage
    {
        private static PhonePage _phonePage;

        public static PhonePage Instance => _phonePage ??= new PhonePage();

        public override string GetPath() =>
            PagePrefix.PhoneShop.GetPrefix();
    }
}