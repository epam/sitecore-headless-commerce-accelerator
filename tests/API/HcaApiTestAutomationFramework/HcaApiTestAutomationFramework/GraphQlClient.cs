using System.Collections.Generic;
using System.Configuration;
using System.Net;
using Braintree;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace HcaApiTestAutomationFramework
{
	public class GraphQlClient
	{
		private RestClient _client;

		public string GetClientToken()
		{
			var gateway = new BraintreeGateway
			{
				Environment = Braintree.Environment.SANDBOX,
				MerchantId = ConfigurationManager.AppSettings["BraintreeMerchantId"],
				PublicKey = ConfigurationManager.AppSettings["BraintreePublicKey"],
				PrivateKey = ConfigurationManager.AppSettings["BraintreePrivateKey"],
			};
			gateway.Configuration.Timeout = 10000;
			var clientToken = gateway.ClientToken.Generate();
			return clientToken;
		}

		public string Base64Decoder(string stringText)
		{
			var base64Encoded = stringText;
			var data = System.Convert.FromBase64String(base64Encoded);
			var base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
			return base64Decoded;
		}

		public string GetAuthorizationFingerprint()
		{
			var clientToken = GetClientToken();
			var jsonValue = Base64Decoder(clientToken);
			var data = (JObject) JsonConvert.DeserializeObject(jsonValue);
			var authorizationFingerprint = data["authorizationFingerprint"].Value<string>();
			return authorizationFingerprint;
		}

		public string GetAuthorization()
		{
			return "Bearer " + GetAuthorizationFingerprint();
		}

		public GraphQlClient(string GraphQLApiUrl)
		{
			_client = new RestClient(GraphQLApiUrl);

			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
		}

		public dynamic Execute(object clientSdkMetadata, string operationName, string query, object variables = null, Dictionary<string, string> additionalHeaders = null, int timeout = 0)
		{
			var request = new RestRequest("/", Method.POST);
			AddRequestHeaders(request);
			request.Timeout = timeout;

			if (additionalHeaders != null && additionalHeaders.Count > 0)
			{
				foreach (var additionalHeader in additionalHeaders)
				{
					request.AddHeader(additionalHeader.Key, additionalHeader.Value);
				}
			}

			request.AddJsonBody(new
			{
				clientSdkMetadata = clientSdkMetadata,
				operationName = operationName,
				query = query,
				variables = variables
			});

			return JObject.Parse(_client.Execute(request).Content);
		}

		private void AddRequestHeaders(RestRequest request)
		{
			request.AddHeader("accept", "*/*");
			request.AddHeader("braintree-version", ConfigurationManager.AppSettings["braintree-version"]);
			request.AddHeader("accept-encoding", "gzip, deflate, br");
			request.AddHeader("Cache-Control", "no-cache");
			request.AddHeader("Content-Type", ConfigurationManager.AppSettings["content-type"]);
			request.AddHeader("authorization", GetAuthorization());
		}
	}
}
