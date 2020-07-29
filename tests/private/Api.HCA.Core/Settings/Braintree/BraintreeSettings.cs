namespace Api.HCA.Core.Settings.Braintree
{
    public class BraintreeSettings
    {
        public GatewaySettings Gateway { get; set; }
        public string GraphQlSandboxUrl { get; set; }
        public string Version { get; set; }
    }
}