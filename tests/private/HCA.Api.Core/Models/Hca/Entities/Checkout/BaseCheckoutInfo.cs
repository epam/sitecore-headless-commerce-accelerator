using HCA.Api.Core.Models.Hca.Entities.Addresses;
using System.Collections.Generic;

namespace HCA.Api.Core.Models.Hca.Entities.Checkout
{
    public class BaseCheckoutInfo
    {
        public IList<Address> UserAddresses { get; set; }
    }
}
