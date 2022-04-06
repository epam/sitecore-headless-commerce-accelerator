using System;
using System.Collections.Generic;
using HCA.Foundation.Commerce.Models.Entities.Addresses;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using TypeLite;

namespace HCA.Foundation.Commerce.Models.Entities.Payment
{
    [ExcludeFromCodeCoverage]
    [TsClass]
    public class PaymentData
    {
        public FederatedPaymentInfo FederatedPaymentInfo { get; set;}
        public Address BillingAddress { get; set; }
    }
}