using Newtonsoft.Json;

namespace AutoTests.HCA.Core.API.Models.Braitree.PaymentToken.Request
{
    public class Variables
    {
        [JsonProperty(PropertyName = "input")] public Input Input { get; set; }
    }
}