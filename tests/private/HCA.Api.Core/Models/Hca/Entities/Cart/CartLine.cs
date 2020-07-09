using HCA.Api.Core.Models.Hca.Entities.Catalog;

namespace HCA.Api.Core.Models.Hca.Entities.Cart
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
