using System;
using System.Collections.Generic;
using AutoTests.AutomationFramework.API.Services.RestService;
using AutoTests.HCA.Core.API.BraintreeApi.Models.PaymentToken.Request;
using AutoTests.HCA.Core.API.BraintreeApi.Models.PaymentToken.Response;
using AutoTests.HCA.Core.API.BraintreeApi.Models.RequestResult;
using AutoTests.HCA.Core.API.BraintreeApi.Settings;
using RestSharp;

namespace AutoTests.HCA.Core.API.BraintreeApi.Services
{
    public class BraintreeApiService : IBraintreeApiService
    {
        private readonly BraintreeSettings _braintreeSettings;
        private readonly IHttpClientService _httpClientService;
        private readonly IJwtTokenService _jwtTokenService;

        public BraintreeApiService(BraintreeSettings braintreeSettings)
        {
            _braintreeSettings = braintreeSettings;
            _jwtTokenService = new JwtTokenService(braintreeSettings.Gateway);
            _httpClientService = new HttpClientService(new Uri(_braintreeSettings.GraphQlSandboxUrl));
            InitClientService();
        }

        public BraintreeResponse<PaymentResult> GetPaymentToken(CreditCardRequest creditCard)
        {
            return _httpClientService.ExecuteJsonRequest<BraintreeResponse<PaymentResult>, PaymentResult, object>(
                new Uri(_braintreeSettings.GraphQlSandboxUrl), Method.POST, ConfigurePaymentRequest(creditCard));
        }

        private void InitClientService()
        {
            var dictionary = new Dictionary<string, string>
            {
                {"braintree-version", _braintreeSettings.Version},
                {"authorization", _jwtTokenService.GetJwtToken()}
            };
            _httpClientService.AddDefaultHeaders(dictionary);
        }

        private PaymentRequest ConfigurePaymentRequest(CreditCardRequest creditCard)
        {
            return new PaymentRequest
            {
                ClientSdkMetaData = new ClientSdkMetaData
                {
                    Integration = "custom",
                    SessionId = "866d30d0-7342-4373-91e9-c3c57a823d67",
                    Source = "client"
                },
                OperationName = "TokenizeCreditCard",
                Query =
                    "mutation TokenizeCreditCard($input:TokenizeCreditCardInput!){tokenizeCreditCard(input:$input){token creditCard{bin brandCode last4 binData{prepaid healthcare debit durbinRegulated commercial payroll issuingBank countryOfIssuance productId}}}}",
                Variables = new Variables
                {
                    Input = new Input
                    {
                        CreditCard = creditCard,
                        Options = new TokenRequestOptions
                        {
                            Validate = true
                        }
                    }
                }
            };
        }
    }
}