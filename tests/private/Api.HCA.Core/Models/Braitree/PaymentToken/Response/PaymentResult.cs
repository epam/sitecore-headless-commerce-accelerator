using HCA.Api.Core.Models.Braitree.Token.Response;

namespace Api.HCA.Core.Models.Braitree.PaymentToken.Response
{
    public class PaymentResult
    {
        public PaymentData Data { get; set; }

        public Extensions Extensions { get; set; }
    }
}