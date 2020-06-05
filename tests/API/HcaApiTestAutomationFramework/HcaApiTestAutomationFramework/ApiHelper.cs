using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Configuration;

namespace HcaApiTestAutomationFramework
{
	public class ApiHelper<T>
	{
		public RestClient RestClient;
		public RestRequest RestRequest;
		public string Host = ConfigurationManager.AppSettings["Host"];
		public string BaseUrl = AppSettingsExpander.Expand("BaseUrl");

		public RestClient SetUrl(string endpoint)
		{
			var url = Path.Combine(BaseUrl, endpoint);
			var restClient = new RestClient(url);
			return restClient;
		}

		public RestRequest CreatePostRequest()
		{
			RestRequest = new RestRequest(Method.POST);
			AddRequestCookies(RestRequest);
			AddRequestHeaders(RestRequest);
			return RestRequest;
		}

		public RestRequest CreatePostRequest(string requestBody)
		{
			RestRequest = new RestRequest(Method.POST);
			AddRequestCookies(RestRequest);
			AddRequestHeaders(RestRequest);
			RestRequest.AddHeader("Content-Type", "application/json");
			RestRequest.AddParameter("application/json", requestBody, ParameterType.RequestBody);
			return RestRequest;
		}

		public RestRequest CreatePutRequest(string requestBody)
		{
			RestRequest = new RestRequest(Method.PUT);
			AddRequestCookies(RestRequest);
			AddRequestHeaders(RestRequest);
			RestRequest.AddHeader("Content-Type", "application/json");
			RestRequest.AddParameter("application/json", requestBody, ParameterType.RequestBody);
			return RestRequest;
		}

		public RestRequest CreateGetRequest()
		{
			RestRequest = new RestRequest(Method.GET);
			AddRequestCookies(RestRequest);
			AddRequestHeaders(RestRequest);
			return RestRequest;
		}

		public RestRequest CreateDeleteRequest()
		{
			RestRequest = new RestRequest(Method.DELETE);
			AddRequestCookies(RestRequest);
			AddRequestHeaders(RestRequest);
			return RestRequest;
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
