using System.Collections.Generic;
using Api.HCA.Core.Models.Hca.Entities.Addresses;

namespace Api.HCA.Core.Models.Hca.Entities.Checkout.Shipping
{
    public class SetShippingOptionsRequest
    {
        public string OrderShippingPreferenceType { get; set; }

        public IList<Address> ShippingAddresses { get; set; }

        public IList<ShippingMethod> ShippingMethods { get; set; }
    }
}