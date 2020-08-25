using AutoTests.HCA.Core.API.Models.Hca.Entities.Addresses;

namespace AutoTests.HCA.Core.API.Models.Hca.Entities.Checkout.Payment
{
    public class SetPaymentInfoRequest
    {
        public Address BillingAddress { get; set; }

        public FederatedPaymentInfo FederatedPayment { get; set; }
    }
}