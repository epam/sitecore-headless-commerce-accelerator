using System.Collections.Generic;

namespace AutoTests.HCA.Core.API.Models.Hca.Entities.Checkout.Shipping
{
    public class ShippingMethod
    {
        public string Description { get; set; }

        public string ExternalId { get; set; }

        public string Name { get; set; }

        public IList<string> LineIds { get; set; }

        public string PartyId { get; set; }

        public string ShippingPreferenceType { get; set; }
    }
}