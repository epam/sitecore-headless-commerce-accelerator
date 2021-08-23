namespace HCA.Foundation.Commerce.Services.Search
{
    using Foundation.Search.Models.Common;

    using Models.Entities.Search;

    using Sitecore.Data.Items;

    /// <summary>
    /// Converts search results to products search results
    /// </summary>
    public interface ISearchResultsConverter
    {
        /// <summary>
        /// Converts search results to products search results 
        /// </summary>
        /// <param name="searchResults">Search results</param>
        /// <returns>Products search results</returns>
        ProductSearchResults Convert(SearchResults<Item> searchResults);
    }
}