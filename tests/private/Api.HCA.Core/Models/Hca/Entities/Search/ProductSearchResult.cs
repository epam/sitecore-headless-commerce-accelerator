using System.Collections.Generic;
using Api.HCA.Core.Models.Hca.Entities.Catalog;

namespace Api.HCA.Core.Models.Hca.Entities.Search
{
    public class ProductSearchResult
    {
        public IList<Product> Products { get; set; }

        public IList<Facet> Facets { get; set; }

        public int TotalItemCount { get; set; }

        public int TotalPageCount { get; set; }
    }
}