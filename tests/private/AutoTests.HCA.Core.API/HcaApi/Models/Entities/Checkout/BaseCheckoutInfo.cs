using System.Collections.Generic;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Addresses;

namespace AutoTests.HCA.Core.API.HcaApi.Models.Entities.Checkout
{
    public class BaseCheckoutInfo
    {
        public IList<Address> UserAddresses { get; set; }
    }
}