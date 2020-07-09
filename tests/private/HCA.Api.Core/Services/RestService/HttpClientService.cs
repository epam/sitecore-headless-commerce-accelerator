using HCA.Api.Core.Models.Common;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Net;

namespace HCA.Api.Core.Services.RestService
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IRestClient _restClient;

        public HttpClientService(Uri baseUri)
        {
            _restClient = new RestClient(baseUri) { CookieContainer = new CookieContainer() };
            _restClient.UseNewtonsoftJson();
        }

        public void AddDefaultHeaders(Dictionary<string, string> headers) =>
            _restClient.AddDefaultHeaders(headers);

        public void AddClientCookie(string name, string value) =>
            _restClient.CookieContainer.Add(new Cookie(name, value) { Domain = _restClient.BaseUrl.Host });

        public void SetTimeOut(int timeOut) =>
            _restClient.Timeout = timeOut;

        public TModel ExecuteJsonRequest<TModel, TData, TErrors>(Uri endpoint, Method method, object obj = null)
            where TData : class
            where TErrors : class
            where TModel : class, IResponse<TData, TErrors>, new()
        {
            var request = GetJsonRequest(endpoint, obj);
            var result = _restClient.Execute(request, method);

            return result.IsSuccessful
                ? new TModel { IsSuccessful = result.IsSuccessful, OkResponseData = JsonConvert.DeserializeObject<TData>(result.Content) }
                : new TModel { IsSuccessful = result.IsSuccessful, Errors = JsonConvert.DeserializeObject<TErrors>(result.Content) };
        }

        private IRestRequest GetJsonRequest(Uri endpoint, object obj)
        {
            var request = new RestRequest();
            _restClient.BaseUrl = endpoint;
            if (obj != null) request.AddJsonBody(obj);

            return request;
        }
    }
}
