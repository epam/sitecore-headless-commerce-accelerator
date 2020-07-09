using HCA.Api.Core.Models.Hca.Entities.Checkout.Shipping;
using System.Collections.Generic;

namespace HCA.Api.Core.Models.Hca.Entities.Checkout.Delivery
{
    public class DeliveryInfoResult : BaseCheckoutInfo
    {
        public string NewPartyId { get; set; }

        public IList<ShippingOption> ShippingOptions { get; set; }
    }
}
