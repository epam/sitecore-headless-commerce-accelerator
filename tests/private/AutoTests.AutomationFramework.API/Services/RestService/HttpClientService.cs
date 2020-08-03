using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoTests.AutomationFramework.API.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace AutoTests.AutomationFramework.API.Services.RestService
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IRestClient _restClient;

        public HttpClientService(Uri baseUri)
        {
            _restClient = new RestClient(baseUri) { CookieContainer = new CookieContainer() };
            _restClient.UseNewtonsoftJson();
        }

        public void AddDefaultHeaders(Dictionary<string, string> headers)
        {
            _restClient.AddDefaultHeaders(headers);
        }

        public CookieCollection GetCookies()
        {
            return _restClient.CookieContainer.GetCookies(_restClient.BaseUrl);
        }

        public void SetCookieIfNotSet(string name, string value)
        {
            if (GetCookies().All(x => x.Name != name))
                _restClient.CookieContainer.Add(new Cookie(name, value) { Domain = _restClient.BaseUrl.Host });
        }

        public void SetHttpBasicAuthenticator(string userName, string password)
        {
            _restClient.Authenticator = new HttpBasicAuthenticator(userName, password);
        }

        public void SetTimeOut(int timeOut)
        {
            _restClient.Timeout = timeOut;
        }

        public TModel ExecuteJsonRequest<TModel, TData, TErrors>(Uri endpoint, Method method, object obj = null)
            where TData : class
            where TErrors : class
            where TModel : class, IResponse<TData, TErrors>, new()
        {
            return DeserializeJsonResponse<TModel, TData, TErrors>(ExecuteJsonRequest(endpoint, obj, method));
        }

        private IRestResponse ExecuteJsonRequest(Uri endpoint, object obj, Method method)
        {
            var request = new RestRequest();
            _restClient.BaseUrl = endpoint;

            if (obj != null) request.AddJsonBody(obj);

            return _restClient.Execute(request, method);
        }

        private static TModel DeserializeJsonResponse<TModel, TData, TErrors>(IRestResponse result)
            where TData : class
            where TErrors : class
            where TModel : class, IResponse<TData, TErrors>, new()
        {
            return result.IsSuccessful
                ? new TModel
                {
                    IsSuccessful = result.IsSuccessful,
                    StatusCode = result.StatusCode,
                    OkResponseData = JsonConvert.DeserializeObject<TData>(result.Content)
                }
                : new TModel
                {
                    IsSuccessful = result.IsSuccessful,
                    StatusCode = result.StatusCode,
                    Errors = JsonConvert.DeserializeObject<TErrors>(result.Content)
                };

        }
    }
}