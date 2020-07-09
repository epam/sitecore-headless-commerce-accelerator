using Microsoft.Extensions.Configuration;
using System;
using UIAutomationFramework.Core;
using UIAutomationFramework.Entities.ConfigurationEntities;
using UIAutomationFramework.Entities.WebSiteEntities;
using UIAutomationFramework.Utils;

namespace UIAutomationFramework
{
	public static class Configuration
	{
		public static string OperaBinaryPath => GetAppSetting("OperaBinary");
		private static Uri GridUrl => new Uri(GetAppSetting("gridUrl"));
		public static Uri GridAddress => UriManager.AddPostfix(GridUrl, "wd/hub");

		internal static int DefaultTimeout => int.Parse(SeleniumTestContext.IsExtendedWaiting()
			? GetAppSetting("LongWaitTimeout")
			: GetAppSetting("DefaultTimeout"));

		public static string ReportFolderPath => AppDomain.CurrentDomain.BaseDirectory + "\\Report\\";
		internal static bool ContinuousIntegration => bool.Parse(GetAppSetting("IsCI"));
		public static string TestProjectAssembly => GetAppSetting("TestProjectAssembly");

		public static Uri GetEnvironmentUri(string environmentName)
		{
			var featureName = GetFeatureName();
			if (string.IsNullOrEmpty(featureName)) return new Uri(GetAppSetting(environmentName));
			//TODO realize for feature tesing
			var url = ""; //$"http://{featureName}.environment";
			return new Uri(url);
		}

		public static IConfiguration InitConfiguration()
		{
			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", false, true)
				.Build();
			return config;
		}

		public static void LogConfiguration(string environmentName)
		{
			TestLogger.Common($"Environment URL: {environmentName}");
			TestLogger.Common("DefaultTimeout: " + DefaultTimeout);
			TestLogger.Common("GridUrl: " + GridUrl);
			TestLogger.Common("ReportFolderPath: " + ReportFolderPath);
		}

		public static BraintreeSettings GetBraintreeSetting() =>
			GetAppConfigurationSection(nameof(BraintreeSettings)).Get<BraintreeSettings>();

		internal static IConfigurationSection GetAppConfigurationSection(string name) =>
			InitConfiguration().GetSection(name);


		internal static string GetAppSetting(string name)
		{
			return GetAppConfigurationSection(name).Value;
		}

		public static string GetFeatureName()
		{
			return GetAppSetting("FeatureName");
		}

		public static UserLoginEntity GetDefaultUserLogin() =>
			new UserLoginEntity(GetAppSetting("DefaultUser:Email"),
				GetAppSetting("DefaultUser:Password"));
	}
}