namespace AutoTests.HCA.Core.API.Models.Hca.Entities.Checkout.Payment
{
    public class FederatedPaymentInfo
    {
        public string CardToken { get; set; }

        public string PartyId { get; set; }

        public string PaymentMethodId { get; set; }
    }
}