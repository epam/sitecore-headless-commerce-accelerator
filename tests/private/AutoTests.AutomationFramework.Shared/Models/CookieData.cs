namespace AutoTests.AutomationFramework.Shared.Models
{
    public class CookieData
    {
        public CookieData()
        {}

        public CookieData(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}