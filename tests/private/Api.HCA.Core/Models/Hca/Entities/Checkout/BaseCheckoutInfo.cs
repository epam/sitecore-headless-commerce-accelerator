using System.Collections.Generic;
using Api.HCA.Core.Models.Hca.Entities.Addresses;

namespace Api.HCA.Core.Models.Hca.Entities.Checkout
{
    public class BaseCheckoutInfo
    {
        public IList<Address> UserAddresses { get; set; }
    }
}