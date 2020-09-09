using Newtonsoft.Json;

namespace AutoTests.HCA.Core.API.BraintreeApi.Models.PaymentToken.Response
{
    public class PaymentData
    {
        [JsonProperty(PropertyName = "tokenizeCreditCard")]
        public TokenizeCreditCard TokenizeCreditCard { get; set; }
    }
}