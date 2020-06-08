using System.Configuration;
using System.Text.RegularExpressions;

namespace HcaApiTestAutomationFramework
{
	public class AppSettingsExpander
	{
		private const string VARIABLE_PATTERN = @"\$\((\w+)\)";
		private static Regex _regex = new Regex(VARIABLE_PATTERN);

		public static string Expand(string key)
		{
			var value = ConfigurationManager.AppSettings[key];

			if (value == null)
				return null;

			var result = _regex.Replace(value, match =>
			{
				var refKey = match.Groups[1];
				return Expand(refKey.Value);
			});

			return result;
		}
    }
}
