using Newtonsoft.Json;

namespace Api.HCA.Core.Models.Braitree.PaymentToken.Response
{
    public class TokenizeCreditCard
    {
        [JsonProperty(PropertyName = "creditCard")]
        public CreditCardInfo CreditCard { get; set; }

        [JsonProperty(PropertyName = "token")] 
        public string Token { get; set; }
    }
}