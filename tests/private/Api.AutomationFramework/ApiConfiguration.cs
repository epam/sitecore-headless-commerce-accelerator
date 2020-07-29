using Microsoft.Extensions.Configuration;

namespace Api.AutomationFramework
{
    public class ApiConfiguration
    {
        private readonly IConfigurationRoot _configuration;

        public ApiConfiguration(string path)
        {
            _configuration = new ConfigurationBuilder().AddJsonFile(path).Build();
        }

        public T Get<T>(string sectionName = null)
        {
            return _configuration.GetSection(sectionName ?? typeof(T).Name).Get<T>();
        }
    }
}