namespace Wooli.Foundation.Base.Services.Configuration
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Models.Configuration;
    using Sitecore.Configuration;

    [ExcludeFromCodeCoverage]
    public abstract class ConfigurationProvider<TConfiguration> : IConfigurationProvider<TConfiguration> where TConfiguration : Configuration, new()
    {
        private readonly Lazy<TConfiguration> configuration;
        protected abstract string Path { get; set; }

        protected ConfigurationProvider()
        {
            this.configuration = new Lazy<TConfiguration>(this.Read, true);
        }

        public TConfiguration Get()
        {
            return this.configuration.Value;
        }

        private TConfiguration Read()
        {
            var xmlNode = Factory.GetConfigNode(this.Path);
            return xmlNode != null ? Factory.CreateObject<TConfiguration>(xmlNode) : null;
        }
    }
}