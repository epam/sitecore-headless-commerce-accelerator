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
				Configuration = {Timeout = 10000},
			};
			return gateway.ClientToken.Generate();
		}

		public string Base64Decoder(string base64Encoded)
		{
			var data = System.Convert.FromBase64String(base64Encoded);
			return System.Text.ASCIIEncoding.ASCII.GetString(data);
		}

		public string GetAuthorizationFingerprint()
		{
			var clientToken = GetClientToken();
			var jsonValue = Base64Decoder(clientToken);
			var data = (JObject) JsonConvert.DeserializeObject(jsonValue);
			return data["authorizationFingerprint"].Value<string>();
		}

		public string GetAuthorization()
		{
			return $"Bearer {GetAuthorizationFingerprint()}";
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
			request.AddHeader("accept-encoding", "gzip, deflate, br");
			request.AddHeader("braintree-version", ConfigurationManager.AppSettings["braintree-version"]);
			request.AddHeader("accept-language", "en-US,en;q=0.9");
			request.AddHeader("Cache-Control", "no-cache");
			request.AddHeader("Cache-Control", "1401");
			request.AddHeader("content-type", ConfigurationManager.AppSettings["application/json"]);
			request.AddHeader("authorization", GetAuthorization());
		}
	}
}
