using Newtonsoft.Json;

namespace AutoTests.HCA.Core.API.Models.Braitree.PaymentToken.Response
{
    public class TokenizeCreditCard
    {
        [JsonProperty(PropertyName = "creditCard")]
        public CreditCardInfo CreditCard { get; set; }

        [JsonProperty(PropertyName = "token")] public string Token { get; set; }
    }
}