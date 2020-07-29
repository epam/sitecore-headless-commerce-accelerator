using System;
using System.Collections.Generic;
using Api.AutomationFramework.Models.Common;
using RestSharp;

namespace Api.AutomationFramework.Services.RestService
{
    public interface IHttpClientService
    {
        void AddDefaultHeaders(Dictionary<string, string> headers);

        void AddClientCookie(string name, string value);

        void SetHttpBasicAuthenticator(string userName, string password);

        void SetTimeOut(int timeOut);

        TModel ExecuteJsonRequest<TModel, TData, TErrors>(Uri endpoint, Method method, object obj = null)
            where TData : class
            where TErrors : class
            where TModel : class, IResponse<TData, TErrors>, new();
    }
}