using Braintree;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace HCA.Api.Core.Services.BraintreeServices
{
    internal class JwtTokenService : IJwtTokenService
    {
        private readonly IClientTokenGateway _clientTokenGateway;

        public JwtTokenService()
        {
            var gatewaySettings = UIAutomationFramework.Configuration.GetBraintreeSetting().Gateway;
            _clientTokenGateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = gatewaySettings.MerchantId,
                PublicKey = gatewaySettings.PublicKey,
                PrivateKey = gatewaySettings.PrivateKey,
            }.ClientToken;
        }

        public string GetJwtToken()
        {
            var clientToken = _clientTokenGateway.Generate();
            var jsonValue = GetBase64DecodedString(clientToken);
            //todo: add jwt model
            var data = JsonConvert.DeserializeObject<JObject>(jsonValue);
            data.TryGetValue("authorizationFingerprint", out var token);
            return $"Bearer {token}";
        }

        private static string GetBase64DecodedString(string encodedString) =>
            System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(encodedString));
    }
}
