using HCA.Foundation.Commerce.Models.Entities.Addresses;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TypeLite;

namespace HCA.Foundation.Commerce.Models.Entities.Shipping
{
    [ExcludeFromCodeCoverage]
    [TsClass]
    public class ShippingData
    {
        public string ShippingPreferenceType { get; set; }
        public IEnumerable<Address> ShippingAddresses { get; set; }
        public IEnumerable<ShippingMethod> ShippingMethods { get; set; }
    }
}