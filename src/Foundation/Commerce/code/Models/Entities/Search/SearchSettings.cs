namespace Wooli.Foundation.Commerce.Models.Entities.Search
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using TypeLite;

    [ExcludeFromCodeCoverage]
    [TsClass]
    public class SearchSettings
    {
        public IEnumerable<string> SortFieldNames { get; set; }

        public IEnumerable<Facet> Facets { get; set; }

        public int ItemsPerPage { get; set; }
    }
}