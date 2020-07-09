using System.Collections.Generic;

namespace HCA.Api.Core.Models.Hca.Entities.Search
{
    public class Facet
    {
        public string DisplayName { get; set; }

        public IList<FacetValue> FoundValues { get; set; }

        public IList<object> Values { get; set; }

        public string Name { get; set; }
    }
}
