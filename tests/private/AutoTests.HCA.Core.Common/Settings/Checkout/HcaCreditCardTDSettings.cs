namespace AutoTests.HCA.Core.Common.Settings.Checkout
{
    public class HcaCreditCardTDSettings : BaseHcaEntityTestsDataSettings
    {
        public string Number { get; set; }

        public string ExpirationMonth { get; set; }

        public string ExpirationYear { get; set; }

        public string Cvv { get; set; }
    }
}
