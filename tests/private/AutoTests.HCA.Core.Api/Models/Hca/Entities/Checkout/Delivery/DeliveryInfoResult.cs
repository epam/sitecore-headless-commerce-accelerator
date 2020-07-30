using System.Collections.Generic;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Checkout.Shipping;

namespace AutoTests.HCA.Core.API.Models.Hca.Entities.Checkout.Delivery
{
    public class DeliveryInfoResult : BaseCheckoutInfo
    {
        public string NewPartyId { get; set; }

        public IList<ShippingOption> ShippingOptions { get; set; }
    }
}