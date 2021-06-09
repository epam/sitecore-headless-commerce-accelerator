namespace HCA.Foundation.Commerce.Providers.Search
{
    using Foundation.Search.Models.Common;

    using Models.Entities.Search;

    /// <summary>
    /// Provides product suggestions
    /// </summary>
    public interface IProductSuggestionProvider
    {
        /// <summary>
        /// Gets product suggestion result
        /// </summary>
        /// <param name="suggestionResult">Solr suggestion result</param>
        /// <returns>Product suggestion result</returns>
        ProductSuggestionResults GetProductSuggestion(SuggestionResult suggestionResult);
    }
}
