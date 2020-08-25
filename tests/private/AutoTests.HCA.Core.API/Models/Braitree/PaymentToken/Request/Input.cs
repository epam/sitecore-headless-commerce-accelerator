using Newtonsoft.Json;

namespace AutoTests.HCA.Core.API.Models.Braitree.PaymentToken.Request
{
    public class Input
    {
        [JsonProperty(PropertyName = "creditCard")]
        public CreditCardRequest CreditCard { get; set; }

        [JsonProperty(PropertyName = "options")]
        public TokenRequestOptions Options { get; set; }
    }
}