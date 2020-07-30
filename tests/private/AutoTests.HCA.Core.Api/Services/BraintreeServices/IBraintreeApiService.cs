using AutoTests.HCA.Core.API.Models.Braitree.PaymentToken.Request;
using AutoTests.HCA.Core.API.Models.Braitree.PaymentToken.Response;
using AutoTests.HCA.Core.API.Models.Braitree.RequestResult;

namespace AutoTests.HCA.Core.API.Services.BraintreeServices
{
    public interface IBraintreeApiService
    {
        BraintreeResponse<PaymentResult> GetPaymentToken(CreditCardRequest creditCard);
    }
}