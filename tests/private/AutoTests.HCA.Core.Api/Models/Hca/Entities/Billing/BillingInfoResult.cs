using System.Collections.Generic;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Checkout.Payment;

namespace AutoTests.HCA.Core.API.Models.Hca.Entities.Billing
{
    public class BillingInfoResult
    {
        public string PaymentClientToken { get; set; }

        public IList<PaymentMethod> PaymentMethods { get; set; }

        public IList<PaymentOption> PaymentOptions { get; set; }
    }
}