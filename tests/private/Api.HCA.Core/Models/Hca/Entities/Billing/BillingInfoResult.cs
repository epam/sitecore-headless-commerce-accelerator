using System.Collections.Generic;
using Api.HCA.Core.Models.Hca.Entities.Checkout.Payment;

namespace Api.HCA.Core.Models.Hca.Entities.Billing
{
    public class BillingInfoResult
    {
        public string PaymentClientToken { get; set; }

        public IList<PaymentMethod> PaymentMethods { get; set; }

        public IList<PaymentOption> PaymentOptions { get; set; }
    }
}