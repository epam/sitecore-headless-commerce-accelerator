namespace Wooli.Feature.Account.Configuration.Providers
{
    using Foundation.Base.Services.Configuration;
    using Foundation.DependencyInjection;
    using Models;

    [Service(typeof(IConfigurationProvider<ExampleConfiguration>), Lifetime = Lifetime.Singleton)]
    public class ExampleProvider : ConfigurationProvider<ExampleConfiguration>
    {
        protected override string Path { get; set; } = "example/settings";
    }
}