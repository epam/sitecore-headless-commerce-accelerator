using System.Collections.Generic;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Addresses;

namespace AutoTests.HCA.Core.API.Models.Hca.Entities.Checkout
{
    public class BaseCheckoutInfo
    {
        public IList<Address> UserAddresses { get; set; }
    }
}