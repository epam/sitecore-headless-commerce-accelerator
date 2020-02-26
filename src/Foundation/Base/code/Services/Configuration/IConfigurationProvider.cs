namespace Wooli.Foundation.Base.Services.Configuration
{
    /// <summary>
    /// Get specific configuration node from Sitecore
    /// </summary>
    /// <typeparam name="TConfiguration"></typeparam>
    public interface IConfigurationProvider<TConfiguration> where TConfiguration : class
    {
        /// <summary>
        /// Get configuration node
        /// </summary>
        /// <returns>Configuration model.</returns>
        TConfiguration Get();
    }
}
