using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Configuration;

namespace HcaApiTestAutomationFramework
{
	public class ApiHelper<T>
	{
		public RestClient restClient;
		public RestRequest restRequest;
		public string Host = ConfigurationManager.AppSettings["Host"];
		public string baseUrl = AppSettingsExpander.Expand("BaseUrl");

		public RestClient SetUrl(string endpoint)
		{
			var url = Path.Combine(baseUrl, endpoint);
			var restClient = new RestClient(url);
			return restClient;
		}

		public RestRequest CreatePostRequest()
		{
			restRequest = new RestRequest(Method.POST);
			AddRequestCookies(restRequest);
			AddRequestHeaders(restRequest);
			return restRequest;
		}

		public RestRequest CreatePostRequest(string requestBody)
		{
			restRequest = new RestRequest(Method.POST);
			AddRequestCookies(restRequest);
			AddRequestHeaders(restRequest);
			restRequest.AddHeader("Content-Type", "application/json");
			restRequest.AddParameter("application/json", requestBody, ParameterType.RequestBody);
			return restRequest;
		}

		public RestRequest CreatePutRequest(string requestBody)
		{
			restRequest = new RestRequest(Method.PUT);
			AddRequestCookies(restRequest);
			AddRequestHeaders(restRequest);
			restRequest.AddHeader("Content-Type", "application/json");
			restRequest.AddParameter("application/json", requestBody, ParameterType.RequestBody);
			return restRequest;
		}

		public RestRequest CreateGetRequest()
		{
			restRequest = new RestRequest(Method.GET);
			AddRequestCookies(restRequest);
			AddRequestHeaders(restRequest);
			return restRequest;
		}

		public RestRequest CreateDeleteRequest()
		{
			restRequest = new RestRequest(Method.DELETE);
			AddRequestCookies(restRequest);
			AddRequestHeaders(restRequest);
			return restRequest;
		}

		public IRestResponse GetResponse(RestClient restClient, RestRequest restRequest)
		{
			return restClient.Execute(restRequest);
		}

		public DTO GetContent<DTO>(IRestResponse response)
		{
			var content = response.Content;
			DTO dtoObject = JsonConvert.DeserializeObject<DTO>(content);
			return dtoObject;
		}

		public string Serialize(dynamic content)
		{
			string serializeObject = JsonConvert.SerializeObject(content, Formatting.Indented);
			return serializeObject;
		}


		private void AddRequestCookies(RestRequest request)
		{
			request.AddCookie("SC_ANALYTICS_GLOBAL_COOKIE", ConfigurationManager.AppSettings["ScAnalyticsGlobalCookie"]);
		}

		private void AddRequestHeaders(RestRequest request)
		{
			request.AddHeader("Accept", "*/*");
			request.AddHeader("Accept-Enclding", "gzip,deflat,sdch");
			request.AddHeader("Cache-Control", "no-cache");
			request.AddHeader("Connection", "keep-alive");
		}
	}
}
