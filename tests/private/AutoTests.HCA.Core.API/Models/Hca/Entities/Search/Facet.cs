using System.Collections.Generic;

namespace AutoTests.HCA.Core.API.Models.Hca.Entities.Search
{
    public class Facet
    {
        public string DisplayName { get; set; }

        public IList<FacetValue> FoundValues { get; set; }

        public IList<string> Values { get; set; }

        public string Name { get; set; }
    }
}