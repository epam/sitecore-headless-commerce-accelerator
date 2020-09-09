using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Cart;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Catalog;

namespace AutoTests.HCA.Core.API.HcaApi.Models.Entities.WishList
{
    public class WishListLine
    {
        public string Id { get; set; }

        public decimal Quantity { get; set; }

        public Product Product { get; set; }

        public TotalPrice Total { get; set; }
    }
}