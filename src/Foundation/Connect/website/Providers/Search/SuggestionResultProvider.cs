namespace HCA.Foundation.Connect.Providers.Search
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SolrNetExtension;
    using Sitecore.ContentSearch.SolrProvider.SolrNetIntegration;
    using Sitecore.Diagnostics;

    public abstract class SuggestionResultProvider : ISuggestionResultProvider
    {
        protected abstract ISearchIndex SearchIndex { get; set; }

        public SuggestResult GetSuggestionResults(SuggestHandlerQueryOptions options)
        {
            Assert.ArgumentNotNull(options, nameof(options));

            using (var searchContext = this.SearchIndex?.CreateSearchContext())
            {
                var suggestResult = searchContext?.Suggest(options.Parameters.Query, options)?.Suggestions[options.Parameters.Dictionary];

                return suggestResult ?? new SuggestResult();
            }
        }
    }
}