using Newtonsoft.Json;

namespace Api.HCA.Core.Models.Braitree.PaymentToken.Request
{
    public class TokenRequestOptions
    {
        [JsonProperty(PropertyName = "validate")]
        public bool Validate { get; set; }
    }
}