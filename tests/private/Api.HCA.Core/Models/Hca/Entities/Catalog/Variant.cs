using System.Collections.Generic;

namespace Api.HCA.Core.Models.Hca.Entities.Catalog
{
    public class Variant : BaseProduct
    {
        public string VariantId { get; set; }

        public IDictionary<string, string> Properties { get; set; }
    }
}