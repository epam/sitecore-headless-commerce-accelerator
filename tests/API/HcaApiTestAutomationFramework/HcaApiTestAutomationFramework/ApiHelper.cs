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
		public string Host = AppSettingsExpander.Expand("Host");
		public string BaseUrl = AppSettingsExpander.Expand("BaseUrl");

		public RestClient SetUrl(string endpoint)
		{
			var url = Path.Combine(BaseUrl, endpoint);
			return new RestClient(url);
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
			return JsonConvert.DeserializeObject<DTO>(response.Content);
		}

		public string Serialize(dynamic content)
		{
			return JsonConvert.SerializeObject(content, Formatting.Indented);
		}


		private void AddRequestCookies(RestRequest request)
		{
			request.AddCookie("SC_ANALYTICS_GLOBAL_COOKIE", ConfigurationManager.AppSettings["ScAnalyticsGlobalCookie"]);
		}

		private void AddRequestHeaders(RestRequest request)
		{
			request.AddHeader("Accept", "*/*");
			request.AddHeader("accept-encoding", "gzip,deflat,sdch");
			request.AddHeader("Cache-Control", "no-cache");
			request.AddHeader("Connection", "keep-alive");
		}
	}
}
