using System.Collections.Generic;

namespace AutoTests.HCA.Core.API.HcaApi.Models.Entities.Catalog
{
    public class Variant : BaseProduct
    {
        public string VariantId { get; set; }

        public IDictionary<string, string> Properties { get; set; }
    }
}