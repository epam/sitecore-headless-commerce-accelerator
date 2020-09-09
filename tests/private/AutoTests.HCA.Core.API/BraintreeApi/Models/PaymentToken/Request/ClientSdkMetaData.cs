using Newtonsoft.Json;

namespace AutoTests.HCA.Core.API.BraintreeApi.Models.PaymentToken.Request
{
    public class ClientSdkMetaData
    {
        [JsonProperty(PropertyName = "integration")]
        public string Integration { get; set; }

        [JsonProperty(PropertyName = "sessionId")]
        public string SessionId { get; set; }

        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }
    }
}