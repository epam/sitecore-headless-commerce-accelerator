using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Configuration;

namespace HcaApiTestAutomationFramework
{
	public class ApiHelper
	{
		public IRestRequest RestRequest;
		public string BaseUrl = AppSettingsExpander.Expand("BaseUrl");

		public RestClient SetUrl(string endpoint)
		{
			var url = Path.Combine(BaseUrl, endpoint);
			return new RestClient(url);
		}

		public IRestRequest CreatePostRequest()
		{
			RestRequest = new RestRequest(Method.POST);
			AddRequestCookies(RestRequest);
			AddRequestHeaders(RestRequest);
			return RestRequest;
		}

		public IRestRequest CreatePostRequest(string requestBody)
		{
			RestRequest = new RestRequest(Method.POST);
			AddRequestCookies(RestRequest);
			AddRequestHeaders(RestRequest);
			RestRequest.AddHeader("Content-Type", "application/json");
			RestRequest.AddParameter("application/json", requestBody, ParameterType.RequestBody);
			return RestRequest;
		}

		public IRestRequest CreatePutRequest(string requestBody)
		{
			RestRequest = new RestRequest(Method.PUT);
			AddRequestCookies(RestRequest);
			AddRequestHeaders(RestRequest);
			RestRequest.AddHeader("Content-Type", "application/json");
			RestRequest.AddParameter("application/json", requestBody, ParameterType.RequestBody);
			return RestRequest;
		}

		public IRestRequest CreateGetRequest()
		{
			RestRequest = new RestRequest(Method.GET);
			AddRequestCookies(RestRequest);
			AddRequestHeaders(RestRequest);
			return RestRequest;
		}

		public IRestRequest CreateDeleteRequest()
		{
			RestRequest = new RestRequest(Method.DELETE);
			AddRequestCookies(RestRequest);
			AddRequestHeaders(RestRequest);
			return RestRequest;
		}

		public IRestResponse GetResponse(IRestClient restClient, IRestRequest restRequest)
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


		private void AddRequestCookies(IRestRequest request)
		{
			request.AddCookie("SC_ANALYTICS_GLOBAL_COOKIE", ConfigurationManager.AppSettings["ScAnalyticsGlobalCookie"]);
		}

		private void AddRequestHeaders(IRestRequest request)
		{
			request.AddHeader("Accept", "*/*");
			request.AddHeader("accept-encoding", "gzip,deflat,sdch");
			request.AddHeader("Cache-Control", "no-cache");
			request.AddHeader("Connection", "keep-alive");
		}
	}
}
