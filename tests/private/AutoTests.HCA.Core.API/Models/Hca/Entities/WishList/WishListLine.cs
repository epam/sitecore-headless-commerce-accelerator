using AutoTests.HCA.Core.API.Models.Hca.Entities.Cart;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Catalog;

namespace AutoTests.HCA.Core.API.Models.Hca.Entities.WishList
{
    public class WishListLine
    {
        public string Id { get; set; }

        public decimal Quantity { get; set; }

        public Product Product { get; set; }

        public TotalPrice Total { get; set; }
    }
}