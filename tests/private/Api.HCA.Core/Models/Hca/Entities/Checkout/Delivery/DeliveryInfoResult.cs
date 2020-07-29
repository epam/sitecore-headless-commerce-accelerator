using System.Collections.Generic;
using Api.HCA.Core.Models.Hca.Entities.Checkout.Shipping;

namespace Api.HCA.Core.Models.Hca.Entities.Checkout.Delivery
{
    public class DeliveryInfoResult : BaseCheckoutInfo
    {
        public string NewPartyId { get; set; }

        public IList<ShippingOption> ShippingOptions { get; set; }
    }
}