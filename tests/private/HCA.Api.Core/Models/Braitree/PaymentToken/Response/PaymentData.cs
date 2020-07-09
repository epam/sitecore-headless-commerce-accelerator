using Newtonsoft.Json;

namespace HCA.Api.Core.Models.Braitree.PaymentToken.Response
{
    public class PaymentData
    {
        [JsonProperty(PropertyName = "tokenizeCreditCard")]
        public TokenizeCreditCard TokenizeCreditCard { get; set; }
    }
}