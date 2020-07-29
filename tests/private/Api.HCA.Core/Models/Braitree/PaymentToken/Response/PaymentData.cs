using Newtonsoft.Json;

namespace Api.HCA.Core.Models.Braitree.PaymentToken.Response
{
    public class PaymentData
    {
        [JsonProperty(PropertyName = "tokenizeCreditCard")]
        public TokenizeCreditCard TokenizeCreditCard { get; set; }
    }
}