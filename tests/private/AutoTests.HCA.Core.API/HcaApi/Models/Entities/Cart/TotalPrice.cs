namespace AutoTests.HCA.Core.API.HcaApi.Models.Entities.Cart
{
    public class TotalPrice
    {
        public string CurrencyCode { get; set; }

        public string CurrencySymbol { get; set; }

        public decimal? Total { get; set; }

        public decimal? Subtotal { get; set; }

        public decimal? HandlingTotal { get; set; }

        public decimal? ShippingTotal { get; set; }

        public decimal TaxTotal { get; set; }

        public decimal? TotalSavings { get; set; }
    }
}