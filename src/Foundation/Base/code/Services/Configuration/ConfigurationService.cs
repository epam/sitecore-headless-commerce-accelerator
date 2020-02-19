namespace Wooli.Foundation.Base.Services.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Models.Configuration;
    using Sitecore.DependencyInjection;

    [ExcludeFromCodeCoverage]
    [Service(typeof(IConfigurationService))]
    public class ConfigurationService : IConfigurationService
    {
        public TConfiguration Get<TConfiguration>() where TConfiguration : Configuration, new() 
            => ServiceLocator.ServiceProvider.GetService<IConfigurationProvider<TConfiguration>>().Get();
    }
}