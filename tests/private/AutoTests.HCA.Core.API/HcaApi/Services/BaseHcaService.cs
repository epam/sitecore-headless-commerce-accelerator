using System;
using AutoTests.AutomationFramework.API.Services.RestService;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.HcaApi.Models.RequestResult;
using AutoTests.HCA.Core.API.HcaApi.Models.RequestResult.Results.ErrorResult;
using AutoTests.HCA.Core.API.HcaApi.Models.RequestResult.Results.OkResult;
using RestSharp;

namespace AutoTests.HCA.Core.API.HcaApi.Services
{
    public class BaseHcaService
    {
        private readonly IHttpClientService _httpClientService;

        public BaseHcaService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        private Uri GetAbsoluteUri(string urlPostfix)
        {
            return _httpClientService.BaseUri.AddPostfix(urlPostfix);
        }

        protected HcaResponse<TData> ExecuteJsonRequest<TData>(string postfix, Method method, object obj = null)
            where TData : class
        {
            return _httpClientService.ExecuteJsonRequest<HcaResponse<TData>, HcaOkResult<TData>, HcaErrorsResult>(
                GetAbsoluteUri(postfix), method, obj);
        }

        protected HcaVoidResponse ExecuteJsonRequest(string postfix, Method method, object obj = null)
        {
            return _httpClientService.ExecuteJsonRequest<HcaVoidResponse, HcaOkResult<object>, HcaErrorsResult>(
                GetAbsoluteUri(postfix), method, obj);
        }
    }
}