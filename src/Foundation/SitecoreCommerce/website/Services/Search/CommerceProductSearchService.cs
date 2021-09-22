namespace HCA.Foundation.SitecoreCommerce.Services.Search
{
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models.Result;

    using Commerce.Converters.Search;
    using Commerce.Models.Entities.Catalog;
    using Commerce.Models.Entities.Search;
    using Commerce.Services.Search;

    using Connect.Converters.Products;

    using Foundation.Search.Models.Common;
    using Foundation.Search.Models.Entities.Category;
    using Foundation.Search.Services.Category;
    using Mappers.Search;

    using Product;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    
    public class CommerceProductSearchService : IProductSearchService
    {
        private readonly ICategorySearchService categorySearchService;
        private readonly ICommerceProductSearchService productSearchService;
        private readonly ISearchOptionsConverter searchOptionsConverter;
        private readonly ISearchMapper searchMapper;
        private readonly IProductConverter<Item> productConverter;

        public CommerceProductSearchService(
            ISearchOptionsConverter searchOptionsConverter,
            ICommerceProductSearchService productSearchService,
            ICategorySearchService categorySearchService,
            ISearchMapper searchMapper, 
            IProductConverter<Item> productConverter)
        {
            Assert.ArgumentNotNull(searchOptionsConverter, nameof(searchOptionsConverter));
            Assert.ArgumentNotNull(productSearchService, nameof(productSearchService));
            Assert.ArgumentNotNull(categorySearchService, nameof(categorySearchService));
            Assert.ArgumentNotNull(searchMapper, nameof(searchMapper));
            Assert.ArgumentNotNull(searchMapper, nameof(searchMapper));

            this.searchOptionsConverter = searchOptionsConverter;
            this.productSearchService = productSearchService;
            this.categorySearchService = categorySearchService;
            this.searchMapper = searchMapper;
            this.productConverter = productConverter;
        }
        
        public Result<ProductSearchResults> GetProducts(ProductSearchOptions productSearchOptions)
        {
            Assert.ArgumentNotNull(productSearchOptions, nameof(productSearchOptions));

            var searchOptions = this.searchOptionsConverter.Convert(productSearchOptions);
            var searchResults = this.productSearchService.GetSearchResults(searchOptions);

            var products = this.productConverter.Convert(searchResults.Results, true);

            var results = this.searchMapper.Map<SearchResults<Item>, ProductSearchResults>(searchResults);
            results.Products = this.searchMapper.Map<IEnumerable<Connect.Models.Catalog.Product>, IEnumerable<Product>>(products).ToList();

            return new Result<ProductSearchResults>(results);
        }
        
        public Result<Category> GetCategoryByName(string categoryName)
        {
            Assert.ArgumentNotNullOrEmpty(categoryName, nameof(categoryName));

            var searchResults = this.categorySearchService.GetSearchResults(
                new CategorySearchOptions
                {
                    CategoryName = categoryName,
                    NumberOfItemsToReturn = 1
                });

            return new Result<Category>(
                this.searchMapper.Map<CategorySearchResultItem, Category>(searchResults?.Results?.FirstOrDefault()));
        }
    }
}