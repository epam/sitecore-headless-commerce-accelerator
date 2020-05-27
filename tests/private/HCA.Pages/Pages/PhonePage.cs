namespace HCA.Pages.Pages
{
    public class PhonePage : ShopPage
    {
        private static PhonePage _phonePage;

        public static PhonePage Instance =>
            _phonePage ?? (_phonePage = new PhonePage());

        public override string GetPath()
        {
            return "/shop/Phones";
        }
    }
}