using System.Collections.Generic;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Catalog;

namespace AutoTests.HCA.Core.API.Models.Hca.Entities.Search
{
    public class ProductSearchResult
    {
        public IList<Product> Products { get; set; }

        public IList<Facet> Facets { get; set; }

        public int TotalItemCount { get; set; }

        public int TotalPageCount { get; set; }
    }
}