using System.Collections.Generic;

namespace HCA.Api.Core.Models.Hca.Entities.Catalog
{
    public class Variant : BaseProduct
    {
        public string VariantId { get; set; }

        public IDictionary<string, string> Properties { get; set; }
    }
}
