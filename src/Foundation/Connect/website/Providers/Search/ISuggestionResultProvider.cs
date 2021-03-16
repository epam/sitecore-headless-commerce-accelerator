namespace HCA.Foundation.Connect.Providers.Search
{
    using Sitecore.ContentSearch.SolrNetExtension;
    /// <summary>
    /// Provides solr suggestions
    /// </summary>
    public interface ISuggestionResultProvider
    {
        /// <summary>
        /// Gets suggest result
        /// </summary>
        /// <param name="options">Suggest query options</param>
        /// <returns>Solr suggestion result</returns>
        SuggestResult GetSuggestionResults(SuggestHandlerQueryOptions options);
    }
}
