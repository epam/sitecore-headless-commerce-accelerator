namespace Wooli.Foundation.Connect.Models.Search
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class SearchSettings
    {
        public IEnumerable<string> SortFieldNames { get; set; }

        public IEnumerable<Facet> Facets { get; set; }

        public int ItemsPerPage { get; set; }
    }
}