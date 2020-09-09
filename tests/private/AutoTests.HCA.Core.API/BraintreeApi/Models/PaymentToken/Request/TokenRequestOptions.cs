using Newtonsoft.Json;

namespace AutoTests.HCA.Core.API.BraintreeApi.Models.PaymentToken.Request
{
    public class TokenRequestOptions
    {
        [JsonProperty(PropertyName = "validate")]
        public bool Validate { get; set; }
    }
}