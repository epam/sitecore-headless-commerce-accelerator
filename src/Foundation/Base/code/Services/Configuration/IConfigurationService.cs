namespace Wooli.Foundation.Base.Services.Configuration
{
    using Models.Configuration;

    /// <summary>
    /// Get Sitecore config
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// Get specific Sitecore config node
        /// </summary>
        /// <typeparam name="TConfiguration"></typeparam>
        /// <returns></returns>
        TConfiguration Get<TConfiguration>() where TConfiguration : Configuration, new();
    }
}
