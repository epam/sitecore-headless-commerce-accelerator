using System.Collections.Generic;

namespace AutoTests.HCA.Core.API.HcaApi.Models.Entities.Checkout.Shipping
{
    public class ShippingInfoResult
    {
        public IEnumerable<ShippingMethod> ShippingMethods { get; set; }
    }
}