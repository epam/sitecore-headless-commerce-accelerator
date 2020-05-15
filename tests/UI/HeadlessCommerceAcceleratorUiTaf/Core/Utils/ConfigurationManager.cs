using Microsoft.Extensions.Configuration;

namespace Core.Utils
{
    public static class ConfigurationManager
    {
        public static string BaseUrl => ConfigurationBuilder.GetSection("BaseUrl").Value;

        public static int DefaultTimeout => int.Parse(ConfigurationBuilder.GetSection("DefaultTimeout").Value);

        private static IConfiguration ConfigurationBuilder => new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
    }
}
