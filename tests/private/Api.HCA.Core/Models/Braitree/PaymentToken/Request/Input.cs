using Newtonsoft.Json;

namespace Api.HCA.Core.Models.Braitree.PaymentToken.Request
{
    public class Input
    {
        [JsonProperty(PropertyName = "creditCard")]
        public CreditCardRequest CreditCard { get; set; }

        [JsonProperty(PropertyName = "options")]
        public TokenRequestOptions Options { get; set; }
    }
}