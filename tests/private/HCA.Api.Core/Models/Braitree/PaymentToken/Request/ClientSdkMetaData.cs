using Newtonsoft.Json;

namespace HCA.Api.Core.Models.Braitree.PaymentToken.Request
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
