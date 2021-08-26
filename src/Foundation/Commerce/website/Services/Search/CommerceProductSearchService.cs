namespace HCA.Foundation.Commerce.Services.Search
{
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models.Result;

    using Connect.Converters.Products;

    using Converters.Search;

    using Foundation.Search.Models.Common;
    using Foundation.Search.Models.Entities.Category;
    using Foundation.Search.Services.Category;
    using Foundation.Search.Services.Product;

    using Mappers.Search;

    using Models.Entities.Catalog;
    using Models.Entities.Search;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    using ProductSearchOptions = Models.Entities.Search.ProductSearchOptions;
    using Search = Foundation.Search.Models.Entities.Product;

    public class CommerceProductSearchService : IProductSearchService
    {
        private readonly ICommerceCategorySearchService commerceCategorySearchService;
        private readonly ICommerceProductSearchService productSearchService;
        private readonly ISearchOptionsConverter searchOptionsConverter;
        private readonly ISearchMapper searchMapper;
        private readonly IProductConverter<Item> productConverter;

        public CommerceProductSearchService(
            ISearchOptionsConverter searchOptionsConverter,
            ICommerceProductSearchService productSearchService,
            ICommerceCategorySearchService commerceCategorySearchService,
            ISearchMapper searchMapper, 
            IProductConverter<Item> productConverter)
        {
            Assert.ArgumentNotNull(searchOptionsConverter, nameof(searchOptionsConverter));
            Assert.ArgumentNotNull(productSearchService, nameof(productSearchService));
            Assert.ArgumentNotNull(commerceCategorySearchService, nameof(commerceCategorySearchService));
            Assert.ArgumentNotNull(searchMapper, nameof(searchMapper));
            Assert.ArgumentNotNull(searchMapper, nameof(searchMapper));

            this.searchOptionsConverter = searchOptionsConverter;
            this.productSearchService = productSearchService;
            this.commerceCategorySearchService = commerceCategorySearchService;
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

        public Item GetCategoryByName(string categoryName)
        {
            Assert.ArgumentNotNullOrEmpty(categoryName, nameof(categoryName));

            var searchResults = this.commerceCategorySearchService.GetSearchResults(
                new CategorySearchOptions
                {
                    CategoryName = categoryName,
                    NumberOfItemsToReturn = 1
                });

            return searchResults?.Results?.FirstOrDefault()?.GetItem();
        }

        public Item GetProductByName(string productName)
        {
            Assert.ArgumentNotNullOrEmpty(productName, nameof(productName));

            var searchResults = this.productSearchService.GetSearchResults(
                new Search.ProductSearchOptions
                {
                    SearchKeyword = productName,
                    NumberOfItemsToReturn = 1
                });

            return searchResults?.Results?.FirstOrDefault();
        }
    }
}