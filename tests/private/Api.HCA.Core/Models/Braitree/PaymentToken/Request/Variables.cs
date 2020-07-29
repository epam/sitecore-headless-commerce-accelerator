using Newtonsoft.Json;

namespace Api.HCA.Core.Models.Braitree.PaymentToken.Request
{
    public class Variables
    {
        [JsonProperty(PropertyName = "input")] 
        public Input Input { get; set; }
    }
}