using Newtonsoft.Json;

namespace AutoTests.HCA.Core.API.Models.Braitree.PaymentToken.Request
{
    public class TokenRequestOptions
    {
        [JsonProperty(PropertyName = "validate")]
        public bool Validate { get; set; }
    }
}