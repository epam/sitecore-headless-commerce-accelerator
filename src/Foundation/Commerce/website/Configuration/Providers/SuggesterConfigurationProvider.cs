using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCA.Foundation.Commerce.Configuration.Providers
{
    using Base.Providers.Configuration;
    using Base.Providers.Object;

    using DependencyInjection;

    using Models;

    [Service(typeof(ISuggesterConfigurationProvider), Lifetime = Lifetime.Singleton)]
    public class SuggesterConfigurationProvider : ConfigurationProvider<SuggesterConfiguration>, ISuggesterConfigurationProvider
    {
        public SuggesterConfigurationProvider(IObjectProvider objectProvider) : base(objectProvider)
        {
        }

        protected override string Path { get; set; } = "suggester/settings";
    }
}