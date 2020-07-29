using System;
using System.Text;
using Api.HCA.Core.Settings.Braintree;
using Braintree;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Environment = Braintree.Environment;

namespace Api.HCA.Core.Services.BraintreeServices
{
    internal class JwtTokenService : IJwtTokenService
    {
        private readonly IClientTokenGateway _clientTokenGateway;

        public JwtTokenService(GatewaySettings gatewaySettings)
        {
            _clientTokenGateway = new BraintreeGateway
            {
                Environment = Environment.SANDBOX,
                MerchantId = gatewaySettings.MerchantId,
                PublicKey = gatewaySettings.PublicKey,
                PrivateKey = gatewaySettings.PrivateKey
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

        private static string GetBase64DecodedString(string encodedString)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(encodedString));
        }
    }
}