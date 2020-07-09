using HCA.Api.Core.Models.Hca.Entities.Addresses;
using System.Collections.Generic;

namespace HCA.Api.Core.Models.Hca.Entities.Checkout.Shipping
{
    public class SetShippingOptionsRequest
    {
        public string OrderShippingPreferenceType { get; set; }

        public IList<Address> ShippingAddresses { get; set; }

        public IList<ShippingMethod> ShippingMethods { get; set; }
    }
}
