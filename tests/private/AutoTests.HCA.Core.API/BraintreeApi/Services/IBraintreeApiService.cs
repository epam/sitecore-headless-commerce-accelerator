using AutoTests.HCA.Core.API.BraintreeApi.Models.PaymentToken.Request;
using AutoTests.HCA.Core.API.BraintreeApi.Models.PaymentToken.Response;
using AutoTests.HCA.Core.API.BraintreeApi.Models.RequestResult;

namespace AutoTests.HCA.Core.API.BraintreeApi.Services
{
    public interface IBraintreeApiService
    {
        BraintreeResponse<PaymentResult> GetPaymentToken(CreditCardRequest creditCard);
    }
}