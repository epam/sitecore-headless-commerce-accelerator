using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Addresses;

namespace AutoTests.HCA.Core.API.HcaApi.Models.Entities.Checkout.Payment
{
    public class SetPaymentInfoRequest
    {
        public Address BillingAddress { get; set; }

        public FederatedPaymentInfo FederatedPayment { get; set; }
    }
}