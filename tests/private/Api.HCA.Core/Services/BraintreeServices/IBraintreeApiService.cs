using Api.HCA.Core.Models.Braitree.PaymentToken.Request;
using Api.HCA.Core.Models.Braitree.PaymentToken.Response;
using Api.HCA.Core.Models.Braitree.RequestResult;

namespace Api.HCA.Core.Services.BraintreeServices
{
    public interface IBraintreeApiService
    {
        BraintreeResponse<PaymentResult> GetPaymentToken(CreditCardRequest creditCard);
    }
}