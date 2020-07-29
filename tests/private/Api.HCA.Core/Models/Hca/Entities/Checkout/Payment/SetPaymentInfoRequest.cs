using Api.HCA.Core.Models.Hca.Entities.Addresses;

namespace Api.HCA.Core.Models.Hca.Entities.Checkout.Payment
{
    public class SetPaymentInfoRequest
    {
        public Address BillingAddress { get; set; }

        public FederatedPaymentInfo FederatedPayment { get; set; }
    }
}