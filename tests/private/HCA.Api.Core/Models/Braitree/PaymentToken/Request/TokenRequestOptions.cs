using Newtonsoft.Json;

namespace HCA.Api.Core.Models.Braitree.PaymentToken.Request
{
    public class TokenRequestOptions
    {
        [JsonProperty(PropertyName = "validate")]
        public bool Validate { get; set; }
    }
}
