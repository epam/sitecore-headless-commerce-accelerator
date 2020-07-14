using HCA.Api.Core.Models.Common;
using RestSharp;
using System;
using System.Collections.Generic;

namespace HCA.Api.Core.Services.RestService
{
    public interface IHttpClientService
    {
        void AddDefaultHeaders(Dictionary<string, string> headers);

        void AddClientCookie(string name, string value);

        void SetTimeOut(int timeOut);

        TModel ExecuteJsonRequest<TModel, TData, TErrors>(Uri endpoint, Method method, object obj = null)
            where TData : class
            where TErrors : class
            where TModel : class, IResponse<TData, TErrors>, new();
    }
}
