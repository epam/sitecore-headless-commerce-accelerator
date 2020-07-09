using HCA.Api.Core.Models.Hca.Entities.Addresses;

namespace HCA.Api.Core.Models.Hca.Entities.Checkout.Payment
{
    public class SetPaymentInfoRequest
    {
        public Address BillingAddress { get; set; }

        public FederatedPaymentInfo FederatedPayment { get; set; }
    }
}
