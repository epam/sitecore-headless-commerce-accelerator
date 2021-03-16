namespace HCA.Foundation.Commerce.Configuration.Models
{
    using System.Diagnostics.CodeAnalysis;

    using Base.Models.Configuration;

    [ExcludeFromCodeCoverage]
    public class SuggesterConfiguration : Configuration
    {
        public string DictionaryName { get; set; }

        public string FilterTemplateId { get; set; }

        public int SuggestionsMaxCount { get; set; }

        public bool Build { get; set; }
    }
}