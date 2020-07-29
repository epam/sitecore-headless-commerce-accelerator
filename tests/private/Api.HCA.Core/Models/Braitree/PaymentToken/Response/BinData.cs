using Newtonsoft.Json;

namespace Api.HCA.Core.Models.Braitree.PaymentToken.Response
{
    public class BinData
    {
        [JsonProperty(PropertyName = "commercial")]
        public string Commercial { get; set; }

        [JsonProperty(PropertyName = "countryOfIssuance")]
        public string CountryOfIssuance { get; set; }

        [JsonProperty(PropertyName = "debit")] 
        public string Debit { get; set; }

        [JsonProperty(PropertyName = "durbinRegulated")]
        public string DurbinRegulated { get; set; }

        [JsonProperty(PropertyName = "issuingBank")]
        public string IssuingBank { get; set; }

        [JsonProperty(PropertyName = "payRoll")]
        public string PayRoll { get; set; }

        [JsonProperty(PropertyName = "prepaid")]
        public string Prepaid { get; set; }

        [JsonProperty(PropertyName = "productId")]
        public string ProductId { get; set; }
    }
}