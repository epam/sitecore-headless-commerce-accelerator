using Microsoft.Extensions.Configuration;

namespace AutoTests.AutomationFramework.Shared.Configuration
{
    public class ConfigurationManager
    {
        private readonly IConfigurationRoot _configuration;

        public ConfigurationManager(string path)
        {
            _configuration = new ConfigurationBuilder().AddJsonFile(path).Build();
        }

        public T Get<T>(string sectionName = null)
        {
            return _configuration.GetSection(sectionName ?? typeof(T).Name).Get<T>();
        }
    }
}