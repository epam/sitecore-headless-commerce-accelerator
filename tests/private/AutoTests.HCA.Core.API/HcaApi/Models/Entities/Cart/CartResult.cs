using System.Collections.Generic;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Addresses;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Checkout.Payment;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Checkout.Shipping;

namespace AutoTests.HCA.Core.API.HcaApi.Models.Entities.Cart
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