using HCA.Api.Core.Models.Braitree.Token.Response;

namespace HCA.Api.Core.Models.Braitree.PaymentToken.Response
{
    public class PaymentResult
    {
        public PaymentData Data { get; set; }

        public Extensions Extensions { get; set; }
    }
}
