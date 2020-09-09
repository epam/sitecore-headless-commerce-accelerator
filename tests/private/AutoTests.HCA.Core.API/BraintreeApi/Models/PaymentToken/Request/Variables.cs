using Newtonsoft.Json;

namespace AutoTests.HCA.Core.API.BraintreeApi.Models.PaymentToken.Request
{
    public class Variables
    {
        [JsonProperty(PropertyName = "input")] public Input Input { get; set; }
    }
}