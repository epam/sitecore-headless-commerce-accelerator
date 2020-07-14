using HCA.Api.Core.Models.Braitree.PaymentToken.Request;
using HCA.Api.Core.Models.Braitree.PaymentToken.Response;
using HCA.Api.Core.Models.Braitree.RequestResult;
using HCA.Api.Core.Services.RestService;
using RestSharp;
using System;
using System.Collections.Generic;
using UIAutomationFramework;

namespace HCA.Api.Core.Services.BraintreeServices
{
    public class BraintreeApiService : IBraintreeApiService
    {
        protected readonly Uri BaseUri;
        private readonly IHttpClientService _httpClientService;
        private readonly IJwtTokenService _jwtTokenService;

        public BraintreeApiService()
        {
            //TODO delete using configuration
            BaseUri = new Uri(Configuration.GetBraintreeSetting().GraphQLSandboxUrl);
            _jwtTokenService = new JwtTokenService();
            _httpClientService = new HttpClientService(BaseUri);
            InitClientService();
        }

        public BraintreeResponse<PaymentResult> GetPaymentToken(CreditCardRequest creditCard) =>
            _httpClientService.ExecuteJsonRequest<BraintreeResponse<PaymentResult>, PaymentResult, object>(
                BaseUri, Method.POST, ConfigurePaymentRequest(creditCard));

        private void InitClientService()
        {
            var dictionary = new Dictionary<string, string>
            {
                { "braintree-version", Configuration.GetBraintreeSetting().Version},
                { "authorization", _jwtTokenService.GetJwtToken()}
            };
            _httpClientService.AddDefaultHeaders(dictionary);
            _httpClientService.SetTimeOut(1000);
        }

        private PaymentRequest ConfigurePaymentRequest(CreditCardRequest creditCard) =>
            new PaymentRequest
            {
                ClientSdkMetaData = new ClientSdkMetaData
                {
                    Integration = "custom",
                    SessionId = "866d30d0-7342-4373-91e9-c3c57a823d67",
                    Source = "client"
                },
                OperationName = "TokenizeCreditCard",
                Query = "mutation TokenizeCreditCard($input:TokenizeCreditCardInput!){tokenizeCreditCard(input:$input){token creditCard{bin brandCode last4 binData{prepaid healthcare debit durbinRegulated commercial payroll issuingBank countryOfIssuance productId}}}}",
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
