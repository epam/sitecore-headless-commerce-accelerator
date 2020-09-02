using RestSharp;

namespace AutoTests.AutomationFramework.API.Models
{
    public class RequestInfo
    {
        public Method Method { get; set; }

        public string BaseUrl { get; set; }
    }
}