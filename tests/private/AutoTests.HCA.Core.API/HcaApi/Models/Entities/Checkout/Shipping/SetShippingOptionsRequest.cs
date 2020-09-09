using System.Collections.Generic;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Addresses;

namespace AutoTests.HCA.Core.API.HcaApi.Models.Entities.Checkout.Shipping
{
    public class SetShippingOptionsRequest
    {
        public string OrderShippingPreferenceType { get; set; }

        public IList<Address> ShippingAddresses { get; set; }

        public IList<ShippingMethod> ShippingMethods { get; set; }
    }
}