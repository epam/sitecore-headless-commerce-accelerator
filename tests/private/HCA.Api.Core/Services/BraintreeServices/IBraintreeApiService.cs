using HCA.Api.Core.Models.Braitree.PaymentToken.Request;
using HCA.Api.Core.Models.Braitree.PaymentToken.Response;
using HCA.Api.Core.Models.Braitree.RequestResult;

namespace HCA.Api.Core.Services.BraintreeServices
{
    public interface IBraintreeApiService
    {
        BraintreeResponse<PaymentResult> GetPaymentToken(CreditCardRequest creditCard);
    }
}
