using Newtonsoft.Json;

namespace HCA.Api.Core.Models.Braitree.PaymentToken.Request
{
    public class Variables
    {
        [JsonProperty(PropertyName = "input")]
        public Input Input { get; set; }
    }
}
