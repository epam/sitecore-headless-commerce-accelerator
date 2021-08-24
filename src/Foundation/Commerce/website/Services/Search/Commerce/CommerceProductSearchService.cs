namespace HCA.Foundation.Commerce.Services.Search.Commerce
{
    using System.Linq;

    using Base.Models.Result;

    using Builders.Search;

    using Foundation.Search.Models.Entities.Category;
    using Foundation.Search.Services.Category;

    using Models.Entities.Search;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Search = Foundation.Search.Services.Product;

    public class CommerceProductSearchService : IProductSearchService
    {
        private readonly ICategorySearchService categorySearchService;
        private readonly Search.IProductSearchService productSearchService;

        private readonly ISearchOptionsConverter searchOptionsConverter;

        private readonly ISearchResultsConverter searchResultsConverter;

        public CommerceProductSearchService(
            ISearchOptionsConverter searchOptionsConverter,
            Foundation.Search.Services.Product.IProductSearchService productSearchService,
            ICategorySearchService categorySearchService,
            ISearchResultsConverter searchResultsConverter)
        {
            Assert.ArgumentNotNull(searchOptionsConverter, nameof(searchOptionsConverter));
            Assert.ArgumentNotNull(productSearchService, nameof(productSearchService));
            Assert.ArgumentNotNull(categorySearchService, nameof(categorySearchService));
            Assert.ArgumentNotNull(searchResultsConverter, nameof(searchResultsConverter));

            this.searchOptionsConverter = searchOptionsConverter;
            this.productSearchService = productSearchService;
            this.categorySearchService = categorySearchService;
            this.searchResultsConverter = searchResultsConverter;
        }

        public Result<ProductSearchResults> GetProducts(ProductSearchOptions productSearchOptions)
        {
            Assert.ArgumentNotNull(productSearchOptions, nameof(productSearchOptions));

            var searchOptions = this.searchOptionsConverter.Convert(productSearchOptions);
            var searchResults = this.productSearchService.GetSearchResults(searchOptions);

            return new Result<ProductSearchResults>(this.searchResultsConverter.Convert(searchResults));
        }

        public Item GetCategoryByName(string categoryName)
        {
            Assert.ArgumentNotNullOrEmpty(categoryName, nameof(categoryName));

            var searchResults = this.categorySearchService.GetSearchResults(
                new CategorySearchOptions
                {
                    CategoryName = categoryName
                });

            return searchResults?.Results?.FirstOrDefault()?.GetItem();
        }

        public Item GetProductByName(string productName)
        {
            Assert.ArgumentNotNullOrEmpty(productName, nameof(productName));

            var searchResults = this.productSearchService.GetProductItemByProductId(productName);

            return searchResults;
        }
    }
}