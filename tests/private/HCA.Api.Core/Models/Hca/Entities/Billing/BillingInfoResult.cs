using HCA.Api.Core.Models.Hca.Entities.Checkout.Payment;
using System.Collections.Generic;

namespace HCA.Api.Core.Models.Hca.Entities.Billing
{
    public class BillingInfoResult
    {
        public string PaymentClientToken { get; set; }

        public IList<PaymentMethod> PaymentMethods { get; set; }

        public IList<PaymentOption> PaymentOptions { get; set; }
    }
}
