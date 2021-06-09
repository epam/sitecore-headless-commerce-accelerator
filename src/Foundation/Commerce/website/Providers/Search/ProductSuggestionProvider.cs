namespace HCA.Foundation.Commerce.Providers.Search
{
    using System.Linq;
    
    using DependencyInjection;

    using Foundation.Search.Models.Common;

    using Mappers.Catalog;

    using Models.Entities.Catalog;
    using Models.Entities.Search;

    using Services.Catalog;

    using Sitecore.Diagnostics;

    [Service(typeof(IProductSuggestionProvider), Lifetime = Lifetime.Singleton)]
    public class ProductSuggestionProvider : IProductSuggestionProvider
    {
        private readonly ICatalogMapper catalogMapper;
        private readonly ICatalogService catalogService;

        public ProductSuggestionProvider(ICatalogMapper catalogMapper, ICatalogService catalogService)
        {
            Assert.ArgumentNotNull(catalogMapper, nameof(catalogMapper));
            Assert.ArgumentNotNull(catalogService, nameof(catalogService));

            this.catalogMapper = catalogMapper;
            this.catalogService = catalogService;
        }

        public ProductSuggestionResults GetProductSuggestion(SuggestionResult suggestionResults)
        {
            var productIds = suggestionResults.Suggestions.Select(res => res.Payload)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct();

            var productSuggestionResults = new ProductSuggestionResults
            {
                Products = productIds.Select(
                        id => this.catalogMapper.Map<Product, ProductSuggestion>(
                            this.catalogService.GetProduct(id).Data))
                    .ToList()
            };

            return productSuggestionResults;
        }
    }
}