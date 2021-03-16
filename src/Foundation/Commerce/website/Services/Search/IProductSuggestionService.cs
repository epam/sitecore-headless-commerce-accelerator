namespace HCA.Foundation.Commerce.Services.Search
{
    using Base.Models.Result;

    using Models.Entities.Search;

    /// <summary>
    /// Performs products suggestion
    /// </summary>
    public interface IProductSuggestionService
    {
        /// <summary>
        /// Suggest products using searching options
        /// </summary>
        /// <returns>Product suggested results</returns>
        Result<ProductSuggestionResults> GetSuggestedProducts(string search);
    }
}