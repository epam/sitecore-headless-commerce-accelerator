namespace HCA.Foundation.Commerce.Services.Search.Commerce
{
    using System.Collections.Generic;
    using System.Linq;

    using Connect.Builders.Products;

    using Foundation.Search.Models.Common;

    using Mappers.Search;

    using Models.Entities.Catalog;
    using Models.Entities.Search;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    public class CommerceSearchResultsConverter : ISearchResultsConverter
    {
        private readonly ISearchMapper searchMapper;
        private readonly IProductConverter<Item> productConverter;

        public CommerceSearchResultsConverter(ISearchMapper searchMapper, IProductConverter<Item> productConverter)
        {
            Assert.ArgumentNotNull(searchMapper, nameof(searchMapper));
            Assert.ArgumentNotNull(searchMapper, nameof(searchMapper));

            this.searchMapper = searchMapper;
            this.productConverter = productConverter;
        }

        public ProductSearchResults Convert(SearchResults<Item> searchResults)
        {
            var results = this.searchMapper.Map<SearchResults<Item>, ProductSearchResults>(searchResults);

            results.Products = this.searchMapper.Map<List<Connect.Models.Catalog.Product>, List<Product>>(
                this.productConverter.Convert(searchResults.Results, true).ToList());

            return results;
        }
    }
}