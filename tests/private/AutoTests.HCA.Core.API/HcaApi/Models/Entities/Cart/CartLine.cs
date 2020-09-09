using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Catalog;

namespace AutoTests.HCA.Core.API.HcaApi.Models.Entities.Cart
{
    public class CartLine
    {
        public string Id { get; set; }

        public Product Product { get; set; }

        public Variant Variant { get; set; }

        public decimal Quantity { get; set; }

        public TotalPrice Price { get; set; }
    }
}