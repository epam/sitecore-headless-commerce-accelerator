using Newtonsoft.Json;

namespace AutoTests.HCA.Core.API.BraintreeApi.Models.PaymentToken.Response
{
    public class CreditCardInfo
    {
        [JsonProperty(PropertyName = "bin")] public int Bin { get; set; }

        [JsonProperty(PropertyName = "binData")]
        public BinData BinData { get; set; }

        [JsonProperty(PropertyName = "brainCode")]
        public string BrainCode { get; set; }

        [JsonProperty(PropertyName = "last4")] public int Last4 { get; set; }
    }
}