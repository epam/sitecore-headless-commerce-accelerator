using System.IO;
using Microsoft.Extensions.Configuration;

namespace AutoTests.AutomationFramework.Shared.Configuration
{
    public class ConfigurationManager
    {
        private readonly IConfigurationRoot _configuration;

        public ConfigurationManager(string path)
        {
            var nameOfConfigFile = Path.GetFileNameWithoutExtension(path);
            var fullPathToLocalFile = path.Replace(nameOfConfigFile, nameOfConfigFile + ".local");
            var finalPathToConfig = File.Exists(fullPathToLocalFile) ? fullPathToLocalFile : path;
            _configuration = new ConfigurationBuilder().AddJsonFile(finalPathToConfig).Build();
        }

        public T Get<T>(string sectionName = null)
        {
            return _configuration.GetSection(sectionName ?? typeof(T).Name).Get<T>();
        }
    }
}