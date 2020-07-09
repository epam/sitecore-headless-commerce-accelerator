using System.Collections.Generic;

namespace HCA.Api.Core.Models.Hca.Entities.Catalog
{
    public class Product : BaseProduct
    {
        public string SitecoreId { get; set; }

        public IList<Variant> Variants { get; set; }
    }
}
