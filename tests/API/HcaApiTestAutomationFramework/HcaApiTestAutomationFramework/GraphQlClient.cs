using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Braintree;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace HcaApiTestAutomationFramework
{
	public class GraphQlClient
	{
		private RestClient _client;

		public string getClientToken()
		{
			BraintreeGateway gateway = new BraintreeGateway
			{
				Environment = Braintree.Environment.SANDBOX,
				MerchantId = "r7vzc3fhvkx5zkvb",
				PublicKey = "zmncp624j8wghkft",
				PrivateKey = "c5ea09089b15cef4346ca042053f81f5",
			};
			gateway.Configuration.Timeout = 10000;
			var clientToken = gateway.ClientToken.Generate();
			return clientToken;
		}

		public string Base64Decoder(string stringText)
		{
			string base64Encoded = stringText;
			string base64Decoded;
			byte[] data = System.Convert.FromBase64String(base64Encoded);
			base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
			return base64Decoded;
		}

		public string GetAutorizationFingerprint()
		{
			var clientToken = getClientToken();
			string jsonValue = Base64Decoder(clientToken);
			var data = (JObject) JsonConvert.DeserializeObject(jsonValue);
			string autorizationFingerprint = data["authorizationFingerprint"].Value<string>();
			return autorizationFingerprint;
		}

		public string getAuthorization()
		{
			return "Bearer " + GetAutorizationFingerprint();
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
			//request.AddHeader("HOST", "int-cd.hca.azure.epmc-stc.projects.epam.com");
			request.AddHeader("braintree-version", ConfigurationManager.AppSettings["braintree-version"]);
			request.AddHeader("Accept-Enclding", "gzip,deflat,sdch");
			request.AddHeader("Cache-Control", "no-cache");
			request.AddHeader("content-type", ConfigurationManager.AppSettings["content-type"]);
			request.AddHeader("authorization", getAuthorization());
		}
	}
}
