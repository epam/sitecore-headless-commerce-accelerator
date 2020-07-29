using System.Collections.Generic;
using Api.HCA.Core.Models.Hca.Entities.Addresses;
using Api.HCA.Core.Models.Hca.Entities.Checkout.Payment;
using Api.HCA.Core.Models.Hca.Entities.Checkout.Shipping;

namespace Api.HCA.Core.Models.Hca.Entities.Cart
{
    public class CartResult
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public TotalPrice Price { get; set; }

        public IList<CartLine> CartLines { get; set; }

        public IList<Address> Addresses { get; set; }

        public IList<string> Adjustments { get; set; }

        public IList<ShippingMethod> Shipping { get; set; }

        public IList<FederatedPaymentInfo> Payment { get; set; }
    }
}