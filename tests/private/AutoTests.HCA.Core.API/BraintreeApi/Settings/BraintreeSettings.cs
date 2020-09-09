namespace AutoTests.HCA.Core.API.BraintreeApi.Settings
{
    public class BraintreeSettings
    {
        public GatewaySettings Gateway { get; set; }
        public string GraphQlSandboxUrl { get; set; }
        public string Version { get; set; }
    }
}