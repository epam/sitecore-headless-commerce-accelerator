﻿using Newtonsoft.Json;

namespace AutoTests.HCA.Core.API.BraintreeApi.Models.PaymentToken.Response
{
    public class TokenizeCreditCard
    {
        [JsonProperty(PropertyName = "creditCard")]
        public CreditCardInfo CreditCard { get; set; }

        [JsonProperty(PropertyName = "token")] public string Token { get; set; }
    }
}